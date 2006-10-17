using System;
using System.Collections.Generic;
using System.Text;

namespace JustTalk {
	class ImageComboBoxItem {
		private string _text;

		public string Text {
		  get {return _text;}
		  set {_text = value;}
		}

		private int _imageIndex;
		public int ImageIndex {
		  get {return _imageIndex;}
		  set {_imageIndex = value;}
		}

		public ImageComboBoxItem() : this("") {
		}

		public ImageComboBoxItem(string text) : this(text, -1) {
		}

		public ImageComboBoxItem(string text, int imageIndex) {
		  _text = text;
		  _imageIndex = imageIndex;
		}

		public override string ToString() {
		  return _text;
		}
	}
}
