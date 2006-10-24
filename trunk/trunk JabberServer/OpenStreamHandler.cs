using System.IO;

using System;

using Goodware.Jabber.Library;





namespace Goodware.Jabber.Server {

	public class OpenStreamHandler : PacketListener {

	

		UserIndex userIndex;

	

		public OpenStreamHandler(UserIndex index) {

			userIndex = index;

		}

	

		public void notify (Packet packet) {

			Console.WriteLine("Opening stream connection");

	

			try {

				Session session = packet.Session;

	

				session.setStreamID(Authenticator.randomToken());

	

//				Ова е Јава код кој не успеав да го Ц Шарпувам

//				session.setStreamID(Integer.toHexString(streamID++));

//				Милош има направено сопствена класа за претворање на стринг во хекса, уште од број до стринг да стигнев

//				byte[] rez = ??(streamID++);

//				session.StreamID = Authenticator.GetAsHexaDecimal(rez);

	

				StreamWriter output = session.Writer;

				output.Write("<?xml version='1.0' encoding='UTF-8' ?><stream:stream xmlns='jabber:client' from='");

				output.Write(JabberServer.server_name);

				output.Write("' id='");

				output.Write(session.StreamID);

				output.Write("' xmlns:stream='http://etherx.jabber.org/streams'>");

				output.Flush();

	

				session.Status = Session.SessionStatus.streaming;

			} catch (Exception ex) {

	//			ex.printStackTrace();

			}

		}

	}

}

