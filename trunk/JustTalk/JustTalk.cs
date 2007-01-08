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
			//InitializeModel();
            this.connected = false;
			Group def = new Group("Default Group");
			def.ContextMenuStrip = defaultGroupContextMenuStrip;
			contactsTreeView.Nodes.Add(def);
			contactsTreeView.TreeViewNodeSorter = Comparer<IComparable>.Default;	// Sorter to sort the contacts in the groups
			conversations = new Dictionary<string, ConverstationWindow>();
			contacts = new Dictionary<string, Contact>();
			model = new JabberModel(this);		// TODO: Revise this
        }

		private void InitializeModel() {			
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
					this.connectTrayMenuItem.Text = "Disconnect";
					this.connectTrayMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.disconnect;
					this.connectToolStripButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.disconnect;
					this.connectToolStripButton.ToolTipText = "Disconnect";
					this.connectedStatus.Text = "Connected";
					this.connectedStatus.Image = global::Goodware.Jabber.GUI.Properties.Resources.connect;
					this.trayIcon.Icon = global::Goodware.Jabber.GUI.Properties.Resources.lightbulbico;
					this.optionsToolStripButton.Enabled = false;
					this.Text = model.User + "@" + model.ServerName + " - JustTalk";
					this.connected = true;					
				} else {
					MessageBox.Show("Unable to connect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
            } else {
				if(this.disconnect()) {
					this.connectTrayMenuItem.Text = "Connect";
					this.connectTrayMenuItem.Image = global::Goodware.Jabber.GUI.Properties.Resources.connect;
					this.connectToolStripButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.connect;
					this.connectToolStripButton.ToolTipText = "Connect";
					this.connectedStatus.Text = "Disconnected";
					this.connectedStatus.Image = global::Goodware.Jabber.GUI.Properties.Resources.disconnect;
					this.trayIcon.Icon = global::Goodware.Jabber.GUI.Properties.Resources.lightbulboff;
					this.optionsToolStripButton.Enabled = true;
					this.Text = "JustTalk";
					this.connected = false;
				}
			}			
        }

        public bool connect() {
			try {				
				model.ServerName = Properties.Settings.Default.ServerName;
				model.ServerAddress = Properties.Settings.Default.ServerAddress;
				model.Port = Properties.Settings.Default.Port;
				model.User = Properties.Settings.Default.Username;
				model.AuthMode = Properties.Settings.Default.AuthMode;	//Додадено од Милош/Васко
				model.Resource = Properties.Settings.Default.Resource;
				model.Password = Properties.Settings.Default.Password;
				model.connect();
				model.authenticate();
			} catch (Exception ex) {
				return false;
			}
			return true;		// Actual connect
        }

		public bool disconnect() {
			try {
				model.disconnect();
			} catch (Exception ex) {
				return false;
			}
			return true;	// Actual disconnect
		}

        // Add a new contact
        private void addContactToolStripMenuItem_Click(object sender, EventArgs e) {
			/*AddContact dialog = new AddContact();
			if(dialog.ShowDialog() == DialogResult.OK) {
				//dialog.nameTextBox.Text;		
				//dialog.groupComboBox.SelectedItem;
				//model.sendPresence(dialog.jabberIDTextBox.Text, )
			}*/
			AddContact dialog = new AddContact();
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
			}
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
                //TreeNode temp = new TreeNode(dialog.nameTextBox.Text);
				Group temp = new Group(dialog.nameTextBox.Text);
                temp.ContextMenuStrip = gropupContextMenuStrip;
                contactsTreeView.Nodes.Add(temp);
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
                    + "' could actually be a cool person. Proceed?", "Realy Remove Contact", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    contactsTreeView.SelectedNode.Remove();
                }
            }
        }

        // Open a conversation window
        private void talkToolStripMenuItem_Click(object sender, EventArgs e) {
            //ConverstationWindow c = new ConverstationWindow();
            //c.Show();
        }

        // Open a conversation window
        private void contactsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if(e.Node.Level == 1) {
				Contact contact = (Contact)e.Node;
				try {
					conversations[contact.JabberID].Focus();
				} catch (KeyNotFoundException ex) {
					conversations[contact.JabberID] = new ConverstationWindow(contact, this);
					//Console.WriteLine("Hash val: " + contact.JabberID);
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
            Point position = new Point(e.X, e.Y);
            position = contactsTreeView.PointToClient(position);
            TreeNode dropNode = contactsTreeView.GetNodeAt(position);
            if(dropNode.Level == 1) {
                dropNode = dropNode.Parent;
            }
            contactsTreeView.Nodes.Remove(NodeToBeMoved);
            dropNode.Nodes.Add(NodeToBeMoved);
        }

		// Rename a group
		private void renameGroupToolStripMenuItem_Click(object sender, EventArgs e) {
			TreeNode node = contactsTreeView.SelectedNode;
			RenameGroup dialog = new RenameGroup(node.Text);
			if(dialog.ShowDialog() == DialogResult.OK) {
				node.Text = dialog.newNameTextBox.Text;
			}
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

		// Set status online
		private void onlineToolStripMenuItem_Click(object sender, EventArgs e) {
			statusToolStripDropDownButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb;
		}

		// Set status away
		private void awayToolStripMenuItem_Click(object sender, EventArgs e) {
			statusToolStripDropDownButton.Image = global::Goodware.Jabber.GUI.Properties.Resources.lightbulb_off;
		}

		// Set status busy
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
			if(!conversations.ContainsKey(from.User + "@" + from.Domain)) {
				Contact c;
				if(contacts.ContainsKey(from.User + "@" + from.Domain))
					c = contacts[from.User + "@" + from.Domain];
				else
					c = new Contact(from.User + "@" + from.Domain);
				conversations[from.User + "@" + from.Domain] = new ConverstationWindow(c, this);
				conversations[from.User + "@" + from.Domain].Show();
			} else {
				conversations[from.User + "@" + from.Domain].Activate();
			}
			conversations[from.User + "@" + from.Domain].ReceiveMessage(body);
			//del rcvm = conversations[from.User + "@" + from.Domain].ReceiveMessage;
			//conversations[from.User + "@" + from.Domain].Invoke(rcvm, new Object[] { body });*/
		}



		private void groupchatToolStripButton_Click(object sender, EventArgs e) {
			GroupchatDialog dialog = new GroupchatDialog();
			if(dialog.ShowDialog() == DialogResult.OK) {
				GroupchatWindow groupWindow = new GroupchatWindow();
				groupWindow.Show();
			}
		}

		private void JustTalk_FormClosing(object sender, FormClosingEventArgs e) {
			this.disconnect();
		}
	}
}