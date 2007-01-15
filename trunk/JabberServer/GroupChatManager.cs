using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server {
	public class GroupChatManager {
		public class Group {
			String jid;

			public Group(String jabberID) {
				this.jid = jabberID;
			}

			public String JabberID {
				get {
					return this.jid;
				}
			}

			[System.Obsolete("use property instead")]
			public String getJabberID() {
				return this.jid;
			}

			public Dictionary<String, String> nick2jid = new Dictionary<String, String>();
			public Dictionary<String, Packet> jid2presence = new Dictionary<String, Packet>();
			public Dictionary<String, String> jid2nick = new Dictionary<String, String>(); 

		}


		static GroupChatManager man;

		private GroupChatManager() { }  

		public static GroupChatManager Manager{
			get {
				if (man == null) {
					man = new GroupChatManager();
				}
				return man;
			}

		}
		[System.Obsolete("use property instead")]
		public static GroupChatManager getManager() {
			if (man == null) {
				man = new GroupChatManager();
			}
			return man;
		}

		public Dictionary<String, Group> groups = new Dictionary<string, Group>();

		public Group this[String name] {
			get {
				if (!groups.ContainsKey(name)) {
					groups.Add(name, new Group(name + "@" + JabberServer.server_name));
				}
				return groups[name];
			}
		}

		[System.Obsolete("use indexer instead")]
		public Group getGroup(String name) {
			if (!groups.ContainsKey(name)) {
				groups.Add(name, new Group(name + "@" + JabberServer.server_name));
			}
			return groups[name];
		}

		public bool isChatPacket(Packet packet) {
			JabberID recipient;
			if (packet.To == null) {
				return false;
			}
			recipient = new JabberID(packet.To);
			
			//try {
			//    recipient = new JabberID(packet.To);
			//} catch (Exception ex) {
			//    return false;
			//}
			
			String user = recipient.User;
			if (user == null) {
				return false;
			}
			return user.EndsWith(".group") && recipient.equalsDomain(JabberServer.server_name);
		}

		public void handleChatPresence(Packet packet) {
			JabberID recipient = new JabberID(packet.To);
			if (!isChatPacket(packet)) {
				return;
			}

			Group group = this[recipient.User];
			String nick = recipient.Resource;
			String jid = packet.From;

			if (group.nick2jid.ContainsKey(nick)) {
				if (group.nick2jid[nick] == packet.From) {

					updatePresence(group, packet);
				} else {
					sendConflictingNicknameError(packet);
				}
			} else {
				if(group.jid2nick.ContainsKey(jid)) {

					sendConflictingUserError(packet);
					
				} else {
					joinGroup(group, packet);
				}
			}
		}

		public void joinGroup(Group group, Packet packet) {
			JabberID jid = new JabberID(packet.To);
			String sender = packet.From;

			group.jid2nick.Add(sender, jid.Resource);
			group.nick2jid.Add(jid.Resource, sender);
			updatePresence(group, packet);

			foreach (KeyValuePair<String, Packet> pair in group.jid2presence) {
				Packet p = pair.Value;
				if (p.From != jid.ToString()) {
					p.To = sender;
					MessageHandler.deliverPacket(p);
				}
			}
			serverMessage(group, jid.Resource + " has joined the group");
		}

		public void updatePresence(Group group, Packet packet) {
			String sender = packet.From;
			packet.From = group.JabberID + "/" + group.jid2nick[sender];

			deliverToGroup(group, packet);
			group.jid2presence[sender] = packet;

			if (packet.Type == null) {
				return;
			}
			//try {
			//    String t = packet.Type;
			//} catch (Exception ex) {
			//    return;
			//}

			if (packet.Type == "unavailable") {
				removeUser(group, sender);
			}
		}

		public void sendConflictingNicknameError(Packet packet) {
			try {
				Packet presence = new Packet("presence");
				presence.From = packet.To;
				presence.To = packet.From;

				Packet ePacket = new Packet("error");
				ePacket["code"] = 409.ToString();
				ePacket.Children.Add("Conflict: nickname taken");
				ePacket.Parent = presence;
				packet.Session.Writer.Write(presence.ToString());
				packet.Session.Writer.Flush();
			} catch (Exception ex) {			
			}
		}

		public void sendConflictingUserError(Packet packet) {
			try {
				Packet presence = new Packet("presence");
				presence.From = packet.To;
				presence.To = packet.From;

				Packet ePacket = new Packet("error");
				ePacket["code"] = 409.ToString();
				ePacket.Children.Add("Conflict: cannot sign in twice in the same group");
				ePacket.Parent = presence;
				packet.Session.Writer.Write(presence.ToString());
				packet.Session.Writer.Flush();
			} catch(Exception ex) {
			}
		}
		public void handleChatMessage(Packet packet) {
			JabberID recipient = new JabberID(packet.To);
			Group group = this[recipient.User];

			packet.From = group.JabberID + "/" + group.jid2nick[packet.From];

			if (recipient.Resource == null) {
				deliverToGroup(group, packet);

			} else {
				packet.To = group.nick2jid[recipient.Resource];
				MessageHandler.deliverPacket(packet);
			}
		}

		public void serverMessage(Group group, String msg) {
			Packet packet = new Packet("message");
			packet.From = group.JabberID;
			packet.Type = "groupchat";

			Packet body = new Packet("body", msg);
			body.Parent = packet;
			deliverToGroup(group, packet);
		}

		public void deliverToGroup(Group group, Packet packet) {
			foreach (KeyValuePair<String, String> kvp in group.jid2nick) {
				String rec = kvp.Key;
				packet.To = rec;
				MessageHandler.deliverPacket(packet);
			}
		}

		public void removeUser(Group group, String jabberID) {
			String nick;
			try {
				nick = group.jid2nick[jabberID];
			} catch (Exception ex) {
				return;
			}
			group.jid2nick.Remove(jabberID);
			group.nick2jid.Remove(nick);
			group.jid2presence.Remove(jabberID);

			if (group.jid2nick.Count == 0) {
				groups.Remove(group.JabberID);
			} else {
				serverMessage(group, nick + " has left");
			}
		}

		public void removeUser(String jabberID) {
			foreach (Group group in groups.Values) {
				removeUser(group, jabberID);
			}
		}
	}
}
