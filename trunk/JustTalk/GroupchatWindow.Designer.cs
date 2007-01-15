namespace Goodware.Jabber.GUI {
	partial class GroupchatWindow {
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
			this.mainContainer = new System.Windows.Forms.SplitContainer();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.membersListBox = new System.Windows.Forms.ListBox();
			this.dialogView = new System.Windows.Forms.RichTextBox();
			this.inputTextBox = new System.Windows.Forms.TextBox();
			this.mainContainer.Panel1.SuspendLayout();
			this.mainContainer.Panel2.SuspendLayout();
			this.mainContainer.SuspendLayout();
			this.groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainContainer
			// 
			this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainContainer.Location = new System.Drawing.Point(0, 0);
			this.mainContainer.Name = "mainContainer";
			// 
			// mainContainer.Panel1
			// 
			this.mainContainer.Panel1.Controls.Add(this.groupBox);
			// 
			// mainContainer.Panel2
			// 
			this.mainContainer.Panel2.Controls.Add(this.dialogView);
			this.mainContainer.Panel2.Controls.Add(this.inputTextBox);
			this.mainContainer.Size = new System.Drawing.Size(435, 268);
			this.mainContainer.SplitterDistance = 145;
			this.mainContainer.TabIndex = 1;
			// 
			// groupBox
			// 
			this.groupBox.AutoSize = true;
			this.groupBox.Controls.Add(this.membersListBox);
			this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox.Location = new System.Drawing.Point(0, 0);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(145, 268);
			this.groupBox.TabIndex = 1;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "Members";
			// 
			// membersListBox
			// 
			this.membersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.membersListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.membersListBox.FormattingEnabled = true;
			this.membersListBox.Location = new System.Drawing.Point(3, 16);
			this.membersListBox.Name = "membersListBox";
			this.membersListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.membersListBox.Size = new System.Drawing.Size(139, 249);
			this.membersListBox.TabIndex = 0;
			this.membersListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.membersListBox_DrawItem);
			// 
			// dialogView
			// 
			this.dialogView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dialogView.Location = new System.Drawing.Point(0, 0);
			this.dialogView.Name = "dialogView";
			this.dialogView.ReadOnly = true;
			this.dialogView.Size = new System.Drawing.Size(286, 248);
			this.dialogView.TabIndex = 1;
			this.dialogView.Text = "";
			// 
			// inputTextBox
			// 
			this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.inputTextBox.Location = new System.Drawing.Point(0, 248);
			this.inputTextBox.Name = "inputTextBox";
			this.inputTextBox.Size = new System.Drawing.Size(286, 20);
			this.inputTextBox.TabIndex = 0;
			this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
			// 
			// GroupchatWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(435, 268);
			this.Controls.Add(this.mainContainer);
			this.Name = "GroupchatWindow";
			this.Text = "Groupchat";
			this.mainContainer.Panel1.ResumeLayout(false);
			this.mainContainer.Panel1.PerformLayout();
			this.mainContainer.Panel2.ResumeLayout(false);
			this.mainContainer.Panel2.PerformLayout();
			this.mainContainer.ResumeLayout(false);
			this.groupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer mainContainer;
		private System.Windows.Forms.RichTextBox dialogView;
		private System.Windows.Forms.TextBox inputTextBox;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.ListBox membersListBox;
	}
}