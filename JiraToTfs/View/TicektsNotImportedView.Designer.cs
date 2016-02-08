namespace JiraToTfs.View
{
    partial class TicektsNotImportedView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicektsNotImportedView));
            this.failedTicketTree = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.doneBtn = new System.Windows.Forms.Button();
            this.fieldMappingsLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // failedTicketTree
            // 
            this.failedTicketTree.Location = new System.Drawing.Point(25, 40);
            this.failedTicketTree.Name = "failedTicketTree";
            this.failedTicketTree.Size = new System.Drawing.Size(487, 313);
            this.failedTicketTree.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(486, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "The following tickets failed validation and were not imported into TFS.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(486, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Correct problems outlined above and re-run Import Utility. ";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 382);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(486, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "(Re-running will only bring across new / skipped tickets not previously imported)" +
    ".";
            // 
            // doneBtn
            // 
            this.doneBtn.Location = new System.Drawing.Point(381, 414);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(131, 23);
            this.doneBtn.TabIndex = 5;
            this.doneBtn.Text = "&Close";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.skipAndContinueBtn_Click);
            // 
            // fieldMappingsLink
            // 
            this.fieldMappingsLink.AutoSize = true;
            this.fieldMappingsLink.Location = new System.Drawing.Point(22, 419);
            this.fieldMappingsLink.Name = "fieldMappingsLink";
            this.fieldMappingsLink.Size = new System.Drawing.Size(116, 13);
            this.fieldMappingsLink.TabIndex = 6;
            this.fieldMappingsLink.TabStop = true;
            this.fieldMappingsLink.Text = "Custom &Field Mappings";
            this.fieldMappingsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.fieldMappingsLink_LinkClicked);
            // 
            // TicektsNotImportedView
            // 
            this.AcceptButton = this.doneBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 451);
            this.Controls.Add(this.fieldMappingsLink);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.failedTicketTree);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TicektsNotImportedView";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tickets not import";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView failedTicketTree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.LinkLabel fieldMappingsLink;
    }
}