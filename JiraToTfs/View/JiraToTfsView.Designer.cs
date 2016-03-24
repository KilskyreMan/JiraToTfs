namespace JiraToTfs.View
{
    partial class JiraToTfsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JiraToTfsView));
            this.ImportBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.selectProjectBtn = new System.Windows.Forms.Button();
            this.tfsProject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.jiraServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.jiraUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.jiraPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.jiraProject = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.progressMessage = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tfsTeams = new System.Windows.Forms.ComboBox();
            this.detailedProgress = new System.Windows.Forms.Label();
            this.advancedSettingsBtn = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.areaPaths = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.includeAttachments = new System.Windows.Forms.CheckBox();
            this.viewReport = new System.Windows.Forms.LinkLabel();
            this.impersonationPanel = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.impersonationLink = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pleaseWaitSpinner = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.noUsersPanel = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.noUsersAssignedLink = new System.Windows.Forms.LinkLabel();
            this.impersonationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pleaseWaitSpinner)).BeginInit();
            this.noUsersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ImportBtn
            // 
            this.ImportBtn.Location = new System.Drawing.Point(145, 411);
            this.ImportBtn.Margin = new System.Windows.Forms.Padding(4);
            this.ImportBtn.Name = "ImportBtn";
            this.ImportBtn.Size = new System.Drawing.Size(337, 30);
            this.ImportBtn.TabIndex = 10;
            this.ImportBtn.Text = "&Start Import";
            this.ImportBtn.UseVisualStyleBackColor = true;
            this.ImportBtn.Click += new System.EventHandler(this.ImportBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBtn.Location = new System.Drawing.Point(471, 534);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(4);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(100, 28);
            this.CloseBtn.TabIndex = 11;
            this.CloseBtn.Text = "&Close";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // selectProjectBtn
            // 
            this.selectProjectBtn.Location = new System.Drawing.Point(491, 191);
            this.selectProjectBtn.Margin = new System.Windows.Forms.Padding(4);
            this.selectProjectBtn.Name = "selectProjectBtn";
            this.selectProjectBtn.Size = new System.Drawing.Size(80, 28);
            this.selectProjectBtn.TabIndex = 6;
            this.selectProjectBtn.Text = "&Select";
            this.selectProjectBtn.UseVisualStyleBackColor = true;
            this.selectProjectBtn.Click += new System.EventHandler(this.selectProjectBtn_Click);
            // 
            // tfsProject
            // 
            this.tfsProject.Enabled = false;
            this.tfsProject.Location = new System.Drawing.Point(145, 193);
            this.tfsProject.Margin = new System.Windows.Forms.Padding(4);
            this.tfsProject.Name = "tfsProject";
            this.tfsProject.Size = new System.Drawing.Size(336, 22);
            this.tfsProject.TabIndex = 5;
            this.tfsProject.TextChanged += new System.EventHandler(this.tfsProjectName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 198);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Team Project";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Import Jira tickets from";
            // 
            // jiraServer
            // 
            this.jiraServer.Location = new System.Drawing.Point(145, 52);
            this.jiraServer.Margin = new System.Windows.Forms.Padding(4);
            this.jiraServer.Name = "jiraServer";
            this.jiraServer.Size = new System.Drawing.Size(424, 22);
            this.jiraServer.TabIndex = 0;
            this.jiraServer.TextChanged += new System.EventHandler(this.jiraServer_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Jira Server";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 89);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Username";
            // 
            // jiraUserName
            // 
            this.jiraUserName.Location = new System.Drawing.Point(145, 84);
            this.jiraUserName.Margin = new System.Windows.Forms.Padding(4);
            this.jiraUserName.Name = "jiraUserName";
            this.jiraUserName.Size = new System.Drawing.Size(167, 22);
            this.jiraUserName.TabIndex = 1;
            this.jiraUserName.TextChanged += new System.EventHandler(this.jiraUserName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(324, 90);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Password";
            // 
            // jiraPassword
            // 
            this.jiraPassword.Location = new System.Drawing.Point(403, 85);
            this.jiraPassword.Margin = new System.Windows.Forms.Padding(4);
            this.jiraPassword.Name = "jiraPassword";
            this.jiraPassword.Size = new System.Drawing.Size(167, 22);
            this.jiraPassword.TabIndex = 2;
            this.jiraPassword.UseSystemPasswordChar = true;
            this.jiraPassword.TextChanged += new System.EventHandler(this.jiraPassword_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(21, 162);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(230, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Create matching TFS tickets in";
            // 
            // jiraProject
            // 
            this.jiraProject.Location = new System.Drawing.Point(145, 116);
            this.jiraProject.Margin = new System.Windows.Forms.Padding(4);
            this.jiraProject.Name = "jiraProject";
            this.jiraProject.Size = new System.Drawing.Size(167, 22);
            this.jiraProject.TabIndex = 3;
            this.jiraProject.TextChanged += new System.EventHandler(this.jiraProject_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 121);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Project Key";
            // 
            // progressMessage
            // 
            this.progressMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.progressMessage.Location = new System.Drawing.Point(141, 460);
            this.progressMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.progressMessage.Name = "progressMessage";
            this.progressMessage.Size = new System.Drawing.Size(443, 22);
            this.progressMessage.TabIndex = 18;
            this.progressMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 267);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "Assign tickets to";
            // 
            // tfsTeams
            // 
            this.tfsTeams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tfsTeams.FormattingEnabled = true;
            this.tfsTeams.Location = new System.Drawing.Point(145, 262);
            this.tfsTeams.Margin = new System.Windows.Forms.Padding(4);
            this.tfsTeams.Name = "tfsTeams";
            this.tfsTeams.Size = new System.Drawing.Size(336, 24);
            this.tfsTeams.TabIndex = 8;
            this.tfsTeams.TextChanged += new System.EventHandler(this.tfsTeams_TextChanged);
            // 
            // detailedProgress
            // 
            this.detailedProgress.ForeColor = System.Drawing.Color.Blue;
            this.detailedProgress.Location = new System.Drawing.Point(141, 491);
            this.detailedProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.detailedProgress.Name = "detailedProgress";
            this.detailedProgress.Size = new System.Drawing.Size(443, 32);
            this.detailedProgress.TabIndex = 21;
            // 
            // advancedSettingsBtn
            // 
            this.advancedSettingsBtn.AutoSize = true;
            this.advancedSettingsBtn.Location = new System.Drawing.Point(145, 379);
            this.advancedSettingsBtn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.advancedSettingsBtn.Name = "advancedSettingsBtn";
            this.advancedSettingsBtn.Size = new System.Drawing.Size(126, 17);
            this.advancedSettingsBtn.TabIndex = 4;
            this.advancedSettingsBtn.TabStop = true;
            this.advancedSettingsBtn.Text = "&Advanced Settings";
            this.advancedSettingsBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.jiraMappingsBtn_LinkClicked);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(21, 319);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 17);
            this.label9.TabIndex = 23;
            this.label9.Text = "Import";
            // 
            // areaPaths
            // 
            this.areaPaths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.areaPaths.FormattingEnabled = true;
            this.areaPaths.Location = new System.Drawing.Point(145, 226);
            this.areaPaths.Margin = new System.Windows.Forms.Padding(4);
            this.areaPaths.Name = "areaPaths";
            this.areaPaths.Size = new System.Drawing.Size(336, 24);
            this.areaPaths.TabIndex = 7;
            this.areaPaths.TextChanged += new System.EventHandler(this.areaPaths_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 231);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "Area path:";
            // 
            // includeAttachments
            // 
            this.includeAttachments.AutoSize = true;
            this.includeAttachments.Location = new System.Drawing.Point(145, 347);
            this.includeAttachments.Margin = new System.Windows.Forms.Padding(4);
            this.includeAttachments.Name = "includeAttachments";
            this.includeAttachments.Size = new System.Drawing.Size(157, 21);
            this.includeAttachments.TabIndex = 9;
            this.includeAttachments.Text = "&Include Attachments";
            this.includeAttachments.UseVisualStyleBackColor = true;
            // 
            // viewReport
            // 
            this.viewReport.AutoSize = true;
            this.viewReport.Location = new System.Drawing.Point(141, 485);
            this.viewReport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.viewReport.Name = "viewReport";
            this.viewReport.Size = new System.Drawing.Size(84, 17);
            this.viewReport.TabIndex = 26;
            this.viewReport.TabStop = true;
            this.viewReport.Text = "&View Report";
            this.viewReport.Visible = false;
            this.viewReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.viewReport_LinkClicked);
            // 
            // impersonationPanel
            // 
            this.impersonationPanel.Controls.Add(this.label11);
            this.impersonationPanel.Controls.Add(this.impersonationLink);
            this.impersonationPanel.Controls.Add(this.pictureBox1);
            this.impersonationPanel.Location = new System.Drawing.Point(145, 297);
            this.impersonationPanel.Margin = new System.Windows.Forms.Padding(4);
            this.impersonationPanel.Name = "impersonationPanel";
            this.impersonationPanel.Size = new System.Drawing.Size(336, 22);
            this.impersonationPanel.TabIndex = 27;
            this.impersonationPanel.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(121, 2);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 17);
            this.label11.TabIndex = 30;
            this.label11.Text = "not enabled.";
            // 
            // impersonationLink
            // 
            this.impersonationLink.AutoSize = true;
            this.impersonationLink.Location = new System.Drawing.Point(29, 2);
            this.impersonationLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.impersonationLink.Name = "impersonationLink";
            this.impersonationLink.Size = new System.Drawing.Size(97, 17);
            this.impersonationLink.TabIndex = 29;
            this.impersonationLink.TabStop = true;
            this.impersonationLink.Text = "Impersonation";
            this.impersonationLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.impersonationLink_LinkClicked);
            this.impersonationLink.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnClickImpersonateHelp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::JiraToTfs.Properties.Resources.info;
            this.pictureBox1.Location = new System.Drawing.Point(4, 1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 18);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // pleaseWaitSpinner
            // 
            this.pleaseWaitSpinner.Image = global::JiraToTfs.Properties.Resources.pleasewait;
            this.pleaseWaitSpinner.Location = new System.Drawing.Point(79, 458);
            this.pleaseWaitSpinner.Margin = new System.Windows.Forms.Padding(4);
            this.pleaseWaitSpinner.Name = "pleaseWaitSpinner";
            this.pleaseWaitSpinner.Size = new System.Drawing.Size(32, 32);
            this.pleaseWaitSpinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pleaseWaitSpinner.TabIndex = 25;
            this.pleaseWaitSpinner.TabStop = false;
            this.pleaseWaitSpinner.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(22, 540);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(106, 17);
            this.linkLabel1.TabIndex = 28;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Tell me more ...";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnClickTellMeMore);
            // 
            // noUsersPanel
            // 
            this.noUsersPanel.Controls.Add(this.noUsersAssignedLink);
            this.noUsersPanel.Controls.Add(this.pictureBox2);
            this.noUsersPanel.Location = new System.Drawing.Point(145, 297);
            this.noUsersPanel.Margin = new System.Windows.Forms.Padding(4);
            this.noUsersPanel.Name = "noUsersPanel";
            this.noUsersPanel.Size = new System.Drawing.Size(337, 22);
            this.noUsersPanel.TabIndex = 31;
            this.noUsersPanel.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::JiraToTfs.Properties.Resources.info;
            this.pictureBox2.Location = new System.Drawing.Point(4, 1);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(18, 18);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            // 
            // noUsersAssignedLink
            // 
            this.noUsersAssignedLink.AutoSize = true;
            this.noUsersAssignedLink.Location = new System.Drawing.Point(27, 2);
            this.noUsersAssignedLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.noUsersAssignedLink.Name = "noUsersAssignedLink";
            this.noUsersAssignedLink.Size = new System.Drawing.Size(277, 17);
            this.noUsersAssignedLink.TabIndex = 32;
            this.noUsersAssignedLink.TabStop = true;
            this.noUsersAssignedLink.Text = "No users assigned to selected TFS Project";
            this.noUsersAssignedLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.noUsersAssignedLink_LinkClicked);
            // 
            // JiraToTfsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.CloseBtn;
            this.ClientSize = new System.Drawing.Size(603, 580);
            this.Controls.Add(this.noUsersPanel);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.impersonationPanel);
            this.Controls.Add(this.viewReport);
            this.Controls.Add(this.pleaseWaitSpinner);
            this.Controls.Add(this.includeAttachments);
            this.Controls.Add(this.areaPaths);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.advancedSettingsBtn);
            this.Controls.Add(this.detailedProgress);
            this.Controls.Add(this.tfsTeams);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.progressMessage);
            this.Controls.Add(this.jiraProject);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.jiraPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.jiraUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.jiraServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectProjectBtn);
            this.Controls.Add(this.tfsProject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.ImportBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JiraToTfsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jira to TFS";
            this.Shown += new System.EventHandler(this.JiraToTfsView_Shown);
            this.impersonationPanel.ResumeLayout(false);
            this.impersonationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pleaseWaitSpinner)).EndInit();
            this.noUsersPanel.ResumeLayout(false);
            this.noUsersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ImportBtn;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Button selectProjectBtn;
        private System.Windows.Forms.TextBox tfsProject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox jiraServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox jiraUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox jiraPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox jiraProject;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label progressMessage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox tfsTeams;
        private System.Windows.Forms.Label detailedProgress;
        private System.Windows.Forms.LinkLabel advancedSettingsBtn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox areaPaths;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox includeAttachments;
        private System.Windows.Forms.PictureBox pleaseWaitSpinner;
        private System.Windows.Forms.LinkLabel viewReport;
        private System.Windows.Forms.Panel impersonationPanel;
        private System.Windows.Forms.LinkLabel impersonationLink;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel noUsersPanel;
        private System.Windows.Forms.LinkLabel noUsersAssignedLink;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}