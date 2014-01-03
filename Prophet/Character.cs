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

        public static void AcceptInvite() {
            Prophet.CustomNormalLog("Accepted invite.");
            Lua.DoString("AcceptGroup()");
        }

        public static void DeclineInvite() {
            Prophet.CustomNormalLog("Declined invite.");
            Lua.DoString("DeclineGroup()");

            HandleStaticPopup();
        }

        public static void HandlePartyInviteRequest(object sender, LuaEventArgs args) {
            if(PartySettings.Instance.PartyClassification == "Party Leader") {
                DeclineInvite();
                return;
            }

            var partyInviteSender = args.Args[0].ToString();

            Prophet.CustomNormalLog("We got an invite from " + partyInviteSender + ".");

            if(partyInviteSender == PartyLeader.Name) {
                AcceptInvite();
            } else {
                DeclineInvite();
            }
        }

        public static void HandleStaticPopup() {
            // If there is a popup visible, get rid of that shit
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

        public static bool GroupMemberExistsInParty(string name) {
            if(GetNumGroupMembers() <= 1) {
                return false;
            }

            for(var i = 1; i <= GetNumGroupMembers(); i++) {
                var raidRosterInfo = Lua.GetReturnVal<string>(String.Format("return (select(1, GetRaidRosterInfo({0})))", i), 0);

                //Prophet.CustomNormalLog("GroupMemberExistsInParty: raidrosterinfo name = {0}", raidRosterInfo);
                if(PartySettings.Instance.PartyClassification == "Party Leader") {
                    for(var j = 0; j < PartySettings.Instance.PartyMemberName.Length; j++) {
                        if(raidRosterInfo == PartyMember.Name[j]) {
                            //Prophet.CustomNormalLog("GroupMemberExistsInParty: raidRosterInfo == nameNoRealm, i = {0}", i);
                            return true;
                        }
                    }
                } else {
                    if(raidRosterInfo == PartyLeader.Name) {
                        //Prophet.CustomNormalLog("GroupMemberExistsInParty: raidRosterInfo == nameNoRealm, i = {0}", i);
                        return true;
                    }
                }
            }

            //Prophet.CustomNormalLog("GroupMemberExistsInParty: name = {0}", nameNoRealm);

            return false;
        }

        // method, partyMaster, raidMaster
        public static List<string> GetLootMethod() {
            return Lua.GetReturnValues("return GetLootMethod()");
        }

        public static void SetLootMethod(string method) {
            /*
             * freeforall - Free for All - any group member can take any loot at any time
             * group - Group Loot - like Round Robin, but items above a quality threshold are rolled on
             * master - Master Looter - like Round Robin, but items above a quality threshold are left for a designated loot master to
             * needbeforegreed - Need before Greed - like Group Loot, but members automatically pass on items
             * roundrobin - Round Robin - group members take turns being able to loot
             */
            Lua.DoString(method == "master" ? String.Format("SetLootMethod('{0}', '{1}')", method, Me.Name) : String.Format("SetLootMethod('{0}')", method));
        }

        public static void HandleLootMethod() {
            var lootMethod = GetLootMethod()[0];

            switch(PartySettings.Instance.Loot) {
                case "Free For All":
                    if(lootMethod == "freeforall") {
                        return;
                    }

                    SetLootMethod("freeforall");

                    break;
                case "Group Loot":
                    if(lootMethod == "group") {
                        return;
                    }

                    SetLootMethod("group");

                    break;
                case "Master Looter":
                    if(lootMethod == "master") {
                        return;
                    }

                    SetLootMethod("master");

                    break;
                case "Need Before Greed":
                    if(lootMethod == "needbeforegreed") {
                        return;
                    }

                    SetLootMethod("needbeforegreed");

                    break;
                case "Round Robin":
                    if(lootMethod == "roundrobin") {
                        return;
                    }

                    SetLootMethod("roundrobin");

                    break;
            }
        }

        public static int GetLootThreshold() {
            return Lua.GetReturnVal<int>("return GetLootThreshold()", 0);
        }

        public static void SetLootThreshold(int threshold) {
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

        public static void HandleLootThreshold() {
            var lootThreshold = GetLootThreshold();

            switch(PartySettings.Instance.LootThreshold) {
                case "Uncommon":
                    if(lootThreshold == 2) {
                        return;
                    }

                    SetLootThreshold(2);

                    break;
                case "Rare":
                    if(lootThreshold == 3) {
                        return;
                    }

                    SetLootThreshold(3);

                    break;
                case "Epic":
                    if(lootThreshold == 4) {
                        return;
                    }

                    SetLootThreshold(4);

                    break;
            }
        }

        public static int GetDungeonDifficultyID() {
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

        public static void SetDungeonDifficultyID(int difficultyIndex) {
            Lua.DoString("SetDungeonDifficultyID({0})", difficultyIndex);
        }

        public static void HandleDungeonDifficulty() {
            var dungeonDifficulty = GetDungeonDifficultyID();

            switch(PartySettings.Instance.DungeonDifficulty) {
                case "5 Player Normal":
                    if(dungeonDifficulty == 1) {
                        return;
                    }

                    SetDungeonDifficultyID(1);

                    break;
                case "5 Player Heroic":
                    if(dungeonDifficulty == 2) {
                        return;
                    }

                    SetDungeonDifficultyID(2);

                    break;
                case "Challenge Mode":
                    if(dungeonDifficulty == 8) {
                        return;
                    }

                    SetDungeonDifficultyID(8);

                    break;
            }
        }

        public static int GetOptOutOfLoot() {
            // Returns 1 if opted out, otherwise nil
            return Lua.GetReturnVal<int>("return GetOptOutOfLoot()", 0);
        }

        public static void SetOptOutOfLoot(string optOut) {
            // True to opt out, false to participate in loot rolls
            Lua.DoString(String.Format("SetOptOutOfLoot({0})", optOut));
        }

        public static void HandlePassOnLoot() {
            var passOnLoot = GetOptOutOfLoot();

            switch(PartySettings.Instance.PassOnLoot) {
                case "No":
                    if(passOnLoot == 0) {
                        return;
                    }

                    SetOptOutOfLoot("false");

                    break;
                case "Yes":
                    if(passOnLoot == 1) {
                        return;
                    }

                    SetOptOutOfLoot("true");

                    break;
            }
        }

        public static string UnitGroupRoleAssigned() {
            /*
             * DAMAGER
             * HEALER
             * NONE
             * TANK
             */
            return Lua.GetReturnVal<string>("return UnitGroupRolesAssigned('player')", 0);
        }

        public static void UnitSetRole(string role) {
            /*
             * DAMAGER
             * HEALER
             * NONE
             * TANK
             */
            Lua.DoString(String.Format("UnitSetRole('player', '{0}')", role));
        }

        public static void HandleSetRole() {
            var role = UnitGroupRoleAssigned();

            switch(PartySettings.Instance.SetRole) {
                case "No Role":
                    if(role == "NONE") {
                        return;
                    }

                    UnitSetRole("NONE");

                    break;
                case "Tank":
                    if(role == "TANK") {
                        return;
                    }

                    UnitSetRole("TANK");

                    break;
                case "Healer":
                    if(role == "HEALER") {
                        return;
                    }

                    UnitSetRole("HEALER");

                    break;
                case "Damage":
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
    }
}
