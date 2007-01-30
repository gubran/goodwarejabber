using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Goodware.Jabber.Client;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.GUI {
    public partial class JustTalk : Form {
        private bool connected;													// To save the connected state
        private TreeNode NodeToBeMoved;											// For drag and drop purposes 
		internal JabberModel model;												// The model is the key class, which most of the communication goes through
		private Dictionary<string, ConverstationWindow> conversations;			// A hashtable to store references to all open conversations
		private Dictionary<string, Contact> contacts;							// Hashtable for easy access to the contacts (Without checking all the tree nodes)
		private Dictionary<string, GroupchatWindow> groupChats;						// Hashtable for groupChats

        public JustTalk() {
            InitializeComponent();

			// Load Toolbars state
			ToolStripManager.LoadSettings(this);			

			model = new JabberModel(this);	// TODO: Revise this
            this.connected = false;	
        }

		private void JustTalk_Shown(object sender, EventArgs e) {
			// Set the values of the menu items to the real values (maintained by the ToolStripManager)		
			this.mainToolStripMenuItem.Checked = this.mainToolStrip.Visible;
			this.statusToolStripMenuItem.Checked = this.statusToolStrip.Visible;
			this.contactsToolStripMenuItem.Checked = this.contactsToolStrip.Visible;
		}

		private void JustTalk_Load(object sender, EventArgs e) {
			// Load size from settings
			if(Properties.Settings.Default.WindowSize != null) {
				this.Size = Properties.Settings.Default.WindowSize;
			}
		}

		private void JustTalk_FormClosing(object sender, FormClosingEventArgs e) {
			this.WindowState = FormWindowState.Minimized;
			this.ShowInTaskbar = false;
			e.Cancel = true;
		}

		private void JustTalk_FormClosed(object sender, FormClosedEventArgs e) {
			// Save size
			if(this.WindowState == FormWindowState.Normal) {
				Properties.Settings.Default.WindowSize = this.Size;
			} else {
				Properties.Settings.Default.WindowSize = this.RestoreBounds.Size;
			}

			Properties.Settings.Default.Save();			// Save setting manually
			ToolStripManager.SaveSettings(this);		// Save toolstrips state
			this.disconnect();							// Disconnect from server
		}

		// Exit
        private void exitTrayMenuItem_Click(object sender, EventArgs e) {			
			this.disconnect();
            this.Dispose(true);
        }
		
		// Connect switch (connect or disconnect, and update the gui)
        private void connectTrayMenuItem_Click(object sender, EventArgs e) {
            if(!this.connected) {
				if (this.connect()) {
					connectGUI();					
				} else {
					MessageBox.Show("Unable to connect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
            } else {
				if(this.disconnect()) {
					disconnectGUI();
				}
			}			
        }

		// Update the gui to a connected state
		private void connectGUI() {
			// Enable contacts strip
			foreach(ToolStripItem item in this.contactsToolStrip.Items) {
				item.Enabled = true;
			}

			// Update visual components
			this.connectTrayMenuItem.Text = "Disconnect";
			this.connectTrayMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.disconnect;
			this.connectToolStripButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.disconnect;
			this.connectToolStripButton.ToolTipText = "Disconnect";
			this.connectedStatus.Text = "Connected";
			this.connectedStatus.Image = global::Goodware.Jabber.GUI.Properties.Resources.connect;
			this.trayIcon.Icon = global::Goodware.Jabber.GUI.Properties.Resources.lightbulbico;
			this.optionsToolStripButton.Enabled = false;
			this.statusToolStripDropDownButton.Enabled = true;
			this.statusMessageToolStripComboBox.Enabled = true;
			this.contactsTreeView.ContextMenuStrip = contactsViewContextMenuStrip;
			this.Text = model.User + "@" + model.ServerName + " - JustTalk";
			this.trayIcon.Text = "Connected";

			// Initialize the structures needed
			Group def = new Group("Default Group");
			def.ContextMenuStrip = defaultGroupContextMenuStrip;
			contactsTreeView.Nodes.Add(def);
			contactsTreeView.TreeViewNodeSorter = Comparer<IComparable>.Default;	// Sorter to sort the contacts in the groups
			conversations = new Dictionary<string, ConverstationWindow>();
			contacts = new Dictionary<string, Contact>();
			groupChats = new Dictionary<string, GroupchatWindow>();

			this.connected = true;			// Set state to connected
		}


		private void disconnectGUI() {
			// Disable contacts strip
			foreach(ToolStripItem item in this.contactsToolStrip.Items) {
				item.Enabled = false;
			}

			// Update visual components
			this.connectTrayMenuItem.Text = "Connect";
			this.connectTrayMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.connect;
			this.connectToolStripButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.connect;
			this.connectToolStripButton.ToolTipText = "Connect";
			this.connectedStatus.Text = "Disconnected";
			this.connectedStatus.Image = global::Goodware.Jabber.GUI.Properties.Resources.disconnect;
			this.trayIcon.Icon = global::Goodware.Jabber.GUI.Properties.Resources.lightbulboff;
			this.optionsToolStripButton.Enabled = true;
			this.statusToolStripDropDownButton.Enabled = false;
			this.statusMessageToolStripComboBox.Enabled = false;
			this.contactsTreeView.ContextMenuStrip = null;
			this.Text = "JustTalk";
			this.trayIcon.Text = "Disconnected";

			// Release memory
			foreach(TreeNode node in contactsTreeView.Nodes) {
				node.Remove();
			}
			contactsTreeView.TreeViewNodeSorter = null;
			foreach(ConverstationWindow cv in conversations.Values) {
				cv.Close();
				cv.Dispose();
			}
			conversations = null;		
			contacts = null;

			this.connected = false;		// Set state to disconnected
		}
		
		public bool connect() {			// Actual connect
			try {
				// Initialize model
				model.ServerName = Properties.Settings.Default.ServerName;
				model.ServerAddress = Properties.Settings.Default.ServerAddress;
				model.Port = Properties.Settings.Default.Port;
				model.User = Properties.Settings.Default.Username;
				model.AuthMode = Properties.Settings.Default.AuthMode;	//Додадено од Милош/Васко
				model.Resource = Properties.Settings.Default.Resource;
				model.Password = Properties.Settings.Default.Password;
				model.Me = new Contact(model.User + "@" + model.ServerName);
				model.Me.Status = Status.chat;

				// Send initial messages
				model.connect();
				model.authenticate();
				model.sendRosterGet();											// Request contacts
				model.sendPresence(model.Me.Status, model.Me.StatusMessage);	// Send my presence
			} catch (Exception ex) {
				Console.WriteLine("Exception while connecting" + ex.StackTrace);
				return false;
			}
			return true;
        }

		public bool disconnect() {		// Actual disconnect
			try {
				model.disconnect();
			} catch (Exception ex) {
				return false;
			}
			return true;
		}

        // Add a new contact
        private void addContactToolStripMenuItem_Click(object sender, EventArgs e) {
			// Initialize dialog
			AddContact dialog = new AddContact();
			dialog.groupComboBox.DataSource = contactsTreeView.Nodes;
			dialog.groupComboBox.SelectedItem = contactsTreeView.SelectedNode;
			if(dialog.ShowDialog() == DialogResult.OK) {
				model.sendPresence(dialog.jabberIDTextBox.Text.ToLower(), "subscribe", null, null, null);			// Subscribe to contacts presence notifications
				if(dialog.groupComboBox.SelectedItem != null && !dialog.groupComboBox.SelectedItem.ToString().Equals("Default Group"))
					model.sendRosterSet(dialog.jabberIDTextBox.Text.ToLower(), dialog.nameTextBox.Text, dialog.groupComboBox.SelectedItem.ToString());	// Set contacts name and group
				else
					model.sendRosterSet(dialog.jabberIDTextBox.Text.ToLower(), dialog.nameTextBox.Text, (String)null);		// Set contacts name
			}
        }

        // Remove a group
        private void removeGroupToolStripMenuItem_Click(object sender, EventArgs e) {
            if(MessageBox.Show("The group '" + contactsTreeView.SelectedNode.Text 
                + "' will be removed. Proceed?", "Remove Group", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
				if(MessageBox.Show("Do you also want to delete the contacts in the group '" + contactsTreeView.SelectedNode.Text + "'",
					"Remove Contacts", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes) {
					foreach(TreeNode node in contactsTreeView.SelectedNode.Nodes) {
						Contact c = (Contact)node;
						model.sendRosterSet(c.JabberID, c.Name, (String)null);						// Quick fix: first move to default group
						model.sendPresence(c.JabberID, "unsubscribe", null, null, null);			// Unsubscribe to contacts presence notifications
						model.sendPresence(c.JabberID, "unsubscribed", null, null, null);			
					}
				} else {
					foreach(TreeNode node in contactsTreeView.SelectedNode.Nodes) {
						Contact c = (Contact)node;
						model.sendRosterSet(c.JabberID, c.Name, (String)null);	// Move to default group
					}
				}
				contactsTreeView.SelectedNode.Remove();
            }
        }

        // Add a new group (a group is added only to the GUI, if no contact is put in it the group will be lost)
        private void addGroupToolStripMenuItem_Click(object sender, EventArgs e) {
            AddGroup dialog = new AddGroup();
            if(dialog.ShowDialog() == DialogResult.OK) {                
				Group newGroup = new Group(dialog.nameTextBox.Text);
				newGroup.ContextMenuStrip = gropupContextMenuStrip;
				contactsTreeView.Nodes.Add(newGroup);
            }
        }

        private void contactsTreeView_MouseClick(object sender, MouseEventArgs e) {
            // Make sure something is always selected
            contactsTreeView.SelectedNode = contactsTreeView.GetNodeAt(e.X, e.Y);
        }

        // Remove a contact
        private void removeContactToolStripMenuItem_Click(object sender, EventArgs e) {
            if(MessageBox.Show("The contact '" + contactsTreeView.SelectedNode.Text
                + "' will be removed. Proceed?", "Remove Contact", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                if(MessageBox.Show("The contact '" + contactsTreeView.SelectedNode.Text
					+ "' could actually be a cool person. Proceed?", "Really Remove Contact", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
					Contact c = (Contact)contactsTreeView.SelectedNode;
					model.sendPresence(c.JabberID, "unsubscribe", null, null, null);			// Unsubscribe to contacts presence notifications
					model.sendPresence(c.JabberID, "unsubscribed", null, null, null);			
                }
            }
        }

        // Open a conversation window
        private void talkToolStripMenuItem_Click(object sender, EventArgs e) {
			if(contactsTreeView.SelectedNode.Level == 1) {						// If a contact is double clicked			
				Contact contact = (Contact)contactsTreeView.SelectedNode;		// Extract it
				try {
					conversations[contact.JabberID].Show();						// Conversation is open
					conversations[contact.JabberID].Focus();					// Focus on it
				} catch(KeyNotFoundException ex) {
					conversations[contact.JabberID] = new ConverstationWindow(contact, this);	// Make a new conversation
					conversations[contact.JabberID].Show();										// Show it
				} catch(Exception ex) {
					// TODO: Do something here if needed
				} finally {
				}
			}
        }

        // Open a conversation window
        private void contactsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
			if(e.Node.Level == 1) {																			// See if a contact is double clicked
				Contact contact = (Contact)e.Node;
				if(contact.Status == Status.inviteAccepted || contact.Status == Status.inviteSent)			// If contats is not yet confirmed
					return;
				try {
					conversations[contact.JabberID].Show();													// Try to show the conversation window
					conversations[contact.JabberID].Focus();
				} catch (KeyNotFoundException ex) {
					conversations[contact.JabberID] = new ConverstationWindow(contact, this);				// Make a conversation window
					conversations[contact.JabberID].Show();
				} catch(Exception ex) {
					// TODO: Do something here if needed
				} finally {
				}					                
            }
        }

        // Start moving a node
        private void contactsTreeView_ItemDrag(object sender, ItemDragEventArgs e) {
            NodeToBeMoved = (TreeNode)e.Item;
            if(NodeToBeMoved.Level == 1) {
                string strItem = e.Item.ToString();
                DoDragDrop(strItem, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

		// Make a moving effect
        private void contactsTreeView_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
        }

		// Move a contact to a another group
        private void contactsTreeView_DragDrop(object sender, DragEventArgs e) {
			Point position = new Point(e.X, e.Y);							// Get coordinates
            position = contactsTreeView.PointToClient(position);
            TreeNode dropNode = contactsTreeView.GetNodeAt(position);
            if(dropNode.Level == 1) {										// Select parent if dropped on another contact
                dropNode = dropNode.Parent;
            }
			Group group = (Group)dropNode;
			Contact contact = (Contact)NodeToBeMoved;
			model.sendRosterSet(contact.JabberID, contact.Name, group.Name);	// Move to other group     
        }

		// Rename a group
		private void renameGroupToolStripMenuItem_Click(object sender, EventArgs e) {
			Group group = (Group)contactsTreeView.SelectedNode;				
			RenameGroup dialog = new RenameGroup(group.Name);
			String newGroup;
			if(dialog.ShowDialog() == DialogResult.OK) {
				newGroup = dialog.newNameTextBox.Text;
				foreach(TreeNode node in group.Nodes) {
					Contact c = (Contact)node;
					model.sendRosterSet(c.JabberID, c.Name, newGroup);
				}
			}
			group.Remove();
		}
		
		// Show about box
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			AboutBox about = new AboutBox();
			about.ShowDialog();
		}
		
		// Add group from tool strip
		private void addGroupToolStripButton_Click(object sender, EventArgs e) {
			addGroupToolStripMenuItem_Click(sender, e);
		}
		
		// Connect from tool strip
		private void connectToolStripButton_Click(object sender, EventArgs e) {
			connectTrayMenuItem_Click(sender, e);
		}

		// Add contact from menu
		private void addContactToolStripButton_Click(object sender, EventArgs e) {
			addContactToolStripMenuItem_Click(sender, e);
		}

		private void optionsToolStripButton_Click(object sender, EventArgs e) {
			OptionsDialog options = new OptionsDialog();
			options.ShowDialog();
		}

		// Communication methods ----------------------------------------------------
		public void SendMessage(String recipient, String body) {
			Console.WriteLine("Send message: " + recipient + ": " + body);
			model.sendMessage(recipient, null, null, null, null, body);
		}

		// Invokes a method to receive message in a conversation window, delegate is used for cross thread calls
		public void ReceiveMessage(JabberID from, string body) {
			Console.WriteLine("Gui received message from: " + from);
			if(!conversations.ContainsKey(from.User + "@" + from.Domain)) {		// Check if there is already open conversation window
				Contact c;
				if(contacts.ContainsKey(from.User + "@" + from.Domain))			// Check if sender is in Contact list
					c = contacts[from.User + "@" + from.Domain];
				else
					c = new Contact(from.User + "@" + from.Domain);
				conversations[from.User + "@" + from.Domain] = new ConverstationWindow(c, this);	// Open new conversation window
				conversations[from.User + "@" + from.Domain].Show();								// Show if new window
			} else {
				if(!conversations[from.User + "@" + from.Domain].Visible)
					conversations[from.User + "@" + from.Domain].Show();
				conversations[from.User + "@" + from.Domain].Activate();							// Just activate if already opened
			}
			conversations[from.User + "@" + from.Domain].ReceiveMessage(body);						// Receive message
		}

		// Update contact's information
		public void UpdateContact(String jid, String name, String group, Status status) {
			Console.WriteLine("Adding: " + jid + ", " + name+ ", " + group + ", " + status);

			// Find group
			Group groupNode;
			if(group == null)										// If no group is sent use default(0)
				groupNode = (Group)contactsTreeView.Nodes[0];
			else {															// Group is sent
				if(!contactsTreeView.Nodes.ContainsKey(group)) {			// If the group doesn't exists
					Group newGroup = new Group(group);
					newGroup.ContextMenuStrip = gropupContextMenuStrip;
					contactsTreeView.Nodes.Add(newGroup);					// Make one					
				}
				groupNode = (Group)contactsTreeView.Nodes[group];	// we have Group
			}

			// Find contact
			Contact contact;
			if(contacts.ContainsKey(jid)) {					// Such contact exists
				contact = contacts[jid];					// use it
				if(contact.Parent != groupNode) {			// We should change group
					contact.Remove();						// Remove from original
					groupNode.Nodes.Add(contact);			// Add to new
				}
			} else {
				contact = new Contact(jid);			// else make it
				contacts[jid] = contact;			// Put it in the dictionary

				// Set context menu
				if(status != Status.inviteSent && status != Status.inviteAccepted)
					contact.ContextMenuStrip = this.contactsContextMenuStrip;
				else
					contact.ContextMenuStrip = this.pendingContactContextMenuStrip;

				groupNode.Nodes.Add(contact);		// and add it to gui
			}			
			contact.Name = name;
			contact.Status = status;			
		}

		// Update contact's presence information
		public void UpdateContactPresence(String jid, Status status, String statusMessage) {
			try {
				Contact contact = contacts[jid];
				Contact previous = new Contact(contact);
				contact.Status = status;
				contact.StatusMessage = statusMessage;

				// Set context menu accordingly
				if(status != Status.inviteSent && status != Status.inviteAccepted)
					contact.ContextMenuStrip = this.contactsContextMenuStrip;
				else
					contact.ContextMenuStrip = this.pendingContactContextMenuStrip;

				if(conversations.ContainsKey(jid) && previous.Status != contact.Status)		// If a conversation window is opened update it
					conversations[jid].UpdateContactPresence(status, statusMessage);
			} catch (KeyNotFoundException ex) {
				Console.WriteLine("Contact not present " + ex.StackTrace);
			}
		}

		public Contact GetContact(String jid) {
			if(contacts.ContainsKey(jid))
				return contacts[jid];
			else
				return null;
		}

		// Just bring up dialog when a invitation arrives
		public bool AcceptInvitation(String jid) {
			if(MessageBox.Show(
				this,
				jid + " wants to be able to chat with you. ",
				"Accept Invitation",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question) == DialogResult.Yes)
				return true;
			else
				return false;
		}

		// Remove contact from gui and hashtable
		public void RemoveContact(String jid) {
			if(contacts.ContainsKey(jid)) {
				Contact c = contacts[jid];
				c.Remove();
				contacts.Remove(jid);
			}
		}

		// Toolbars visibility
		private void contactsToolStripMenuItem_Click(object sender, EventArgs e) {
			this.contactsToolStripMenuItem.Checked = !this.contactsToolStripMenuItem.Checked;
			this.contactsToolStrip.Visible = this.contactsToolStripMenuItem.Checked;
		}

		private void mainToolStripMenuItem_Click(object sender, EventArgs e) {
			this.mainToolStripMenuItem.Checked = !this.mainToolStripMenuItem.Checked;
			this.mainToolStrip.Visible = this.mainToolStripMenuItem.Checked;
		}

		private void statusToolStripMenuItem_Click(object sender, EventArgs e) {
			this.statusToolStripMenuItem.Checked = !this.statusToolStripMenuItem.Checked;
			this.statusToolStrip.Visible = this.statusToolStripMenuItem.Checked;
		}

		private void goRightToolStripButton_Click(object sender, EventArgs e) {
			this.Location = new Point(Screen.PrimaryScreen.Bounds.Right - this.Size.Width, Screen.PrimaryScreen.Bounds.Top);
		}
		//////////////////////////////////////////////////////////////////////////		

		// Set presence status (show)
		// Set status chat
		private void onlineToolStripMenuItem_Click_1(object sender, EventArgs e) {
			model.Me.Status = Status.chat;
			model.sendPresence(model.Me.Status, model.Me.StatusMessage);
			this.statusToolStripDropDownButton.Image = Properties.Resources.lightbulb;
		}

		// Set status away
		private void awayToolStripMenuItem_Click_1(object sender, EventArgs e) {
			model.Me.Status = Status.away;
			model.sendPresence(model.Me.Status, model.Me.StatusMessage);
			this.statusToolStripDropDownButton.Image = Properties.Resources.lightbulb_off;
		}

		// Set status dnd
		private void busyToolStripMenuItem_Click_1(object sender, EventArgs e) {
			model.Me.Status = Status.dnd;
			model.sendPresence(model.Me.Status, model.Me.StatusMessage);
			this.statusToolStripDropDownButton.Image = Properties.Resources.lightbulb_delete;			
		}

		private void statusMessageToolStripComboBox_Click(object sender, EventArgs e) {
			this.statusMessageToolStripComboBox.SelectAll();
		}

		// Set the status message and keep old ones in history
		private String previousStatusMessage;
		private void statusMessageToolStripComboBox_KeyPress(object sender, KeyPressEventArgs e) {
			if(e.KeyChar == '\r') {
				model.Me.StatusMessage = this.statusMessageToolStripComboBox.Text;
				model.sendPresence(model.Me.Status, model.Me.StatusMessage);
				if(!String.IsNullOrEmpty(previousStatusMessage)) {
					this.statusMessageToolStripComboBox.Items.Add(previousStatusMessage);
				}
				previousStatusMessage = this.statusMessageToolStripComboBox.Text;
			}
		}
		//////////////////////////////////////////////////////////////////////////		

		// Bring the edit contact dialog and set information accordingly
		private void editContactToolStripMenuItem_Click(object sender, EventArgs e) {
			if(contactsTreeView.SelectedNode.Level == 1) {
				Contact contact = (Contact)contactsTreeView.SelectedNode;
				Group group = (Group)contactsTreeView.SelectedNode.Parent;
				EditContact dialog = new EditContact();
				dialog.nameTextBox.Text = contact.Name;
				dialog.jabberIDLabel.Text = contact.JabberID;
				dialog.groupComboBox.DataSource = contactsTreeView.Nodes;
				dialog.groupComboBox.SelectedItem = group;
				if(dialog.ShowDialog() == DialogResult.OK) {
					if(dialog.groupComboBox.SelectedItem != null && !dialog.groupComboBox.SelectedItem.ToString().Equals("Default Group"))
						model.sendRosterSet(contact.JabberID, dialog.nameTextBox.Text, dialog.groupComboBox.SelectedItem.ToString());
					else
						model.sendRosterSet(contact.JabberID, dialog.nameTextBox.Text, (String)null);
				}
			}
		}

		// Open a group chat
		private void groupchatToolStripButton_Click(object sender, EventArgs e) {
			GroupchatDialog dialog = new GroupchatDialog();
			dialog.groupSufixLabel.Text += "@" + model.ServerName;
			if(dialog.ShowDialog() == DialogResult.OK) {
				model.sendPresence(dialog.groupTextBox.Text+".group" + "@" + model.ServerName + @"/" + dialog.nickTextBox.Text, null, null, null, null);
			}
		}

		private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
			if(this.WindowState == FormWindowState.Minimized)
				this.WindowState = FormWindowState.Normal;
			this.ShowInTaskbar = true;			
			this.Show();
			this.Activate();
			this.Invalidate();				// The combobox is behaves funny without this
		}

		// Group chat
		public void UpdateGroupPresence(String groupName, String userNick, Show show, String statusMessage) {
			if(groupChats.ContainsKey(groupName))
				groupChats[groupName].ReceivePresence(userNick, show, statusMessage);
			else {
				groupChats[groupName] = new GroupchatWindow(groupName, userNick, this);
				groupChats[groupName].Show();
			}
		}

		public void ShowGroupError(String code, String description) {
			MessageBox.Show(this, "Error: " + code + "\n" + description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public void RemoveGroupChat(String groupName) {
			if(groupChats.ContainsKey(groupName)) {
				groupChats.Remove(groupName);
			}
		}

		public void RemoveGroupMember(String groupName, String nick) {
			if(groupChats.ContainsKey(groupName))
				groupChats[groupName].RemoveMember(nick);
		}

		public void ReceiveGroupMessage(String groupJID, String userNick, String message) {
			if(groupChats.ContainsKey(groupJID)) {
				groupChats[groupJID].ReceiveMessage(userNick, message);
				groupChats[groupJID].Activate();
			}
		}

		//////////////////////////////////////////////////////////////////////////
		
	}
}