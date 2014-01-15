using System;
using System.Collections.Generic;
using System.Linq;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

namespace Prophet {
    public class Character {

        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        public static LocalPlayer Me = StyxWoW.Me;

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public static void HandlePartyInviteRequest(object sender, LuaEventArgs args) {
            if(PartySettings.Instance.PartyClassification == PartySettings.StringPartyLeader) {
                DeclineInvite();
                return;
            }

            var partyInviteSender = args.Args[0].ToString();

            Prophet.CustomNormalLog("We got an invite from " + partyInviteSender + ".");

            if(partyInviteSender == PartyLeader.NameAndRealm || partyInviteSender == PartyLeader.Name) {
                AcceptInvite();
            } else {
                DeclineInvite();
            }
        }

        public static void HandleStaticPopup() {
            // If there is a popup visible, get rid of that
            var partyInvitePopup = Lua.GetReturnVal<bool>("return StaticPopup_Visible('PARTY_INVITE')", 0);
            var partyInviteCrossrealmPopup = Lua.GetReturnVal<bool>("return StaticPopup_Visible('PARTY_INVITE_XREALM')", 0);

            if(!partyInvitePopup && !partyInviteCrossrealmPopup) {
                return;
            }

            Lua.DoString("StaticPopup_Hide('PARTY_INVITE')");
            Lua.DoString("StaticPopup_Hide('PARTY_INVITE_XREALM')");
        }

        public static int GetNumGroupMembers() {
            return StyxWoW.Me.GroupInfo.RaidMembers.Count();
        }

        // Can pass in a name alone or both name and realm
        public static bool GroupMemberExistsInParty(string nameAndRealm) {
            if(GetNumGroupMembers() <= 1) {
                return false;
            }

            var nameNoRealm = nameAndRealm.Replace(" ", "");

            if(nameNoRealm.Contains('-')) {
                var index = nameNoRealm.IndexOf('-');
                var toonRealm = nameNoRealm.Substring(index + 1);

                var leaderRealm = Me.RealmName;

                if(leaderRealm == toonRealm) {
                    nameNoRealm = nameAndRealm.Substring(0, index);
                }
            }

            for(var i = 1; i <= GetNumGroupMembers(); i++) {
                var raidRosterInfo = Lua.GetReturnVal<string>(String.Format("return (select(1, GetRaidRosterInfo({0})))", i), 0);

                if(raidRosterInfo != nameNoRealm) {
                    continue;
                }

                return true;
            }

            return false;
        }

        public static void HandlePartyLeaderPromotion() {
            if(!Me.IsGroupLeader) {
                return;
            }

            if(!GroupMemberExistsInParty(PartyLeader.NameAndRealm)) {
                return;
            }

            Prophet.CustomNormalLog("I am the leader, trying to promote designated leader.");
            Lua.DoString(string.Format("PromoteToLeader('{0}');", PartyLeader.Name));
            Lua.DoString(string.Format("PromoteToLeader('{0}');", PartyLeader.NameAndRealm));
        }

        public static void HandleLootMethod() {
            var lootMethod = GetLootMethod()[0];

            switch(PartySettings.Instance.Loot) {
                case PartySettings.StringFreeForAll:
                    if(lootMethod == "freeforall") {
                        return;
                    }

                    SetLootMethod("freeforall");

                    break;
                case PartySettings.StringGroupLoot:
                    if(lootMethod == "group") {
                        return;
                    }

                    SetLootMethod("group");

                    break;
                case PartySettings.StringMasterLooter:
                    if(lootMethod == "master") {
                        return;
                    }

                    SetLootMethod("master");

                    break;
                case PartySettings.StringNeedBeforeGreed:
                    if(lootMethod == "needbeforegreed") {
                        return;
                    }

                    SetLootMethod("needbeforegreed");

                    break;
                case PartySettings.StringRoundRobin:
                    if(lootMethod == "roundrobin") {
                        return;
                    }

                    SetLootMethod("roundrobin");

                    break;
            }
        }

        public static void HandleLootThreshold() {
            var lootThreshold = GetLootThreshold();

            switch(PartySettings.Instance.LootThreshold) {
                case PartySettings.StringUncommon:
                    if(lootThreshold == 2) {
                        return;
                    }

                    SetLootThreshold(2);

                    break;
                case PartySettings.StringRare:
                    if(lootThreshold == 3) {
                        return;
                    }

                    SetLootThreshold(3);

                    break;
                case PartySettings.StringEpic:
                    if(lootThreshold == 4) {
                        return;
                    }

                    SetLootThreshold(4);

                    break;
            }
        }

        public static void HandleDungeonDifficulty() {
            var dungeonDifficulty = GetDungeonDifficultyID();

            switch(PartySettings.Instance.DungeonDifficulty) {
                case PartySettings.String5PlayerNormal:
                    if(dungeonDifficulty == 1) {
                        return;
                    }

                    SetDungeonDifficultyID(1);

                    break;
                case PartySettings.String5PlayerHeroic:
                    if(dungeonDifficulty == 2) {
                        return;
                    }

                    SetDungeonDifficultyID(2);

                    break;
                case PartySettings.StringChallengeMode:
                    if(dungeonDifficulty == 8) {
                        return;
                    }

                    SetDungeonDifficultyID(8);

                    break;
            }
        }

        public static void HandlePassOnLoot() {
            var passOnLoot = GetOptOutOfLoot();

            switch(PartySettings.Instance.PassOnLoot) {
                case PartySettings.StringNo:
                    if(passOnLoot == 0) {
                        return;
                    }

                    SetOptOutOfLoot("false");

                    break;
                case PartySettings.StringYes:
                    if(passOnLoot == 1) {
                        return;
                    }

                    SetOptOutOfLoot("true");

                    break;
            }
        }

        public static void HandleSetRole() {
            var role = UnitGroupRoleAssigned();

            switch(PartySettings.Instance.SetRole) {
                case PartySettings.StringNoRole:
                    if(role == "NONE") {
                        return;
                    }

                    UnitSetRole("NONE");

                    break;
                case PartySettings.StringTank:
                    if(role == "TANK") {
                        return;
                    }

                    UnitSetRole("TANK");

                    break;
                case PartySettings.StringHealer:
                    if(role == "HEALER") {
                        return;
                    }

                    UnitSetRole("HEALER");

                    break;
                case PartySettings.StringDamage:
                    if(role == "DAMAGER") {
                        return;
                    }

                    UnitSetRole("DAMAGER");

                    break;
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        private static void AcceptInvite() {
            Prophet.CustomNormalLog("Accepted invite.");
            Lua.DoString("AcceptGroup()");
        }

        private static void DeclineInvite() {
            Prophet.CustomNormalLog("Declined invite due to invalid leader.");
            Lua.DoString("DeclineGroup()");

            HandleStaticPopup();
        }

        // method, partyMaster, raidMaster
        private static List<string> GetLootMethod() {
            return Lua.GetReturnValues("return GetLootMethod()");
        }

        private static void SetLootMethod(string method) {
            /*
             * freeforall - Free for All - any group member can take any loot at any time
             * group - Group Loot - like Round Robin, but items above a quality threshold are rolled on
             * master - Master Looter - like Round Robin, but items above a quality threshold are left for a designated loot master to
             * needbeforegreed - Need before Greed - like Group Loot, but members automatically pass on items
             * roundrobin - Round Robin - group members take turns being able to loot
             */
            Lua.DoString(method == "master" ? String.Format("SetLootMethod('{0}', '{1}')", method, Me.Name) : String.Format("SetLootMethod('{0}')", method));
        }

        private static int GetLootThreshold() {
            return Lua.GetReturnVal<int>("return GetLootThreshold()", 0);
        }

        private static void SetLootThreshold(int threshold) {
            /*
             * 0. Poor (gray)
             * 1. Common (white)
             * 2. Uncommon (green)
             * 3. Rare / Superior (blue)
             * 4. Epic (purple)
             * 5. Legendary (orange)
             * 6. Artifact (golden yellow)
             * 7. Heirloom (light yellow)
             */
            Lua.DoString(String.Format("SetLootThreshold({0})", threshold));
        }

        private static int GetDungeonDifficultyID() {
            /*
             * 1 - "Normal"
             * 2 - "Heroic"
             * 3 - "10 Player"
             * 4 - "25 Player"
             * 5 - "10 Player (Heroic)"
             * 6 - "25 Player (Heroic)"
             * 7 - "Looking For Raid"
             * 8 - "Challenge Mode"
             * 9 - "40 Player"
             * 10 - nil
             * 11 - "Heroic Scenario"
             * 12 - "Normal Scenario"
             * 13 - nil
             * 14 - "Flexible"
             */
            return Lua.GetReturnVal<int>("return GetDungeonDifficultyID()", 0);
        }

        private static void SetDungeonDifficultyID(int difficultyIndex) {
            Lua.DoString("SetDungeonDifficultyID({0})", difficultyIndex);
        }

        private static int GetOptOutOfLoot() {
            // Returns 1 if opted out, otherwise nil
            return Lua.GetReturnVal<int>("return GetOptOutOfLoot()", 0);
        }

        private static void SetOptOutOfLoot(string optOut) {
            // True to opt out, false to participate in loot rolls
            Lua.DoString(String.Format("SetOptOutOfLoot({0})", optOut));
        }

        private static string UnitGroupRoleAssigned() {
            /*
             * DAMAGER
             * HEALER
             * NONE
             * TANK
             */
            return Lua.GetReturnVal<string>("return UnitGroupRolesAssigned('player')", 0);
        }

        private static void UnitSetRole(string role) {
            /*
             * DAMAGER
             * HEALER
             * NONE
             * TANK
             */
            Lua.DoString(String.Format("UnitSetRole('player', '{0}')", role));
        }
    }
}
