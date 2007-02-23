using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Net.Sockets;
using Goodware.Jabber.Library;
using Goodware.Jabber.GUI;

namespace Goodware.Jabber.Client {
	public class JabberModel {

		public JustTalk gui;
		private QueueThread queueThread;
		private PacketQueue packetQueue;
		private Session session;
		String jabberVersion = "v. 1.0 - ch. 4";
		String sName;
		String sAddress;
		String sPort;
		String user;
		String resource;
		public Contact Me;

		public JabberModel(JustTalk gui) {
			this.gui = gui;
			packetQueue = new PacketQueue();
			queueThread = new QueueThread(packetQueue);
			queueThread.addPacketListener(new OpenStreamHandler(), "stream:stream");
			queueThread.addPacketListener(new CloseStreamHandler(this), "/stream:stream");
			queueThread.addPacketListener(new MessageHandler(this), "message");

			//Додадено од Милош/Васко
			queueThread.addPacketListener(new AuthHandler(this), "jabber:iq:auth");

			queueThread.addPacketListener(new IQHandler(this), "iq");
			//Крај додадено

			//marko
			queueThread.addPacketListener(new RosterHandler(this), "jabber:iq:roster");
			//kraj marko

			queueThread.addPacketListener(new PresenceHandler(this), "presence");
			queueThread.addPacketListener(new RegisterHandler(this), "jabber:iq:register");
			queueThread.setDaemon(true);
			queueThread.start();
		}


		public String Version {
			get {
				return jabberVersion;
			}
		}

		public String ServerName {
			get {
				return sName;
			}
			set {
				sName = value;
			}
		}

		public String ServerAddress {
			get {
				return sAddress;
			}
			set {
				sAddress = value;
			}
		}

		public String Port {
			get {
				return sPort;
			}
			set {
				sPort = value;
			}
		}

		public String User {
			get {
				return user;
			}
			set {
				user = value;
			}
		}

		public String Resource {
			get {
				return resource;
			}
			set {
				resource = value;
			}
		}

		public void addStatusListener(StatusListener listener) {
			session.addStatusListener(listener);
		}

		public void removeStatusListener(StatusListener listener) {
			session.removeStatusListener(listener);
		}

		public int SessionStatus {
			get {
				return Convert.ToInt32(session.Status);
			}
		}

		public void connect(String server, int port, String serverName, String user, String resource) {
			TcpClient tempClient = new TcpClient(server, port);		// TcpClient eases connection
			session = new Session(tempClient.Client);				// Get the socket of the TcpClient
			session.Status = (Session.SessionStatus.connected);
			(new ProcessThread(packetQueue, session, this)).start();
			string senderJabID = user + "@" + serverName + "/" + resource;
			StreamWriter output = session.Writer;
			session.JID = (new JabberID(User, ServerName, resource));
			output.Write("<?xml version='1.0' encoding='utf-8' ?>");
			output.Write("<stream:stream to='");
			output.Write(serverName);
			output.Write("' from='");
			output.Write(senderJabID);
			output.Write("' xmlns='jabber:client' ");
			output.Write("xmlns:stream='http://etherx.jabber.org/streams'>");
			output.Flush();
		}

		public void connect() {
			TcpClient tempClient = new TcpClient(ServerAddress, Convert.ToInt32(Port));	// TcpClient eases connection
			session = new Session(tempClient.Client);									// Get the socket of the TcpClient
			session.Status = (Session.SessionStatus.connected);
			(new ProcessThread(packetQueue, session, this)).start();
			string senderJabID = User + "@" + ServerName + "/" + Resource;
			StreamWriter output = session.Writer;
			session.JID = (new JabberID(User, ServerName, Resource));
			output.Write("<?xml version='1.0' encoding='utf-8' ?>");
			output.Write("<stream:stream to='");
			output.Write(ServerName);
			output.Write("' from='");
			output.Write(senderJabID);
			output.Write("' xmlns='jabber:client' ");
			output.Write("xmlns:stream='http://etherx.jabber.org/streams'>");
			output.Flush();
		}

		public void disconnect() {
			session.Writer.Write("</stream:stream> ");
			session.Writer.Flush();		
		}

		public void sendMessage(string recipient, string subject, string thread, string type, string id, string body) {
			Packet packet = new Packet("message");
			if (recipient != null) {
				packet.To = (recipient);
			}
			if (id != null) {
				packet.ID = (id);
			}
			if (type != null) {
				packet.Type = (type);
			}
			if (subject != null) {
				packet.Children.Add(new Packet("subject", subject));
			}
			if (thread != null) {
				packet.Children.Add(new Packet("thread", thread));
			}
			if (body != null) {
				packet.Children.Add(new Packet("body", body));
			}
			packet.From = Me.JabberID;
			
			session.Writer.Write(packet.ToString()); ;
			session.Writer.Flush();
		}

		public void sendPresence(String recipient, String type, String show, String status, String prioriry) {
			Packet packet = new Packet("presence");

			packet.From = User + "@" + ServerName + "/" + Resource;
			if (recipient != null) {
				packet.To = recipient;
			}
			if (type != null) {
				packet.Type = type;
			}
			if (show != null) {
				packet.Children.Add(new Packet("show", show));
			}
			if (status != null) {
				packet.Children.Add(new Packet("status", status));
			}
			if (prioriry != null) {
				packet.Children.Add(new Packet("priority", prioriry));
			}

			packet.From = Me.JabberID;
			session.Writer.Write(packet.ToString());
			session.Writer.Flush();
		}

		public void sendPresence(Status status, String statusMessage) {
			switch(status) {
				case Status.unavailable :
					sendPresence(null, "unavailable", null, statusMessage, null);
					break;
				case Status.chat:
				case Status.away:
				case Status.xa:
				case Status.dnd:
					sendPresence(null, "available", status.ToString(), statusMessage, null);
					break;

			}


		}

		//Додадено од Милош/Васко
		/// <summary>
		/// Тип на автентикација
		/// </summary>
		Authenticator authenticator = new Authenticator();
		String authmode;
		public String AuthMode {
			get {
				return this.authmode;
			}
			set {
				this.authmode = value;
			}
		}
		/// <summary>
		/// Лозинка
		/// </summary>
		String password;
		public String Password {
			get {
				return this.password;
			}
			set {
				this.password = value;
			}
		}

		PacketListener authHandler;
		Hashtable resultHandlers = new Hashtable();

		public void addResultHandler(String id_code, PacketListener listener) {
			resultHandlers.Add(id_code, listener);
		}

		public PacketListener removeResultHandler(String id_code) {
			PacketListener result = (PacketListener)resultHandlers[id_code];
			resultHandlers.Remove(id_code);
			return result;
		}

		public void register() {
			StreamWriter output = session.Writer;
			String temp = "<iq type='set' id='reg_id'><query xmlns='jabber:iq:register'><username>" +
this.user + "</username><password>" + this.Password + "</password></query></iq>";
			/*            output.Write("<iq type='set' id='reg_id'><query xmlns='jabber:iq:register'>");
						output.Write("<username>");
						output.Write(this.user);
						output.Write("</username><password>");
						output.Write(this.Password);
						output.Write("</password></query></iq>");*/
			output.Write(temp);
			output.Flush();
			addResultHandler("reg_id", new RegisterHandler(this));
		}

		public void registerOK() {
			String token = Authenticator.randomToken();
			String hash = authenticator.getZeroKHash(100, Encoding.UTF8.GetBytes(token), Encoding.UTF8.GetBytes(Password));
			StreamWriter output = session.Writer;
			output.Write("<iq type='set' id='reg_id'><query xmlns='jabber:iq:register'>");
			output.Write("<username>");
			output.Write(this.user);
			output.Write("</username><sequence>");
			output.Write("100");
			output.Write("</sequence><token>");
			output.Write(token);
			output.Write("</token><hash>");
			output.Write(hash);
			output.Write("</hash></query></iq>");
			output.Flush();
			addResultHandler("reg_id", new RegisterHandler(this));
			Console.WriteLine("Register OK");
		}


		int counter;
		/// <summary>
		/// Автентицирање со методи за трите типа. Ќе треба да фрлаат IOException
		/// </summary>
		public void authenticate() {
			if (this.AuthMode.Equals("Ok")) {
				authenticateOK();
			} else if (this.AuthMode.Equals("digest")) {
				authenticateDigest();
			} else {
				authenticatePlain();
			}
		}

		void authenticatePlain() {
			addResultHandler("plain_auth_" + Convert.ToString(counter), authHandler);
			StreamWriter output = session.Writer;
			output.Write("<iq type='set' id='plain_auth_");
			output.Write(Convert.ToString(counter++));
			output.Write("'><query xmlns='jabber:iq:auth'><username>");
			output.Write(this.user);
			output.Write("</username><resource>");
			output.Write(this.resource);
			output.Write("</resource><password>");
			output.Write(this.Password);
			output.Write("</password></query></iq>");
			output.Flush();
			Console.WriteLine("Sent plain_auth\n");
		}

		void authenticateDigest() {
			addResultHandler("digest_auth_" + Convert.ToString(counter), authHandler);
			StreamWriter output = session.Writer;
			output.Write("<iq type='set' id='digest_auth_");
			output.Write(Convert.ToString(counter++));
			output.Write("'><query xmlns='jabber:iq:auth'><username>");
			output.Write(this.user);
			output.Write("</username><resource>");
			output.Write(this.resource);
			output.Write("</resource><digest>");
			output.Write(authenticator.getDigest(session.StreamID, this.Password));
			output.Write("</digest></query></iq>");
			output.Flush();
		}

		void authenticateOK() {
			StreamWriter output = session.Writer;
			output.Write("<iq type='get' id='auth_get_");
			output.Write(Convert.ToString(counter++));
			output.Write("'><query xmlns='jabber:iq:auth'><username>");
			output.Write(this.user);
			output.Write("</username></query></iq>");
			output.Flush();
		}
		//Крај додадено

		//added by marko
		//roster/presence management
		public void sendRosterGet() {
			Packet packet = new Packet("iq");
			packet.Type = "get";
			packet.ID = "roster_get_id";
			Packet query = new Packet("query");
			query.setAttribute("xmlns", "jabber:iq:roster");
			query.setParent(packet);
			session.Writer.Write(packet.ToString());
			session.Writer.Flush();
		}
		public void sendRosterRemove(String jid) {
			Packet packet = new Packet("iq");
			packet.Type = "set";
			packet.setID("roster_remove");
			Packet query = new Packet("query");
			query.setAttribute("xmlns", "jabber:iq:roster");
			query.Parent = packet;
			Packet item = new Packet("item");
			item.setAttribute("jid", jid);
			item.setAttribute("subscription", "remove");
			item.Parent = query;
			session.Writer.Write(packet.ToString());
			session.Writer.Flush();
		}


		public void sendRosterSet(String jid, String name, Hashtable groups)//hashtable type may be changed
		{
			Packet packet = new Packet("iq");
			packet.Type = "set";
			packet.ID = "roster_set";
			Packet query = new Packet("query");
			query.setAttribute("xmlns", "jabber:iq:roster");
			query.setParent(packet);
			Packet item = new Packet("item");
			item.setAttribute("jid", jid);
			item.setAttribute("name", name);
			item.setParent(query);
			//code to form the packets
			foreach (object key in groups.Keys) {
				new Packet("group", groups[key].ToString()).setParent(item);
			}
			session.Writer.Write(packet.ToString());
			session.Writer.Flush();
		}

		//end added by marko

		public void sendRosterSet(String jid, String name, string group) {			//hashtable type may be changed
			Packet packet = new Packet("iq");
			packet.Type = "set";
			packet.ID = "roster_set";
			Packet query = new Packet("query");
			query.setAttribute("xmlns", "jabber:iq:roster");
			query.setParent(packet);
			Packet item = new Packet("item");
			item.setAttribute("jid", jid);
			if(name != null) {
				item.setAttribute("name", name);
			}
			item.setParent(query);
			//code to form the packets
			if(group != null) {
				new Packet("group", group).setParent(item);
			}
			session.Writer.Write(packet.ToString());
			session.Writer.Flush();
		}

		public void closeSession() {
			session.Socket.Disconnect(true);
		}
	}
}
