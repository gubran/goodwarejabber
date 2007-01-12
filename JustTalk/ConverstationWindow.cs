using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Goodware.Jabber.GUI {
    public partial class ConverstationWindow : Form {
		// private StreamWriter writer;
		private Contact contact;
		private String username;
		private JustTalk mainWindow;
		private String[] messages = new String[] {
			"",
			"- User does not want to be disturbed -",
			"- User is offline and will receive messages upon logon -",
			"- Users is away and may not read messages at the moment -",			
		};

        public ConverstationWindow(Contact c, JustTalk mainWind) {
            InitializeComponent();
			this.contact = c;
			mainWindow = mainWind;
			this.Text = contact.Name + " - JustTalk";
			username = Properties.Settings.Default.Username;

			if(!contact.StatusMessage.Equals("Chat"))
				this.ReceiveMessage(contact.StatusMessage);
			if(contact.Status != Status.chat)
				this.ReceiveMessage(messages[(int)contact.Status - 1]);			
        }

		private bool modified = false;
		private void inputTextBox_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				if(!inputTextBox.Text.EndsWith("\n")) {
					SendMessage(inputTextBox.Text);
					dialogView.AppendText(username + ": " + inputTextBox.Text/*.Substring(1, inputTextBox.Text.Length - 1)*/ + "\n");
					dialogView.ScrollToCaret();
				}
				inputTextBox.Clear();
				modified = true;
			}
			//e.Handled = true;
		}

		private void SendMessage(String body) {
			mainWindow.SendMessage(contact.JabberID, body);
		}

		public void ReceiveMessage(string body) {
			dialogView.AppendText(contact.Name + ": " + body + "\n");
		}

		public void UpdateContactPresence(Status status, String message) {
			contact.Status = status;
			contact.StatusMessage = message;

			if(contact.Status != Status.chat) {
				int i = (int)contact.Status;
				this.ReceiveMessage(messages[i-1]);
			}
		}

		private void inputTextBox_KeyPress(object sender, KeyPressEventArgs e) {
			/*if(e.KeyChar == (char)(byte)13) {
				inputTextBox.Clear();
			}*/
		}

		private void inputTextBox_TextChanged(object sender, EventArgs e) {
			/*if(modified) {
				inputTextBox.Clear();
				modified = false;
			}*/
		}

		protected override void OnClosing(CancelEventArgs e) {
			this.Hide();
			e.Cancel = true;
			base.OnClosing(e);
		}
	}
}