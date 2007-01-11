using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Goodware.Jabber.GUI {
	// Enumeration for the presence status
	public enum Status { chat = 1, dnd, unavailable, away, xa, inviteSent, inviteAccepted }

	public class Contact : TreeNode, IComparable {
		private String jabberID;
		private String name;
		private Status status;
		private String statusMessage;

		public Contact() {
			Status = Status.unavailable;						
		}
		
		/// <summary>
		/// Makes a new contact from a jabber ID
		/// </summary>
		/// <param name="jabberID">The jabber ID of the new contact</param>
		public Contact(String jabbberID):this() {
			this.JabberID = jabbberID;			
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
				return jabberID;
			}
			set {
				jabberID = value;
				if(String.IsNullOrEmpty(name)) {
					base.Text = value;
				}
				this.setToolTipText();
			}
		}

		// The contact's name (optional)
		public new String Name {
			get {
				if(String.IsNullOrEmpty(name))
					return this.JabberID;
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

		/// <summary>
		/// Message for displaying to the GUI
		/// </summary>
		public String StatusMessage {
			get {
				if(String.IsNullOrEmpty(statusMessage)) {
					switch(status) {
						case Status.chat:
							return "Chat";
						case Status.away:
							return "Away";
						case Status.dnd:
							return "Do Not Disturb";
						case Status.unavailable:
							return "Unavailable";
						case Status.xa:
							return "Extended Away";
						case Status.inviteSent:
							return "Invitation Sent";
						case Status.inviteAccepted:
							return "Invitation Accepted";
						default:
							return "WTF?";
					}
				} else
					return statusMessage;
			}
			set {
				if(!String.IsNullOrEmpty(value)) {
					statusMessage = value;
					this.setToolTipText();
				}
			}
		}

		// Sets the tool tip on every change
		private void setToolTipText() {
			base.ToolTipText = "Name: " + this.Name + "\n"
							 + "Jabber ID: " + this.jabberID + "\n"
							 + "Status: " + this.StatusMessage;
		}

		// New to string operation
		public override string ToString() {
			return base.Text;
		}
	}
}
