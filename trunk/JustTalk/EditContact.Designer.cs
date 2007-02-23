namespace Goodware.Jabber.GUI {
	partial class EditContact {
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
			this.groupLabel = new System.Windows.Forms.Label();
			this.groupComboBox = new System.Windows.Forms.ComboBox();
			this.serverLabel = new System.Windows.Forms.Label();
			this.OkButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.jabberIDLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// groupLabel
			// 
			this.groupLabel.AutoSize = true;
			this.groupLabel.Location = new System.Drawing.Point(3, 69);
			this.groupLabel.Name = "groupLabel";
			this.groupLabel.Size = new System.Drawing.Size(39, 13);
			this.groupLabel.TabIndex = 24;
			this.groupLabel.Text = "Group:";
			// 
			// groupComboBox
			// 
			this.groupComboBox.FormattingEnabled = true;
			this.groupComboBox.Location = new System.Drawing.Point(62, 66);
			this.groupComboBox.Name = "groupComboBox";
			this.groupComboBox.Size = new System.Drawing.Size(136, 21);
			this.groupComboBox.TabIndex = 19;
			// 
			// serverLabel
			// 
			this.serverLabel.AutoSize = true;
			this.serverLabel.Location = new System.Drawing.Point(3, 42);
			this.serverLabel.Name = "serverLabel";
			this.serverLabel.Size = new System.Drawing.Size(56, 13);
			this.serverLabel.TabIndex = 23;
			this.serverLabel.Text = "Jabber ID:";
			// 
			// OkButton
			// 
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.Location = new System.Drawing.Point(39, 100);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 20;
			this.OkButton.Text = "OK";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(123, 100);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 21;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// label
			// 
			this.label.AutoSize = true;
			this.label.Location = new System.Drawing.Point(3, 16);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(38, 13);
			this.label.TabIndex = 22;
			this.label.Text = "Name:";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(62, 13);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(136, 20);
			this.nameTextBox.TabIndex = 17;
			// 
			// jabberIDLabel
			// 
			this.jabberIDLabel.AutoSize = true;
			this.jabberIDLabel.Location = new System.Drawing.Point(63, 42);
			this.jabberIDLabel.Name = "jabberIDLabel";
			this.jabberIDLabel.Size = new System.Drawing.Size(18, 13);
			this.jabberIDLabel.TabIndex = 25;
			this.jabberIDLabel.Text = "ID";
			// 
			// EditContact
			// 
			this.AcceptButton = this.OkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(220, 144);
			this.Controls.Add(this.jabberIDLabel);
			this.Controls.Add(this.groupLabel);
			this.Controls.Add(this.groupComboBox);
			this.Controls.Add(this.serverLabel);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.label);
			this.Controls.Add(this.nameTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "EditContact";
			this.ShowInTaskbar = false;
			this.Text = "Edit Contact";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label groupLabel;
		public System.Windows.Forms.ComboBox groupComboBox;
		private System.Windows.Forms.Label serverLabel;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label;
		public System.Windows.Forms.TextBox nameTextBox;
		public System.Windows.Forms.Label jabberIDLabel;
	}
}