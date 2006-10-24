using System;

using System.Collections.Generic;

using System.Text;

using Goodware.Jabber.Library;

using System.IO;

using System.Net.Sockets;



namespace Goodware.Jabber.Client

{

    /// <summary>

    /// Хендлер за пакети за автентикација - ги има трите типа

    /// </summary>

    public class AuthHandler : PacketListener

    {

        JabberModel jabberModel;

        public AuthHandler(JabberModel model)
        {
            jabberModel = model;
        }

        Authenticator auth = new Authenticator();

        int counter;



        public void notify(Packet packet)

        {

            try

            {

                if (packet.ID.StartsWith("auth_get"))

                {

                    Packet query = packet.getFirstChild("query");

                    String token = query.getChildValue("token");

                    int sequence = Convert.ToInt32(query.getChildValue("sequence"));

                    String hash = auth.getZeroKHash(sequence, Encoding.UTF8.GetBytes(token), Encoding.UTF8.GetBytes(jabberModel.Password));

                    jabberModel.addResultHandler("0k_auth_" + Convert.ToString(counter), this);

                    StreamWriter output = packet.Session.Writer;

                    output.Write("<iq type='set' id='0k_auth_");

                    output.Write(Convert.ToString(counter++));

                    output.Write("'><query xmlns='jabber:iq:auth'><username>");

                    output.Write(jabberModel.User);

                    output.Write("</username><resource>");

                    output.Write(jabberModel.Resource);                    

                    output.Write("</resource><hash>");

                    output.Write(hash);

                    output.Write("</hash></query></iq>");

                    output.Flush();

                }

                else if (packet.Type.Equals("result"))

                {

                    packet.Session.Status = Session.SessionStatus.authenticated;

                }

                else if (packet.Type.Equals("error"))

                {

                    Console.WriteLine("Failed to authenticate: " + packet.getChildValue("error"));

                }

                else

                {

                    Console.WriteLine("Unknown result: " + packet.ToString());

                }

            }

            catch (IOException ex)

            {

                Console.WriteLine(ex.StackTrace);

            }

        }

    }

}

