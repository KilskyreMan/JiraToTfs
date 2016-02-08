namespace JiraToTfs.View
{
    partial class AdvancedSettingsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedSettingsView));
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.typeTab = new System.Windows.Forms.TabPage();
            this.pleaseCheckJira = new System.Windows.Forms.Label();
            this.noJiraTicketTypes = new System.Windows.Forms.Label();
            this.restoreDefaultTypesBtn = new System.Windows.Forms.Button();
            this.jiraGrid = new System.Windows.Forms.DataGridView();
            this.jiraColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maptoColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.fieldTab = new System.Windows.Forms.TabPage();
            this.checkFieldSettings = new System.Windows.Forms.Label();
            this.noFieldsFound = new System.Windows.Forms.Label();
            this.tfsFieldGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tfsFieldValueColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tfsUserTab = new System.Windows.Forms.TabPage();
            this.defaultUserLabel = new System.Windows.Forms.Label();
            this.defaultCreatorList = new System.Windows.Forms.ComboBox();
            this.defaultCreatorLabel = new System.Windows.Forms.Label();
            this.defaultAssigneeList = new System.Windows.Forms.ComboBox();
            this.detailedNoteOnDefaultUser = new System.Windows.Forms.Label();
            this.defaultAsigneeLabel = new System.Windows.Forms.Label();
            this.checkUserSettings = new System.Windows.Forms.Label();
            this.noUsersFound = new System.Windows.Forms.Label();
            this.workItemTab = new System.Windows.Forms.TabPage();
            this.seeMicrosoftLink = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.restoreStateDefaults = new System.Windows.Forms.Button();
            this.workItemGrid = new System.Windows.Forms.DataGridView();
            this.workItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.initialState = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tfsBugTab = new System.Windows.Forms.TabPage();
            this.priorityPanel = new System.Windows.Forms.Panel();
            this.restorePriorityDefaults = new System.Windows.Forms.Button();
            this.priorityGrid = new System.Windows.Forms.DataGridView();
            this.priorityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toPriorityListColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.toPriorityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customPriorityField = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.customPriorityNote = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.typeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jiraGrid)).BeginInit();
            this.fieldTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tfsFieldGrid)).BeginInit();
            this.tfsUserTab.SuspendLayout();
            this.workItemTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workItemGrid)).BeginInit();
            this.tfsBugTab.SuspendLayout();
            this.priorityPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priorityGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(469, 612);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.Text = "&Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(388, 612);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "&Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.typeTab);
            this.tabControl.Controls.Add(this.fieldTab);
            this.tabControl.Controls.Add(this.tfsUserTab);
            this.tabControl.Controls.Add(this.workItemTab);
            this.tabControl.Controls.Add(this.tfsBugTab);
            this.tabControl.Location = new System.Drawing.Point(15, 13);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(529, 593);
            this.tabControl.TabIndex = 2;
            // 
            // typeTab
            // 
            this.typeTab.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.typeTab.Controls.Add(this.pleaseCheckJira);
            this.typeTab.Controls.Add(this.noJiraTicketTypes);
            this.typeTab.Controls.Add(this.restoreDefaultTypesBtn);
            this.typeTab.Controls.Add(this.jiraGrid);
            this.typeTab.Location = new System.Drawing.Point(4, 22);
            this.typeTab.Name = "typeTab";
            this.typeTab.Padding = new System.Windows.Forms.Padding(3);
            this.typeTab.Size = new System.Drawing.Size(521, 567);
            this.typeTab.TabIndex = 0;
            this.typeTab.Text = "Ticket Type";
            // 
            // pleaseCheckJira
            // 
            this.pleaseCheckJira.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pleaseCheckJira.ForeColor = System.Drawing.Color.Red;
            this.pleaseCheckJira.Location = new System.Drawing.Point(119, 185);
            this.pleaseCheckJira.Name = "pleaseCheckJira";
            this.pleaseCheckJira.Size = new System.Drawing.Size(283, 40);
            this.pleaseCheckJira.TabIndex = 21;
            this.pleaseCheckJira.Text = "Close this dialog and ensure you have specified the right Jira Server, user name " +
    "and password.";
            this.pleaseCheckJira.Visible = false;
            // 
            // noJiraTicketTypes
            // 
            this.noJiraTicketTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noJiraTicketTypes.ForeColor = System.Drawing.Color.Red;
            this.noJiraTicketTypes.Location = new System.Drawing.Point(119, 162);
            this.noJiraTicketTypes.Name = "noJiraTicketTypes";
            this.noJiraTicketTypes.Size = new System.Drawing.Size(283, 19);
            this.noJiraTicketTypes.TabIndex = 20;
            this.noJiraTicketTypes.Text = "Unable to determine available Jira types.";
            this.noJiraTicketTypes.Visible = false;
            // 
            // restoreDefaultTypesBtn
            // 
            this.restoreDefaultTypesBtn.Location = new System.Drawing.Point(3, 535);
            this.restoreDefaultTypesBtn.Name = "restoreDefaultTypesBtn";
            this.restoreDefaultTypesBtn.Size = new System.Drawing.Size(97, 23);
            this.restoreDefaultTypesBtn.TabIndex = 19;
            this.restoreDefaultTypesBtn.Text = "&Restore defaults";
            this.restoreDefaultTypesBtn.UseVisualStyleBackColor = true;
            this.restoreDefaultTypesBtn.Click += new System.EventHandler(this.OnClickRestoreTicketTypeDefaults);
            // 
            // jiraGrid
            // 
            this.jiraGrid.AllowUserToAddRows = false;
            this.jiraGrid.AllowUserToDeleteRows = false;
            this.jiraGrid.AllowUserToResizeRows = false;
            this.jiraGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jiraGrid.BackgroundColor = System.Drawing.Color.White;
            this.jiraGrid.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.jiraGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.jiraGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.jiraGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.jiraColumn,
            this.maptoColumn});
            this.jiraGrid.Location = new System.Drawing.Point(0, 17);
            this.jiraGrid.Name = "jiraGrid";
            this.jiraGrid.RowHeadersVisible = false;
            this.jiraGrid.ShowCellErrors = false;
            this.jiraGrid.ShowEditingIcon = false;
            this.jiraGrid.ShowRowErrors = false;
            this.jiraGrid.Size = new System.Drawing.Size(518, 508);
            this.jiraGrid.TabIndex = 18;
            // 
            // jiraColumn
            // 
            this.jiraColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.jiraColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.jiraColumn.FillWeight = 50F;
            this.jiraColumn.HeaderText = "Jira Ticket Type";
            this.jiraColumn.Name = "jiraColumn";
            this.jiraColumn.ReadOnly = true;
            this.jiraColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // maptoColumn
            // 
            this.maptoColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.maptoColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.maptoColumn.HeaderText = "Maps to";
            this.maptoColumn.Name = "maptoColumn";
            this.maptoColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // fieldTab
            // 
            this.fieldTab.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.fieldTab.Controls.Add(this.checkFieldSettings);
            this.fieldTab.Controls.Add(this.noFieldsFound);
            this.fieldTab.Controls.Add(this.tfsFieldGrid);
            this.fieldTab.Location = new System.Drawing.Point(4, 22);
            this.fieldTab.Name = "fieldTab";
            this.fieldTab.Padding = new System.Windows.Forms.Padding(3);
            this.fieldTab.Size = new System.Drawing.Size(521, 567);
            this.fieldTab.TabIndex = 1;
            this.fieldTab.Text = "Default TFS Field Values";
            // 
            // checkFieldSettings
            // 
            this.checkFieldSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkFieldSettings.ForeColor = System.Drawing.Color.Red;
            this.checkFieldSettings.Location = new System.Drawing.Point(119, 185);
            this.checkFieldSettings.Name = "checkFieldSettings";
            this.checkFieldSettings.Size = new System.Drawing.Size(283, 40);
            this.checkFieldSettings.TabIndex = 23;
            this.checkFieldSettings.Text = "Close this dialog and ensure you have connected to the right TFS server.";
            this.checkFieldSettings.Visible = false;
            // 
            // noFieldsFound
            // 
            this.noFieldsFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noFieldsFound.ForeColor = System.Drawing.Color.Red;
            this.noFieldsFound.Location = new System.Drawing.Point(119, 162);
            this.noFieldsFound.Name = "noFieldsFound";
            this.noFieldsFound.Size = new System.Drawing.Size(283, 19);
            this.noFieldsFound.TabIndex = 22;
            this.noFieldsFound.Text = "Unable to connect to TFS.";
            this.noFieldsFound.Visible = false;
            // 
            // tfsFieldGrid
            // 
            this.tfsFieldGrid.AllowUserToAddRows = false;
            this.tfsFieldGrid.AllowUserToDeleteRows = false;
            this.tfsFieldGrid.AllowUserToResizeRows = false;
            this.tfsFieldGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tfsFieldGrid.BackgroundColor = System.Drawing.Color.White;
            this.tfsFieldGrid.CausesValidation = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tfsFieldGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tfsFieldGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tfsFieldGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.tfsFieldValueColumn});
            this.tfsFieldGrid.Location = new System.Drawing.Point(0, 17);
            this.tfsFieldGrid.Name = "tfsFieldGrid";
            this.tfsFieldGrid.RowHeadersVisible = false;
            this.tfsFieldGrid.ShowCellErrors = false;
            this.tfsFieldGrid.ShowEditingIcon = false;
            this.tfsFieldGrid.ShowRowErrors = false;
            this.tfsFieldGrid.Size = new System.Drawing.Size(518, 550);
            this.tfsFieldGrid.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "TFS Field";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.Width = 77;
            // 
            // tfsFieldValueColumn
            // 
            this.tfsFieldValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tfsFieldValueColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.tfsFieldValueColumn.HeaderText = "Defaults to";
            this.tfsFieldValueColumn.Name = "tfsFieldValueColumn";
            this.tfsFieldValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tfsUserTab
            // 
            this.tfsUserTab.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tfsUserTab.Controls.Add(this.defaultUserLabel);
            this.tfsUserTab.Controls.Add(this.defaultCreatorList);
            this.tfsUserTab.Controls.Add(this.defaultCreatorLabel);
            this.tfsUserTab.Controls.Add(this.defaultAssigneeList);
            this.tfsUserTab.Controls.Add(this.detailedNoteOnDefaultUser);
            this.tfsUserTab.Controls.Add(this.defaultAsigneeLabel);
            this.tfsUserTab.Controls.Add(this.checkUserSettings);
            this.tfsUserTab.Controls.Add(this.noUsersFound);
            this.tfsUserTab.Location = new System.Drawing.Point(4, 22);
            this.tfsUserTab.Name = "tfsUserTab";
            this.tfsUserTab.Padding = new System.Windows.Forms.Padding(3);
            this.tfsUserTab.Size = new System.Drawing.Size(521, 567);
            this.tfsUserTab.TabIndex = 2;
            this.tfsUserTab.Text = "Default TFS User";
            // 
            // defaultUserLabel
            // 
            this.defaultUserLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultUserLabel.ForeColor = System.Drawing.Color.Black;
            this.defaultUserLabel.Location = new System.Drawing.Point(79, 153);
            this.defaultUserLabel.Name = "defaultUserLabel";
            this.defaultUserLabel.Size = new System.Drawing.Size(283, 16);
            this.defaultUserLabel.TabIndex = 32;
            this.defaultUserLabel.Text = "Default TFS User";
            // 
            // defaultCreatorList
            // 
            this.defaultCreatorList.FormattingEnabled = true;
            this.defaultCreatorList.Location = new System.Drawing.Point(79, 117);
            this.defaultCreatorList.Name = "defaultCreatorList";
            this.defaultCreatorList.Size = new System.Drawing.Size(268, 21);
            this.defaultCreatorList.TabIndex = 31;
            // 
            // defaultCreatorLabel
            // 
            this.defaultCreatorLabel.AutoSize = true;
            this.defaultCreatorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultCreatorLabel.Location = new System.Drawing.Point(79, 96);
            this.defaultCreatorLabel.Name = "defaultCreatorLabel";
            this.defaultCreatorLabel.Size = new System.Drawing.Size(97, 13);
            this.defaultCreatorLabel.TabIndex = 30;
            this.defaultCreatorLabel.Text = "Default Creator:";
            // 
            // defaultAssigneeList
            // 
            this.defaultAssigneeList.FormattingEnabled = true;
            this.defaultAssigneeList.Location = new System.Drawing.Point(79, 67);
            this.defaultAssigneeList.Name = "defaultAssigneeList";
            this.defaultAssigneeList.Size = new System.Drawing.Size(268, 21);
            this.defaultAssigneeList.TabIndex = 29;
            // 
            // detailedNoteOnDefaultUser
            // 
            this.detailedNoteOnDefaultUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailedNoteOnDefaultUser.ForeColor = System.Drawing.Color.Black;
            this.detailedNoteOnDefaultUser.Location = new System.Drawing.Point(79, 176);
            this.detailedNoteOnDefaultUser.Name = "detailedNoteOnDefaultUser";
            this.detailedNoteOnDefaultUser.Size = new System.Drawing.Size(283, 40);
            this.detailedNoteOnDefaultUser.TabIndex = 28;
            this.detailedNoteOnDefaultUser.Text = "The Default Assignee / Creator will be used when unable to find in TFS, the Jira " +
    "user who created or is assigned to a ticket being imported.";
            // 
            // defaultAsigneeLabel
            // 
            this.defaultAsigneeLabel.AutoSize = true;
            this.defaultAsigneeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultAsigneeLabel.Location = new System.Drawing.Point(79, 46);
            this.defaultAsigneeLabel.Name = "defaultAsigneeLabel";
            this.defaultAsigneeLabel.Size = new System.Drawing.Size(107, 13);
            this.defaultAsigneeLabel.TabIndex = 27;
            this.defaultAsigneeLabel.Text = "Default Assignee:";
            // 
            // checkUserSettings
            // 
            this.checkUserSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkUserSettings.ForeColor = System.Drawing.Color.Red;
            this.checkUserSettings.Location = new System.Drawing.Point(79, 176);
            this.checkUserSettings.Name = "checkUserSettings";
            this.checkUserSettings.Size = new System.Drawing.Size(283, 40);
            this.checkUserSettings.TabIndex = 26;
            this.checkUserSettings.Text = "Close this dialog and ensure you have connected to the right TFS server.";
            this.checkUserSettings.Visible = false;
            // 
            // noUsersFound
            // 
            this.noUsersFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noUsersFound.ForeColor = System.Drawing.Color.Red;
            this.noUsersFound.Location = new System.Drawing.Point(79, 153);
            this.noUsersFound.Name = "noUsersFound";
            this.noUsersFound.Size = new System.Drawing.Size(283, 19);
            this.noUsersFound.TabIndex = 25;
            this.noUsersFound.Text = "Unable to connect to TFS.";
            this.noUsersFound.Visible = false;
            // 
            // workItemTab
            // 
            this.workItemTab.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.workItemTab.Controls.Add(this.seeMicrosoftLink);
            this.workItemTab.Controls.Add(this.label2);
            this.workItemTab.Controls.Add(this.label1);
            this.workItemTab.Controls.Add(this.restoreStateDefaults);
            this.workItemTab.Controls.Add(this.workItemGrid);
            this.workItemTab.Location = new System.Drawing.Point(4, 22);
            this.workItemTab.Name = "workItemTab";
            this.workItemTab.Padding = new System.Windows.Forms.Padding(3);
            this.workItemTab.Size = new System.Drawing.Size(521, 567);
            this.workItemTab.TabIndex = 3;
            this.workItemTab.Text = "New TFS WorkItems";
            // 
            // seeMicrosoftLink
            // 
            this.seeMicrosoftLink.AutoSize = true;
            this.seeMicrosoftLink.Location = new System.Drawing.Point(2, 66);
            this.seeMicrosoftLink.Name = "seeMicrosoftLink";
            this.seeMicrosoftLink.Size = new System.Drawing.Size(126, 13);
            this.seeMicrosoftLink.TabIndex = 24;
            this.seeMicrosoftLink.TabStop = true;
            this.seeMicrosoftLink.Text = "See Microsoft Work-Flow";
            this.seeMicrosoftLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.seeMicrosoftLink_LinkClicked);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(432, 30);
            this.label2.TabIndex = 23;
            this.label2.Text = "This can be useful where the assumption is that everything logged in Jira has bee" +
    "n approved by a Product Owner and are regarded as no longer \'new\' in TFS.";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(432, 22);
            this.label1.TabIndex = 22;
            this.label1.Text = "Jira tickets marked as \'To-do\' can be set to their next \'state\' when imported int" +
    "o TFS.";
            // 
            // restoreStateDefaults
            // 
            this.restoreStateDefaults.Location = new System.Drawing.Point(3, 535);
            this.restoreStateDefaults.Name = "restoreStateDefaults";
            this.restoreStateDefaults.Size = new System.Drawing.Size(97, 23);
            this.restoreStateDefaults.TabIndex = 21;
            this.restoreStateDefaults.Text = "&Restore defaults";
            this.restoreStateDefaults.UseVisualStyleBackColor = true;
            this.restoreStateDefaults.Click += new System.EventHandler(this.restoreStateDefaults_Click);
            // 
            // workItemGrid
            // 
            this.workItemGrid.AllowUserToAddRows = false;
            this.workItemGrid.AllowUserToDeleteRows = false;
            this.workItemGrid.AllowUserToResizeRows = false;
            this.workItemGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workItemGrid.BackgroundColor = System.Drawing.Color.White;
            this.workItemGrid.CausesValidation = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.workItemGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.workItemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.workItemGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.workItem,
            this.initialState});
            this.workItemGrid.Location = new System.Drawing.Point(2, 94);
            this.workItemGrid.Name = "workItemGrid";
            this.workItemGrid.RowHeadersVisible = false;
            this.workItemGrid.ShowCellErrors = false;
            this.workItemGrid.ShowEditingIcon = false;
            this.workItemGrid.ShowRowErrors = false;
            this.workItemGrid.Size = new System.Drawing.Size(518, 418);
            this.workItemGrid.TabIndex = 20;
            this.workItemGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.OnDataError);
            // 
            // workItem
            // 
            this.workItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.workItem.DefaultCellStyle = dataGridViewCellStyle6;
            this.workItem.FillWeight = 50F;
            this.workItem.HeaderText = "Work-Item";
            this.workItem.Name = "workItem";
            this.workItem.ReadOnly = true;
            this.workItem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // initialState
            // 
            this.initialState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.initialState.HeaderText = "Initial \'State\' in TFS";
            this.initialState.Name = "initialState";
            this.initialState.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.initialState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tfsBugTab
            // 
            this.tfsBugTab.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tfsBugTab.Controls.Add(this.priorityPanel);
            this.tfsBugTab.Location = new System.Drawing.Point(4, 22);
            this.tfsBugTab.Name = "tfsBugTab";
            this.tfsBugTab.Padding = new System.Windows.Forms.Padding(3);
            this.tfsBugTab.Size = new System.Drawing.Size(521, 567);
            this.tfsBugTab.TabIndex = 4;
            this.tfsBugTab.Text = "TFS Bug Priority";
            // 
            // priorityPanel
            // 
            this.priorityPanel.BackColor = System.Drawing.SystemColors.Control;
            this.priorityPanel.Controls.Add(this.restorePriorityDefaults);
            this.priorityPanel.Controls.Add(this.priorityGrid);
            this.priorityPanel.Controls.Add(this.customPriorityField);
            this.priorityPanel.Controls.Add(this.label4);
            this.priorityPanel.Controls.Add(this.label3);
            this.priorityPanel.Controls.Add(this.customPriorityNote);
            this.priorityPanel.Location = new System.Drawing.Point(7, 9);
            this.priorityPanel.Name = "priorityPanel";
            this.priorityPanel.Size = new System.Drawing.Size(511, 558);
            this.priorityPanel.TabIndex = 0;
            // 
            // restorePriorityDefaults
            // 
            this.restorePriorityDefaults.Location = new System.Drawing.Point(313, 268);
            this.restorePriorityDefaults.Name = "restorePriorityDefaults";
            this.restorePriorityDefaults.Size = new System.Drawing.Size(97, 23);
            this.restorePriorityDefaults.TabIndex = 27;
            this.restorePriorityDefaults.Text = "&Restore defaults";
            this.restorePriorityDefaults.UseVisualStyleBackColor = true;
            this.restorePriorityDefaults.Click += new System.EventHandler(this.restorePriorityDefaults_Click);
            // 
            // priorityGrid
            // 
            this.priorityGrid.AllowUserToAddRows = false;
            this.priorityGrid.AllowUserToDeleteRows = false;
            this.priorityGrid.AllowUserToResizeRows = false;
            this.priorityGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.priorityGrid.BackgroundColor = System.Drawing.Color.White;
            this.priorityGrid.CausesValidation = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.priorityGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.priorityGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.priorityGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.priorityColumn,
            this.toPriorityListColumn,
            this.toPriorityColumn});
            this.priorityGrid.Location = new System.Drawing.Point(173, 123);
            this.priorityGrid.MultiSelect = false;
            this.priorityGrid.Name = "priorityGrid";
            this.priorityGrid.RowHeadersVisible = false;
            this.priorityGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.priorityGrid.ShowCellErrors = false;
            this.priorityGrid.ShowEditingIcon = false;
            this.priorityGrid.ShowRowErrors = false;
            this.priorityGrid.Size = new System.Drawing.Size(237, 137);
            this.priorityGrid.TabIndex = 26;
            this.priorityGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.OnDataError);
            // 
            // priorityColumn
            // 
            this.priorityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this.priorityColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.priorityColumn.FillWeight = 50F;
            this.priorityColumn.HeaderText = "Priority";
            this.priorityColumn.Name = "priorityColumn";
            this.priorityColumn.ReadOnly = true;
            this.priorityColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.priorityColumn.Width = 63;
            // 
            // toPriorityListColumn
            // 
            this.toPriorityListColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.toPriorityListColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.toPriorityListColumn.HeaderText = "Maps to";
            this.toPriorityListColumn.Name = "toPriorityListColumn";
            this.toPriorityListColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.toPriorityListColumn.Visible = false;
            // 
            // toPriorityColumn
            // 
            this.toPriorityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.toPriorityColumn.HeaderText = "Maps to";
            this.toPriorityColumn.Name = "toPriorityColumn";
            this.toPriorityColumn.Visible = false;
            // 
            // customPriorityField
            // 
            this.customPriorityField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customPriorityField.FormattingEnabled = true;
            this.customPriorityField.Location = new System.Drawing.Point(173, 85);
            this.customPriorityField.Name = "customPriorityField";
            this.customPriorityField.Size = new System.Drawing.Size(237, 21);
            this.customPriorityField.Sorted = true;
            this.customPriorityField.TabIndex = 25;
            this.customPriorityField.SelectedIndexChanged += new System.EventHandler(this.customPriorityField_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Priority Field";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(80, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(350, 31);
            this.label3.TabIndex = 23;
            this.label3.Text = "Instead of using Microsoft\'s standard Priority Field, you can choose a custom fie" +
    "ld in which to hold bug prioritisation.";
            // 
            // customPriorityNote
            // 
            this.customPriorityNote.Location = new System.Drawing.Point(80, 21);
            this.customPriorityNote.Name = "customPriorityNote";
            this.customPriorityNote.Size = new System.Drawing.Size(350, 19);
            this.customPriorityNote.TabIndex = 22;
            this.customPriorityNote.Text = "Some organisations define their own unique prioritisation listing for bugs.";
            // 
            // AdvancedSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(554, 643);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedSettingsView";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Settings";
            this.Shown += new System.EventHandler(this.OnFirstShow);
            this.tabControl.ResumeLayout(false);
            this.typeTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.jiraGrid)).EndInit();
            this.fieldTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tfsFieldGrid)).EndInit();
            this.tfsUserTab.ResumeLayout(false);
            this.tfsUserTab.PerformLayout();
            this.workItemTab.ResumeLayout(false);
            this.workItemTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workItemGrid)).EndInit();
            this.tfsBugTab.ResumeLayout(false);
            this.priorityPanel.ResumeLayout(false);
            this.priorityPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priorityGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage typeTab;
        private System.Windows.Forms.TabPage fieldTab;
        private System.Windows.Forms.Label pleaseCheckJira;
        private System.Windows.Forms.Label noJiraTicketTypes;
        private System.Windows.Forms.Button restoreDefaultTypesBtn;
        private System.Windows.Forms.DataGridView jiraGrid;
        private System.Windows.Forms.DataGridView tfsFieldGrid;
        private System.Windows.Forms.Label checkFieldSettings;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn tfsFieldValueColumn;
        private System.Windows.Forms.Label noFieldsFound;
        private System.Windows.Forms.TabPage tfsUserTab;
        private System.Windows.Forms.Label checkUserSettings;
        private System.Windows.Forms.Label noUsersFound;
        private System.Windows.Forms.ComboBox defaultAssigneeList;
        private System.Windows.Forms.Label detailedNoteOnDefaultUser;
        private System.Windows.Forms.Label defaultAsigneeLabel;
        private System.Windows.Forms.ComboBox defaultCreatorList;
        private System.Windows.Forms.Label defaultCreatorLabel;
        private System.Windows.Forms.Label defaultUserLabel;
        private System.Windows.Forms.TabPage workItemTab;
        private System.Windows.Forms.DataGridView workItemGrid;
        private System.Windows.Forms.Button restoreStateDefaults;
        private System.Windows.Forms.LinkLabel seeMicrosoftLink;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn workItem;
        private System.Windows.Forms.DataGridViewComboBoxColumn initialState;
        private System.Windows.Forms.TabPage tfsBugTab;
        private System.Windows.Forms.Panel priorityPanel;
        private System.Windows.Forms.DataGridView priorityGrid;
        private System.Windows.Forms.ComboBox customPriorityField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label customPriorityNote;
        private System.Windows.Forms.Button restorePriorityDefaults;
        private System.Windows.Forms.DataGridViewTextBoxColumn priorityColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn toPriorityListColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn toPriorityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn jiraColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn maptoColumn;
    }
}