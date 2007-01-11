using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;
using Goodware.Jabber.GUI;

namespace Goodware.Jabber.Client {
	public delegate Contact GetContactDelegate(String jid);
	public delegate void UpdateContactPresenceDelegate(String jid, Status status, String statusMessage);
	public delegate bool AcceptInvitationDelegate(String jid);

	public class PresenceHandler : PacketListener {
		JabberModel model;
		public PresenceHandler(JabberModel model) {
			this.model = model;
		}

		public void notify(Packet packet) {
			String type = packet["type"];
			String from = packet["from"];
			String show = packet.getChildValue("show");
			if (show == null) {
				show = "chat";
			}
			String statusMessage = packet.getChildValue("status");
			

			if (type == null) {
				type = "available";
			}
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
						if((bool)model.gui.Invoke(aid, new Object[] {from} )) { //invitation accepted
							model.sendPresence(from, "subscribed", null, null, null);
							model.sendPresence(from, "subscribe", null, null, null);


							

						} else { //invitation refused
							model.sendPresence(from, "unsubscribed", null, null, null);
						}

						//raise PopUp dialog
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

				} //ToDo : unsubscribe & unsubscribed 
			
			
			

			
			
			}


		}
	}
}
