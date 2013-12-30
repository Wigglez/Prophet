﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Styx;
using Styx.Common;
using Styx.Plugins;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

namespace Prophet {
    public class Prophet : HBPlugin {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        public static LocalPlayer Me = StyxWoW.Me;

        public static int PartyMembers = 0;

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public static int PresenceID { get; set; }

        public static int NumFriends { get; set; }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override string Name {
            get { return "Prophet"; }
        }

        public override string Author {
            get { return "Akna & Wigglez"; }
        }

        public override Version Version {
            get { return new Version(1, 0); }
        }

        public override bool WantButton { get { return true; } }
        public override string ButtonText { get { return "User Interface"; } }

        public override void OnButtonPress() {
            var gui = new ProphetGUI();
            gui.ShowDialog();
        }

        public override void OnEnable() {
            try {
                Lua.Events.AttachEvent("PARTY_MEMBERS_CHANGED", HandlePartyMembersChanged);
                Lua.Events.AttachEvent("PARTY_INVITE_REQUEST", HandlePartyInviteRequest);
                Lua.Events.AttachEvent("UI_ERROR_MESSAGE", HandleErrorMessage);
                base.OnEnable();
            } catch(Exception e) {
                CustomNormalLog("Could not initialize. Message = " + e.Message + " Stacktrace = " + e.StackTrace);
            } finally {
                CustomNormalLog("Initialization complete.");
            }

        }

        public override void OnDisable() {
            try {
                Lua.Events.DetachEvent("UI_ERROR_MESSAGE", HandleErrorMessage);
                Lua.Events.DetachEvent("PARTY_MEMBERS_CHANGED", HandlePartyMembersChanged);
                Lua.Events.DetachEvent("PARTY_INVITE_REQUEST", HandlePartyInviteRequest);
                base.OnDisable();
            } catch(Exception e) {
                CustomNormalLog("Could not dispose. Message = " + e.Message + " Stacktrace = " + e.StackTrace);
            } finally {
                CustomNormalLog("Shutdown complete.");
            }

        }

        public override void Pulse() {
            if(!CanInvite()) {
                return;
            }

            // The default settings
            if(!PartySettings.Instance.PartyLeader && !PartySettings.Instance.PartyMember) {
                return;
            }

            // If the user is a retard and set both to true
            if(PartySettings.Instance.PartyLeader && PartySettings.Instance.PartyMember) {
                CustomNormalLog("You can't be both the party leader and a party member.");
                return;
            }

            // If the user is the party leader
            if(PartySettings.Instance.PartyLeader) {
                if(!PartyMemberExists()) {
                    CustomNormalLog("You have to provide at least one party member name.");
                    return;
                }

                SendOutInvites();
            }

            // If the user is the party member
            if(!PartySettings.Instance.PartyMember) return;

            if(!PartyLeaderExists()) {
                CustomNormalLog("You have to provide the party leader name.");
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public static void CustomNormalLog(string message, params object[] args) {
            Logging.Write(Colors.DeepSkyBlue, "[Prophet]: " + message, args);
        }

        public static void HandleErrorMessage(object sender, LuaEventArgs args) {
            var errorMessage = args.Args[0].ToString();

            var errGroupFull = Lua.GetReturnVal<string>("return ERR_GROUP_FULL", 0); // "Your party is full."
            var errInvitedAlreadyInGroup = Lua.GetReturnVal<string>("return ERR_INVITED_ALREADY_IN_GROUP_SS", 0); // "|Hplayer:%s|h[%s]|h invited you to a group, but you could not accept because you are already in a group."
            var errInvitedToGroup = Lua.GetReturnVal<string>("return ERR_INVITED_TO_GROUP_SS", 0); // "|Hplayer:%s|h[%s]|h has invited you to join a group."
            var errInvitePlayer = Lua.GetReturnVal<string>("return ERR_INVITE_PLAYER_S", 0); // "You have invited %s to join your group."
            var errInviteRestricted = Lua.GetReturnVal<string>("return ERR_INVITE_RESTRICTED", 0); // "Trial accounts cannot invite characters into groups."
            var errInviteSelf = Lua.GetReturnVal<string>("return ERR_INVITE_SELF", 0); // "You can't invite yourself to a group."

            if(errorMessage.Equals(errGroupFull)) {
                CustomNormalLog("");
            } else if(errorMessage.Equals(errInvitedAlreadyInGroup)) {
                CustomNormalLog("");
            } else if(errorMessage.Equals(errInvitedToGroup)) {
                CustomNormalLog("");
            } else if(errorMessage.Equals(errInvitePlayer)) {
                CustomNormalLog("You invited " + "some name" + " to your party.");
            } else if(errorMessage.Equals(errInviteRestricted)) {
                CustomNormalLog("You are on a trial account and cannot invite characters into groups.");
            } else if(errorMessage.Equals(errInviteSelf)) {
                CustomNormalLog("You cannot invite yourself to the group.");
            }
        }

        public static bool CanInvite() {
            return Me.IsValid && StyxWoW.IsInGame;
        }

        public static bool PartyMemberExists() {
            return PartySettings.Instance.PartyMemberName1 != "" || PartySettings.Instance.PartyMemberName2 != "" || PartySettings.Instance.PartyMemberName3 != "" || PartySettings.Instance.PartyMemberName4 != "";
        }

        public static bool PartyLeaderExists() {
            return PartySettings.Instance.PartyLeaderName != "";
        }

        public static int GetNumGroupMembers() {
            return StyxWoW.Me.GroupInfo.RaidMembers.Count();
        }

        public static int BNGetNumFriends() {
            return Lua.GetReturnVal<int>("return BNGetNumFriends()", 0);
        }

        // presenceID, givenName, surname, toonName, toonID, client, isOnline, lastOnline, isAFK, isDND, broadcastText, noteText, isFriend, broadcastTime  = BNGetFriendInfo(friendIndex)
        public static List<string> BNGetFriendInfo(int i) {
            return Lua.GetReturnValues(string.Format("return BNGetFriendInfo({0})", i));
        }

        // hasFocus, toonName, client, realmName, realmID, faction, race, class, guild, zoneName, level, gameText, broadcastText, broadcastTime, canSoR, toonID = BNGetToonInfo(presenceID or toonID)
        public static List<string> BNGetToonInfo(int presenceID) {
            return Lua.GetReturnValues(string.Format("return BNGetToonInfo({0})", presenceID));
        }

        public static bool BNCanInvite(string name, string realm) {
            var numFriends = BNGetNumFriends();
            for(var i = 1; i <= numFriends; i++) {

                var presenceID = Convert.ToInt32(BNGetFriendInfo(i)[0]);
                var isOnline = Convert.ToBoolean(BNGetFriendInfo(i)[6]);

                var toonName = BNGetToonInfo(presenceID)[1];
                var realmName = BNGetToonInfo(presenceID)[3];

                if(toonName != name || realmName != realm || !isOnline) {
                    continue;
                }

                PresenceID = presenceID;
                return true;
            }

            return false;
        }

        public static void BNInviteFriend() {
            Lua.DoString(string.Format("BNInviteFriend({0})", PresenceID));
        }

        public static void LeaveParty() {
            if(!PartySettings.Instance.PartyMember) { return; }

            if(GroupMemberExist(PartySettings.Instance.PartyLeaderName)) { return; }

            CustomNormalLog(string.Format("Party leader {0} isn't in group, leaving group.", PartySettings.Instance.PartyLeaderName));
            Lua.DoString("LeaveParty()");
        }

        public static bool GroupMemberExist(string name) {
            if(GetNumGroupMembers() <= 1) return false;

            for(var i = 1; i <= GetNumGroupMembers(); i++) {
                if(Lua.GetReturnVal<string>(string.Format("return (select(1, GetRaidRosterInfo({0})))", i), 0) == name) {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckInvites(string name) {
            return name != "" && !GroupMemberExist(name);
        }

        public static void SendOutInvites() {
            if(!PartySettings.Instance.PartyLeader) { return; }

            if(CheckInvites(PartySettings.Instance.PartyMemberName1)) {
                var toonName = PartySettings.Instance.PartyMemberName1;
                var realmName = Me.RealmName;
                CustomNormalLog("PartyMemberName1 toonName = " + toonName);
                CustomNormalLog("PartyMemberName1 realmName = " + realmName);

                // No idea if this works until we can test it, but I guess it should.
                if(PartySettings.Instance.PartyMemberName1.Contains("-")) {
                    var index = PartySettings.Instance.PartyMemberName1.IndexOf('-');
                    toonName = PartySettings.Instance.PartyMemberName1.Substring(0, index - 1);
                    realmName = PartySettings.Instance.PartyMemberName1.Substring(index);
                    CustomNormalLog("PartyMemberName1 contained a -");
                    CustomNormalLog("toonName = " + toonName);
                    CustomNormalLog("realmName = " + realmName);
                }

                if(BNCanInvite(toonName, realmName)) {
                    // Make a timer to not spam invite on every pulse but rather wait a while before sending it again.
                    BNInviteFriend();
                }
            }

            if(CheckInvites(PartySettings.Instance.PartyMemberName2)) {
                var toonName = PartySettings.Instance.PartyMemberName2;
                var realmName = Me.RealmName;
                CustomNormalLog("PartyMemberName2 toonName = " + toonName);
                CustomNormalLog("PartyMemberName2 realmName = " + realmName);

                // No idea if this works until we can test it, but I guess it should.
                if(PartySettings.Instance.PartyMemberName2.Contains("-")) {
                    var index = PartySettings.Instance.PartyMemberName2.IndexOf('-');
                    toonName = PartySettings.Instance.PartyMemberName2.Substring(0, index - 1);
                    realmName = PartySettings.Instance.PartyMemberName2.Substring(index);
                    CustomNormalLog("PartyMemberName2 contained a - so who to invite is :");
                    CustomNormalLog("toonName = " + toonName);
                    CustomNormalLog("realmName = " + realmName);
                }
                if(BNCanInvite(toonName, realmName)) {
                    // Make a timer to not spam invite on every pulse but rather wait a while before sending it again.
                    BNInviteFriend();
                }

            }

            if(CheckInvites(PartySettings.Instance.PartyMemberName3)) {
                var toonName = PartySettings.Instance.PartyMemberName3;
                var realmName = Me.RealmName;
                CustomNormalLog("PartyMemberName3 toonName = " + toonName);
                CustomNormalLog("PartyMemberName3 realmName = " + realmName);

                // No idea if this works until we can test it, but I guess it should.
                if(PartySettings.Instance.PartyMemberName3.Contains("-")) {
                    var index = PartySettings.Instance.PartyMemberName3.IndexOf('-');
                    toonName = PartySettings.Instance.PartyMemberName3.Substring(0, index - 1);
                    realmName = PartySettings.Instance.PartyMemberName3.Substring(index);
                    CustomNormalLog("PartyMemberName3 contained a -");
                    CustomNormalLog("toonName = " + toonName);
                    CustomNormalLog("realmName = " + realmName);
                }

                if(BNCanInvite(toonName, realmName)) {
                    // Make a timer to not spam invite on every pulse but rather wait a while before sending it again.
                    BNInviteFriend();
                }
            }

            if(CheckInvites(PartySettings.Instance.PartyMemberName4)) {
                var toonName = PartySettings.Instance.PartyMemberName4;
                var realmName = Me.RealmName;
                CustomNormalLog("PartyMemberName4 toonName = " + toonName);
                CustomNormalLog("PartyMemberName4 realmName = " + realmName);

                // No idea if this works until we can test it, but I guess it should.
                if(PartySettings.Instance.PartyMemberName4.Contains("-")) {
                    var index = PartySettings.Instance.PartyMemberName4.IndexOf('-');
                    toonName = PartySettings.Instance.PartyMemberName4.Substring(0, index - 1);
                    realmName = PartySettings.Instance.PartyMemberName4.Substring(index);
                    CustomNormalLog("PartyMemberName4 contained a -");
                    CustomNormalLog("toonName = " + toonName);
                    CustomNormalLog("realmName = " + realmName);
                }

                if(BNCanInvite(toonName, realmName)) {
                    // Make a timer to not spam invite on every pulse but rather wait a while before sending it again.
                    BNInviteFriend();
                }
            }
        }

        public static void AcceptInvite() {
            CustomNormalLog("Accepted invite.");
            Lua.DoString("AcceptGroup()");
        }

        public static void DeclineInvite() {
            CustomNormalLog("Declined invite.");
            Lua.DoString("DeclineGroup()");

            // If there is a popup visible, get rid of that shit
            var partyInvitePopup = Lua.GetReturnVal<bool>("return StaticPopup_Visible('PARTY_INVITE')", 0);
            var partyInviteCrossrealmPopup = Lua.GetReturnVal<bool>("return StaticPopup_Visible('PARTY_INVITE_XREALM')", 0);

            if(!partyInvitePopup && !partyInviteCrossrealmPopup) {
                return;
            }

            Lua.DoString("StaticPopup_Hide('PARTY_INVITE')");
            Lua.DoString("StaticPopup_Hide('PARTY_INVITE_XREALM')");
        }

        public static void HandlePartyInviteRequest(object sender, LuaEventArgs args) {
            if(PartySettings.Instance.PartyLeader) {
                DeclineInvite();
                return;
            }

            var partyInviteSender = args.Args[0].ToString();

            CustomNormalLog("We got an invite from " + partyInviteSender + ".");

            if(partyInviteSender == PartySettings.Instance.PartyLeaderName) {
                AcceptInvite();
            } else {
                DeclineInvite();
            }
        }

        public static void HandlePartyMembersChanged(object sender, LuaEventArgs args) {
            var eventHappened = args.Args[0].ToString();

            CustomNormalLog("Event happened = " + eventHappened);

            LeaveParty();
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

    }
}
