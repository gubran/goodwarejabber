using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

public delegate void RecieveMessageDelegate(JabberID jid, string body);

namespace Goodware.Jabber.Client {
    public class MessageHandler : PacketListener {
		private JabberModel model;

		public MessageHandler(JabberModel model) {
			this.model = model;
		}

		public void notify(Packet packet) {
            Console.WriteLine("in message handler");
			String type = packet.Type == null ? "normal" : packet.Type;
			Console.WriteLine("Received " + type + " message: " + packet.getChildValue("body"));
			Console.WriteLine("    To: " + packet.To);
			Console.WriteLine("  From: " + packet.From);
			JabberID jid = new JabberID(packet.From);

			RecieveMessageDelegate del = new RecieveMessageDelegate(model.gui.ReceiveMessage);
			model.gui.Invoke(del, new Object[] { jid, packet.getChildValue("body") });
			//model.gui.ReceiveMessage(jid, packet.getChildValue("body"));
		}
	}
}
