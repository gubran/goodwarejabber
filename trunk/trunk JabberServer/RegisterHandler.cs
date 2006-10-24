using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

// darkoA

namespace Goodware.Jabber.Server {

    class RegisterHandler : PacketListener {
        
        static UserIndex userIndex;
        Packet required;
        Authenticator auth = new Authenticator();

        public RegisterHandler(UserIndex index) {
            userIndex = index;
            required = new Packet("iq");
            required.From = JabberServer.server_name;
            required.Type = "result";
            new Packet("username").Parent = required;
            new Packet("password").Parent = required;
            new Packet("hash").Parent = required;
        }

        public void notify(Packet packet) {
            Console.WriteLine("Register handling" + packet.ToString());

            String type = packet.getType();
            Packet query = packet.getFirstChild("query");

            if (type.Equals("get")) {
                required.setSession(packet.getSession());
                required.setID(packet.getID());
                MessageHandler.deliverPacket(required);
                return;
            } else if (type.Equals("set")) {
                //String t1 = query.Children[0].ToString();
                //t1 = query.getFirstChild("username").getValue();
                //int l = t1.Length;
                //t1 = query.Children[2].ToString();
                //t1 = query.Children[3].ToString();

                String username = query.getChildValue("username");
                User user = userIndex.getUser(username);
                if (user != null) {
                    if (packet.getSession().getStatus() != Session.SessionStatus.authenticated || !username.Equals(packet.getSession().getJID().getUser())) {
                        Packet iq = new Packet("iq");
                        iq.setSession(packet.getSession());
                        iq.setID(packet.getID());
                        //ErrorTool.setError(iq, 401, "User account already exists");
                        MessageHandler.deliverPacket(iq);
                        return;
                    }
                } else {
                    user = userIndex.addUser(username);
                }
                user.setPassword(query.getChildValue("password"));
                user.setHash(query.getChildValue("hash"));
                user.setSequence(query.getChildValue("sequence"));
                user.setToken(query.getChildValue("token"));
                if (user.getHash() == null || user.getSequence() == null || user.getToken() == null) {
                    if (user.getPassword() != null) {
                        user.setToken("randomtoken");// ovde smeni
                        user.setSequence("99");
                        user.setHash(auth.getZeroKHash(100, Encoding.UTF8.GetBytes(user.getToken()), Encoding.UTF8.GetBytes(user.getPassword())
                            /* ovde da se proveri isprakjanjeto !!! */                                         
                                             ));
                        
                    }
                } else {
                    // Adjust sequence number to be ready for next request.
                    // Book readers.  In the book this was listed earlier resulting in a thrown
                    // exception if 0k registration was not used.  This fixes it.  :)
                    int i = (int.Parse(user.getSequence()) - 1);
                    user.setSequence(i.ToString());
                }
                Packet iqpacket = new Packet("iq");
                iqpacket.setSession(packet.getSession());
                iqpacket.setID(packet.getID());
                iqpacket.setType("result");
                MessageHandler.deliverPacket(iqpacket);

                // Temporarily needed as we'll use registration as authentication until Chp7.
                // packet.getSession().getJID().setResource("none");
                // userIndex.addSession(packet.getSession());
                //Log.trace("Register successfully registered " + username + " with password " + query.getChildValue("password"));
                Console.WriteLine("Register successfully registered " + username + " with password " + query.getChildValue("password"));

            } else {
                Console.WriteLine("Register ignoring " + packet.ToString());
            }

        }
    }


}
