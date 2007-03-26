using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Goodware.Jabber.GUI {
	public partial class GroupchatDialog : Form {
		public GroupchatDialog() {
			InitializeComponent();
		}

		private void nickTextBox_Validating(object sender, CancelEventArgs e) {
			this.ValidateFrom();
		}

		private void groupTextBox_Validating(object sender, CancelEventArgs e) {
			this.ValidateFrom();
		}

		private void ValidateFrom() {
			if(String.IsNullOrEmpty(this.groupTextBox.Text)) {
				nickErrorProvider.SetError(this.groupSufixLabel, "You must enter a group!");
				this.joinCreateButton.DialogResult = DialogResult.None;
			} else if(String.IsNullOrEmpty(this.nickTextBox.Text)) {
				nickErrorProvider.SetError(nickTextBox, "You must enter a Nick!");
				this.joinCreateButton.DialogResult = DialogResult.None;
			} else {
				this.joinCreateButton.DialogResult = DialogResult.OK;
				nickErrorProvider.Clear();
			}
		}
	}
}