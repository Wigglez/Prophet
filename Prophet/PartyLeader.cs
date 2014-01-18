using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Chameleon.Helpers;
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

        public static Stopwatch HandlePartyFunctionsTimer = new Stopwatch();
        public static Stopwatch InviteTimer = new Stopwatch();

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public static string Name { get; set; }
        public static string Realm { get; set; }
        public static string NameAndRealm { get; set; }

        public static int PresenceID { get; set; }

        public static int RequiredPartyCount { get; set; }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public static void DetermineNameAndRealm() {
            if(string.IsNullOrEmpty(PartySettings.Instance.PartyLeaderName)) {
                return;
            }

            if(!PartySettings.Instance.PartyLeaderName.Contains('-')) {
                Name = PartySettings.Instance.PartyLeaderName;
                Realm = StyxWoW.Me.RealmName;
            } else {
                var indexOfDash = PartySettings.Instance.PartyLeaderName.IndexOf('-');
                Name = PartySettings.Instance.PartyLeaderName.Substring(0, indexOfDash);
                Realm = PartySettings.Instance.PartyLeaderName.Substring(indexOfDash + 1);
            }

            NameAndRealm = Name + "-" + Realm;
        }

        public static bool CanInvite() {
            return StyxWoW.Me.IsValid && StyxWoW.IsInGame;
        }

        public static bool Exists() {
            return !string.IsNullOrEmpty(PartySettings.Instance.PartyLeaderName);
        }

        public static int GetRequiredPartyCount() {
            return PartySettings.Instance.PartyMemberName.Count(t => !string.IsNullOrEmpty(t));
        }

        public static void SendOutInvites() {
            if(PartySettings.Instance.PartyClassification != PartySettings.StringPartyLeader) {
                return;
            }

            for(var i = 0; i < RequiredPartyCount; i++) {
                if(StyxWoW.Me.GroupInfo.IsInParty) {
                    // Is the party count satisfied?
                    if(Character.GetNumGroupMembers() >= RequiredPartyCount + 1) {
                        continue;
                    }
                }

                if(!ShouldInvite(PartyMember.NameAndRealm[i])) {
                    continue;
                }

                if(!InviteTimer.IsRunning) {
                    if(CustomBlacklist.ContainsName(PartyMember.Name[i])) {
                        continue;
                    }

                    // Send out invites
                    Lua.DoString(string.Format("InviteUnit('{0}')", PartyMember.NameAndRealm[i]));

                    if(PartyMember.Realm[i] != StyxWoW.Me.RealmName) {
                        if(!BNCanInvite(PartyMember.NameAndRealm[i])) {
                            Prophet.CustomNormalLog("Friend could not be found on Battle.net Friends List or is offline.");
                        } else {
                            BNInviteFriend();
                        }
                    }

                    Prophet.CustomNormalLog("Invited {0}.", PartyMember.NameAndRealm[i]);

                    // Start timer and blacklist
                    InviteTimer.Start();

                    var timeToBlacklist = RandomNumber.GenerateRandomInt(61, 75);
                    CustomBlacklist.AddName(PartyMember.Name[i], TimeSpan.FromSeconds(timeToBlacklist));
                    Prophet.CustomDiagnosticLog("Blacklisted {0} from invites for {1} seconds.", PartyMember.Name[i], timeToBlacklist);
                } else {
                    if(InviteTimer.ElapsedMilliseconds >= 1000) {
                        InviteTimer.Reset();
                    }
                }
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        private static bool ShouldInvite(string nameAndRealm) {
            return !string.IsNullOrEmpty(nameAndRealm) && !Character.GroupMemberExistsInParty(nameAndRealm);
        }

        private static int BNGetNumFriends() {
            return Lua.GetReturnVal<int>("return BNGetNumFriends()", 0);
        }

        // presenceID, givenName, surname, toonName, toonID, client, isOnline, lastOnline, isAFK, isDND, broadcastText, noteText, isFriend, broadcastTime  = BNGetFriendInfo(friendIndex)
        private static List<string> BNGetFriendInfo(int i) {
            return Lua.GetReturnValues(String.Format("return BNGetFriendInfo({0})", i));
        }

        // hasFocus, toonName, client, realmName, realmID, faction, race, class, guild, zoneName, level, gameText, broadcastText, broadcastTime, canSoR, toonID = BNGetToonInfo(presenceID or toonID)
        private static List<string> BNGetToonInfo(int presenceID) {
            return Lua.GetReturnValues(String.Format("return BNGetToonInfo({0})", presenceID));
        }

        private static bool BNCanInvite(string nameAndRealm) {
            var numFriends = BNGetNumFriends();

            for(var i = 1; i <= numFriends; i++) {
                try {
                    var presenceID = BNGetFriendInfo(i)[0].ToInt32();
                    var currentClient = BNGetFriendInfo(i)[6];
                    var isOnline = currentClient == "WoW";

                    var toonName = BNGetToonInfo(presenceID)[1];
                    var realmName = BNGetToonInfo(presenceID)[3];

                    var combinedName = toonName + "-" + realmName;

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

        private static void BNInviteFriend() {
            Lua.DoString(String.Format("BNInviteFriend({0})", PresenceID));
        }
    }
}
