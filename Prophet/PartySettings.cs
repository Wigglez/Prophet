using System;
using System.ComponentModel;
using System.IO;
using Styx;
using Styx.Common;
using Styx.Helpers;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;

namespace Prophet {
    public class PartySettings {

        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        public static PartySettings Instance = new PartySettings();

        private string _partyClassification = "None";
        private string _loot = "Group Loot";
        private string _lootThreshold = "Uncommon";
        private string _passOnLoot = "No";
        private string _setRole = "None";

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

        // Classification shit
        public string PartyClassification { get { return _partyClassification; } set { _partyClassification = value; } }

        // Party leader shit
        public string PartyMemberName1 { get; set; }
        public string PartyMemberName2 { get; set; }
        public string PartyMemberName3 { get; set; }
        public string PartyMemberName4 { get; set; }

        // Party member shit
        public string PartyLeaderName { get; set; }

        // Privileges shit
        public string Loot { get { return _loot; } set { _loot = value; } }
        public string LootThreshold { get { return _lootThreshold; } set { _lootThreshold = value; }}
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
