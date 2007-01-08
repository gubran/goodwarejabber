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
		String username;
		JustTalk mainWindow;

        public ConverstationWindow(Contact c, JustTalk mainWind) {
            InitializeComponent();
			this.contact = c;
			mainWindow = mainWind;
			this.Text = contact.Name + " - JustTalk";
			username = Properties.Settings.Default.Username;
        }

		private bool modified = false;
		private void inputTextBox_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				//if (!inputTextBox.Text.EndsWith("\n")) {
				SendMessage(inputTextBox.Text);
				dialogView.AppendText(username + ": " + inputTextBox.Text/*.Substring(1, inputTextBox.Text.Length - 1)*/ + "\n");
				dialogView.ScrollToCaret();
				//Console.WriteLine();
				//}
				inputTextBox.Clear();
				modified = true;
			}
			//e.Handled = true;
		}

		private void SendMessage(String body) {
			mainWindow.SendMessage(contact.JabberID, body);
		}

		internal void ReceiveMessage(string body) {
			dialogView.AppendText(contact.Name + ": " + body + "\n");
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
	}
}