namespace Goodware.Jabber.GUI
{
    partial class JustTalk
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JustTalk));
			this.defaultGroupContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addContactDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gropupContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.renameGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.connectTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.topMenu = new System.Windows.Forms.MenuStrip();
			this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.contactsTreeView = new System.Windows.Forms.TreeView();
			this.contactsViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contactsImageList = new System.Windows.Forms.ImageList(this.components);
			this.bottomStatusStrip = new System.Windows.Forms.StatusStrip();
			this.connectedStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.statusToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.awayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.busyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.optionsToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.contactsToolStrip = new System.Windows.Forms.ToolStrip();
			this.addGroupToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.addContactToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.contactsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.talkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.editContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.defaultGroupContextMenuStrip.SuspendLayout();
			this.gropupContextMenuStrip.SuspendLayout();
			this.trayMenu.SuspendLayout();
			this.topMenu.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.contactsViewContextMenuStrip.SuspendLayout();
			this.bottomStatusStrip.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.contactsToolStrip.SuspendLayout();
			this.contactsContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// defaultGroupContextMenuStrip
			// 
			this.defaultGroupContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addContactDefaultToolStripMenuItem});
			this.defaultGroupContextMenuStrip.Name = "gropupContextMenuStrip";
			this.defaultGroupContextMenuStrip.Size = new System.Drawing.Size(146, 26);
			// 
			// addContactDefaultToolStripMenuItem
			// 
			this.addContactDefaultToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.user_add;
			this.addContactDefaultToolStripMenuItem.Name = "addContactDefaultToolStripMenuItem";
			this.addContactDefaultToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.addContactDefaultToolStripMenuItem.Text = "&Add Contact";
			this.addContactDefaultToolStripMenuItem.Click += new System.EventHandler(this.addContactToolStripMenuItem_Click);
			// 
			// gropupContextMenuStrip
			// 
			this.gropupContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addContactToolStripMenuItem,
            this.toolStripSeparator2,
            this.renameGroupToolStripMenuItem,
            this.removeGroupToolStripMenuItem});
			this.gropupContextMenuStrip.Name = "gropupContextMenuStrip";
			this.gropupContextMenuStrip.Size = new System.Drawing.Size(157, 76);
			// 
			// addContactToolStripMenuItem
			// 
			this.addContactToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.user_add;
			this.addContactToolStripMenuItem.Name = "addContactToolStripMenuItem";
			this.addContactToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.addContactToolStripMenuItem.Text = "&Add Contact";
			this.addContactToolStripMenuItem.Click += new System.EventHandler(this.addContactToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
			// 
			// renameGroupToolStripMenuItem
			// 
			this.renameGroupToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.group_edit;
			this.renameGroupToolStripMenuItem.Name = "renameGroupToolStripMenuItem";
			this.renameGroupToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.renameGroupToolStripMenuItem.Text = "Re&name Group";
			this.renameGroupToolStripMenuItem.Click += new System.EventHandler(this.renameGroupToolStripMenuItem_Click);
			// 
			// removeGroupToolStripMenuItem
			// 
			this.removeGroupToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.group_delete;
			this.removeGroupToolStripMenuItem.Name = "removeGroupToolStripMenuItem";
			this.removeGroupToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.removeGroupToolStripMenuItem.Text = "&Remove Group";
			this.removeGroupToolStripMenuItem.Click += new System.EventHandler(this.removeGroupToolStripMenuItem_Click);
			// 
			// trayMenu
			// 
			this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectTrayMenuItem,
            this.toolStripSeparator1,
            this.exitTrayMenuItem});
			this.trayMenu.Name = "contextMenuStrip1";
			this.trayMenu.Size = new System.Drawing.Size(126, 54);
			// 
			// connectTrayMenuItem
			// 
			this.connectTrayMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.connect;
			this.connectTrayMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.connectTrayMenuItem.Name = "connectTrayMenuItem";
			this.connectTrayMenuItem.Size = new System.Drawing.Size(125, 22);
			this.connectTrayMenuItem.Text = "&Connect";
			this.connectTrayMenuItem.Click += new System.EventHandler(this.connectTrayMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
			// 
			// exitTrayMenuItem
			// 
			this.exitTrayMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.door;
			this.exitTrayMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.exitTrayMenuItem.Name = "exitTrayMenuItem";
			this.exitTrayMenuItem.Size = new System.Drawing.Size(125, 22);
			this.exitTrayMenuItem.Text = "E&xit";
			this.exitTrayMenuItem.Click += new System.EventHandler(this.exitTrayMenuItem_Click);
			// 
			// topMenu
			// 
			this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.topMenu.Location = new System.Drawing.Point(0, 0);
			this.topMenu.Name = "topMenu";
			this.topMenu.Size = new System.Drawing.Size(184, 24);
			this.topMenu.TabIndex = 1;
			this.topMenu.Text = "topMenu";
			// 
			// connectToolStripMenuItem
			// 
			this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
			this.connectToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.connectToolStripMenuItem.Text = "Connect";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// helpToolStripMenuItem1
			// 
			this.helpToolStripMenuItem1.Image = global::Goodware.Jabber.GUI.Properties.Resources.help;
			this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			this.helpToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
			this.helpToolStripMenuItem1.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenuStrip = this.trayMenu;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "Disconnected";
			this.trayIcon.Visible = true;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.contactsTreeView);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.bottomStatusStrip);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(184, 293);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 24);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(184, 318);
			this.toolStripContainer1.TabIndex = 2;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mainToolStrip);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.contactsToolStrip);
			// 
			// contactsTreeView
			// 
			this.contactsTreeView.AllowDrop = true;
			this.contactsTreeView.ContextMenuStrip = this.contactsViewContextMenuStrip;
			this.contactsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contactsTreeView.ImageIndex = 0;
			this.contactsTreeView.ImageList = this.contactsImageList;
			this.contactsTreeView.Location = new System.Drawing.Point(0, 0);
			this.contactsTreeView.Name = "contactsTreeView";
			this.contactsTreeView.SelectedImageIndex = 0;
			this.contactsTreeView.ShowLines = false;
			this.contactsTreeView.ShowNodeToolTips = true;
			this.contactsTreeView.Size = new System.Drawing.Size(184, 271);
			this.contactsTreeView.TabIndex = 1;
			this.contactsTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.contactsTreeView_NodeMouseDoubleClick);
			this.contactsTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.contactsTreeView_DragDrop);
			this.contactsTreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contactsTreeView_MouseClick);
			this.contactsTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.contactsTreeView_DragEnter);
			this.contactsTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.contactsTreeView_ItemDrag);
			// 
			// contactsViewContextMenuStrip
			// 
			this.contactsViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGroupToolStripMenuItem});
			this.contactsViewContextMenuStrip.Name = "contactsContextMenuStrip";
			this.contactsViewContextMenuStrip.Size = new System.Drawing.Size(137, 26);
			// 
			// addGroupToolStripMenuItem
			// 
			this.addGroupToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.group_add;
			this.addGroupToolStripMenuItem.Name = "addGroupToolStripMenuItem";
			this.addGroupToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.addGroupToolStripMenuItem.Text = "&Add Group";
			this.addGroupToolStripMenuItem.Click += new System.EventHandler(this.addGroupToolStripMenuItem_Click);
			// 
			// contactsImageList
			// 
			this.contactsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("contactsImageList.ImageStream")));
			this.contactsImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.contactsImageList.Images.SetKeyName(0, "group.png");
			this.contactsImageList.Images.SetKeyName(1, "user.png");
			this.contactsImageList.Images.SetKeyName(2, "user_busy.png");
			this.contactsImageList.Images.SetKeyName(3, "user_offline.png");
			// 
			// bottomStatusStrip
			// 
			this.bottomStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectedStatus});
			this.bottomStatusStrip.Location = new System.Drawing.Point(0, 271);
			this.bottomStatusStrip.Name = "bottomStatusStrip";
			this.bottomStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.bottomStatusStrip.Size = new System.Drawing.Size(184, 22);
			this.bottomStatusStrip.TabIndex = 0;
			this.bottomStatusStrip.Text = "statusStrip1";
			// 
			// connectedStatus
			// 
			this.connectedStatus.Image = global::Goodware.Jabber.GUI.Properties.Resources.disconnect;
			this.connectedStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.connectedStatus.Name = "connectedStatus";
			this.connectedStatus.Size = new System.Drawing.Size(87, 17);
			this.connectedStatus.Text = "Disconnected";
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripDropDownButton,
            this.connectToolStripButton,
            this.optionsToolStripButton});
			this.mainToolStrip.Location = new System.Drawing.Point(3, 0);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(131, 25);
			this.mainToolStrip.TabIndex = 0;
			// 
			// statusToolStripDropDownButton
			// 
			this.statusToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.awayToolStripMenuItem,
            this.busyToolStripMenuItem});
			this.statusToolStripDropDownButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb;
			this.statusToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.statusToolStripDropDownButton.Name = "statusToolStripDropDownButton";
			this.statusToolStripDropDownButton.Size = new System.Drawing.Size(67, 22);
			this.statusToolStripDropDownButton.Text = "Status";
			// 
			// onlineToolStripMenuItem
			// 
			this.onlineToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb;
			this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
			this.onlineToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.onlineToolStripMenuItem.Text = "Online";
			this.onlineToolStripMenuItem.Click += new System.EventHandler(this.onlineToolStripMenuItem_Click);
			// 
			// awayToolStripMenuItem
			// 
			this.awayToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb_off;
			this.awayToolStripMenuItem.Name = "awayToolStripMenuItem";
			this.awayToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.awayToolStripMenuItem.Text = "Away";
			this.awayToolStripMenuItem.Click += new System.EventHandler(this.awayToolStripMenuItem_Click);
			// 
			// busyToolStripMenuItem
			// 
			this.busyToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb_delete;
			this.busyToolStripMenuItem.Name = "busyToolStripMenuItem";
			this.busyToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.busyToolStripMenuItem.Text = "Busy";
			this.busyToolStripMenuItem.Click += new System.EventHandler(this.busyToolStripMenuItem_Click);
			// 
			// connectToolStripButton
			// 
			this.connectToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.connectToolStripButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.connect;
			this.connectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.connectToolStripButton.Name = "connectToolStripButton";
			this.connectToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.connectToolStripButton.Text = "toolStripButton1";
			this.connectToolStripButton.ToolTipText = "Connect";
			this.connectToolStripButton.Click += new System.EventHandler(this.connectToolStripButton_Click);
			// 
			// optionsToolStripButton
			// 
			this.optionsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.optionsToolStripButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.wrench;
			this.optionsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.optionsToolStripButton.Name = "optionsToolStripButton";
			this.optionsToolStripButton.Size = new System.Drawing.Size(23, 20);
			this.optionsToolStripButton.Text = "toolStripButton1";
			this.optionsToolStripButton.ToolTipText = "Options";
			this.optionsToolStripButton.Click += new System.EventHandler(this.optionsToolStripButton_Click);
			// 
			// contactsToolStrip
			// 
			this.contactsToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.contactsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGroupToolStripButton,
            this.addContactToolStripButton});
			this.contactsToolStrip.Location = new System.Drawing.Point(134, 0);
			this.contactsToolStrip.Name = "contactsToolStrip";
			this.contactsToolStrip.Size = new System.Drawing.Size(50, 25);
			this.contactsToolStrip.TabIndex = 1;
			// 
			// addGroupToolStripButton
			// 
			this.addGroupToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addGroupToolStripButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.group_add;
			this.addGroupToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addGroupToolStripButton.Name = "addGroupToolStripButton";
			this.addGroupToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.addGroupToolStripButton.Text = "toolStripButton1";
			this.addGroupToolStripButton.ToolTipText = "Add Group";
			this.addGroupToolStripButton.Click += new System.EventHandler(this.addGroupToolStripButton_Click);
			// 
			// addContactToolStripButton
			// 
			this.addContactToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addContactToolStripButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.user_add;
			this.addContactToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addContactToolStripButton.Name = "addContactToolStripButton";
			this.addContactToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.addContactToolStripButton.Text = "toolStripButton1";
			this.addContactToolStripButton.ToolTipText = "Add Contact";
			this.addContactToolStripButton.Click += new System.EventHandler(this.addContactToolStripButton_Click);
			// 
			// contactsContextMenuStrip
			// 
			this.contactsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.talkToolStripMenuItem,
            this.toolStripSeparator3,
            this.editContactToolStripMenuItem,
            this.removeContactToolStripMenuItem});
			this.contactsContextMenuStrip.Name = "contactsContextMenuStrip";
			this.contactsContextMenuStrip.Size = new System.Drawing.Size(166, 76);
			// 
			// talkToolStripMenuItem
			// 
			this.talkToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.user_comment;
			this.talkToolStripMenuItem.Name = "talkToolStripMenuItem";
			this.talkToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.talkToolStripMenuItem.Text = "&Talk";
			this.talkToolStripMenuItem.Click += new System.EventHandler(this.talkToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(162, 6);
			// 
			// editContactToolStripMenuItem
			// 
			this.editContactToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.user_edit;
			this.editContactToolStripMenuItem.Name = "editContactToolStripMenuItem";
			this.editContactToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.editContactToolStripMenuItem.Text = "&Edit Contact";
			// 
			// removeContactToolStripMenuItem
			// 
			this.removeContactToolStripMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.user_delete;
			this.removeContactToolStripMenuItem.Name = "removeContactToolStripMenuItem";
			this.removeContactToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.removeContactToolStripMenuItem.Text = "&Remove Contact";
			this.removeContactToolStripMenuItem.Click += new System.EventHandler(this.removeContactToolStripMenuItem_Click);
			// 
			// JustTalk
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(184, 342);
			this.ContextMenuStrip = this.trayMenu;
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.topMenu);
			this.MainMenuStrip = this.topMenu;
			this.Name = "JustTalk";
			this.Text = "JustTalk";
			this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.defaultGroupContextMenuStrip.ResumeLayout(false);
			this.gropupContextMenuStrip.ResumeLayout(false);
			this.trayMenu.ResumeLayout(false);
			this.topMenu.ResumeLayout(false);
			this.topMenu.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.PerformLayout();
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.contactsViewContextMenuStrip.ResumeLayout(false);
			this.bottomStatusStrip.ResumeLayout(false);
			this.bottomStatusStrip.PerformLayout();
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.contactsToolStrip.ResumeLayout(false);
			this.contactsToolStrip.PerformLayout();
			this.contactsContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.MenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ToolStripMenuItem connectTrayMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitTrayMenuItem;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.StatusStrip bottomStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel connectedStatus;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.ToolStripDropDownButton statusToolStripDropDownButton;
		private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem awayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem busyToolStripMenuItem;
        private System.Windows.Forms.ImageList contactsImageList;
        private System.Windows.Forms.ContextMenuStrip gropupContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addContactToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem renameGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeGroupToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contactsViewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addGroupToolStripMenuItem;
        private System.Windows.Forms.TreeView contactsTreeView;
        private System.Windows.Forms.ContextMenuStrip contactsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem talkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editContactToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeContactToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton connectToolStripButton;
		private System.Windows.Forms.ToolStrip contactsToolStrip;
		private System.Windows.Forms.ToolStripButton addGroupToolStripButton;
		private System.Windows.Forms.ToolStripButton addContactToolStripButton;
		private System.Windows.Forms.ToolStripButton optionsToolStripButton;
		private System.Windows.Forms.ContextMenuStrip defaultGroupContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addContactDefaultToolStripMenuItem;
    }
}

