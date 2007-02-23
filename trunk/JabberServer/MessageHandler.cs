using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server {
	public class MessageHandler : PacketListener {

		static UserIndex userIndex;

		GroupChatManager chatMan = GroupChatManager.Manager;

		public MessageHandler(UserIndex userIndex) {
			MessageHandler.userIndex = userIndex;
		}

		
		public void notify(Packet packet) {
            /* od Ch 7
            if (packet.Session.Status != Session.SessionStatus.authenticated) {
                JabberServer.output.WriteLine("Dropping packet (no auth): " + packet.ToString());
                return;
            } */
            String recipient = packet.To;

            if (recipient == null) { // to server
                JabberServer.output.WriteLine("Dropping packet: " + packet.ToString());
                return;
            }

            if (recipient.Equals(JabberServer.server_name, StringComparison.OrdinalIgnoreCase )) { // to server
                JabberServer.output.WriteLine("Dropping packet: " + packet.ToString());
                return;
            }

            // Fill in sender as resource that sent message (anti-spoofing)
            packet.setFrom(packet.getSession().getJID().ToString());

            if (packet.getAttribute("type")!=null && packet.getAttribute("type").Equals("groupchat",StringComparison.OrdinalIgnoreCase)) {
                if (chatMan.isChatPacket(packet)) {
                    chatMan.handleChatMessage(packet);
                } else {
                    JabberServer.output.WriteLine("Dropping packet: " + packet.ToString());
                }
                return;
            }

            JabberServer.output.WriteLine("Delivering packet To: " + packet.To);
            deliverPacket(packet);

		}

        // TODO : deliverPacketToAll()

		public static void deliverPacket(Packet packet) {
            try {
              String recipient = packet.To;
              StreamWriter output;

              if (recipient == null){
                output = packet.getSession().getWriter();
                if (output == null){
                    JabberServer.output.WriteLine("Undeliverable packet " + packet.ToString());
                    return;
                }
              } else {
                  if (userIndex.containsUser(recipient))
                 {
                      output = userIndex.getWriter(recipient);
                 }
                 else
                  {
                      return;
                  }
//				JabberServer.output.WriteLine("String"+userIndex.getUser(recipient).ToString());
              }
              if (output != null){
                JabberServer.output.WriteLine("Delivering packet: " + packet.ToString());
                output.Write(packet.ToString());    // smeneto od DarkoA
                output.Flush();                     // smeneto od DarkoA
              } else {
                  //begin change by marko
                  if (!packet.Element.Equals("presence") || (packet["type"] != null && (packet["type"].Equals("subscribe") || packet["type"].Equals("subscribed") || packet["type"].Equals("unsubscribe") || packet["type"].Equals("unsubscribed"))))
                  {     //update presence packets (available & unavailable) don't need to be stored for offline use
                      JabberServer.output.WriteLine("Store & forward: " + packet.ToString());
                      User user = userIndex.getUser(new JabberID(recipient).getUser());
                      user.storeMessage(packet);
                  }
                  //end change by marko
              }
			} catch (Exception ex){
				JabberServer.output.WriteLine(ex.StackTrace);

				
                JabberServer.output.WriteLine("MessageHandler: ", ex);
            }

		}
		
		// Od Marko
		static public void deliverPacketToAll(String username, Packet packet) {
			
      User user = userIndex.getUser(username);

      Hashtable sessions = user.getSessions();
     foreach (Session session in sessions.Values) {
          if (session.getPriority() >= 0) {
              packet.setSession(session);
              deliverPacket(packet);
          }
      }
    }

	}
}
