using System;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Client {
    public class ProcessThread : AThread {
		Session session;
		PacketQueue packetQueue;

		private JabberModel model;
		public ProcessThread(PacketQueue queue, Session session, JabberModel model) {
			packetQueue = queue;
			this.session = session;
			this.model = model;
		}

		public override void run() {
			JabberInputHandler handler = new JabberInputHandler(packetQueue);
			try {
				handler.process(session);
			} catch(Exception ex) {
				try {
					// Most likely disconnected
					DisconnectGUIDelegate dgui = new DisconnectGUIDelegate(model.gui.disconnectGUI);
					model.gui.Invoke(dgui);
				} catch (Exception exc) {
					// Probably gui has already been destroyed
					Console.WriteLine(exc.StackTrace);
				}
			}
		}
	}
}
