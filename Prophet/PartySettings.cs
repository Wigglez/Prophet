using System;
using System.IO;
using Styx;
using Styx.Common;

namespace Prophet {
    public class PartySettings {

        // ===========================================================
        // Constants
        // ===========================================================

        // Party Classification
        public const string StringNone = "None";
        public const string StringPartyLeader = "Party Leader";
        public const string StringPartyMember = "Party Member";

        // Loot
        public const string StringFreeForAll = "Free For All";
        public const string StringRoundRobin = "Round Robin";
        public const string StringMasterLooter = "Master Looter";
        public const string StringGroupLoot = "Group Loot";
        public const string StringNeedBeforeGreed = "Need Before Greed";
    
        // Loot Threshold
        public const string StringUncommon = "Uncommon";
        public const string StringRare = "Rare";
        public const string StringEpic = "Epic";

        // Dungeon Difficulty
        public const string String5PlayerNormal = "5 Player Normal";
        public const string String5PlayerHeroic = "5 Player Heroic";
        public const string StringChallengeMode = "Challenge Mode";

        // Pass on Loot
        public const string StringNo = "No";
        public const string StringYes = "Yes";
        
        // Set Role
        public const string StringNoRole = "No Role";
        public const string StringTank = "Tank";
        public const string StringHealer = "Healer";
        public const string StringDamage = "Damage";

        // ===========================================================
        // Fields
        // ===========================================================

        public static PartySettings Instance = new PartySettings();

        private string[] _partyMemberName = new string[4];

        private string _partyClassification = StringNone;
        private string _loot = StringGroupLoot;
        private string _lootThreshold = StringUncommon;
        private string _dungeonDifficulty = String5PlayerNormal;
        private string _passOnLoot = StringNo;
        private string _setRole = StringNoRole;

        // ===========================================================
        // Constructors
        // ===========================================================

        static PartySettings() {
            var folderPath = Path.GetDirectoryName(SettingsFilePath);

            if(folderPath != null && !Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
            }

            Load();
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public static string SettingsFilePath {
            get { return Path.Combine(Utilities.AssemblyDirectory, string.Format(@"Settings\{0}\{1}-{2}\{3}.xml", "Prophet", StyxWoW.Me.Name, StyxWoW.Me.RealmName, "PartySettings")); }
        }

        // Classification
        public string PartyClassification { get { return _partyClassification; } set { _partyClassification = value; } }

        // Party leader
        public string[] PartyMemberName { get { return _partyMemberName; } set { _partyMemberName = value; } }

        // Party member
        public string PartyLeaderName { get; set; }

        // Privileges
        public string Loot { get { return _loot; } set { _loot = value; } }
        public string LootThreshold { get { return _lootThreshold; } set { _lootThreshold = value; }}
        public string DungeonDifficulty { get { return _dungeonDifficulty; } set { _dungeonDifficulty = value; } }
        public string PassOnLoot { get { return _passOnLoot; } set { _passOnLoot = value; } }
        public string SetRole { get { return _setRole; } set { _setRole = value; } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public static void Load() {
            try {
                Instance = ObjectXMLSerializer<PartySettings>.Load(SettingsFilePath);
            } catch(Exception) {
                Instance = new PartySettings();
            }
        }

        public static void Save() {
            ObjectXMLSerializer<PartySettings>.Save(Instance, SettingsFilePath);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

    }
}
