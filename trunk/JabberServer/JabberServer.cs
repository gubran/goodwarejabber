using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Goodware.Jabber.Library;
using System.Collections;


namespace Goodware.Jabber.Server {
	

    public class JabberServer {

        //addeed by marko
        public static Hashtable RosterHashtable = new Hashtable();//needed for new roster managment
        //end added
        
        public static int jabber_port = 5222;
		public static String server_name = "localhost";

        static UserIndex index = new UserIndex();
        public static UserIndex getUserIndex()
        { 
            return index;
        }
        PacketQueue packetQueue = new PacketQueue();

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
            Authenticator.randomToken();
            createQueueThread();

			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint ep = new IPEndPoint(IPAddress.Any, jabber_port);
			serverSocket.Bind(ep);
			serverSocket.Listen(20);
            // Додадено од Милош/Васко
            // Нов „статички“ јузер
            User nov=index.addUser("misos");
            nov.setPassword("test");

            User nov2 = index.addUser("misos2");
            nov2.setPassword("test");
            
            User nov3 = index.addUser("misos3");
            nov3.setPassword("test");
            
            // Крај додадено

            

			while (true) {
				Socket newSock = serverSocket.Accept();
				Session session = new Session(newSock);

				ProcessThread processor = new ProcessThread(packetQueue, session);
				processor.start();
			}

		}

		void createQueueThread() {
			QueueThread qThread = new QueueThread(packetQueue);

			qThread.setDaemon(true);

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


		public static void Main(String[] args) {
			Console.WriteLine("Jabber Server -- " + server_name + ":" + jabber_port);
			new JabberServer();

		}
	}

}