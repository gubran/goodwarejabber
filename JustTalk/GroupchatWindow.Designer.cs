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
			this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
			this.mainContainer = new System.Windows.Forms.SplitContainer();
			this.inputTextBox = new System.Windows.Forms.TextBox();
			this.dialogView = new System.Windows.Forms.RichTextBox();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.membersListBox = new System.Windows.Forms.ListBox();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusToolStrip = new System.Windows.Forms.ToolStrip();
			this.statusToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.awayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.busyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.statusMessageToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.mainContainer.Panel1.SuspendLayout();
			this.mainContainer.Panel2.SuspendLayout();
			this.mainContainer.SuspendLayout();
			this.groupBox.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// BottomToolStripPanel
			// 
			this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.BottomToolStripPanel.Name = "BottomToolStripPanel";
			this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// TopToolStripPanel
			// 
			this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.TopToolStripPanel.Name = "TopToolStripPanel";
			this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// RightToolStripPanel
			// 
			this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.RightToolStripPanel.Name = "RightToolStripPanel";
			this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// LeftToolStripPanel
			// 
			this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.LeftToolStripPanel.Name = "LeftToolStripPanel";
			this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// ContentPanel
			// 
			this.ContentPanel.AutoScroll = true;
			this.ContentPanel.Size = new System.Drawing.Size(435, 243);
			// 
			// mainContainer
			// 
			this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainContainer.Location = new System.Drawing.Point(0, 0);
			this.mainContainer.Name = "mainContainer";
			// 
			// mainContainer.Panel1
			// 
			this.mainContainer.Panel1.Controls.Add(this.toolStripContainer1);
			// 
			// mainContainer.Panel2
			// 
			this.mainContainer.Panel2.Controls.Add(this.dialogView);
			this.mainContainer.Panel2.Controls.Add(this.inputTextBox);
			this.mainContainer.Size = new System.Drawing.Size(435, 268);
			this.mainContainer.SplitterDistance = 155;
			this.mainContainer.TabIndex = 1;
			// 
			// inputTextBox
			// 
			this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.inputTextBox.Location = new System.Drawing.Point(0, 248);
			this.inputTextBox.Name = "inputTextBox";
			this.inputTextBox.Size = new System.Drawing.Size(276, 20);
			this.inputTextBox.TabIndex = 0;
			this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
			// 
			// dialogView
			// 
			this.dialogView.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.dialogView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dialogView.Location = new System.Drawing.Point(0, 0);
			this.dialogView.Name = "dialogView";
			this.dialogView.ReadOnly = true;
			this.dialogView.Size = new System.Drawing.Size(276, 248);
			this.dialogView.TabIndex = 1;
			this.dialogView.Text = "";
			// 
			// groupBox
			// 
			this.groupBox.AutoSize = true;
			this.groupBox.Controls.Add(this.membersListBox);
			this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox.Location = new System.Drawing.Point(0, 0);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(155, 243);
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
			this.membersListBox.Size = new System.Drawing.Size(149, 224);
			this.membersListBox.TabIndex = 0;
			this.membersListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.membersListBox_DrawItem);
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.groupBox);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(155, 243);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(155, 268);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.statusToolStrip);
			// 
			// statusToolStrip
			// 
			this.statusToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.statusToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.statusToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripDropDownButton,
            this.toolStripSeparator5,
            this.statusMessageToolStripComboBox});
			this.statusToolStrip.Location = new System.Drawing.Point(3, 0);
			this.statusToolStrip.Name = "statusToolStrip";
			this.statusToolStrip.Size = new System.Drawing.Size(152, 25);
			this.statusToolStrip.TabIndex = 3;
			// 
			// statusToolStripDropDownButton
			// 
			this.statusToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.statusToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.awayToolStripMenuItem,
            this.busyToolStripMenuItem});
			this.statusToolStripDropDownButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb;
			this.statusToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.statusToolStripDropDownButton.Name = "statusToolStripDropDownButton";
			this.statusToolStripDropDownButton.Size = new System.Drawing.Size(29, 22);
			this.statusToolStripDropDownButton.Text = "Status";
			// 
			// onlineToolStripMenuItem
			// 
			this.onlineToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb;
			this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
			this.onlineToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.onlineToolStripMenuItem.Text = "Chat";
			// 
			// awayToolStripMenuItem
			// 
			this.awayToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb_off;
			this.awayToolStripMenuItem.Name = "awayToolStripMenuItem";
			this.awayToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.awayToolStripMenuItem.Text = "Away";
			// 
			// busyToolStripMenuItem
			// 
			this.busyToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb_delete;
			this.busyToolStripMenuItem.Name = "busyToolStripMenuItem";
			this.busyToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.busyToolStripMenuItem.Text = "Do Not Disturb";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// statusMessageToolStripComboBox
			// 
			this.statusMessageToolStripComboBox.Name = "statusMessageToolStripComboBox";
			this.statusMessageToolStripComboBox.Size = new System.Drawing.Size(110, 25);
			this.statusMessageToolStripComboBox.ToolTipText = "Status Message";
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
			this.mainContainer.Panel2.ResumeLayout(false);
			this.mainContainer.Panel2.PerformLayout();
			this.mainContainer.ResumeLayout(false);
			this.groupBox.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.PerformLayout();
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusToolStrip.ResumeLayout(false);
			this.statusToolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
		private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
		private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
		private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
		private System.Windows.Forms.ToolStripContentPanel ContentPanel;
		private System.Windows.Forms.SplitContainer mainContainer;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.ListBox membersListBox;
		private System.Windows.Forms.RichTextBox dialogView;
		private System.Windows.Forms.TextBox inputTextBox;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip statusToolStrip;
		private System.Windows.Forms.ToolStripDropDownButton statusToolStripDropDownButton;
		private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem awayToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem busyToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripComboBox statusMessageToolStripComboBox;

	}
}