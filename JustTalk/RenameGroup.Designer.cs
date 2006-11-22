namespace Goodware.Jabber.GUI {
    partial class RenameGroup {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.OkButton = new System.Windows.Forms.Button();
			this.inputErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.newNameLabel = new System.Windows.Forms.Label();
			this.newNameTextBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.inputErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(157, 59);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 11;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// label
			// 
			this.label.AutoSize = true;
			this.label.Location = new System.Drawing.Point(1, 9);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(72, 13);
			this.label.TabIndex = 7;
			this.label.Text = "Current Name";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Enabled = false;
			this.nameTextBox.Location = new System.Drawing.Point(79, 6);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(153, 20);
			this.nameTextBox.TabIndex = 6;
			// 
			// OkButton
			// 
			this.OkButton.Location = new System.Drawing.Point(73, 59);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 12;
			this.OkButton.Text = "OK";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// inputErrorProvider
			// 
			this.inputErrorProvider.ContainerControl = this;
			// 
			// newNameLabel
			// 
			this.newNameLabel.AutoSize = true;
			this.newNameLabel.Location = new System.Drawing.Point(1, 40);
			this.newNameLabel.Name = "newNameLabel";
			this.newNameLabel.Size = new System.Drawing.Size(60, 13);
			this.newNameLabel.TabIndex = 13;
			this.newNameLabel.Text = "New Name";
			// 
			// newNameTextBox
			// 
			this.newNameTextBox.Location = new System.Drawing.Point(79, 33);
			this.newNameTextBox.Name = "newNameTextBox";
			this.newNameTextBox.Size = new System.Drawing.Size(153, 20);
			this.newNameTextBox.TabIndex = 14;
			this.newNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.newNameTextBox_Validating);
			// 
			// RenameGroup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(254, 91);
			this.Controls.Add(this.newNameTextBox);
			this.Controls.Add(this.newNameLabel);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.label);
			this.Controls.Add(this.nameTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "RenameGroup";
			this.ShowInTaskbar = false;
			this.Text = "Rename Group";
			((System.ComponentModel.ISupportInitialize)(this.inputErrorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button OkButton;
        public System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ErrorProvider inputErrorProvider;
		private System.Windows.Forms.Label newNameLabel;
		public System.Windows.Forms.TextBox newNameTextBox;
    }
}