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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.labelServerName = new System.Windows.Forms.Label();
			this.labelServerAdress = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.labelUser = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.labelPass = new System.Windows.Forms.Label();
			this.labelResource = new System.Windows.Forms.Label();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.groupBoxServer = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.groupBoxServer.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(92, 19);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(174, 20);
			this.textBox1.TabIndex = 0;
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
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(92, 49);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(174, 20);
			this.textBox2.TabIndex = 3;
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
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(92, 79);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(68, 20);
			this.textBoxPort.TabIndex = 5;
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
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(92, 21);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(169, 20);
			this.textBox4.TabIndex = 7;
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
			// labelResource
			// 
			this.labelResource.AutoSize = true;
			this.labelResource.Location = new System.Drawing.Point(8, 84);
			this.labelResource.Name = "labelResource";
			this.labelResource.Size = new System.Drawing.Size(53, 13);
			this.labelResource.TabIndex = 10;
			this.labelResource.Text = "Resource";
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(92, 81);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(169, 20);
			this.textBox6.TabIndex = 11;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 114);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(105, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Authentication Mode";
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "plain"});
			this.comboBox1.Location = new System.Drawing.Point(193, 111);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(68, 21);
			this.comboBox1.TabIndex = 13;
			// 
			// groupBoxServer
			// 
			this.groupBoxServer.Controls.Add(this.textBox2);
			this.groupBoxServer.Controls.Add(this.textBox1);
			this.groupBoxServer.Controls.Add(this.labelServerName);
			this.groupBoxServer.Controls.Add(this.labelServerAdress);
			this.groupBoxServer.Controls.Add(this.label2);
			this.groupBoxServer.Controls.Add(this.textBoxPort);
			this.groupBoxServer.Location = new System.Drawing.Point(14, 12);
			this.groupBoxServer.Name = "groupBoxServer";
			this.groupBoxServer.Size = new System.Drawing.Size(277, 109);
			this.groupBoxServer.TabIndex = 14;
			this.groupBoxServer.TabStop = false;
			this.groupBoxServer.Text = "Server";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.maskedTextBox1);
			this.groupBox1.Controls.Add(this.labelUser);
			this.groupBox1.Controls.Add(this.textBox4);
			this.groupBox1.Controls.Add(this.comboBox1);
			this.groupBox1.Controls.Add(this.labelPass);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textBox6);
			this.groupBox1.Controls.Add(this.labelResource);
			this.groupBox1.Location = new System.Drawing.Point(14, 127);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(277, 147);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "User";
			// 
			// maskedTextBox1
			// 
			this.maskedTextBox1.Location = new System.Drawing.Point(92, 51);
			this.maskedTextBox1.Name = "maskedTextBox1";
			this.maskedTextBox1.Size = new System.Drawing.Size(169, 20);
			this.maskedTextBox1.TabIndex = 14;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(216, 287);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 16;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(135, 286);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 17;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			// 
			// OptionsDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(309, 322);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBoxServer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsDialog";
			this.Text = "Options";
			this.groupBoxServer.ResumeLayout(false);
			this.groupBoxServer.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label labelServerName;
		private System.Windows.Forms.Label labelServerAdress;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.Label labelUser;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label labelPass;
		private System.Windows.Forms.Label labelResource;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.GroupBox groupBoxServer;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.MaskedTextBox maskedTextBox1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
	}
}