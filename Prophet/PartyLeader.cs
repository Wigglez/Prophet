using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Styx;
using Styx.Helpers;
using Styx.WoWInternals;

namespace Prophet {
    public class PartyLeader {

        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        public static Stopwatch PartyLeaderTimer = new Stopwatch();

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public static int PresenceID { get; set; }

        public static int RequiredPartyCount { get; set; }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public static bool CanInvite() { return Character.Me.IsValid && StyxWoW.IsInGame; }

        public static bool Exists() { return !string.IsNullOrEmpty(PartySettings.Instance.PartyLeaderName); }

        public static int GetRequiredPartyCount() {
            var count = 0;

            if(!string.IsNullOrEmpty(PartySettings.Instance.PartyMemberName1)) {
                if(PartySettings.Instance.PartyMemberName1.Contains('-')) {
                    PartyMember.Name[0] = PartySettings.Instance.PartyMemberName1;
                } else {
                    PartyMember.Name[0] = PartySettings.Instance.PartyMemberName1 + '-' + Character.Me.RealmName;
                }

                count++;
            }

            if(!string.IsNullOrEmpty(PartySettings.Instance.PartyMemberName2)) {
                if(PartySettings.Instance.PartyMemberName2.Contains('-')) {
                    PartyMember.Name[1] = PartySettings.Instance.PartyMemberName2;
                } else {
                    PartyMember.Name[1] = PartySettings.Instance.PartyMemberName2 + '-' + Character.Me.RealmName;
                }

                count++;
            }

            if(!string.IsNullOrEmpty(PartySettings.Instance.PartyMemberName3)) {
                if(PartySettings.Instance.PartyMemberName3.Contains('-')) {
                    PartyMember.Name[2] = PartySettings.Instance.PartyMemberName3;
                } else {
                    PartyMember.Name[2] = PartySettings.Instance.PartyMemberName3 + '-' + Character.Me.RealmName;
                }

                count++;
            }

            if(!string.IsNullOrEmpty(PartySettings.Instance.PartyMemberName4)) {
                if(PartySettings.Instance.PartyMemberName4.Contains('-')) {
                    PartyMember.Name[3] = PartySettings.Instance.PartyMemberName4;
                } else {
                    PartyMember.Name[3] = PartySettings.Instance.PartyMemberName4 + '-' + Character.Me.RealmName;
                }

                count++;
            }

            return count;
        }

        public static bool ShouldInvite(string name) {
            return name != "" && !Character.GroupMemberExistsInParty(name);
        }

        public static void SendOutInvites() {
            if(PartySettings.Instance.PartyClassification != "Party Leader") { return; }

            for(var i = 0; i < RequiredPartyCount; i++) {
                if(Character.Me.GroupInfo.IsInParty) {
                    if(Character.GetNumGroupMembers() >= RequiredPartyCount + 1) { continue; }
                }
                
                if(!ShouldInvite(PartyMember.Name[i])) { continue; }
                //Prophet.CustomNormalLog("RequiredPartyCount = {0}", RequiredPartyCount);
                //Prophet.CustomNormalLog("GetNumGroupMembers = {0}", GetNumGroupMembers());

                //Prophet.CustomNormalLog("SendOutInvites: Name = {0}", PartyMember.Name[i]);

                // Scan the friends list for our friend
                if(!BNCanInvite(PartyMember.Name[i])) {
                    Prophet.CustomNormalLog("SendOutInvites: Can't invite {0}", PartyMember.Name[i]);
                    continue;
                }

                // Invite the friend using the current presence id
                if(!PartyLeaderTimer.IsRunning) {
                    //Prophet.CustomNormalLog("SendOutInvites: Presence ID = {0}", PresenceID);
                    BNInviteFriend();
                    PartyLeaderTimer.Start();
                } else {
                    if(PartyLeaderTimer.ElapsedMilliseconds >= 2000) {
                        PartyLeaderTimer.Reset();
                    }
                }
            }
        }

        public static int BNGetNumFriends() {
            return Lua.GetReturnVal<int>("return BNGetNumFriends()", 0);
        }

        // presenceID, givenName, surname, toonName, toonID, client, isOnline, lastOnline, isAFK, isDND, broadcastText, noteText, isFriend, broadcastTime  = BNGetFriendInfo(friendIndex)
        public static List<string> BNGetFriendInfo(int i) {
            return Lua.GetReturnValues(String.Format("return BNGetFriendInfo({0})", i));
        }

        // hasFocus, toonName, client, realmName, realmID, faction, race, class, guild, zoneName, level, gameText, broadcastText, broadcastTime, canSoR, toonID = BNGetToonInfo(presenceID or toonID)
        public static List<string> BNGetToonInfo(int presenceID) {
            return Lua.GetReturnValues(String.Format("return BNGetToonInfo({0})", presenceID));
        }

        public static bool BNCanInvite(string nameAndRealm) {
            var numFriends = BNGetNumFriends();

            //Prophet.CustomNormalLog("BNCanInvite: numFriends = {0}", numFriends);

            for(var i = 1; i <= numFriends; i++) {
                try {
                    var presenceID = BNGetFriendInfo(i)[0].ToInt32();
                    //Prophet.CustomNormalLog("BNCanInvite: presenceID = {0}", presenceID);
                    var currentClient = BNGetFriendInfo(i)[6];
                    var isOnline = currentClient == "WoW";
                    //Prophet.CustomNormalLog("BNCanInvite: isOnline = {0}", isOnline);

                    var toonName = BNGetToonInfo(presenceID)[1];
                    //Prophet.CustomNormalLog("BNCanInvite: toonName = {0}", toonName);
                    var realmName = BNGetToonInfo(presenceID)[3];
                    //Prophet.CustomNormalLog("BNCanInvite: realmName = {0}", realmName);

                    var combinedName = toonName + "-" + realmName;
                    //Prophet.CustomNormalLog("BNCanInvite: combinedName = {0}", combinedName);

                    if(combinedName != nameAndRealm || !isOnline) {
                        continue;
                    }

                    PresenceID = presenceID;
                    return true;
                } catch(Exception e) {
                    Prophet.CustomDiagnosticLog("Exception = {0}, Stacktrace = {1}", e, e.StackTrace);
                }
            }

            return false;
        }

        public static void BNInviteFriend() {
            Lua.DoString(String.Format("BNInviteFriend({0})", PresenceID));
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
