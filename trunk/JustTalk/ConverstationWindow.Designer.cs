namespace Goodware.Jabber.GUI {
    partial class ConverstationWindow {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConverstationWindow));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.dialogView = new System.Windows.Forms.RichTextBox();
			this.inputTextBox = new System.Windows.Forms.TextBox();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.dialogView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.inputTextBox);
			this.splitContainer1.Size = new System.Drawing.Size(292, 271);
			this.splitContainer1.SplitterDistance = 245;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 0;
			// 
			// dialogView
			// 
			this.dialogView.BackColor = System.Drawing.SystemColors.Window;
			this.dialogView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dialogView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dialogView.Location = new System.Drawing.Point(0, 0);
			this.dialogView.Name = "dialogView";
			this.dialogView.ReadOnly = true;
			this.dialogView.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.dialogView.Size = new System.Drawing.Size(292, 245);
			this.dialogView.TabIndex = 0;
			this.dialogView.Text = "";
			// 
			// inputTextBox
			// 
			this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputTextBox.Location = new System.Drawing.Point(0, 0);
			this.inputTextBox.Name = "inputTextBox";
			this.inputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.inputTextBox.Size = new System.Drawing.Size(292, 20);
			this.inputTextBox.TabIndex = 1;
			this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
			// 
			// ConverstationWindow
			// 
			this.ClientSize = new System.Drawing.Size(292, 271);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ConverstationWindow";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox inputTextBox;
		private System.Windows.Forms.RichTextBox dialogView;




	}
}