using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server
{
    public class Roster
    {
        //Users nickname used for presence updates
        String user;
        public Roster(String username)
        {
            user = username;
        }

        public void updatePresence(Packet presence)
        {
            String type = presence.Type;
            Session session = presence.Session;
            Presence sessionPresence = session.getPresence();
            String recipient = presence.To;
            JabberID recipientID;
            Boolean isUserSent;//it is not used yet, may be used in S2S comunication

            //packet filling begins
            if (recipient == null)
            {   //directed to server
                //server name
                recipientID = new JabberID(Server.JabberServer.server_name);
                isUserSent = true;
            }
            else
            {
                recipientID = new JabberID(recipient);
                if (user.Equals(recipientID.User))
                {
                    isUserSent = false;
                }
                else
                {
                    isUserSent = true;
                }
            }
            String sender = presence.From;
            JabberID SenderID;
            if (sender == null)
            {
                SenderID = session.JID;
            }
            else
            {
                SenderID = new JabberID(sender);
            }
            //added by marko
            presence.From = SenderID.User + "@" + SenderID.Domain;

            //original line:
            //String subscriber = isUserSent ? recipientID.ToString() : SenderID.ToString();
            String subscriber = recipientID.ToString();

            if (type == null)
            {
                type = "availible";
            }
            //packet filling ends


            //packet routing begins
            if (type.Equals("availible") || type.Equals("unavailible"))
            {
                //user-managed presence updates are delivered untouched
                if (!recipientID.ToString().Equals(Server.JabberServer.server_name))
                {
                    //may need some work;
                    MessageHandler.deliverPacket(presence);
                    return;
                }


                //Server-managed presence updates are forwarded only to subscribers

                //update the session's presence status

                sessionPresence.Availible = type.Equals("availible");
                sessionPresence.Show = presence.getChildValue("show");
                sessionPresence.Status = presence.getChildValue("status");
                String priority = presence.getChildValue("priority");
                sessionPresence.Priority = priority;
                if (priority != null)
                {
                    //some code to set the priority of the session

                    try
                    {
                        session.setPriority(int.Parse(priority));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in incoming priority, setting priority to default: 1");
                        session.setPriority(0);
                    }
                }

                //deliver to all users presence subscribers
                updateSubscribers(presence);
                informSubscriber();
                return;
            }

            if (type.Equals("probe"))
            {

                //needed for server to server communication!!!!
                Console.WriteLine("Roster: We don't handle probes yet " + presence.ToString());
                return;


            }



            //Marko believes it is checked for exceptions
            //Subscriber sendersSubscriberItem = (Subscriber)subscribers[subscriber];
            Subscriber sendersSubscriberItem = new Subscriber();

            Hashtable Rosters = JabberServer.RosterHashtable;//get reference to rosters


            //prepare sender's roster
            UserRoster senderRoster = Rosters[this.user] as UserRoster;//getting senders roster
            if (senderRoster == null)
            {
                senderRoster = new UserRoster();
                senderRoster.user = this.user;
            }
            if (!Rosters.ContainsKey(this.user))
            {
                Rosters.Add(this.user, senderRoster);
            }

            //prepare recipients roster
            UserRoster recipientRoster = Rosters[recipientID.User] as UserRoster;//getting recipients roster
            if (recipientRoster == null)
            {
                recipientRoster = new UserRoster();
                recipientRoster.user = recipientID.User;
            }
            if (!Rosters.ContainsKey(recipientID.User))
            {
                Rosters.Add(recipientID.User, recipientRoster);
            }

            sendersSubscriberItem = senderRoster.subscribers[subscriber] as Subscriber;//extra

            if (sendersSubscriberItem == null)
            {
                //create subscription item
                sendersSubscriberItem = new Subscriber();
                sendersSubscriberItem.subscription = "none";
                senderRoster.subscribers.Add(recipient, sendersSubscriberItem);
                Packet itempacket = new Packet("item");
                itempacket.setAttribute("jid", subscriber);
                senderRoster.items.Add(subscriber, itempacket);
            }

            //begin subscription managment
            if (type.Equals("subscribe"))
            {
                sendersSubscriberItem.ask = type;//set up subscription status
            }
            else if (type.Equals("subscribed"))//if subscription accepted
            {
                sendersSubscriberItem.ask = null;
                if (sendersSubscriberItem.subscription.Equals("from"))
                {
                    sendersSubscriberItem.subscription = "both";
                }
                else if (sendersSubscriberItem.subscription.Equals("none"))
                {
                    sendersSubscriberItem.subscription = "to";
                }

            }
            else if (type.Equals("unsubscribed"))
            {//sender does not want to give presence updates to recipient
                sendersSubscriberItem.ask = null;

                if (sendersSubscriberItem.subscription.Equals("to"))
                {
                    sendersSubscriberItem.subscription = "none";
                }

                else if (sendersSubscriberItem.subscription.Equals("both"))
                {
                    sendersSubscriberItem.subscription = "from";
                }


                //notify recipient of sender's unavailble status:
                Packet unavailiblePacket = new Packet("presence");
                unavailiblePacket.setFrom(presence.From);
                unavailiblePacket.To = presence.To;
                unavailiblePacket.setAttribute("type", "unavailable");
                MessageHandler.deliverPacket(unavailiblePacket);

            }
            else if (type.Equals("unsubscribe"))
            {//sender no longer interested in recieving presence updates from recipient
                sendersSubscriberItem.ask = null;

                if (sendersSubscriberItem.subscription.Equals("both"))
                {
                    sendersSubscriberItem.subscription = "to";
                }
                else if (sendersSubscriberItem.subscription.Equals("from"))
                {
                    sendersSubscriberItem.subscription = "none";
                }

            }
            //update the corresponding changes in <items> table in sender's record used for delivery
            Packet item = (Packet)senderRoster.items[subscriber];
            if (item != null)
            {
                item.setAttribute("subscription", sendersSubscriberItem.subscription);
                item.setAttribute("ask", sendersSubscriberItem.ask);
                Packet iq = new Packet("iq");
                iq.Type = "set";
                Packet query = new Packet("query");
                query.setAttribute("xmlns", "jabber:iq:roster");
                query.setParent(iq);
                item.setParent(query);
                iq.To = this.user + "@" + Server.JabberServer.server_name;

                //Forward the subscription packet to recipient
                MessageHandler.deliverPacketToAll(user, iq);//may need some correction!
            }

            //processing of recipients roster begins
            if (sendersSubscriberItem.ask == null)
            {
                //the recipient user rosters update!
                Subscriber recipientsSubscriberItem = recipientRoster.subscribers[user + "@" + JabberServer.server_name] as Subscriber;//extra
                if (recipientsSubscriberItem == null)
                {
                    //create subscription item
                    recipientsSubscriberItem = new Subscriber();
                    recipientsSubscriberItem.subscription = "none";
                    recipientRoster.subscribers.Add(user + "@" + JabberServer.server_name, recipientsSubscriberItem);
                }

                //if type is subscribe, changes in this roster are not needed!!!

                if (type.Equals("subscribed"))//if subscription accepted
                {
                    recipientsSubscriberItem.ask = null;
                    if (recipientsSubscriberItem.subscription.Equals("none"))
                    {
                        recipientsSubscriberItem.subscription = "from";
                    }

                    else if (recipientsSubscriberItem.subscription.Equals("to"))
                    {
                        recipientsSubscriberItem.subscription = "both";
                    }

                }
                else if (type.Equals("unsubscribed"))
                {//sender no longer interested in giving presence updates to recipient
                    recipientsSubscriberItem.ask = null;
                    if (recipientsSubscriberItem.subscription.Equals("both"))
                    {
                        recipientsSubscriberItem.subscription = "to";
                    }
                    else if (recipientsSubscriberItem.subscription.Equals("from"))
                    {
                        recipientsSubscriberItem.subscription = "none";
                    }

                }
                else if (type.Equals("unsubscribe"))
                {//sender no longer interested in recieving presence updates from recipient
                    recipientsSubscriberItem.ask = null;
                    if (recipientsSubscriberItem.subscription.Equals("to"))
                    {
                        recipientsSubscriberItem.subscription = "none";
                    }

                    else if (recipientsSubscriberItem.subscription.Equals("both"))
                    {
                        recipientsSubscriberItem.subscription = "from";
                    }

                    //notify sender of unavailble status:
                    Packet unavailiblePacket = new Packet("presence");
                    unavailiblePacket.setFrom(presence.To);
                    unavailiblePacket.To = presence.From;
                    unavailiblePacket.setAttribute("type", "unavailable");
                    MessageHandler.deliverPacket(unavailiblePacket);

                }

                //update the corresponding changes in <items> table in sender's record used for delivery
                Packet item2 = (Packet)recipientRoster.items[user + "@" + JabberServer.server_name];
                if (item2 != null)
                {
                    item2.setAttribute("subscription", recipientsSubscriberItem.subscription);
                    item2.setAttribute("ask", recipientsSubscriberItem.ask);
                    Packet iq = new Packet("iq");
                    iq.Type = "set";
                    Packet query = new Packet("query");
                    query.setAttribute("xmlns", "jabber:iq:roster");
                    query.setParent(iq);
                    item2.setParent(query);
                    iq.To = recipientID.User + "@" + recipientID.Domain;
                    //Forward the subscription packet to recipient
                    MessageHandler.deliverPacketToAll(user, iq);//may need some correction!
                }

            }

            MessageHandler.deliverPacket(presence);


        }//UpdatePresense

        public void updateSubscribers(Packet packet)
        {//checked, works!
            if ((Server.JabberServer.RosterHashtable[this.user] as UserRoster) != null)
            {
                foreach (object obj in (Server.JabberServer.RosterHashtable[this.user] as UserRoster).subscribers.Keys)
                {
                    Subscriber sub = (Server.JabberServer.RosterHashtable[this.user] as UserRoster).subscribers[obj] as Subscriber;
                    if (sub.subscription.Equals("to") || sub.subscription.Equals("both"))//inform only subscribers!
                    {
                        packet.To = obj as String;
                        MessageHandler.deliverPacket(packet);
                    }
                }
            }
        }//Update subscribers


        public Packet getPacket()
        {
            Packet packet = new Packet("query");
            packet.setAttribute("xmlns", "jabber:iq:roster");
            //works, checked!:
            if ((Server.JabberServer.RosterHashtable[this.user] as UserRoster) != null)
            {
                foreach (object obj in (Server.JabberServer.RosterHashtable[this.user] as UserRoster).items.Values)
                {
                    ((Packet)obj).Parent = packet;
                }
                return packet;
            }
            else return packet;
        }//getPacket

        public void updateRoster(Packet packet)
        {
            //Extract the query packet
            Packet rosterQuery = packet.getFirstChild("query");
            rosterQuery.setAttribute("xmlns", "jabber:iq:roster");
            foreach (object rosterItems in rosterQuery.getChildren())
            {
                //for each <item> packet in the query
                Object child = rosterItems;
                if (child is Packet)
                {
                    Packet itemPacket = child as Packet;
                    String subJID = itemPacket.getAttribute("jid");



                    //create Subscriber object if needed
                    //Subscriber sendersSubscriberItem = this.subscribers[subJID] as Subscriber;
                    if ((Server.JabberServer.RosterHashtable[this.user] as UserRoster) == null)
                    {
                        Server.JabberServer.RosterHashtable[this.user] = new UserRoster();
                    }
                    Subscriber sub = (Server.JabberServer.RosterHashtable[this.user] as UserRoster).subscribers[subJID] as Subscriber;
                    if (sub == null)
                    {
                        sub = new Subscriber();
                        sub.subscription = "none";
                        sub.ask = null;
                        (Server.JabberServer.RosterHashtable[this.user] as UserRoster).subscribers.Add(subJID, sub);
                    }

                    if (itemPacket.getAttribute("subscription") != null)
                    {
                        //signal to remove
                        if (itemPacket.getAttribute("subscription").Equals("remove"))
                        {
                            //remove from subscribers item
                            JabberID otherID = new JabberID(subJID);
                            (Server.JabberServer.RosterHashtable[this.user] as UserRoster).subscribers.Remove(otherID.ToString());
                            if ((Server.JabberServer.RosterHashtable[this.user] as UserRoster).items.ContainsKey(subJID))
                            {
                                //remove <item> packet
                                (Server.JabberServer.RosterHashtable[this.user] as UserRoster).items.Remove(subJID);
                            }

                            if ((Server.JabberServer.RosterHashtable[otherID.User] as UserRoster) == null)
                            {
                                Server.JabberServer.RosterHashtable[otherID.User] = new UserRoster();
                            }
                            //set only subscription=none from other item
                            sub = ((Server.JabberServer.RosterHashtable[otherID.User] as UserRoster).subscribers[this.user + "@" + Server.JabberServer.server_name]) as Subscriber;
                            if (sub == null)
                            {
                                sub = new Subscriber();
                            }
                            sub.subscription = "none";
                            sub.ask = null;
                            (Server.JabberServer.RosterHashtable[otherID.User] as UserRoster).subscribers[this.user + "@" + Server.JabberServer.server_name] = sub;
                            //(Server.JabberServer.RosterHashtable[subJID] as UserRoster).subscribers.Add(subJID, sendersSubscriberItem);
                            //}
                            if ((Server.JabberServer.RosterHashtable[otherID.User] as UserRoster).items.ContainsKey(this.user + "@" + Server.JabberServer.server_name))
                            {
                                //update <item> packet
                                //itemPacket.removeAttribute("subscription");

                                Packet mypacket = (Server.JabberServer.RosterHashtable[otherID.User] as UserRoster).items[this.user + "@" + Server.JabberServer.server_name] as Packet;
                                mypacket.setAttribute("subscription", "none");
                                mypacket.setAttribute("ask", null);
                                (Server.JabberServer.RosterHashtable[otherID.User] as UserRoster).items[this.user + "@" + Server.JabberServer.server_name] = mypacket;
                            }
                        }



                    }
                    else
                    {

                        //update <item> with the subscriber info for roster push
                        itemPacket.setAttribute("subscription", sub.subscription);
                        itemPacket.setAttribute("ask", sub.ask);

                        //Packet test = items[subJID];
                        if (!(Server.JabberServer.RosterHashtable[this.user] as UserRoster).items.ContainsKey(subJID))
                        {
                            //store updated <item> packet
                            (Server.JabberServer.RosterHashtable[this.user] as UserRoster).items.Add(subJID, itemPacket);
                        }
                        else
                        {
                            (Server.JabberServer.RosterHashtable[this.user] as UserRoster).items[subJID] = itemPacket;
                        }
                    }
                }
            }
            //Roster push
            packet.Type = "set";
            JabberID jidTo = packet.getSession().getJID();
            packet.To = jidTo.User + "@" + jidTo.Domain;
            packet.removeAttribute("from");
            //needs to be verified
            MessageHandler.deliverPacket(packet);
        }//update roster
        public void informSubscriber()
        {
            //needs to be checked!
            if ((Server.JabberServer.RosterHashtable[this.user] as UserRoster) != null)
            {
                foreach (object obj in (Server.JabberServer.RosterHashtable[this.user] as UserRoster).subscribers.Keys)
                {
                    Subscriber sub = (Server.JabberServer.RosterHashtable[this.user] as UserRoster).subscribers[obj] as Subscriber;
                    if (sub.subscription.Equals("from") || sub.subscription.Equals("both"))//inform sender of his subscribers status!
                    {
                        JabberID myid = new JabberID(obj.ToString());//other's subscribers id
                        if (myid.Domain.Equals(Server.JabberServer.server_name))//if from this server
                        {
                            if (Server.JabberServer.getUserIndex().getUser(myid.User).getSessions().Values == null)
                            {
                                // if Server.JabberServer.getUserIndex().getUser(myid.User).getSessions()
                                //user is online
                                Packet presencePacket = new Packet("presence");
                                presencePacket.From = myid.ToString();
                                presencePacket.To = new JabberID(this.user + "@" + Server.JabberServer.server_name).ToString();
                                presencePacket.setAttribute("type", "availible");
                                MessageHandler.deliverPacket(presencePacket);

                            }// if (Server.JabberServer.getUserIndex().getUser(myid.User).getSessions().Values==null)
                            else
                            {
                                //user is offline
                                Packet presencePacket = new Packet("presence");
                                presencePacket.From = myid.ToString();
                                presencePacket.To = new JabberID(this.user + "@" + Server.JabberServer.server_name).ToString();
                                presencePacket.setAttribute("type", "unavailible");
                                MessageHandler.deliverPacket(presencePacket);
                            }

                        }//  if(myid.Domain.Equals(Server.JabberServer.server_name))
                        else
                        {
                            //generate probe for S2S communication!@TODO
                        }
                        
                    }//if (sub.subscription.Equals("from") || sub.subscription.Equals("both"))
                }//foreach (object obj in (Server.JabberServer.RosterHashtable[this.user] as UserRoster).subscribers.Keys)

            }//if ((Server.JabberServer.RosterHashtable[this.user] as UserRoster) != null)


        }
    }
}