using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Goodware.Jabber.GUI {
    public partial class RenameGroup : Form {
        public RenameGroup(String oldName) {
            InitializeComponent();
			nameTextBox.Text = oldName;
        }

		private void newNameTextBox_Validating(object sender, CancelEventArgs e) {
			if(String.IsNullOrEmpty(newNameTextBox.Text)) {
				inputErrorProvider.SetError(newNameTextBox, "Group name can not be an empty!");
				OkButton.DialogResult = DialogResult.None;
				//e.Cancel = true;
			} else {
				OkButton.DialogResult = DialogResult.OK;
			}
		}
    }
}