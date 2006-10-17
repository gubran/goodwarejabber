using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JustTalk {
    public partial class AddContact : Form {
		public AddContact() {
            InitializeComponent();
        }

        private void nameTextBox_Validating(object sender, CancelEventArgs e) {
			jabberIDTextBox_Validating(sender, e);		
        }

		// Validate if jabberID has been entered
		private void jabberIDTextBox_Validating(object sender, CancelEventArgs e) {
			if(String.IsNullOrEmpty(jabberIDTextBox.Text)) {
				inputErrorProvider.SetError(jabberIDTextBox, "Jabber ID can not be an empty!");
				OkButton.DialogResult = DialogResult.None;
			} else {
				OkButton.DialogResult = DialogResult.OK;
			}
		}
    }
}