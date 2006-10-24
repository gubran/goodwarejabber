using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;
namespace Goodware.Jabber.Client
{
    /// <summary>
    /// Хендлер за IQ пакети
    /// </summary>
    public class IQHandler : PacketListener
    {
        JabberModel jabberModel;
        public IQHandler(JabberModel model) {
            jabberModel = model;
        }
        public void notify(Packet packet)
        {
            if (jabberModel == null)
            {
                Console.WriteLine("IQHandler Error - jabberModel==null");
                /// TODO Не постои ваков метод!?
                /// jabberModel = JabberModel.getModel();
            }
            if (packet.ID != null)
            {
                PacketListener listener = jabberModel.removeResultHandler(packet.ID);
                if (listener != null)
                {
                    listener.notify(packet);
                    return;
                }
            }
            
        }
    }
}
