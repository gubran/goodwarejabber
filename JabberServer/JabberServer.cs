using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Data;
using System.Threading;
using System.ServiceProcess;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server {

    public partial class JabberServer : ServiceBase {

        public static int jabber_port = 5222;
        public static String server_name = "localhost";

        static System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
        public static String users_file_name = a.Location.Remove(a.Location.LastIndexOf(@"\") + 1) + @"users.xml";
        public static String log_file_name = a.Location.Remove(a.Location.LastIndexOf(@"\") + 1) + @"log.log";

        public static TextWriter output = File.CreateText(log_file_name);

        public static int dueTime = 1000;
        public static int period = 60000;

        static UserIndex index = new UserIndex();
        public static UserIndex getUserIndex() {
            return index;
        }
        public static PacketQueue packetQueue = new PacketQueue();
        public static Hashtable RosterHashtable = new Hashtable();

        protected JabberServer() {
            InitializeComponent();
        }

        protected void JabberServerStart() {
            restoreFromFile();

            Authenticator.randomToken();
            createQueueThread();

            //			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //			IPEndPoint ep = new IPEndPoint(IPAddress.Any, jabber_port);
            //			serverSocket.Bind(ep);
            //			serverSocket.Listen(20);
            ListenerThread lt = new ListenerThread(JabberServer.jabber_port);

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


            Timer timer = new Timer(new TimerCallback(TimerProc));
            timer.Change(dueTime, period);


            //while (!JabberServer.output.ReadLine().Equals("stop")) {
            //}
            //packetQueue.enqueue(new Packet("terminate")); //artificial packet for signaling termination
        }

        private void TimerProc(Object state) {
            Packet savePacket = new Packet("savestate");
            packetQueue.enqueue(savePacket);
        }

        void createQueueThread() {
            QueueThread qThread = new QueueThread(packetQueue);

            //qThread.setDaemon(true); DO NOT set Daemon

            qThread.addPacketListener(new OpenStreamHandler(index), "stream:stream");
            qThread.addPacketListener(new CloseStreamHandler(index), "/stream:stream");
            qThread.addPacketListener(new MessageHandler(index), "message");
            qThread.addPacketListener(new PresenceHandler(index), "presence");
            qThread.addPacketListener(new RegisterHandler(index), "jabber:iq:register");
            qThread.addPacketListener(new AuthHandler(index), "jabber:iq:auth");
            qThread.addPacketListener(new RosterHandler(index), "jabber:iq:roster");
            qThread.addPacketListener(new SaveStateHandler(index), "savestate");
            qThread.start();
        }

        private void restoreFromFile() {
            DataSet ds = new DataSet();
            try {
                ds.ReadXml(users_file_name);
            } catch (FileNotFoundException ex) {
                JabberServer.output.WriteLine("Could not load User database!" + ex.Message);
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
                        new Packet("group", group).Parent = itemPacket;
                    }
                    itemsHash[item["jid"]] = itemPacket;

                    Subscriber s = new Subscriber();
                    s.subscription = "both";
                    subscribersHash[item["jid"]] = s;
                }
            }
        }

        //public static void Main(String[] args) {
        //    JabberServer.output.WriteLine("Jabber Server -- " + server_name + ":" + jabber_port);
        //    new JabberServer();
        //}

        protected override void OnStart(String[] args) {
            JabberServerStart();
        }
        protected override void OnStop() {
            packetQueue.enqueue(new Packet("terminate")); //artificial packet for signaling termination
        }
        public static void Main() {
            JabberServer.output.WriteLine("Jabber Server -- " + server_name + ":" + jabber_port);
            ServiceBase.Run(new JabberServer());
            //new JabberServer().JabberServerStart();
        }
    }

}