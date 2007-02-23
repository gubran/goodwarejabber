namespace Goodware.Jabber.GUI {
	partial class OptionsDialog {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
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
			this.labelServerName = new System.Windows.Forms.Label();
			this.labelServerAdress = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelUser = new System.Windows.Forms.Label();
			this.labelPass = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.authModeComboBox = new System.Windows.Forms.ComboBox();
			this.groupBoxServer = new System.Windows.Forms.GroupBox();
			this.serverAddressTextBox = new System.Windows.Forms.TextBox();
			this.serverNameTextBox = new System.Windows.Forms.TextBox();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.passwordReenterMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
			this.passwordMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
			this.usernameTextBox = new System.Windows.Forms.TextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.registerCheckBox = new System.Windows.Forms.CheckBox();
			this.passErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.groupBoxServer.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.passErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// labelServerName
			// 
			this.labelServerName.AutoSize = true;
			this.labelServerName.Location = new System.Drawing.Point(8, 22);
			this.labelServerName.Name = "labelServerName";
			this.labelServerName.Size = new System.Drawing.Size(69, 13);
			this.labelServerName.TabIndex = 1;
			this.labelServerName.Text = "Server Name";
			// 
			// labelServerAdress
			// 
			this.labelServerAdress.AutoSize = true;
			this.labelServerAdress.Location = new System.Drawing.Point(7, 52);
			this.labelServerAdress.Name = "labelServerAdress";
			this.labelServerAdress.Size = new System.Drawing.Size(79, 13);
			this.labelServerAdress.TabIndex = 2;
			this.labelServerAdress.Text = "Server Address";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(26, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Port";
			// 
			// labelUser
			// 
			this.labelUser.AutoSize = true;
			this.labelUser.Location = new System.Drawing.Point(8, 24);
			this.labelUser.Name = "labelUser";
			this.labelUser.Size = new System.Drawing.Size(55, 13);
			this.labelUser.TabIndex = 6;
			this.labelUser.Text = "Username";
			// 
			// labelPass
			// 
			this.labelPass.AutoSize = true;
			this.labelPass.Location = new System.Drawing.Point(8, 54);
			this.labelPass.Name = "labelPass";
			this.labelPass.Size = new System.Drawing.Size(53, 13);
			this.labelPass.TabIndex = 8;
			this.labelPass.Text = "Password";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 116);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(105, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Authentication Mode";
			// 
			// authModeComboBox
			// 
			this.authModeComboBox.FormattingEnabled = true;
			this.authModeComboBox.Items.AddRange(new object[] {
            "plain"});
			this.authModeComboBox.Location = new System.Drawing.Point(193, 113);
			this.authModeComboBox.Name = "authModeComboBox";
			this.authModeComboBox.Size = new System.Drawing.Size(68, 21);
			this.authModeComboBox.TabIndex = 7;
			this.authModeComboBox.Text = global::Goodware.Jabber.GUI.Properties.Settings.Default.AuthMode;
			// 
			// groupBoxServer
			// 
			this.groupBoxServer.Controls.Add(this.serverAddressTextBox);
			this.groupBoxServer.Controls.Add(this.serverNameTextBox);
			this.groupBoxServer.Controls.Add(this.labelServerName);
			this.groupBoxServer.Controls.Add(this.labelServerAdress);
			this.groupBoxServer.Controls.Add(this.label2);
			this.groupBoxServer.Controls.Add(this.textBoxPort);
			this.groupBoxServer.Location = new System.Drawing.Point(14, 12);
			this.groupBoxServer.Name = "groupBoxServer";
			this.groupBoxServer.Size = new System.Drawing.Size(289, 109);
			this.groupBoxServer.TabIndex = 14;
			this.groupBoxServer.TabStop = false;
			this.groupBoxServer.Text = "Server";
			// 
			// serverAddressTextBox
			// 
			this.serverAddressTextBox.Location = new System.Drawing.Point(92, 49);
			this.serverAddressTextBox.Name = "serverAddressTextBox";
			this.serverAddressTextBox.Size = new System.Drawing.Size(174, 20);
			this.serverAddressTextBox.TabIndex = 1;
			this.serverAddressTextBox.Text = global::Goodware.Jabber.GUI.Properties.Settings.Default.ServerAddress;
			// 
			// serverNameTextBox
			// 
			this.serverNameTextBox.Location = new System.Drawing.Point(92, 19);
			this.serverNameTextBox.Name = "serverNameTextBox";
			this.serverNameTextBox.Size = new System.Drawing.Size(174, 20);
			this.serverNameTextBox.TabIndex = 0;
			this.serverNameTextBox.Text = global::Goodware.Jabber.GUI.Properties.Settings.Default.ServerName;
			// 
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(92, 79);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(68, 20);
			this.textBoxPort.TabIndex = 2;
			this.textBoxPort.Text = global::Goodware.Jabber.GUI.Properties.Settings.Default.Port;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.passwordReenterMaskedTextBox);
			this.groupBox1.Controls.Add(this.passwordMaskedTextBox);
			this.groupBox1.Controls.Add(this.labelUser);
			this.groupBox1.Controls.Add(this.usernameTextBox);
			this.groupBox1.Controls.Add(this.authModeComboBox);
			this.groupBox1.Controls.Add(this.labelPass);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Location = new System.Drawing.Point(14, 127);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(289, 147);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "User";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 84);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Re-enter";
			// 
			// passwordReenterMaskedTextBox
			// 
			this.passwordReenterMaskedTextBox.Enabled = false;
			this.passwordReenterMaskedTextBox.Location = new System.Drawing.Point(92, 81);
			this.passwordReenterMaskedTextBox.Name = "passwordReenterMaskedTextBox";
			this.passwordReenterMaskedTextBox.PasswordChar = '*';
			this.passwordReenterMaskedTextBox.Size = new System.Drawing.Size(169, 20);
			this.passwordReenterMaskedTextBox.TabIndex = 6;
			this.passwordReenterMaskedTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.passwordReenterMaskedTextBox_Validating);
			// 
			// passwordMaskedTextBox
			// 
			this.passwordMaskedTextBox.Location = new System.Drawing.Point(92, 51);
			this.passwordMaskedTextBox.Name = "passwordMaskedTextBox";
			this.passwordMaskedTextBox.PasswordChar = '*';
			this.passwordMaskedTextBox.Size = new System.Drawing.Size(169, 20);
			this.passwordMaskedTextBox.TabIndex = 4;
			this.passwordMaskedTextBox.Text = global::Goodware.Jabber.GUI.Properties.Settings.Default.Password;
			// 
			// usernameTextBox
			// 
			this.usernameTextBox.Location = new System.Drawing.Point(92, 21);
			this.usernameTextBox.Name = "usernameTextBox";
			this.usernameTextBox.Size = new System.Drawing.Size(169, 20);
			this.usernameTextBox.TabIndex = 3;
			this.usernameTextBox.Text = global::Goodware.Jabber.GUI.Properties.Settings.Default.Username;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(225, 304);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(144, 303);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// registerCheckBox
			// 
			this.registerCheckBox.AutoSize = true;
			this.registerCheckBox.Location = new System.Drawing.Point(13, 281);
			this.registerCheckBox.Name = "registerCheckBox";
			this.registerCheckBox.Size = new System.Drawing.Size(238, 17);
			this.registerCheckBox.TabIndex = 16;
			this.registerCheckBox.Text = "Use this information to regiser a new account";
			this.registerCheckBox.UseVisualStyleBackColor = true;
			this.registerCheckBox.CheckedChanged += new System.EventHandler(this.registerCheckBox_CheckedChanged);
			// 
			// passErrorProvider
			// 
			this.passErrorProvider.ContainerControl = this;
			// 
			// OptionsDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(318, 339);
			this.Controls.Add(this.registerCheckBox);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBoxServer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsDialog";
			this.ShowInTaskbar = false;
			this.Text = "Options";
			this.groupBoxServer.ResumeLayout(false);
			this.groupBoxServer.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.passErrorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelServerName;
		private System.Windows.Forms.Label labelServerAdress;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelUser;
		private System.Windows.Forms.Label labelPass;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBoxServer;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		public System.Windows.Forms.TextBox serverNameTextBox;
		public System.Windows.Forms.TextBox serverAddressTextBox;
		public System.Windows.Forms.TextBox textBoxPort;
		public System.Windows.Forms.TextBox usernameTextBox;
		public System.Windows.Forms.MaskedTextBox passwordMaskedTextBox;
		public System.Windows.Forms.ComboBox authModeComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MaskedTextBox passwordReenterMaskedTextBox;
		private System.Windows.Forms.ErrorProvider passErrorProvider;
		public System.Windows.Forms.CheckBox registerCheckBox;
	}
}