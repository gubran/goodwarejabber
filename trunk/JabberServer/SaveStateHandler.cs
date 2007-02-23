using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server
{
    class SaveStateHandler : PacketListener
    {

        UserIndex index;

        public SaveStateHandler(UserIndex index)
        {
            this.index = index;
        }


        public void notify(Packet packet)
        {
            JabberServer.output.WriteLine("Saving...");
            QueueThread.saveToFile();
        }
    }
}
