using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Goodware.Jabber.GUI {
	public partial class OptionsDialog : Form {
		public OptionsDialog() {
			InitializeComponent();
			serverNameTextBox.Text = Properties.Settings.Default.ServerName;
			serverAddressTextBox.Text = Properties.Settings.Default.ServerAddress;
			textBoxPort.Text = Properties.Settings.Default.Port;
			usernameTextBox.Text = Properties.Settings.Default.Username;
			passwordMaskedTextBox.Text = Properties.Settings.Default.Password;
			resourceTextBox.Text = Properties.Settings.Default.Resource;
			authModeComboBox.Text = Properties.Settings.Default.AuthMode;
		}

		private void buttonOK_Click(object sender, EventArgs e) {
			Properties.Settings.Default.ServerName = serverNameTextBox.Text;
			Properties.Settings.Default.ServerAddress = serverAddressTextBox.Text;
			Properties.Settings.Default.Port = textBoxPort.Text;
			Properties.Settings.Default.Username = usernameTextBox.Text;
			Properties.Settings.Default.Password = passwordMaskedTextBox.Text;
			Properties.Settings.Default.Resource = resourceTextBox.Text;
			Properties.Settings.Default.AuthMode = authModeComboBox.Text;
			// TODO: Revise this
			// Properties.Settings.Default.Save();
		}
	}
}