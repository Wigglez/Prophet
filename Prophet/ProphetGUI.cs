using System;
using System.Windows.Forms;

namespace Prophet {
    public partial class ProphetGUI : Form {
        public ProphetGUI() {
            InitializeComponent();
        }

        private void ProphetGUI_Load(object sender, EventArgs e) {
            // Party Classification
            characterRoleComboBox.Text = PartySettings.Instance.PartyClassification;



            // Party Leader
            partyMember1TextBox.Text = PartySettings.Instance.PartyMemberName[0];
            partyMember2TextBox.Text = PartySettings.Instance.PartyMemberName[1];
            partyMember3TextBox.Text = PartySettings.Instance.PartyMemberName[2];
            partyMember4TextBox.Text = PartySettings.Instance.PartyMemberName[3];

            if(partyMember1TextBox.Text == "") {
                partyMember2Label.Visible = false;
                partyMember2TextBox.Visible = false;
                partyMember2TextBox.Text = "";
            }

            if(partyMember2TextBox.Text == "") {
                partyMember3Label.Visible = false;
                partyMember3TextBox.Visible = false;
                partyMember3TextBox.Text = "";
            }

            if(partyMember3TextBox.Text == "") {
                partyMember4Label.Visible = false;
                partyMember4TextBox.Visible = false;
                partyMember4TextBox.Text = "";
            }

            // Party Member
            partyLeaderTextBox.Text = PartySettings.Instance.PartyLeaderName;

            // Party Leader Privileges
            partyLeaderLootComboBox.Text = PartySettings.Instance.Loot;
            partyLeaderLootThresholdComboBox.Text = PartySettings.Instance.LootThreshold;
            partyLeaderDungeonDifficultyComboBox.Text = PartySettings.Instance.DungeonDifficulty;
            partyLeaderPassOnLootComboBox.Text = PartySettings.Instance.PassOnLoot;
            partyLeaderSetRoleComboBox.Text = PartySettings.Instance.SetRole;

            // Party Member Privileges
            partyMemberPassOnLootComboBox.Text = PartySettings.Instance.PassOnLoot;
            partyMemberSetRoleComboBox.Text = PartySettings.Instance.SetRole;
        }
        
        // Character Classification
        private void characterRoleComboBox_SelectedValueChanged(object sender, EventArgs e) {
            if(characterRoleComboBox.Text == "None") {
                partyLeaderGroupBox.Visible = false;
                partyMemberGroupBox.Visible = false;

                PartyLeaderPrivilegesGroupBox.Visible = false;
                partyMemberPrivilegesGroupBox.Visible = false;
            }

            if(characterRoleComboBox.Text == "Party Leader") {
                partyLeaderGroupBox.Visible = true;
                partyMemberGroupBox.Visible = false;

                PartyLeaderPrivilegesGroupBox.Visible = true;
                partyMemberPrivilegesGroupBox.Visible = false;
            }

            if(characterRoleComboBox.Text == "Party Member") {
                partyLeaderGroupBox.Visible = false;
                partyMemberGroupBox.Visible = true;

                PartyLeaderPrivilegesGroupBox.Visible = false;
                partyMemberPrivilegesGroupBox.Visible = true;
            }

            PartySettings.Instance.PartyClassification = characterRoleComboBox.Text;
            PartySettings.Save();
        }

        // Party Leader
        private void partyMember1TextBox_TextChanged(object sender, EventArgs e) {
            if(partyMember1TextBox.Text == "") {
                partyMember2Label.Visible = false;
                partyMember2TextBox.Visible = false;
                partyMember2TextBox.Text = "";
            } else {
                partyMember2Label.Visible = true;
                partyMember2TextBox.Visible = true;
            }

            PartySettings.Instance.PartyMemberName[0] = partyMember1TextBox.Text;
            PartySettings.Save();
        }

        private void partyMember2TextBox_TextChanged(object sender, EventArgs e) {
            if(partyMember2TextBox.Text == "") {
                partyMember3Label.Visible = false;
                partyMember3TextBox.Visible = false;
                partyMember3TextBox.Text = "";
            } else {
                partyMember3Label.Visible = true;
                partyMember3TextBox.Visible = true;
            }

            PartySettings.Instance.PartyMemberName[1] = partyMember2TextBox.Text;
            PartySettings.Save();
        }

        private void partyMember3TextBox_TextChanged(object sender, EventArgs e) {
            if(partyMember3TextBox.Text == "") {
                partyMember4Label.Visible = false;
                partyMember4TextBox.Visible = false;
                partyMember4TextBox.Text = "";
            } else {
                partyMember4Label.Visible = true;
                partyMember4TextBox.Visible = true;
            }

            PartySettings.Instance.PartyMemberName[2] = partyMember3TextBox.Text;
            PartySettings.Save();
        }

        private void partyMember4TextBox_TextChanged(object sender, EventArgs e) {
            PartySettings.Instance.PartyMemberName[3] = partyMember4TextBox.Text;
            PartySettings.Save();
        }

        // Party Member
        private void partyLeaderTextBox_TextChanged(object sender, EventArgs e) {
            PartySettings.Instance.PartyLeaderName = partyLeaderTextBox.Text;
            PartySettings.Save();
        }

        // Party Leader Privileges
        private void partyLeaderLootComboBox_SelectedValueChanged(object sender, EventArgs e) {
            PartySettings.Instance.Loot = partyLeaderLootComboBox.Text;
            PartySettings.Save();
        }

        private void partyLeaderLootThresholdComboBox_SelectedValueChanged(object sender, EventArgs e) {
            PartySettings.Instance.LootThreshold = partyLeaderLootThresholdComboBox.Text;
            PartySettings.Save();
        }

        private void partyLeaderDungeonDifficultyComboBox_SelectedValueChanged(object sender, EventArgs e) {
            PartySettings.Instance.DungeonDifficulty = partyLeaderDungeonDifficultyComboBox.Text;
            PartySettings.Save();
        }

        private void partyLeaderPassOnLootComboBox_SelectedValueChanged(object sender, EventArgs e) {
            PartySettings.Instance.PassOnLoot = partyLeaderPassOnLootComboBox.Text;
            PartySettings.Save();
        }

        private void partyLeaderSetRoleComboBox_SelectedValueChanged(object sender, EventArgs e) {
            PartySettings.Instance.SetRole = partyLeaderSetRoleComboBox.Text;
            PartySettings.Save();
        }

        // Party Member Privileges
        private void partyMemberPassOnLootComboBox_SelectedValueChanged(object sender, EventArgs e) {
            PartySettings.Instance.PassOnLoot = partyMemberPassOnLootComboBox.Text;
            PartySettings.Save();
        }

        private void partyMemberSetRoleComboBox_SelectedValueChanged(object sender, EventArgs e) {
            PartySettings.Instance.SetRole = partyMemberSetRoleComboBox.Text;
            PartySettings.Save();
        }
    }
}
