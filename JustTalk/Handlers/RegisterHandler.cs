using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;
using Goodware.Jabber.GUI;
namespace Goodware.Jabber.Client {

	public delegate void RegisterFailedDelegate(String message);
	/// <summary>
	/// Регистрира хендлер за пакети. 
	/// </summary>
	public class RegisterHandler : PacketListener {
		JabberModel jaberModel;

		public RegisterHandler(JabberModel model) {
			this.jaberModel = model;
		}

		public void notify(Packet packet) {
			try {
				if(packet.Type.Equals("result")) {
					voidDel vd = new voidDel(jaberModel.gui.Registered);
					jaberModel.gui.Invoke(vd);
				} else {
					String message = null;
					if(packet.Type.Equals("error")) {
						Packet err = packet.getFirstChild("error");
						message = err["code"] + " : " + err.getValue();

						RegisterFailedDelegate rfd = new RegisterFailedDelegate(jaberModel.gui.RegistrationFailed);
						jaberModel.gui.Invoke(rfd, new Object[] {message});
					}
					Console.WriteLine(message);
				}
			} catch(Exception ex) {
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
