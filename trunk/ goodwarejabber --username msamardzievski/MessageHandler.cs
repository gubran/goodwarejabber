using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

// Stub
namespace Goodware.Jabber.Client {
    public class MessageHandler : PacketListener
    {
		public void notify(Packet packet) {
            Console.WriteLine("in message handler");
			String type = packet.Type == null ? "normal" : packet.Type;
			Console.WriteLine("Received " + type + " message: " + packet.getChildValue("body"));
			Console.WriteLine("    To: " + packet.To);
			Console.WriteLine("  From: " + packet.From);
		}
	}
}
