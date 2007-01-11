using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Goodware.Jabber.Client;
using Goodware.Jabber.Library;

delegate void del(String text);	// A delegate for status

namespace Goodware.Jabber.GUI {
    public partial class JustTalk : Form {
        private bool connected;
        private TreeNode NodeToBeMoved;
		private JabberModel model;
		private Dictionary<string, ConverstationWindow> conversations;
		private Dictionary<string, Contact> contacts;
		private Dictionary<string, Group> groups;

        public JustTalk() {
            InitializeComponent();

			// Load Toolbars state
			ToolStripManager.LoadSettings(this);			

			model = new JabberModel(this);	// TODO: Revise this
            this.connected = false;	
        }

		private void JustTalk_Shown(object sender, EventArgs e) {
			this.mainToolStripMenuItem.Checked = this.mainToolStrip.Visible;
			this.statusToolStripMenuItem.Checked = this.statusToolStrip.Visible;
			this.contactsToolStripMenuItem.Checked = this.contactsToolStrip.Visible;
		}

		// Exit
        private void exitTrayMenuItem_Click(object sender, EventArgs e) {			
			this.disconnect();
            this.Dispose(true);
        }
		
		// Connect switch
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
			foreach(ToolStripItem item in this.contactsToolStrip.Items) {
				item.Enabled = true;
			}
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

			// Initialize the structures needed
			Group def = new Group("Default Group");
			def.ContextMenuStrip = defaultGroupContextMenuStrip;
			contactsTreeView.Nodes.Add(def);
			contactsTreeView.TreeViewNodeSorter = Comparer<IComparable>.Default;	// Sorter to sort the contacts in the groups
			conversations = new Dictionary<string, ConverstationWindow>();
			contacts = new Dictionary<string, Contact>();
			groups = new Dictionary<string, Group>();

			this.connected = true;
		}


		private void disconnectGUI() {
			foreach(ToolStripItem item in this.contactsToolStrip.Items) {
				item.Enabled = false;
			}
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
			groups = null;

			this.connected = false;
		}

		
		public bool connect() {			// Actual connect
			try {				
				model.ServerName = Properties.Settings.Default.ServerName;
				model.ServerAddress = Properties.Settings.Default.ServerAddress;
				model.Port = Properties.Settings.Default.Port;
				model.User = Properties.Settings.Default.Username;
				model.AuthMode = Properties.Settings.Default.AuthMode;	//Додадено од Милош/Васко
				model.Resource = Properties.Settings.Default.Resource;
				model.Password = Properties.Settings.Default.Password;
				model.Me = new Contact(model.User + "@" + model.ServerName);
				model.Me.Status = Status.chat;
				model.connect();
				model.authenticate();
				model.sendRosterGet();
				model.sendPresence(model.Me.Status, model.Me.StatusMessage);
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
			AddContact dialog = new AddContact();
			dialog.groupComboBox.DataSource = contactsTreeView.Nodes;
			dialog.groupComboBox.SelectedItem = contactsTreeView.SelectedNode;
			if(dialog.ShowDialog() == DialogResult.OK) {				
				model.sendPresence(dialog.jabberIDTextBox.Text.ToLower(), "subscribe", null, null, null);
				if(dialog.groupComboBox.SelectedItem != null && !dialog.groupComboBox.SelectedItem.ToString().Equals("Default Group"))
					model.sendRosterSet(dialog.jabberIDTextBox.Text.ToLower(), dialog.nameTextBox.Text, dialog.groupComboBox.SelectedItem.ToString());
				else
					model.sendRosterSet(dialog.jabberIDTextBox.Text.ToLower(), dialog.nameTextBox.Text, (String)null);
			}
/*			AddContact dialog = new AddContact();
			dialog.groupComboBox.DataSource = contactsTreeView.Nodes;
			dialog.groupComboBox.SelectedItem = contactsTreeView.SelectedNode;
			if(dialog.ShowDialog() == DialogResult.OK) {
				Contact temp = new Contact();				
				temp.JabberID = dialog.jabberIDTextBox.Text;
				temp.Name = dialog.nameTextBox.Text;
				temp.Status = (Status)((new Random()).Next(1, 4));	// TODO: Delete test line
				temp.ContextMenuStrip = contactsContextMenuStrip;
				contacts[temp.JabberID] = temp;
				TreeNode group = ((TreeNode)dialog.groupComboBox.SelectedItem);
				group.Nodes.Add(temp);
				contactsTreeView.Sort();
			}*/
        }

        // Remove a group
        private void removeGroupToolStripMenuItem_Click(object sender, EventArgs e) {
            if(MessageBox.Show("The group '" + contactsTreeView.SelectedNode.Text 
                + "' will be removed. Proceed?", "Remove Group", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                contactsTreeView.SelectedNode.Remove();
            }
        }

        // Add a new group
        private void addGroupToolStripMenuItem_Click(object sender, EventArgs e) {
            AddGroup dialog = new AddGroup();
            if(dialog.ShowDialog() == DialogResult.OK) {                
				Group newGroup = new Group(dialog.nameTextBox.Text);
				groups[dialog.nameTextBox.Text] = newGroup;
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
                + "' will be removed. Proceed?", "Remove Contact", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                if(MessageBox.Show("The contact '" + contactsTreeView.SelectedNode.Text
                    + "' could actually be a cool person. Proceed?", "Really Remove Contact", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    //contactsTreeView.SelectedNode.Remove();
                }
            }
        }

        // Open a conversation window
        private void talkToolStripMenuItem_Click(object sender, EventArgs e) {
            if(contactsTreeView.SelectedNode.Level == 1) {
				Contact contact = (Contact)contactsTreeView.SelectedNode;
				try {
					conversations[contact.JabberID].Show();
					conversations[contact.JabberID].Focus();
				} catch(KeyNotFoundException ex) {
					conversations[contact.JabberID] = new ConverstationWindow(contact, this);
					conversations[contact.JabberID].Show();
				} catch(Exception ex) {
					// TODO: Do something here if needed
				} finally {
				}
			}
        }

        // Open a conversation window
        private void contactsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if(e.Node.Level == 1) {
				Contact contact = (Contact)e.Node;
				if(contact.Status == Status.inviteAccepted || contact.Status == Status.inviteSent)
					return;
				try {
					conversations[contact.JabberID].Show();
					conversations[contact.JabberID].Focus();
				} catch (KeyNotFoundException ex) {
					conversations[contact.JabberID] = new ConverstationWindow(contact, this);
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
 /*          Point position = new Point(e.X, e.Y);
            position = contactsTreeView.PointToClient(position);
            TreeNode dropNode = contactsTreeView.GetNodeAt(position);
            if(dropNode.Level == 1) {
                dropNode = dropNode.Parent;
            }
            contactsTreeView.Nodes.Remove(NodeToBeMoved);
            dropNode.Nodes.Add(NodeToBeMoved);*/
        }

		// Rename a group
		private void renameGroupToolStripMenuItem_Click(object sender, EventArgs e) {
/*			TreeNode node = contactsTreeView.SelectedNode;
			RenameGroup dialog = new RenameGroup(node.Text);
			if(dialog.ShowDialog() == DialogResult.OK) {
				node.Text = dialog.newNameTextBox.Text;
			}*/
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

		// Set status chat
		private void onlineToolStripMenuItem_Click(object sender, EventArgs e) {
			statusToolStripDropDownButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb;
		}

		// Set status away
		private void awayToolStripMenuItem_Click(object sender, EventArgs e) {
			statusToolStripDropDownButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb_off;
		}

		// Set status dnd
		private void busyToolStripMenuItem_Click(object sender, EventArgs e) {
			statusToolStripDropDownButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb_delete;
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
			//del rcvm = conversations[from.User + "@" + from.Domain].ReceiveMessage;
			//conversations[from.User + "@" + from.Domain].Invoke(rcvm, new Object[] { body });*/
		}

		public void UpdateContact(String jid, String name, String group, Status status) {
			Console.WriteLine("Adding: " + jid + ", " + name+ ", " + group + ", " + status);

			// Find group
			Group groupNode;
			if(group == null)										// If no group is sent use default(0)
				groupNode = (Group)contactsTreeView.Nodes[0];
			else {													// Group is sent
				if(!contactsTreeView.Nodes.ContainsKey(group))		// If the group doesn't exists
					contactsTreeView.Nodes.Add(new Group(group));	// Make one
				groupNode = (Group)contactsTreeView.Nodes[group];	// we have Group
			}

			// Find contact
			Contact contact;
			if(contacts.ContainsKey(jid)) {					// Such contact exists
				contact = contacts[jid];					// use it
				if(contact.Parent != groupNode) {			// We should change group
					//TreeNode parent = contact.Parent;
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

		public void UpdateContactPresence(String jid, Status status, String statusMessage) {
			try {
				Contact contact = contacts[jid];
				contact.Status = status;

				// Set context menu
				if(status != Status.inviteSent && status != Status.inviteAccepted)
					contact.ContextMenuStrip = this.contactsContextMenuStrip;
				else
					contact.ContextMenuStrip = this.pendingContactContextMenuStrip;

				contact.StatusMessage = statusMessage;
				if(conversations.ContainsKey(jid))
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

		private void groupchatToolStripButton_Click(object sender, EventArgs e) {
			GroupchatDialog dialog = new GroupchatDialog();
			if(dialog.ShowDialog() == DialogResult.OK) {
				GroupchatWindow groupWindow = new GroupchatWindow();
				groupWindow.Show();
			}
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

		private void JustTalk_Load(object sender, EventArgs e) {
			// Load size from settings
			if(Properties.Settings.Default.WindowSize != null) {
				this.Size = Properties.Settings.Default.WindowSize;
			}
		}

		private void JustTalk_FormClosing(object sender, FormClosingEventArgs e) {			

			// Save size
			if(this.WindowState == FormWindowState.Normal) {
				Properties.Settings.Default.WindowSize = this.Size;
			} else {
				Properties.Settings.Default.WindowSize = this.RestoreBounds.Size;
			}

			Properties.Settings.Default.Save();
			ToolStripManager.SaveSettings(this);
			this.disconnect();
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
		//--------------------

		// Set presence status (show)
		private void onlineToolStripMenuItem_Click_1(object sender, EventArgs e) {
			model.Me.Status = Status.chat;
			model.sendPresence(model.Me.Status, model.Me.StatusMessage);
			this.statusToolStripDropDownButton.Image = Properties.Resources.lightbulb;
			Properties.Settings.Default.Save();
		}

		private void awayToolStripMenuItem_Click_1(object sender, EventArgs e) {
			model.Me.Status = Status.away;
			model.sendPresence(model.Me.Status, model.Me.StatusMessage);
			this.statusToolStripDropDownButton.Image = Properties.Resources.lightbulb_off;
			Properties.Settings.Default.Save();
		}

		private void busyToolStripMenuItem_Click_1(object sender, EventArgs e) {
			model.Me.Status = Status.dnd;
			model.sendPresence(model.Me.Status, model.Me.StatusMessage);
			this.statusToolStripDropDownButton.Image = Properties.Resources.lightbulb_delete;			
			Properties.Settings.Default.Save();
		}

		private void statusMessageToolStripComboBox_Click(object sender, EventArgs e) {
			this.statusMessageToolStripComboBox.SelectAll();
		}

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
		//----------------------

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
	}
}