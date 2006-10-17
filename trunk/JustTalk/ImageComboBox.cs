using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JustTalk {
	public partial class ImageComboBox : ComboBox {
		// It's image list
		private ImageList imageList;
		public ImageList ImageList {
			get {
				return imageList;
			}
			set {
				imageList = value;
			}
		}

		public ImageComboBox() {
			InitializeComponent();
			DrawMode = DrawMode.OwnerDrawFixed;
		}

		protected override void OnPaint(PaintEventArgs pe) {
			// TODO: Add custom paint code here

			// Calling the base class OnPaint
			base.OnPaint(pe);
		}

		protected override void OnDrawItem(DrawItemEventArgs ea) {
			ea.DrawBackground();
			ea.DrawFocusRectangle();

			ImageComboBoxItem item;
			Size imageSize = imageList.ImageSize;
			Rectangle bounds = ea.Bounds;

			try {
				item = (ImageComboBoxItem)Items[ea.Index];

				if(item.ImageIndex != -1) {
					imageList.Draw(ea.Graphics, bounds.Left, bounds.Top, item.ImageIndex);
					ea.Graphics.DrawString(item.Text, ea.Font, new SolidBrush(ea.ForeColor), bounds.Left + imageSize.Width, bounds.Top);
				} else {
					ea.Graphics.DrawString(item.Text, ea.Font, new SolidBrush(ea.ForeColor), bounds.Left, bounds.Top);
				}
			} catch {
				if(ea.Index != -1) {
					ea.Graphics.DrawString(Items[ea.Index].ToString(), ea.Font, new SolidBrush(ea.ForeColor), bounds.Left, bounds.Top);
				} else {
					ea.Graphics.DrawString(Text, ea.Font, new SolidBrush(ea.ForeColor), bounds.Left, bounds.Top);
				}
			}
			base.OnDrawItem(ea);
		}
	}
}
