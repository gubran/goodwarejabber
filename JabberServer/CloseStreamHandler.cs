using System;
using Goodware.Jabber.Library;
using System.Collections.Generic;

namespace Goodware.Jabber.Server {
    public class CloseStreamHandler : PacketListener {
        UserIndex userIndex;

        public CloseStreamHandler(UserIndex index) {
            userIndex = index;
        }

        public void notify(Packet packet) {
            try {
                //      Log.trace("Closing session");
                Session session = packet.Session;

                session.Writer.Write("</stream:stream> ");
                session.Writer.Flush();
                
                //notify other subscribers that user is unavailble
                //by Marko
                //begin
                JabberID userid=session.getJID();
               
                Packet unavailiblePacket = new Packet("presence");
                unavailiblePacket.setFrom(userid.ToString());
                unavailiblePacket.setAttribute("type", "unavailable");
                userIndex.getUser(userid.User).getRoster().updateSubscribers(unavailiblePacket);
                //it is not tested, but it should work
                //end

					//send groupchat presence unavailable & remove from groups
					String jid = userid.ToString();

					Packet presence = new Packet("presence");
					presence.Type = "unavailable";
					foreach (KeyValuePair<String, GroupChatManager.Group> kvp in GroupChatManager.Manager.groups) {
						
						GroupChatManager.Group group = kvp.Value;
						try {
							
							String nick = group.jid2nick[jid]; // test whether the user is in the group
							presence.From = group.JabberID + "/" + nick;
							GroupChatManager.Manager.removeUser(group, jid);   // first remove then deliver so that the packet does not come back
							GroupChatManager.Manager.deliverToGroup(group, presence);

						} catch (Exception ex) {
						}
					}
					// end groupchar clean-up	
					

					
					

                session.Socket.Close();
                userIndex.removeSession(session);
                //      Log.trace("Closed session");
            } catch (Exception ex) {
                //      Log.error("CloseStreamHandler: ",ex);
                userIndex.removeSession(packet.Session);
                //      Log.trace("Exception closed session");
            }
        }
    }
}