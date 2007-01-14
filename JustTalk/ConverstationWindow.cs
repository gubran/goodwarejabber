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
		private StringBuilder stringBuilder;
		private String[] messages = new String[] {
			"User is now available",
			"User does not want to be disturbed",
			"User is offline and will receive messages upon logon",
			"Users is away and may not read messages at the moment",			
		};

        public ConverstationWindow(Contact c, JustTalk mainWind) {
            InitializeComponent();
			this.contact = c;
			mainWindow = mainWind;
			this.Text = contact.Name + " - JustTalk";
			username = Properties.Settings.Default.Username;
			// Initialize font, colors and size
			stringBuilder = new StringBuilder(@"{\rtf1\ansi{\fonttbl\f0\fswiss Helvetica;}{\colortbl ;\red75\green100\blue165;\red155\green155\blue155;}\fs16");

			if(!contact.StatusMessage.Equals("Chat"))
				this.ReceiveMessage(contact.Name, contact.StatusMessage, 2);
			if(contact.Status != Status.chat)
				this.ReceiveMessage(contact.Name, messages[(int)contact.Status - 1], 2);
        }

		private bool modified = false;
		private void inputTextBox_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				if(!inputTextBox.Text.EndsWith("\n")) {
					SendMessage(inputTextBox.Text);
					ReceiveMessage(username, inputTextBox.Text, 0);
					/*dialogView.AppendText(username + ": " + inputTextBox.Text/*.Substring(1, inputTextBox.Text.Length - 1) + "\n");
					dialogView.ScrollToCaret();*/
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
			ReceiveMessage(contact.Name, body, 0);
			/*stringBuilder.Append(@"{\b\cf1" + contact.Name + ": }" + body + @"\par");
			dialogView.Rtf = stringBuilder.ToString() + "}";
			//dialogView.AppendText(contact.Name + ": " + body + "\n");
			dialogView.ScrollToCaret();*/
		}

		public void ReceiveMessage(String from, String body, int color) {
			body = body.Replace('\\', ' ');
			body = body.Replace(":)", @"{\b\cf1:)}");
			body = body.Replace(";)", @"{\b\cf1;)}");
			body = body.Replace(":D", @"{\b\cf1:D}");
			body = body.Replace(":(", @"{\b\cf1:(}");
			body = body.Replace("8)", @"{\b\cf18)}");
			body = body.Replace(":P", @"{\b\cf1:P}");
			if(color != 0) {
				stringBuilder.Append(@"{\cf" + color + @"{\b " + from + @": }" + body + @"}\par");
			} else {
				stringBuilder.Append(@"{\b\cf1 " + from + ": }" + body + @"\par");
			}
			dialogView.Rtf = stringBuilder.ToString() + "}";
			dialogView.Select(dialogView.TextLength, 0);
			dialogView.ScrollToCaret();
		}

		public void UpdateContactPresence(Status status, String message) {
			contact.Status = status;
			contact.StatusMessage = message;

	/*		if(contact.Status != Status.chat) {
				int i = (int)contact.Status;
				this.ReceiveMessage(messages[i-1]);
			}*/
			this.ReceiveMessage(contact.Name, messages[(int)contact.Status - 1], 2);
			//this.ReceiveMessage(messages[(int)contact.Status - 1]);
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