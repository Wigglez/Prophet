using System.ComponentModel;
using System.IO;
using Styx;
using Styx.Common;
using Styx.Helpers;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;

namespace Prophet {
    public class PartySettings : Settings {

        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private static PartySettings _instance;

        // ===========================================================
        // Constructors
        // ===========================================================
        public PartySettings()
            : base(Path.Combine(Path.Combine(Utilities.AssemblyDirectory, "Settings"), string.Format(@"Settings\{0}\{1}\{2}.xml", StyxWoW.Me.Name, "Prophet", "PartySettings"))) {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public static PartySettings Instance { get { return _instance ?? (_instance = new PartySettings()); } }

        // Party classification shit
        [Setting]
        [DefaultValue(false)]
        [Category("1). Party Classification")]
        [DisplayName("This character is the party leader")]
        [Description("Toggles if this character should be the leader of the party.")]
        public bool PartyLeader { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("1). Party Classification")]
        [DisplayName("This character is a party member")]
        [Description("Toggles if this character should be a party member.")]
        public bool PartyMember { get; set; }


        // Party leader shit
        [Setting]
        [DefaultValue("")]
        [Category("2). Party Leader")]
        [DisplayName("Party member 1 name")]
        [Description("The character name of party member 1. To make this valid for cross-realm invites, use Name-Server. Ex: Gordonramsay-Sen'jin")]
        public string PartyMemberName1 { get; set; }

        [Setting]
        [DefaultValue("")]
        [Category("2). Party Leader")]
        [DisplayName("Party member 2 name")]
        [Description("The character name of party member 2. To make this valid for cross-realm invites, use Name-Server. Ex: Gordonramsay-Sen'jin")]
        public string PartyMemberName2 { get; set; }

        [Setting]
        [DefaultValue("")]
        [Category("2). Party Leader")]
        [DisplayName("Party member 3 name")]
        [Description("The character name of party member 3. To make this valid for cross-realm invites, use Name-Server. Ex: Gordonramsay-Sen'jin")]
        public string PartyMemberName3 { get; set; }

        [Setting]
        [DefaultValue("")]
        [Category("2). Party Leader")]
        [DisplayName("Party member 4 name")]
        [Description("The character name of party member 4. To make this valid for cross-realm invites, use Name-Server. Ex: Gordonramsay-Sen'jin")]
        public string PartyMemberName4 { get; set; }


        // Party member shit
        [Setting]
        [DefaultValue("")]
        [Category("2). Party Member")]
        [DisplayName("Party leader name")]
        [Description("The character name of your party leader. This is the only character you will accept invites from.")]
        public string PartyLeaderName { get; set; }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

    }
}
