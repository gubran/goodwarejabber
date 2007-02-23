using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;


// Stub
namespace Goodware.Jabber.Client {
	public delegate void DisconnectGUIDelegate();
	public class CloseStreamHandler:PacketListener {
		JabberModel jabberModel;

		public CloseStreamHandler(JabberModel model) {
			jabberModel = model;
		}

		public void notify(Packet packet) {
			//jabberModel.closeSession();
			//DisconnectGUIDelegate dgui = new DisconnectGUIDelegate(jabberModel.gui.disconnectGUI);
			//jabberModel.gui.Invoke(dgui);
		}
	}
}
