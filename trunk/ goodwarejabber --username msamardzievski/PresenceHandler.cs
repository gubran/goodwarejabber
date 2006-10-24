using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Client {
    public class PresenceHandler : PacketListener
    {
		public void notify(Packet packet) {
			Console.WriteLine("Received presence: " + packet.Type);
			Console.WriteLine("    To: " + packet.To);
			Console.WriteLine("  From: " + packet.From);
		}
	}
}
