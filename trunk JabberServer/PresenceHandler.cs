using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server {
	public class PresenceHandler : PacketListener {

        UserIndex userIndex;
		GroupChatManager chatMan = GroupChatManager.Manager;

        public PresenceHandler(UserIndex index) {
			this.userIndex = index;
		}

		public void notify(Packet packet) {
            if (packet.Session.Status != Session.SessionStatus.authenticated) {
                //Log.info("PresenceHandler: Not authenticated" + packet.ToString());
                //send an error message Not Authenticated!;

                packet.To = null;
                packet.From = null;
                //ErrorTool.setError(packet, 401, "You must be authenticated to send presence");
                MessageHandler.deliverPacket(packet);
            } else if (chatMan.isChatPacket(packet)) {
                //Log.trace("PresenceHandler: groupchat presence");
                chatMan.handleChatPresence(packet);
            } else {
                //Log.trace("PresenceHandler: delivering presence packet " + packet.ToString());
                // Od Marko
                User user = userIndex.getUser(packet.getSession().getJID().getUser());
                user.getRoster().updatePresence(packet);
            }        
        }
	}
}
