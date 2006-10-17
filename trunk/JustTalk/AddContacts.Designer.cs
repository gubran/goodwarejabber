namespace JustTalk {
    partial class AddContact {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddContact));
			this.cancelButton = new System.Windows.Forms.Button();
			this.label = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.OkButton = new System.Windows.Forms.Button();
			this.inputErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.serverLabel = new System.Windows.Forms.Label();
			this.jabberIDTextBox = new System.Windows.Forms.TextBox();
			this.groupComboBox = new System.Windows.Forms.ComboBox();
			this.groupLabel = new System.Windows.Forms.Label();
			this.imageComboBox1 = new ImageComboBox();
			this.iconsImageList = new System.Windows.Forms.ImageList(this.components);
			this.iconLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.inputErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(121, 120);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 11;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// label
			// 
			this.label.AutoSize = true;
			this.label.Location = new System.Drawing.Point(1, 15);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(35, 13);
			this.label.TabIndex = 7;
			this.label.Text = "Name";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(60, 12);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(136, 20);
			this.nameTextBox.TabIndex = 6;
			this.nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nameTextBox_Validating);
			// 
			// OkButton
			// 
			this.OkButton.Location = new System.Drawing.Point(37, 120);
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
			// serverLabel
			// 
			this.serverLabel.AutoSize = true;
			this.serverLabel.Location = new System.Drawing.Point(1, 41);
			this.serverLabel.Name = "serverLabel";
			this.serverLabel.Size = new System.Drawing.Size(53, 13);
			this.serverLabel.TabIndex = 13;
			this.serverLabel.Text = "Jabber ID";
			// 
			// jabberIDTextBox
			// 
			this.jabberIDTextBox.Location = new System.Drawing.Point(60, 38);
			this.jabberIDTextBox.Name = "jabberIDTextBox";
			this.jabberIDTextBox.Size = new System.Drawing.Size(136, 20);
			this.jabberIDTextBox.TabIndex = 14;
			this.jabberIDTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.jabberIDTextBox_Validating);
			// 
			// groupComboBox
			// 
			this.groupComboBox.FormattingEnabled = true;
			this.groupComboBox.Location = new System.Drawing.Point(60, 65);
			this.groupComboBox.Name = "groupComboBox";
			this.groupComboBox.Size = new System.Drawing.Size(136, 21);
			this.groupComboBox.TabIndex = 15;
			// 
			// groupLabel
			// 
			this.groupLabel.AutoSize = true;
			this.groupLabel.Location = new System.Drawing.Point(1, 68);
			this.groupLabel.Name = "groupLabel";
			this.groupLabel.Size = new System.Drawing.Size(36, 13);
			this.groupLabel.TabIndex = 16;
			this.groupLabel.Text = "Group";
			// 
			// imageComboBox1
			// 
			this.imageComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.imageComboBox1.FormattingEnabled = true;
			this.imageComboBox1.ImageList = this.iconsImageList;
			/*this.imageComboBox1.Items.AddRange(new object[] {
            "Icon 1",
            "Icon 2",
            "Icon 3",
            ""});*/
			this.imageComboBox1.Items.Add(new ImageComboBoxItem("Icon 1", 0));
			this.imageComboBox1.Items.Add(new ImageComboBoxItem("Icon 2", 1));
			this.imageComboBox1.Items.Add(new ImageComboBoxItem("Icon 3", 2));
			this.imageComboBox1.Location = new System.Drawing.Point(60, 93);
			this.imageComboBox1.Name = "imageComboBox1";
			this.imageComboBox1.Size = new System.Drawing.Size(136, 21);
			this.imageComboBox1.TabIndex = 17;
			// 
			// iconsImageList
			// 
			this.iconsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconsImageList.ImageStream")));
			this.iconsImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.iconsImageList.Images.SetKeyName(0, "user.png");
			this.iconsImageList.Images.SetKeyName(1, "user_female.png");
			this.iconsImageList.Images.SetKeyName(2, "user_suit.png");
			// 
			// iconLabel
			// 
			this.iconLabel.AutoSize = true;
			this.iconLabel.Location = new System.Drawing.Point(1, 96);
			this.iconLabel.Name = "iconLabel";
			this.iconLabel.Size = new System.Drawing.Size(28, 13);
			this.iconLabel.TabIndex = 18;
			this.iconLabel.Text = "Icon";
			// 
			// AddContact
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(220, 152);
			this.Controls.Add(this.iconLabel);
			this.Controls.Add(this.imageComboBox1);
			this.Controls.Add(this.groupLabel);
			this.Controls.Add(this.groupComboBox);
			this.Controls.Add(this.jabberIDTextBox);
			this.Controls.Add(this.serverLabel);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.label);
			this.Controls.Add(this.nameTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AddContact";
			this.ShowInTaskbar = false;
			this.Text = "Add Contact";
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
		private System.Windows.Forms.Label serverLabel;
		public System.Windows.Forms.TextBox jabberIDTextBox;
		private System.Windows.Forms.Label groupLabel;
		public System.Windows.Forms.ComboBox groupComboBox;
		private ImageComboBox imageComboBox1;
		private System.Windows.Forms.Label iconLabel;
		private System.Windows.Forms.ImageList iconsImageList;
    }
}