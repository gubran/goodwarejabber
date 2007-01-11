using System;
using System.Xml;
using System.Collections.Generic;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server{
    public class JabberInputHandler
    {
		PacketQueue packetQ;
		Session session;

		Packet packet;

		public JabberInputHandler(PacketQueue packetQ) {
			this.packetQ = packetQ;
		}

		public void process(Session session) {
			//Assembly assem = Assembly.LoadFrom(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\SaxExpat.dll");
			//IXmlReader reader = SaxReaderFactory.CreateReader(assem, null);
			//reader.ContentHandler = this;
			this.session = session;
			//reader.Parse(new ReaderInputSource(session.getReader()));

			XmlReader reader = XmlReader.Create(session.Reader);


            Console.WriteLine("JIH:server version");

			reader.MoveToContent();


			try {
				do {
					switch (reader.Depth) {
						case 0:
							switch (reader.NodeType) {
								case XmlNodeType.Element:
									if (reader.Name == "stream:stream") {
										Dictionary<String, String> atts = getAttributes(reader);
										Packet openPacket = new Packet(null, reader.Name, reader.NamespaceURI, atts);
										openPacket.Session = (session);
										String from = atts["from"];
										session.JID = (new JabberID(from));
										packetQ.enqueue(openPacket);

									} else {
										throw new XmlException("Root node must be <stream:stream>");
									}
									break;
								case XmlNodeType.EndElement:
									Packet closePacket = new Packet("/stream:stream");
									closePacket.Session = (session);
									packetQ.enqueue(closePacket);
									break;
							}
							break;
						case 1:
							switch (reader.NodeType) {
								case XmlNodeType.Element:
									Dictionary<String, String> atts = getAttributes(reader);
									packet = new Packet(null, reader.Name, reader.NamespaceURI, atts);
									packet.Session = (session);
									if (reader.IsEmptyElement)
										goto case XmlNodeType.EndElement;
									break;
								case XmlNodeType.EndElement:
									packetQ.enqueue(packet);
									break;
							}
							break;
						default:
							switch (reader.NodeType) {
								case XmlNodeType.Element:
									Dictionary<String, String> atts = getAttributes(reader);
									Packet child = new Packet(packet, reader.Name, reader.NamespaceURI, atts);
									packet = child;
									if (reader.IsEmptyElement)
										goto case XmlNodeType.EndElement;
									break;
									
								case XmlNodeType.EndElement:
									packet = packet.Parent;
									break;
								case XmlNodeType.Text:
									packet.Children.Add(reader.Value);
									break;

							}
							break;
					}
				} while (reader.Read());

			} catch (Exception ex) {
                //Console.WriteLine(ex.ToString());
				// Bilokakov problem so xml stream-ot
				// io-exception, invalid xml, zatvoren socket i sl.
				// treba da se napravi clean-up : zatvori stream </stream:stream>, zatvori socket, trgni Session object i sl.
				
				// Ne sekogas znaci greska : posle </stream:stream> se zatvara socket-ot i ne treba da se pravi nisto
 


                UserIndex userIndex = JabberServer.getUserIndex();
                if (!userIndex.containsSession(session))
                {
                    return;
                }
                //clean-up code (copied from CloseStreamHandler)
                try
                {
                    //      Log.trace("Closing session");
                    //Session session = packet.Session;

                    //session.Writer.Write("</stream:stream> ");
                    //session.Writer.Flush();

                    //notify other subscribers that user is unavailble
                    //by Marko
                    //begin
                    JabberID userid = session.getJID();

                    Packet unavailiblePacket = new Packet("presence");
                    unavailiblePacket.setFrom(userid.ToString());
                    unavailiblePacket.setAttribute("type", "unavailable");
                    userIndex.getUser(userid.User).getRoster().updateSubscribers(unavailiblePacket);
                    //it is not tested, but it should work
                    //end



						  //send groupchat presence unavailable & remove from groups
						  String jid = userid.ToString();

						  Packet presence = new Packet("presence");
						  presence.Type = "unavailable";
						  foreach (KeyValuePair<String, GroupChatManager.Group> kvp in GroupChatManager.Manager.groups) {

							  GroupChatManager.Group group = kvp.Value;
							  try {

								  String nick = group.jid2nick[jid]; // test whether the user is in the group
								  presence.From = group.JabberID + "/" + nick;
								  GroupChatManager.Manager.removeUser(group, jid);   // first remove then deliver so that the packet does not come back
								  GroupChatManager.Manager.deliverToGroup(group, presence);

							  } catch (Exception ex) {
							  }
						  }
						  // end groupchar clean-up	

                    //session.Socket.Close();
                    userIndex.removeSession(session);
                    //      Log.trace("Closed session");
                }
                catch (Exception e)
                {
                    //      Log.error("CloseStreamHandler: ",ex);
                    userIndex.removeSession(packet.Session);
                    //      Log.trace("Exception closed session");
                }



			}

			
		}


		private static Dictionary<String, String> getAttributes(XmlReader reader) {
			Dictionary<String, String> attributes = new Dictionary<string, string>();
			while (reader.MoveToNextAttribute()) {
				attributes[reader.Name] = reader.Value;
			}
			reader.MoveToElement();

			return attributes;

		}

		
		//public override void StartElement(string uri, string localName, string qName, IAttributes atts) {
		//    switch(depth++) {
		//        case 0:
		//            if(qName.Equals("stream:stream")) {
		//                Packet openPacket = new Packet(null, qName, uri, atts);
		//                openPacket.setSession(session);
		//                String from = atts.GetValue("from");
		//                session.setJID(new JabberID(from));
		//                packetQ.enqueue(openPacket);
		//                return;
		//            } else {
		//                throw new SaxException("Root element must be <stream:stream>");
		//            }
		//        case 1:
		//            packet = new Packet(null, qName, uri, atts);
		//            packet.setSession(session);
		//            break;
		//        default:
		//            Packet child = new Packet(packet, qName, uri, atts);
		//            packet = child;
		//            break;
		//    }
		//}

		//public override void Characters(char[] ch, int start, int length) {
		//    if(depth > 1) {
		//        packet.getChildren().Add(new String(ch, start, length));
		//    }
		//}

		//public override void EndElement(string uri, string localName, string qName) {
		//    switch(--depth) {
		//        case 0:
		//            Packet closePacket = new Packet("/stream:stream");
		//            closePacket.setSession(session);
		//            packetQ.enqueue(closePacket);
		//            break;
		//        case 1:
		//            packetQ.enqueue(packet);
		//            break;
		//        default:
		//            packet = packet.getParent();
		//            break;
		//    }
		//}
	}


}