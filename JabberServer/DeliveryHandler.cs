using System;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server {
	public class DeliveryHandler : PacketListener {
		SessionIndex sessionIndex;

		public DeliveryHandler(SessionIndex index) {
			sessionIndex = index;
		}

		public void notify(Packet packet) {
			String recipient = packet.To;

			if (recipient.Equals(JabberServer.server_name, StringComparison.OrdinalIgnoreCase)) {
				return;
			}

			Session session = sessionIndex[recipient];

			if (session != null) {
				session.Writer.Write(packet);
				session.Writer.Flush();
			} else {
				return;
			}



		}

	}

}