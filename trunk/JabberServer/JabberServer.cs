using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Data;
using Goodware.Jabber.Library;


namespace Goodware.Jabber.Server {
	

	public class JabberServer {

		public static Hashtable RosterHashtable = new Hashtable();//needed for new roster managment

		public static int jabber_port = 5222;
		public static String server_name = "localhost";
		public static String file_name = "users.xml";


		static UserIndex index = new UserIndex();
		public static UserIndex getUserIndex()
		{ 
			return index;
		}
		public static PacketQueue packetQueue = new PacketQueue();

		protected JabberServer() {
/*            Packet temp = new Packet("<iq type='set' id='reg_id'> <query xmlns='jabber:iq:register'> <username> enci </username> <password> encienci </password> </query> </iq>");
            String temp1 = temp.getChildValue("query");
            if (temp1 != null) {
                Console.WriteLine(temp1.ToString());
            } else {
                Console.WriteLine(temp1);                
            }
            Console.ReadLine();
            return;*/

			restoreFromFile();



            Authenticator.randomToken();
            createQueueThread();

//			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//			IPEndPoint ep = new IPEndPoint(IPAddress.Any, jabber_port);
//			serverSocket.Bind(ep);
//			serverSocket.Listen(20);
				ListenerThread lt = new ListenerThread(JabberServer.jabber_port);

				//
				// mozebi dodavanje na staticki promenlivi za polesen pristap od sekade
				
            // Додадено од Милош/Васко
            // Нов „статички“ јузер

			/*
            User nov=index.addUser("milos");
            nov.setPassword("test");

            User nov2 = index.addUser("darko");
            nov2.setPassword("test");
            
            User nov3 = index.addUser("bojan");
            nov3.setPassword("test");
           */
 
            // Крај додадено

            

//			while (true) {
//				Socket newSock = serverSocket.Accept();
//				Session session = new Session(newSock);

//				ProcessThread processor = new ProcessThread(packetQueue, session);
//				processor.start();
//			}
				lt.setDaemon(true);
				lt.start();



				while (!Console.ReadLine().Equals("stop")) {
				}
				packetQueue.enqueue(new Packet("terminate")); //artificial packet for signaling termination
		}

		void createQueueThread() {
			QueueThread qThread = new QueueThread(packetQueue);

			//qThread.setDaemon(true); DO NOT set Daemon

			qThread.addPacketListener(new OpenStreamHandler(index), "stream:stream");
			qThread.addPacketListener(new CloseStreamHandler(index), "/stream:stream");
			qThread.addPacketListener(new MessageHandler(index), "message");
			qThread.addPacketListener(new PresenceHandler(index), "presence");

			//Додадено од Васко
			
            qThread.addPacketListener(new RegisterHandler(index),"jabber:iq:register");            
            qThread.addPacketListener(new AuthHandler(index),"jabber:iq:auth");

			//Крај додадено

            //Added by Marko:
            qThread.addPacketListener(new RosterHandler(index), "jabber:iq:roster");
            //end added

			qThread.start();

		}

		private void restoreFromFile() {
			DataSet ds = new DataSet();
			try {
				ds.ReadXml(file_name);
			} catch (FileNotFoundException ex) {
				return;
			}


			foreach (DataRow user in ds.Tables["User"].Rows) {
				User u = getUserIndex().addUser(user["username"] as String);
				u.setPassword(user["password"] as String);
				u.setHash(user["hash"] as String);
				u.setSequence(user["sequence"] as String);
				u.setToken(user["token"] as String);

				foreach (DataRow item in user.GetChildRows("Roster")) {
					UserRoster ur = (UserRoster)RosterHashtable[item["username"]];
					Hashtable itemsHash = ur.items;
					Hashtable subscribersHash = ur.subscribers;

					Packet itemPacket = new Packet("item");
					itemPacket["jid"] = item["jid"] as String;
					itemPacket["name"] = item["name"] as String;
					itemPacket["subscription"] = "both";
					String group = item["group"] as String;
					if (group != null) {
						new Packet("group",group).Parent = itemPacket;
					}
					itemsHash[item["jid"]] = itemPacket;

					Subscriber s = new Subscriber();
					s.subscription = "both";
					subscribersHash[item["jid"]] = s;

				}


			}





		}
		 
		 public static void Main(String[] args) {
			Console.WriteLine("Jabber Server -- " + server_name + ":" + jabber_port);
			new JabberServer();

		}
	}

}