using System;
using System.Collections.Generic;
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
            partyMember1TextBox.Text = PartySettings.Instance.PartyMemberName1;
            partyMember2TextBox.Text = PartySettings.Instance.PartyMemberName2;
            partyMember3TextBox.Text = PartySettings.Instance.PartyMemberName3;
            partyMember4TextBox.Text = PartySettings.Instance.PartyMemberName4;

            // Party Member
            partyLeaderTextBox.Text = PartySettings.Instance.PartyLeaderName;

            // Party Leader Privileges
            partyLeaderLootComboBox.Text = PartySettings.Instance.Loot;
            partyLeaderLootThresholdComboBox.Text = PartySettings.Instance.LootThreshold;
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
            PartySettings.Instance.PartyMemberName1 = partyMember1TextBox.Text;
            PartySettings.Save();
        }

        private void partyMember2TextBox_TextChanged(object sender, EventArgs e) {
            PartySettings.Instance.PartyMemberName2 = partyMember2TextBox.Text;
            PartySettings.Save();
        }

        private void partyMember3TextBox_TextChanged(object sender, EventArgs e) {
            PartySettings.Instance.PartyMemberName3 = partyMember3TextBox.Text;
            PartySettings.Save();
        }

        private void partyMember4TextBox_TextChanged(object sender, EventArgs e) {
            PartySettings.Instance.PartyMemberName4 = partyMember4TextBox.Text;
            PartySettings.Save();
        }

        // Party Member
        private void partyLeaderTextBox_TextChanged(object sender, EventArgs e) {
            PartySettings.Instance.PartyLeaderName = partyLeaderTextBox.Text;
            PartySettings.Save();
        }

        // Party Leader Privileges
        private void partyLeaderLootComboBox_SelectedValueChanged(object sender, EventArgs e) {
            if(partyLeaderLootComboBox.Text == "Free For All") {
            }

            if(partyLeaderLootComboBox.Text == "Round Robin") {
            }

            if(partyLeaderLootComboBox.Text == "Master Looter") {
            }

            if(partyLeaderLootComboBox.Text == "Group Loot") {
            }

            if(partyLeaderLootComboBox.Text == "Need Before Greed") {
            }

            PartySettings.Instance.Loot = partyLeaderLootComboBox.Text;
            PartySettings.Save();
        }

        private void partyLeaderLootThresholdComboBox_SelectedValueChanged(object sender, EventArgs e) {
            if(partyLeaderLootThresholdComboBox.Text == "Uncommon") {
            }

            if(partyLeaderLootThresholdComboBox.Text == "Rare") {
            }

            if(partyLeaderLootThresholdComboBox.Text == "Epic") {
            }

            PartySettings.Instance.LootThreshold = partyLeaderLootThresholdComboBox.Text;
            PartySettings.Save();
        }

        private void partyLeaderPassOnLootComboBox_SelectedValueChanged(object sender, EventArgs e) {
            if(partyLeaderPassOnLootComboBox.Text == "No") {
            }

            if(partyLeaderPassOnLootComboBox.Text == "Yes") {
            }

            PartySettings.Instance.PassOnLoot = partyLeaderPassOnLootComboBox.Text;
            PartySettings.Save();
        }

        private void partyLeaderSetRoleComboBox_SelectedValueChanged(object sender, EventArgs e) {
            if(partyLeaderSetRoleComboBox.Text == "None") {
            }

            if(partyLeaderSetRoleComboBox.Text == "Tank") {
            }

            if(partyLeaderSetRoleComboBox.Text == "Healer") {
            }

            if(partyLeaderSetRoleComboBox.Text == "Damage") {
            }

            PartySettings.Instance.SetRole = partyLeaderSetRoleComboBox.Text;
            PartySettings.Save();
        }

        // Party Member Privileges
        private void partyMemberPassOnLootComboBox_SelectedValueChanged(object sender, EventArgs e) {
            if(partyMemberPassOnLootComboBox.Text == "No") {
            }

            if(partyMemberPassOnLootComboBox.Text == "Yes") {
            }

            PartySettings.Instance.PassOnLoot = partyMemberPassOnLootComboBox.Text;
            PartySettings.Save();
        }

        private void partyMemberSetRoleComboBox_SelectedValueChanged(object sender, EventArgs e) {
            if(partyMemberSetRoleComboBox.Text == "None") {
            }

            if(partyMemberSetRoleComboBox.Text == "Tank") {
            }

            if(partyMemberSetRoleComboBox.Text == "Healer") {
            }

            if(partyMemberSetRoleComboBox.Text == "Damage") {
            }

            PartySettings.Instance.SetRole = partyMemberSetRoleComboBox.Text;
            PartySettings.Save();
        }
    }
}
