using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Goodware.Jabber.GUI {
	// Enumaration for the presence status
	enum Status { online = 1, busy, offline }

	class Contact : TreeNode, IComparable {
		private String jabbberID;
		private String name;
		private Status status;

		public Contact() {
			Status = Status.offline;						
		}
		
		/// <summary>
		/// Makes a new contact from a jabber ID
		/// </summary>
		/// <param name="jabbberID">The jabber ID of the new contact</param>
		public Contact(String jabbberID):this() {
			this.jabbberID = jabbberID;			
		}

		// For sorting
		public int CompareTo(Object obj) {
			if(obj.GetType() != Type.GetType("JustTalk.Contact"))
				return 0;	// Don't do anything
			Contact other = (Contact)obj;
			if(this.status == other.status) {
				return this.Text.CompareTo(other.Text);
			}
			return this.status.CompareTo(other.status);
		}

		// Presence status
		public Status Status {
			get {
				return status;
			}
			set {
				status = value;
				base.ImageIndex = (int)status;
				base.SelectedImageIndex = (int)status;
				this.setToolTipText();
			}
		}

		// JabberID for the contact
		public String JabberID {
			get {
				return jabbberID;
			}
			set {
				jabbberID = value;
				if(name == null || String.IsNullOrEmpty(name)) {
					base.Text = value;
				}
				this.setToolTipText();
			}
		}

		// The contact's name (optional)
		public new String Name {
			get {
				return name;
			}
			set {
				if(!String.IsNullOrEmpty(value)) {
					name = value;
					base.Text = value;
					this.setToolTipText();
				}
			}
		}

		// Sets the tool tip on every change
		private void setToolTipText() {
			base.ToolTipText = "Name: " + this.Name + "\n"
							 + "Jabber ID: " + this.jabbberID + "\n"
							 + "Status: " + this.status;
		}

		// New to string operation
		public override string ToString() {
			return base.Text;
		}
	}
}
