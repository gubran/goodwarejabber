using System;

namespace Goodware.Jabber.Library {
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
