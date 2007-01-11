using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Goodware.Jabber.GUI {
	class Group : TreeNode, IComparable {
		public Group(String name) : base(name) {
			base.Name = name;
			base.ToolTipText = "Group: " + name;
		}

		public int CompareTo(Object o) {
			return 0;
		}

		public override string ToString() {
			return this.Text;
		}
	}
}
