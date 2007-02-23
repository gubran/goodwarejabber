namespace Goodware.Jabber.GUI {
	partial class GroupchatDialog {
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
			this.groupTextBox = new System.Windows.Forms.TextBox();
			this.nickTextBox = new System.Windows.Forms.TextBox();
			this.groupSufixLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.joinCreateButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// groupTextBox
			// 
			this.groupTextBox.Location = new System.Drawing.Point(53, 12);
			this.groupTextBox.Name = "groupTextBox";
			this.groupTextBox.Size = new System.Drawing.Size(142, 20);
			this.groupTextBox.TabIndex = 0;
			// 
			// nickTextBox
			// 
			this.nickTextBox.Location = new System.Drawing.Point(53, 39);
			this.nickTextBox.Name = "nickTextBox";
			this.nickTextBox.Size = new System.Drawing.Size(142, 20);
			this.nickTextBox.TabIndex = 1;
			// 
			// groupSufixLabel
			// 
			this.groupSufixLabel.AutoSize = true;
			this.groupSufixLabel.Location = new System.Drawing.Point(195, 15);
			this.groupSufixLabel.Name = "groupSufixLabel";
			this.groupSufixLabel.Size = new System.Drawing.Size(37, 13);
			this.groupSufixLabel.TabIndex = 2;
			this.groupSufixLabel.Text = ".group";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Group:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Nick:";
			// 
			// joinCreateButton
			// 
			this.joinCreateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.joinCreateButton.Location = new System.Drawing.Point(39, 73);
			this.joinCreateButton.Name = "joinCreateButton";
			this.joinCreateButton.Size = new System.Drawing.Size(75, 23);
			this.joinCreateButton.TabIndex = 2;
			this.joinCreateButton.Text = "Join/Create";
			this.joinCreateButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(120, 73);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// GroupchatDialog
			// 
			this.AcceptButton = this.joinCreateButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(305, 108);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.joinCreateButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupSufixLabel);
			this.Controls.Add(this.nickTextBox);
			this.Controls.Add(this.groupTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "GroupchatDialog";
			this.ShowInTaskbar = false;
			this.Text = "Groupchat";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button joinCreateButton;
		private System.Windows.Forms.Button cancelButton;
		public System.Windows.Forms.TextBox groupTextBox;
		public System.Windows.Forms.TextBox nickTextBox;
		public System.Windows.Forms.Label groupSufixLabel;
	}
}