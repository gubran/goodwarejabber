using System;
using Goodware.Jabber.Library;

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