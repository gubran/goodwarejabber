using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;
namespace Goodware.Jabber.Client
{
    /// <summary>
    /// Регистрира хендлер за пакети. 
    /// </summary>
    public class RegisterHandler : PacketListener
    {

        JabberModel jaberModel;

        public RegisterHandler(JabberModel model)
        {
            this.jaberModel = model;
        }

        public void notify(Packet packet)
        {
            try
            {
                if (packet.Type.Equals("result"))
                {
//                    jaberModel.authenticate();
                }
                else
                {
                    String message = "Failed to register";
                    if (packet.Type.Equals("error"))
                    {
                        message = message + ":" + packet.getChildValue("error");
                    }
                    Console.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
