namespace Prophet {
    partial class ProphetGUI {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.partyClassificationGroupBox = new System.Windows.Forms.GroupBox();
            this.characterRoleLabel = new System.Windows.Forms.Label();
            this.characterRoleComboBox = new System.Windows.Forms.ComboBox();
            this.partyLeaderGroupBox = new System.Windows.Forms.GroupBox();
            this.partyMember4TextBox = new System.Windows.Forms.TextBox();
            this.partyMember3TextBox = new System.Windows.Forms.TextBox();
            this.partyMember2TextBox = new System.Windows.Forms.TextBox();
            this.partyMember1TextBox = new System.Windows.Forms.TextBox();
            this.partyMember4Label = new System.Windows.Forms.Label();
            this.partyMember3Label = new System.Windows.Forms.Label();
            this.partyMember2Label = new System.Windows.Forms.Label();
            this.partyMember1Label = new System.Windows.Forms.Label();
            this.partyMemberGroupBox = new System.Windows.Forms.GroupBox();
            this.partyLeaderTextBox = new System.Windows.Forms.TextBox();
            this.partyLeaderLabel = new System.Windows.Forms.Label();
            this.PartyLeaderPrivilegesGroupBox = new System.Windows.Forms.GroupBox();
            this.partyLeaderSetRoleLabel = new System.Windows.Forms.Label();
            this.partyLeaderLootThresholdLabel = new System.Windows.Forms.Label();
            this.partyLeaderPassOnLootLabel = new System.Windows.Forms.Label();
            this.partyLeaderLootLabel = new System.Windows.Forms.Label();
            this.partyLeaderSetRoleComboBox = new System.Windows.Forms.ComboBox();
            this.partyLeaderLootThresholdComboBox = new System.Windows.Forms.ComboBox();
            this.partyLeaderPassOnLootComboBox = new System.Windows.Forms.ComboBox();
            this.partyLeaderLootComboBox = new System.Windows.Forms.ComboBox();
            this.partyMemberPrivilegesGroupBox = new System.Windows.Forms.GroupBox();
            this.partyMemberSetRoleLabel = new System.Windows.Forms.Label();
            this.partyMemberPassOnLootLabel = new System.Windows.Forms.Label();
            this.partyMemberSetRoleComboBox = new System.Windows.Forms.ComboBox();
            this.partyMemberPassOnLootComboBox = new System.Windows.Forms.ComboBox();
            this.partyLeaderDungeonDifficultyLabel = new System.Windows.Forms.Label();
            this.partyLeaderDungeonDifficultyComboBox = new System.Windows.Forms.ComboBox();
            this.partyClassificationGroupBox.SuspendLayout();
            this.partyLeaderGroupBox.SuspendLayout();
            this.partyMemberGroupBox.SuspendLayout();
            this.PartyLeaderPrivilegesGroupBox.SuspendLayout();
            this.partyMemberPrivilegesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // partyClassificationGroupBox
            // 
            this.partyClassificationGroupBox.AutoSize = true;
            this.partyClassificationGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.partyClassificationGroupBox.Controls.Add(this.characterRoleLabel);
            this.partyClassificationGroupBox.Controls.Add(this.characterRoleComboBox);
            this.partyClassificationGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.partyClassificationGroupBox.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.partyClassificationGroupBox.Location = new System.Drawing.Point(0, 0);
            this.partyClassificationGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.partyClassificationGroupBox.Name = "partyClassificationGroupBox";
            this.partyClassificationGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.partyClassificationGroupBox.Size = new System.Drawing.Size(297, 65);
            this.partyClassificationGroupBox.TabIndex = 0;
            this.partyClassificationGroupBox.TabStop = false;
            this.partyClassificationGroupBox.Text = "Party Classification";
            // 
            // characterRoleLabel
            // 
            this.characterRoleLabel.AutoSize = true;
            this.characterRoleLabel.ForeColor = System.Drawing.Color.White;
            this.characterRoleLabel.Location = new System.Drawing.Point(6, 23);
            this.characterRoleLabel.Name = "characterRoleLabel";
            this.characterRoleLabel.Size = new System.Drawing.Size(89, 15);
            this.characterRoleLabel.TabIndex = 8;
            this.characterRoleLabel.Text = "Character Role";
            // 
            // characterRoleComboBox
            // 
            this.characterRoleComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "None",
            "Party Leader",
            "Party Member"});
            this.characterRoleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.characterRoleComboBox.FormattingEnabled = true;
            this.characterRoleComboBox.Items.AddRange(new object[] {
            "None",
            "Party Leader",
            "Party Member"});
            this.characterRoleComboBox.Location = new System.Drawing.Point(121, 21);
            this.characterRoleComboBox.Name = "characterRoleComboBox";
            this.characterRoleComboBox.Size = new System.Drawing.Size(162, 23);
            this.characterRoleComboBox.TabIndex = 0;
            this.characterRoleComboBox.SelectedValueChanged += new System.EventHandler(this.characterRoleComboBox_SelectedValueChanged);
            // 
            // partyLeaderGroupBox
            // 
            this.partyLeaderGroupBox.AutoSize = true;
            this.partyLeaderGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.partyLeaderGroupBox.Controls.Add(this.partyMember4TextBox);
            this.partyLeaderGroupBox.Controls.Add(this.partyMember3TextBox);
            this.partyLeaderGroupBox.Controls.Add(this.partyMember2TextBox);
            this.partyLeaderGroupBox.Controls.Add(this.partyMember1TextBox);
            this.partyLeaderGroupBox.Controls.Add(this.partyMember4Label);
            this.partyLeaderGroupBox.Controls.Add(this.partyMember3Label);
            this.partyLeaderGroupBox.Controls.Add(this.partyMember2Label);
            this.partyLeaderGroupBox.Controls.Add(this.partyMember1Label);
            this.partyLeaderGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.partyLeaderGroupBox.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.partyLeaderGroupBox.Location = new System.Drawing.Point(0, 127);
            this.partyLeaderGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.partyLeaderGroupBox.Name = "partyLeaderGroupBox";
            this.partyLeaderGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.partyLeaderGroupBox.Size = new System.Drawing.Size(297, 133);
            this.partyLeaderGroupBox.TabIndex = 1;
            this.partyLeaderGroupBox.TabStop = false;
            this.partyLeaderGroupBox.Text = "Party Leader";
            // 
            // partyMember4TextBox
            // 
            this.partyMember4TextBox.Location = new System.Drawing.Point(121, 91);
            this.partyMember4TextBox.Name = "partyMember4TextBox";
            this.partyMember4TextBox.Size = new System.Drawing.Size(162, 21);
            this.partyMember4TextBox.TabIndex = 7;
            this.partyMember4TextBox.TextChanged += new System.EventHandler(this.partyMember4TextBox_TextChanged);
            // 
            // partyMember3TextBox
            // 
            this.partyMember3TextBox.Location = new System.Drawing.Point(121, 67);
            this.partyMember3TextBox.Name = "partyMember3TextBox";
            this.partyMember3TextBox.Size = new System.Drawing.Size(162, 21);
            this.partyMember3TextBox.TabIndex = 6;
            this.partyMember3TextBox.TextChanged += new System.EventHandler(this.partyMember3TextBox_TextChanged);
            // 
            // partyMember2TextBox
            // 
            this.partyMember2TextBox.Location = new System.Drawing.Point(121, 44);
            this.partyMember2TextBox.Name = "partyMember2TextBox";
            this.partyMember2TextBox.Size = new System.Drawing.Size(162, 21);
            this.partyMember2TextBox.TabIndex = 5;
            this.partyMember2TextBox.TextChanged += new System.EventHandler(this.partyMember2TextBox_TextChanged);
            // 
            // partyMember1TextBox
            // 
            this.partyMember1TextBox.Location = new System.Drawing.Point(121, 20);
            this.partyMember1TextBox.Name = "partyMember1TextBox";
            this.partyMember1TextBox.Size = new System.Drawing.Size(162, 21);
            this.partyMember1TextBox.TabIndex = 4;
            this.partyMember1TextBox.TextChanged += new System.EventHandler(this.partyMember1TextBox_TextChanged);
            // 
            // partyMember4Label
            // 
            this.partyMember4Label.AutoSize = true;
            this.partyMember4Label.ForeColor = System.Drawing.Color.White;
            this.partyMember4Label.Location = new System.Drawing.Point(6, 94);
            this.partyMember4Label.Name = "partyMember4Label";
            this.partyMember4Label.Size = new System.Drawing.Size(94, 15);
            this.partyMember4Label.TabIndex = 3;
            this.partyMember4Label.Text = "Party Member 4";
            // 
            // partyMember3Label
            // 
            this.partyMember3Label.AutoSize = true;
            this.partyMember3Label.ForeColor = System.Drawing.Color.White;
            this.partyMember3Label.Location = new System.Drawing.Point(6, 70);
            this.partyMember3Label.Name = "partyMember3Label";
            this.partyMember3Label.Size = new System.Drawing.Size(94, 15);
            this.partyMember3Label.TabIndex = 2;
            this.partyMember3Label.Text = "Party Member 3";
            // 
            // partyMember2Label
            // 
            this.partyMember2Label.AutoSize = true;
            this.partyMember2Label.ForeColor = System.Drawing.Color.White;
            this.partyMember2Label.Location = new System.Drawing.Point(6, 47);
            this.partyMember2Label.Name = "partyMember2Label";
            this.partyMember2Label.Size = new System.Drawing.Size(94, 15);
            this.partyMember2Label.TabIndex = 1;
            this.partyMember2Label.Text = "Party Member 2";
            // 
            // partyMember1Label
            // 
            this.partyMember1Label.AutoSize = true;
            this.partyMember1Label.ForeColor = System.Drawing.Color.White;
            this.partyMember1Label.Location = new System.Drawing.Point(6, 23);
            this.partyMember1Label.Name = "partyMember1Label";
            this.partyMember1Label.Size = new System.Drawing.Size(94, 15);
            this.partyMember1Label.TabIndex = 0;
            this.partyMember1Label.Text = "Party Member 1";
            // 
            // partyMemberGroupBox
            // 
            this.partyMemberGroupBox.AutoSize = true;
            this.partyMemberGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.partyMemberGroupBox.Controls.Add(this.partyLeaderTextBox);
            this.partyMemberGroupBox.Controls.Add(this.partyLeaderLabel);
            this.partyMemberGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.partyMemberGroupBox.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.partyMemberGroupBox.Location = new System.Drawing.Point(0, 65);
            this.partyMemberGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.partyMemberGroupBox.Name = "partyMemberGroupBox";
            this.partyMemberGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.partyMemberGroupBox.Size = new System.Drawing.Size(297, 62);
            this.partyMemberGroupBox.TabIndex = 2;
            this.partyMemberGroupBox.TabStop = false;
            this.partyMemberGroupBox.Text = "Party Member";
            // 
            // partyLeaderTextBox
            // 
            this.partyLeaderTextBox.Location = new System.Drawing.Point(121, 20);
            this.partyLeaderTextBox.Name = "partyLeaderTextBox";
            this.partyLeaderTextBox.Size = new System.Drawing.Size(162, 21);
            this.partyLeaderTextBox.TabIndex = 8;
            this.partyLeaderTextBox.TextChanged += new System.EventHandler(this.partyLeaderTextBox_TextChanged);
            // 
            // partyLeaderLabel
            // 
            this.partyLeaderLabel.AutoSize = true;
            this.partyLeaderLabel.ForeColor = System.Drawing.Color.White;
            this.partyLeaderLabel.Location = new System.Drawing.Point(6, 23);
            this.partyLeaderLabel.Name = "partyLeaderLabel";
            this.partyLeaderLabel.Size = new System.Drawing.Size(76, 15);
            this.partyLeaderLabel.TabIndex = 8;
            this.partyLeaderLabel.Text = "Party Leader";
            // 
            // PartyLeaderPrivilegesGroupBox
            // 
            this.PartyLeaderPrivilegesGroupBox.AutoSize = true;
            this.PartyLeaderPrivilegesGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PartyLeaderPrivilegesGroupBox.Controls.Add(this.partyLeaderDungeonDifficultyLabel);
            this.PartyLeaderPrivilegesGroupBox.Controls.Add(this.partyLeaderDungeonDifficultyComboBox);
            this.PartyLeaderPrivilegesGroupBox.Controls.Add(this.partyLeaderSetRoleLabel);
            this.PartyLeaderPrivilegesGroupBox.Controls.Add(this.partyLeaderLootThresholdLabel);
            this.PartyLeaderPrivilegesGroupBox.Controls.Add(this.partyLeaderPassOnLootLabel);
            this.PartyLeaderPrivilegesGroupBox.Controls.Add(this.partyLeaderLootLabel);
            this.PartyLeaderPrivilegesGroupBox.Controls.Add(this.partyLeaderSetRoleComboBox);
            this.PartyLeaderPrivilegesGroupBox.Controls.Add(this.partyLeaderLootThresholdComboBox);
            this.PartyLeaderPrivilegesGroupBox.Controls.Add(this.partyLeaderPassOnLootComboBox);
            this.PartyLeaderPrivilegesGroupBox.Controls.Add(this.partyLeaderLootComboBox);
            this.PartyLeaderPrivilegesGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.PartyLeaderPrivilegesGroupBox.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.PartyLeaderPrivilegesGroupBox.Location = new System.Drawing.Point(0, 260);
            this.PartyLeaderPrivilegesGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.PartyLeaderPrivilegesGroupBox.Name = "PartyLeaderPrivilegesGroupBox";
            this.PartyLeaderPrivilegesGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.PartyLeaderPrivilegesGroupBox.Size = new System.Drawing.Size(297, 177);
            this.PartyLeaderPrivilegesGroupBox.TabIndex = 9;
            this.PartyLeaderPrivilegesGroupBox.TabStop = false;
            this.PartyLeaderPrivilegesGroupBox.Text = "Party Leader Privileges";
            // 
            // partyLeaderSetRoleLabel
            // 
            this.partyLeaderSetRoleLabel.AutoSize = true;
            this.partyLeaderSetRoleLabel.ForeColor = System.Drawing.Color.White;
            this.partyLeaderSetRoleLabel.Location = new System.Drawing.Point(6, 136);
            this.partyLeaderSetRoleLabel.Name = "partyLeaderSetRoleLabel";
            this.partyLeaderSetRoleLabel.Size = new System.Drawing.Size(54, 15);
            this.partyLeaderSetRoleLabel.TabIndex = 12;
            this.partyLeaderSetRoleLabel.Text = "Set Role";
            // 
            // partyLeaderLootThresholdLabel
            // 
            this.partyLeaderLootThresholdLabel.AutoSize = true;
            this.partyLeaderLootThresholdLabel.ForeColor = System.Drawing.Color.White;
            this.partyLeaderLootThresholdLabel.Location = new System.Drawing.Point(8, 52);
            this.partyLeaderLootThresholdLabel.Name = "partyLeaderLootThresholdLabel";
            this.partyLeaderLootThresholdLabel.Size = new System.Drawing.Size(89, 15);
            this.partyLeaderLootThresholdLabel.TabIndex = 11;
            this.partyLeaderLootThresholdLabel.Text = "Loot Threshold";
            // 
            // partyLeaderPassOnLootLabel
            // 
            this.partyLeaderPassOnLootLabel.AutoSize = true;
            this.partyLeaderPassOnLootLabel.ForeColor = System.Drawing.Color.White;
            this.partyLeaderPassOnLootLabel.Location = new System.Drawing.Point(6, 108);
            this.partyLeaderPassOnLootLabel.Name = "partyLeaderPassOnLootLabel";
            this.partyLeaderPassOnLootLabel.Size = new System.Drawing.Size(78, 15);
            this.partyLeaderPassOnLootLabel.TabIndex = 10;
            this.partyLeaderPassOnLootLabel.Text = "Pass on Loot";
            // 
            // partyLeaderLootLabel
            // 
            this.partyLeaderLootLabel.AutoSize = true;
            this.partyLeaderLootLabel.ForeColor = System.Drawing.Color.White;
            this.partyLeaderLootLabel.Location = new System.Drawing.Point(8, 23);
            this.partyLeaderLootLabel.Name = "partyLeaderLootLabel";
            this.partyLeaderLootLabel.Size = new System.Drawing.Size(31, 15);
            this.partyLeaderLootLabel.TabIndex = 9;
            this.partyLeaderLootLabel.Text = "Loot";
            // 
            // partyLeaderSetRoleComboBox
            // 
            this.partyLeaderSetRoleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partyLeaderSetRoleComboBox.FormattingEnabled = true;
            this.partyLeaderSetRoleComboBox.Items.AddRange(new object[] {
            "No Role",
            "Tank",
            "Healer",
            "Damage"});
            this.partyLeaderSetRoleComboBox.Location = new System.Drawing.Point(121, 133);
            this.partyLeaderSetRoleComboBox.Name = "partyLeaderSetRoleComboBox";
            this.partyLeaderSetRoleComboBox.Size = new System.Drawing.Size(162, 23);
            this.partyLeaderSetRoleComboBox.TabIndex = 5;
            this.partyLeaderSetRoleComboBox.SelectedValueChanged += new System.EventHandler(this.partyLeaderSetRoleComboBox_SelectedValueChanged);
            // 
            // partyLeaderLootThresholdComboBox
            // 
            this.partyLeaderLootThresholdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partyLeaderLootThresholdComboBox.FormattingEnabled = true;
            this.partyLeaderLootThresholdComboBox.Items.AddRange(new object[] {
            "Uncommon",
            "Rare",
            "Epic"});
            this.partyLeaderLootThresholdComboBox.Location = new System.Drawing.Point(121, 49);
            this.partyLeaderLootThresholdComboBox.Name = "partyLeaderLootThresholdComboBox";
            this.partyLeaderLootThresholdComboBox.Size = new System.Drawing.Size(162, 23);
            this.partyLeaderLootThresholdComboBox.TabIndex = 4;
            this.partyLeaderLootThresholdComboBox.SelectedValueChanged += new System.EventHandler(this.partyLeaderLootThresholdComboBox_SelectedValueChanged);
            // 
            // partyLeaderPassOnLootComboBox
            // 
            this.partyLeaderPassOnLootComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partyLeaderPassOnLootComboBox.FormattingEnabled = true;
            this.partyLeaderPassOnLootComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.partyLeaderPassOnLootComboBox.Location = new System.Drawing.Point(121, 105);
            this.partyLeaderPassOnLootComboBox.Name = "partyLeaderPassOnLootComboBox";
            this.partyLeaderPassOnLootComboBox.Size = new System.Drawing.Size(162, 23);
            this.partyLeaderPassOnLootComboBox.TabIndex = 3;
            this.partyLeaderPassOnLootComboBox.SelectedValueChanged += new System.EventHandler(this.partyLeaderPassOnLootComboBox_SelectedValueChanged);
            // 
            // partyLeaderLootComboBox
            // 
            this.partyLeaderLootComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partyLeaderLootComboBox.FormattingEnabled = true;
            this.partyLeaderLootComboBox.Items.AddRange(new object[] {
            "Free For All",
            "Round Robin",
            "Master Looter",
            "Group Loot",
            "Need Before Greed"});
            this.partyLeaderLootComboBox.Location = new System.Drawing.Point(121, 20);
            this.partyLeaderLootComboBox.Name = "partyLeaderLootComboBox";
            this.partyLeaderLootComboBox.Size = new System.Drawing.Size(162, 23);
            this.partyLeaderLootComboBox.TabIndex = 1;
            this.partyLeaderLootComboBox.SelectedValueChanged += new System.EventHandler(this.partyLeaderLootComboBox_SelectedValueChanged);
            // 
            // partyMemberPrivilegesGroupBox
            // 
            this.partyMemberPrivilegesGroupBox.AutoSize = true;
            this.partyMemberPrivilegesGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.partyMemberPrivilegesGroupBox.Controls.Add(this.partyMemberSetRoleLabel);
            this.partyMemberPrivilegesGroupBox.Controls.Add(this.partyMemberPassOnLootLabel);
            this.partyMemberPrivilegesGroupBox.Controls.Add(this.partyMemberSetRoleComboBox);
            this.partyMemberPrivilegesGroupBox.Controls.Add(this.partyMemberPassOnLootComboBox);
            this.partyMemberPrivilegesGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.partyMemberPrivilegesGroupBox.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.partyMemberPrivilegesGroupBox.Location = new System.Drawing.Point(0, 437);
            this.partyMemberPrivilegesGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.partyMemberPrivilegesGroupBox.Name = "partyMemberPrivilegesGroupBox";
            this.partyMemberPrivilegesGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.partyMemberPrivilegesGroupBox.Size = new System.Drawing.Size(297, 93);
            this.partyMemberPrivilegesGroupBox.TabIndex = 13;
            this.partyMemberPrivilegesGroupBox.TabStop = false;
            this.partyMemberPrivilegesGroupBox.Text = "Party Member Privileges";
            // 
            // partyMemberSetRoleLabel
            // 
            this.partyMemberSetRoleLabel.AutoSize = true;
            this.partyMemberSetRoleLabel.ForeColor = System.Drawing.Color.White;
            this.partyMemberSetRoleLabel.Location = new System.Drawing.Point(6, 52);
            this.partyMemberSetRoleLabel.Name = "partyMemberSetRoleLabel";
            this.partyMemberSetRoleLabel.Size = new System.Drawing.Size(54, 15);
            this.partyMemberSetRoleLabel.TabIndex = 12;
            this.partyMemberSetRoleLabel.Text = "Set Role";
            // 
            // partyMemberPassOnLootLabel
            // 
            this.partyMemberPassOnLootLabel.AutoSize = true;
            this.partyMemberPassOnLootLabel.ForeColor = System.Drawing.Color.White;
            this.partyMemberPassOnLootLabel.Location = new System.Drawing.Point(6, 23);
            this.partyMemberPassOnLootLabel.Name = "partyMemberPassOnLootLabel";
            this.partyMemberPassOnLootLabel.Size = new System.Drawing.Size(78, 15);
            this.partyMemberPassOnLootLabel.TabIndex = 10;
            this.partyMemberPassOnLootLabel.Text = "Pass on Loot";
            // 
            // partyMemberSetRoleComboBox
            // 
            this.partyMemberSetRoleComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "None",
            "Tank",
            "Healer",
            "Damage"});
            this.partyMemberSetRoleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partyMemberSetRoleComboBox.FormattingEnabled = true;
            this.partyMemberSetRoleComboBox.Items.AddRange(new object[] {
            "No Role",
            "Tank",
            "Healer",
            "Damage"});
            this.partyMemberSetRoleComboBox.Location = new System.Drawing.Point(121, 49);
            this.partyMemberSetRoleComboBox.Name = "partyMemberSetRoleComboBox";
            this.partyMemberSetRoleComboBox.Size = new System.Drawing.Size(162, 23);
            this.partyMemberSetRoleComboBox.TabIndex = 5;
            this.partyMemberSetRoleComboBox.SelectedValueChanged += new System.EventHandler(this.partyMemberSetRoleComboBox_SelectedValueChanged);
            // 
            // partyMemberPassOnLootComboBox
            // 
            this.partyMemberPassOnLootComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "No",
            "Yes"});
            this.partyMemberPassOnLootComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partyMemberPassOnLootComboBox.FormattingEnabled = true;
            this.partyMemberPassOnLootComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.partyMemberPassOnLootComboBox.Location = new System.Drawing.Point(121, 17);
            this.partyMemberPassOnLootComboBox.Name = "partyMemberPassOnLootComboBox";
            this.partyMemberPassOnLootComboBox.Size = new System.Drawing.Size(162, 23);
            this.partyMemberPassOnLootComboBox.TabIndex = 3;
            this.partyMemberPassOnLootComboBox.SelectedValueChanged += new System.EventHandler(this.partyMemberPassOnLootComboBox_SelectedValueChanged);
            // 
            // partyLeaderDungeonDifficultyLabel
            // 
            this.partyLeaderDungeonDifficultyLabel.AutoSize = true;
            this.partyLeaderDungeonDifficultyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partyLeaderDungeonDifficultyLabel.ForeColor = System.Drawing.Color.White;
            this.partyLeaderDungeonDifficultyLabel.Location = new System.Drawing.Point(6, 80);
            this.partyLeaderDungeonDifficultyLabel.Name = "partyLeaderDungeonDifficultyLabel";
            this.partyLeaderDungeonDifficultyLabel.Size = new System.Drawing.Size(106, 15);
            this.partyLeaderDungeonDifficultyLabel.TabIndex = 14;
            this.partyLeaderDungeonDifficultyLabel.Text = "Dungeon Difficulty";
            // 
            // partyLeaderDungeonDifficultyComboBox
            // 
            this.partyLeaderDungeonDifficultyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partyLeaderDungeonDifficultyComboBox.FormattingEnabled = true;
            this.partyLeaderDungeonDifficultyComboBox.Items.AddRange(new object[] {
            "5 Player Normal",
            "5 Player Heroic",
            "Challenge Mode"});
            this.partyLeaderDungeonDifficultyComboBox.Location = new System.Drawing.Point(121, 77);
            this.partyLeaderDungeonDifficultyComboBox.Name = "partyLeaderDungeonDifficultyComboBox";
            this.partyLeaderDungeonDifficultyComboBox.Size = new System.Drawing.Size(162, 23);
            this.partyLeaderDungeonDifficultyComboBox.TabIndex = 13;
            this.partyLeaderDungeonDifficultyComboBox.SelectedValueChanged += new System.EventHandler(this.partyLeaderDungeonDifficultyComboBox_SelectedValueChanged);
            // 
            // ProphetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(297, 665);
            this.Controls.Add(this.partyMemberPrivilegesGroupBox);
            this.Controls.Add(this.PartyLeaderPrivilegesGroupBox);
            this.Controls.Add(this.partyLeaderGroupBox);
            this.Controls.Add(this.partyMemberGroupBox);
            this.Controls.Add(this.partyClassificationGroupBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 0);
            this.Name = "ProphetGUI";
            this.ShowIcon = false;
            this.Text = "Prophet";
            this.Load += new System.EventHandler(this.ProphetGUI_Load);
            this.partyClassificationGroupBox.ResumeLayout(false);
            this.partyClassificationGroupBox.PerformLayout();
            this.partyLeaderGroupBox.ResumeLayout(false);
            this.partyLeaderGroupBox.PerformLayout();
            this.partyMemberGroupBox.ResumeLayout(false);
            this.partyMemberGroupBox.PerformLayout();
            this.PartyLeaderPrivilegesGroupBox.ResumeLayout(false);
            this.PartyLeaderPrivilegesGroupBox.PerformLayout();
            this.partyMemberPrivilegesGroupBox.ResumeLayout(false);
            this.partyMemberPrivilegesGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox partyClassificationGroupBox;
        private System.Windows.Forms.ComboBox characterRoleComboBox;
        private System.Windows.Forms.GroupBox partyLeaderGroupBox;
        private System.Windows.Forms.TextBox partyMember4TextBox;
        private System.Windows.Forms.TextBox partyMember3TextBox;
        private System.Windows.Forms.TextBox partyMember2TextBox;
        private System.Windows.Forms.TextBox partyMember1TextBox;
        private System.Windows.Forms.Label partyMember4Label;
        private System.Windows.Forms.Label partyMember3Label;
        private System.Windows.Forms.Label partyMember2Label;
        private System.Windows.Forms.Label partyMember1Label;
        private System.Windows.Forms.GroupBox partyMemberGroupBox;
        private System.Windows.Forms.TextBox partyLeaderTextBox;
        private System.Windows.Forms.Label partyLeaderLabel;
        private System.Windows.Forms.GroupBox PartyLeaderPrivilegesGroupBox;
        private System.Windows.Forms.Label partyLeaderSetRoleLabel;
        private System.Windows.Forms.Label partyLeaderLootThresholdLabel;
        private System.Windows.Forms.Label partyLeaderPassOnLootLabel;
        private System.Windows.Forms.Label partyLeaderLootLabel;
        private System.Windows.Forms.ComboBox partyLeaderSetRoleComboBox;
        private System.Windows.Forms.ComboBox partyLeaderLootThresholdComboBox;
        private System.Windows.Forms.ComboBox partyLeaderPassOnLootComboBox;
        private System.Windows.Forms.ComboBox partyLeaderLootComboBox;
        private System.Windows.Forms.Label characterRoleLabel;
        private System.Windows.Forms.GroupBox partyMemberPrivilegesGroupBox;
        private System.Windows.Forms.Label partyMemberSetRoleLabel;
        private System.Windows.Forms.Label partyMemberPassOnLootLabel;
        private System.Windows.Forms.ComboBox partyMemberSetRoleComboBox;
        private System.Windows.Forms.ComboBox partyMemberPassOnLootComboBox;
        private System.Windows.Forms.Label partyLeaderDungeonDifficultyLabel;
        private System.Windows.Forms.ComboBox partyLeaderDungeonDifficultyComboBox;

    }
}