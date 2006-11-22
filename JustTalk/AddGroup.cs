using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Goodware.Jabber.GUI {
    public partial class AddGroup : Form {
        public AddGroup() {
            InitializeComponent();
        }

        private void nameTextBox_Validating(object sender, CancelEventArgs e) {
            if(String.IsNullOrEmpty(nameTextBox.Text)) {
                inputErrorProvider.SetError(nameTextBox, "Group name can not be an empty!");
                OkButton.DialogResult = DialogResult.None;
                //e.Cancel = true;
            } else {
                OkButton.DialogResult = DialogResult.OK;
            }
        }
    }
}