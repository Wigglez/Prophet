using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;
using Styx;
using Styx.Common;
using Styx.CommonBot;
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
            get { return "Wigglez"; }
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

            if(!PartySettings.Instance.PartyLeader && !PartySettings.Instance.PartyMember) {
                return;
            }

            if(PartySettings.Instance.PartyLeader && PartySettings.Instance.PartyMember) {
                CustomNormalLog("You can't be both the party leader and a party member.");
                return;
            }

            if(PartySettings.Instance.PartyLeader) {
                if(!PartyMemberExists()) {
                    CustomNormalLog("You have to provide at least one party member name.");
                    return;
                }

                NumFriends = BNGetNumFriends();
            }

            if(PartySettings.Instance.PartyMember) {
                if(!PartyLeaderExists()) {
                    CustomNormalLog("You have to provide the party leader name.");
                    return;
                }

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
            var joinedParty = Lua.GetReturnVal<string>("return JOINED_PARTY", 0); // "%s joins the party."
            var leftParty = Lua.GetReturnVal<string>("return LEFT_PARTY", 0); // "%s leaves the party."
            var notInGroup = Lua.GetReturnVal<string>("return NOT_IN_GROUP", 0); // "You are no longer in a party."
            var errInvitedAlreadyInGroup = Lua.GetReturnVal<string>("return ERR_INVITED_ALREADY_IN_GROUP_SS", 0); // "|Hplayer:%s|h[%s]|h invited you to a group, but you could not accept because you are already in a group."
            var errInvitedToGroup = Lua.GetReturnVal<string>("return ERR_INVITED_TO_GROUP_SS", 0); // "|Hplayer:%s|h[%s]|h has invited you to join a group."
            var errInvitePlayer = Lua.GetReturnVal<string>("return ERR_INVITE_PLAYER_S", 0); // "You have invited %s to join your group."
            var errInviteRestricted = Lua.GetReturnVal<string>("return ERR_INVITE_RESTRICTED", 0); // "Trial accounts cannot invite characters into groups."
            var errInviteSelf = Lua.GetReturnVal<string>("return ERR_INVITE_SELF", 0); // "You can't invite yourself to a group."

            if(errorMessage.Equals(errGroupFull)) {
                CustomNormalLog("");
            } else if(errorMessage.Equals(joinedParty)) {
                CustomNormalLog("");
            } else if(errorMessage.Equals(leftParty)) {
                CustomNormalLog("");
            } else if(errorMessage.Equals(notInGroup)) {
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

        public static int ShouldHavePartyMemberAmount() {
            var count = 1;
            if(PartySettings.Instance.PartyMemberName1 != "") { count++; }
            if(PartySettings.Instance.PartyMemberName2 != "") { count++; }
            if(PartySettings.Instance.PartyMemberName3 != "") { count++; }
            if(PartySettings.Instance.PartyMemberName4 != "") { count++; }
            return count;
        }

        public static bool PartyMemberExists() {
            return PartySettings.Instance.PartyMemberName1 != "" || PartySettings.Instance.PartyMemberName2 != "" || PartySettings.Instance.PartyMemberName3 != "" || PartySettings.Instance.PartyMemberName4 != "";
        }

        public static bool PartyLeaderExists() {
            return PartySettings.Instance.PartyLeaderName != "";
        }

        public static int GetNumGroupMembers() {
            return Lua.GetReturnVal<int>("return GetNumGroupMembers()", 0);
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

        }

        public static void SendOutInvites() {
            if(PartyMembers < ShouldHavePartyMemberAmount()) {
                // Now we should invite members.
            }
        }

        public static void AcceptInvite() {
            Lua.DoString("AcceptGroup()");
            Lua.DoString("StaticPopup_Hide('PARTY_INVITE')");
        }

        public static void DeclineInvite() {
            Lua.DoString("DeclineGroup()");
            Lua.DoString("StaticPopup_Hide('PARTY_INVITE')");
        }

        public static void HandlePartyInviteRequest(object sender, LuaEventArgs args) {
            if(PartySettings.Instance.PartyLeader) { return; }
            if(Me.GroupInfo.IsInParty) {
                return;
            }
            
            // Arg 1 is the party leader name
            var inviteRequest = args.Args[0].ToString();

            if(inviteRequest == PartySettings.Instance.PartyLeaderName) {
                CustomNormalLog("We got an invite from the designated party leader, accepting.");
                AcceptInvite();
            } else {
                CustomNormalLog("We got an invite from some random stranger, declining.");
                DeclineInvite();
            }
        }

        public static void HandlePartyMembersChanged(object sender, LuaEventArgs args) {
            if(!PartySettings.Instance.PartyLeader) {
                return;
            }

            PartyMembers = GetNumGroupMembers();
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

    }
}
