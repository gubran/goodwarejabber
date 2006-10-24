using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server
{
    class RosterHandler : PacketListener
    {
        UserIndex userIndex;
        public RosterHandler(UserIndex index)
        {
            userIndex = index;
        }
        public void notify(Packet packet)
        {
            packet.To = null;
            packet.From = null;
            if (packet.getSession().getStatus() != Session.SessionStatus.authenticated)
            {
                //can not send message:user is not authenticated
                //Error Monitoring must be implemented
                MessageHandler.deliverPacket(packet);
                return;
            }
            User user = userIndex.getUser(packet.getSession());
            if (packet.Type.Equals("set"))
            {
                user.getRoster().updateRoster(packet);
                return;
            }
            if (packet.Type.Equals("get"))
            {
                packet.Type = "result";
                JabberID jidTo =packet.getSession().getJID();
                packet.To = jidTo.User + "@" + jidTo.Domain;
               // packet.From = null;
                packet.removeAttribute("from");
                packet.getChildren().Clear();
                user.getRoster().getPacket().setParent(packet);
                MessageHandler.deliverPacket(packet);
                return;
            }
            //error in packet
            MessageHandler.deliverPacket(packet);


        }

    }
}
