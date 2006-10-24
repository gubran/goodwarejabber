using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;


// Stub
namespace Goodware.Jabber.Client {
    public class OpenStreamHandler : PacketListener
    {
		public void notify(Packet packet) {
			Session session = packet.Session;
			session.StreamID = (packet.ID);
			session.JID = (new JabberID(packet.To));
			session.Status = (Session.SessionStatus.streaming);
		}
	}
}
