using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;
using Goodware.Jabber.GUI;

namespace Goodware.Jabber.Client {
	public delegate Contact GetContactDelegate(String jid);
	public delegate void UpdateContactPresenceDelegate(String jid, Status status, String statusMessage);
	public delegate bool AcceptInvitationDelegate(String jid);
	public delegate void RemoveContactDelegate(String jid);
	public delegate void UpdateGroupPresenceDelegate(String groupJID, String userNick, Show show, String statusMessage);
	public delegate void ShowGroupErrorDelegate(String code, String description);
	public delegate void RemoveGroupMemberDelegate(String groupName, String userNick);

	public class PresenceHandler : PacketListener {
		JabberModel model;
		public PresenceHandler(JabberModel model) {
			this.model = model;
		}

		public void notify(Packet packet) {
			String from = packet["from"];

		
			String type = packet["type"];
			if (type == null) {
				type = "available";
			}

			String show = packet.getChildValue("show");
			if (show == null) {
				show = "chat";
			}

			String statusMessage = packet.getChildValue("status");
			
			//test whether the presence is a groupchat presence or regular presence
			JabberID jid = new JabberID(from);
			String user = jid.User;
			if (user != null && user.EndsWith(".group")) { // groupchat presence
				Packet error = packet.getFirstChild("error"); // possible error packet
				if (error != null) {  // there was an error
					String code = error["code"];
					String description = error.getValue();
					ShowGroupErrorDelegate sged = new ShowGroupErrorDelegate(model.gui.ShowGroupError);
					model.gui.Invoke(sged, new Object[] { code, description });
					//delegate : ShowError(String code, String description);
					return;
				}

				// delegate signature : void UpdateGroupPresence(String groupJID, String userNick, Show show, String statusMessage) 
				UpdateGroupPresenceDelegate ugpd = new UpdateGroupPresenceDelegate(model.gui.UpdateGroupPresence);

				Goodware.Jabber.GUI.Show showStatus = Show.chat;
				String groupName = user.Substring(0, user.LastIndexOf(".group"));
				String userNick = jid.Resource;
				

				if (show.Equals("chat")) {
					showStatus = Show.chat;
				} else if (show.Equals("away")) {
					showStatus = Show.away;
				} else if (show.Equals("xa")) {
					showStatus = Show.xa;
				} else if (show.Equals("dnd")) {
					showStatus = Show.dnd;
				}


				if (type.Equals("available")) {
					// UpdateGroupPresence(groupName, userNick, showStatus, statusMessage);
					model.gui.Invoke(ugpd, new Object[] { groupName, userNick, showStatus, statusMessage });
					// UpdateGroupPresence(groupName, userNick, showStatus, statusMessage);
				} else if (type.Equals("unavailable")) {
					RemoveGroupMemberDelegate rgmd = new RemoveGroupMemberDelegate(model.gui.RemoveGroupMember);
					model.gui.Invoke(rgmd, new Object[] {groupName, userNick});
					// RemoveGroupMember(groupName, userNick);
				}



			} else { // regular presence


				GetContactDelegate del = new GetContactDelegate(model.gui.GetContact);
				Contact contact = (Contact)model.gui.Invoke(del, new Object[] { from });

				UpdateContactPresenceDelegate ucpd = new UpdateContactPresenceDelegate(model.gui.UpdateContactPresence);

				//two cases : presence update & presence subscription
				if (type.Equals("available") || type.Equals("unavailable")) {  // presence update
					Status status;

					if (type.Equals("unavailable")) {
						status = Status.unavailable;
					} else if (show.Equals("chat")) {
						status = Status.chat;
					} else if (show.Equals("away")) {
						status = Status.away;
					} else if (show.Equals("xa")) {
						status = Status.xa;
					} else if (show.Equals("dnd")) {
						status = Status.dnd;
					} else {
						status = Status.unavailable; // some default - execution should never come to this case
					}
					model.gui.Invoke(ucpd, new Object[] { from, status, statusMessage }); 	//delegate : UpdateContactPresence(from, Status.unavailable, null)
				} else {  // presence subscription or unsubscription

					if (type.Equals("subscribe")) {

						if (contact == null) { // this is the first invitation
							AcceptInvitationDelegate aid = new AcceptInvitationDelegate(model.gui.AcceptInvitation);
							if ((bool)model.gui.Invoke(aid, new Object[] { from })) { //invitation accepted
								model.sendPresence(from, "subscribed", null, null, null);
								model.sendPresence(from, "subscribe", null, null, null);
							} else { //invitation refused
								model.sendPresence(from, "unsubscribed", null, null, null);
							}
						} else {  // the contact is already invited
							model.sendPresence(from, "subscribed", null, null, null);
							//contact.Status = Status.unavailable;
							model.gui.Invoke(ucpd, new Object[] { from, Status.unavailable, null }); 	//delegate : UpdateContactPresence(from, Status.unavailable, null)
							model.sendPresence(model.Me.Status, model.Me.StatusMessage);
							//model.sendPresence - presence packet with current presence information for the user (type, status, show)
						}

					} else if (type.Equals("subscribed")) {

						Status status = contact.Status;
						if (status == Status.inviteSent) { //the second stage in the "three-way handshake"
							// do Nothing : immediately after this packet comes a "subscribe" packet and then things are handled
						} else if (status == Status.inviteAccepted) { // the third stage in the "three-way handshake"
							//contact.Status = Status.unavailable;
							model.gui.Invoke(ucpd, new Object[] { from, Status.unavailable, null });   // delegate : UpdateContactPresence(from, Status.unavailable, null)
						}

					} else if (type.Equals("unsubscribe")) {

						// do Nothing : immediately after this packet comes an "unsubscribed" packet and then things are handled
					
					} else if (type.Equals("unsubscribed")) {

						RemoveContactDelegate rcd = new RemoveContactDelegate(model.gui.RemoveContact);
						model.gui.Invoke(rcd, new Object[] { from });

						// delegate : void removeContact(String jid) - actual invocation : removeContact(from)
						// removes the contact <jid> from the structures containing the contacts
					
					} // type of presence

				} // presence update or subscription

			} // groupchat presence or regular presence
		}
	}
}
