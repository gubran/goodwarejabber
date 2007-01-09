using System;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server {
    public class ProcessThread : AThread
    {
		Session session;
		PacketQueue packetQueue;

		public ProcessThread(PacketQueue queue, Session session) {
			packetQueue = queue;
			this.session = session;
		}

		public override void run() {
			JabberInputHandler handler = new JabberInputHandler(packetQueue);
			handler.process(session);
		}


	} 
}
