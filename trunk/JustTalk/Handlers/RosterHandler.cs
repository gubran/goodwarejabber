using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;
using Goodware.Jabber.GUI;

namespace Goodware.Jabber.Client {
	public delegate void UpdateContactDelegate(String jid, String name, String group, Status status);

    class RosterHandler : PacketListener {
		protected JabberModel model;
		public RosterHandler(JabberModel model) {
			this.model = model;
		}

        public void notify(Packet packet) {
            Packet query = packet.getFirstChild("query");
            foreach (Packet item in query.Children) {
                String jid = item["jid"];
                String name = item["name"];
                String subscribtion = item["subscription"];
                String ask = item["ask"];						// TODO: Revise (can be null sometimes and brakes the execution)
                String group = item.getChildValue("group");
                Status status = Status.unavailable;

				if(subscribtion.Equals("none") && ask.Equals("subscribe")) {
					status = Status.inviteSent;
				} else if (subscribtion.Equals("to")) {
					status = Status.inviteAccepted;
				} else if (subscribtion.Equals("both")) {
					status = Status.unavailable;
				}
				UpdateContactDelegate del = new UpdateContactDelegate(model.gui.UpdateContact);
				model.gui.Invoke(del, new Object[] { jid, name, group, status });                
            }
        }

    }
}
