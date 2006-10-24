using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Client
{
    class RosterHandler : PacketListener
    {
        public void notify(Packet packet)
        {
            //handling of roster pushes from server
            //may need to be changed for th GUI
            Console.WriteLine("packet:");
            Console.WriteLine(packet.ToString());
        }

    }
}
