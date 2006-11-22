using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Goodware.Jabber.Client;
//using Goodware.Jabber;

namespace Goodware.Jabber.GUI {
    public partial class JustTalk : Form {
        private bool connected;
        private TreeNode NodeToBeMoved;
		private JabberModel model;

        public JustTalk() {
            InitializeComponent();
			InitializeModel();
            this.connected = false;
			Group def = new Group("Default Group");
			def.ContextMenuStrip = defaultGroupContextMenuStrip;
			contactsTreeView.Nodes.Add(def);
			contactsTreeView.TreeViewNodeSorter = Comparer<IComparable>.Default;	// Sorter to sort the contacts in the groups
        }

		private void InitializeModel() {
			TestThread modelThread = new TestThread();
			model = new JabberModel(modelThread);

			model.ServerName = "localhost";
			model.ServerAddress = "127.0.0.1";
			model.Port = "5222";
			model.User = "misos";

			//Додадено од Милош/Васко
			model.AuthMode = "plain";     // ??
			model.Resource = "home";
			model.Password = "test";
			//Крај додадено

			modelThread.Model = model;
		}

		// Exit
        private void exitTrayMenuItem_Click(object sender, EventArgs e) {
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
					this.connected = false;
				}
			}			
        }

        public bool connect() {
			try {
				model.connect();
			} catch (Exception ex) {
				return false;
			}
			return true;		// Actual connect
        }

		public bool disconnect() {
			return true;	// Actual disconnect
		}

        // Add a new contact
        private void addContactToolStripMenuItem_Click(object sender, EventArgs e) {
			//contactsTreeView.SelectedNode.Nodes.Add(new Contact());
			AddContact dialog = new AddContact();
			dialog.groupComboBox.DataSource = contactsTreeView.Nodes;
			dialog.groupComboBox.SelectedItem = contactsTreeView.SelectedNode;
			if(dialog.ShowDialog() == DialogResult.OK) {
				Contact temp = new Contact();				
				temp.JabberID = dialog.jabberIDTextBox.Text;
				temp.Name = dialog.nameTextBox.Text;
				//temp.Status = Status.offline;
				temp.Status = (Status)((new Random()).Next(1, 4));	// TODO: Delete test line
				temp.ContextMenuStrip = contactsContextMenuStrip;
				//contactsTreeView.SelectedNode.Nodes.Add(temp);
				TreeNode group = ((TreeNode)dialog.groupComboBox.SelectedItem);
				group.Nodes.Add(temp);		
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
                    + "' could actualy be a cool person. Proceed?", "Realy Remove Contact", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    contactsTreeView.SelectedNode.Remove();
                }
            }
        }

        // Open a conversation wiindow
        private void talkToolStripMenuItem_Click(object sender, EventArgs e) {
            ConverstationWindow c = new ConverstationWindow();
            c.Show();
        }

        // Open a conversation wiindow
        private void contactsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if(e.Node.Level == 1) {
                ConverstationWindow c = new ConverstationWindow();
                c.Show();
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

		// Make a moving efect
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
    }
}