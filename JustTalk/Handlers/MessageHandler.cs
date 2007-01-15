using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Client {
	public delegate void RecieveMessageDelegate(JabberID jid, string body);

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

			if (jid.User != null && jid.User.EndsWith(".group")) { // groupchar message
				//void ReceiveGroupMessage(String groupJID, String userNick, String message)
				
				String user = jid.User;
				String groupJID = user.Substring(0, user.LastIndexOf(".group"));
				String userNick = jid.Resource;

				//ReceiveGroupMessage(groupJID, userNick, packet.getChildValue("body");


			} else { // regular message

				RecieveMessageDelegate del = new RecieveMessageDelegate(model.gui.ReceiveMessage);
				model.gui.Invoke(del, new Object[] { jid, packet.getChildValue("body") });
				//model.gui.ReceiveMessage(jid, packet.getChildValue("body"));
			}
		}
	}
}
