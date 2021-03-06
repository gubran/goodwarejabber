using System;
using System.Threading;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server {
	public class QueueThread : AThread {
		PacketQueue packetQueue;

		public QueueThread(PacketQueue queue) {
			packetQueue = queue;
		}

		Dictionary<String, List<PacketListener>> packetListeners = new Dictionary<String, List<PacketListener>>();

		public bool addPacketListener(PacketListener listener, String element) {
			if (listener == null || element == null) {
				return false;
			} else {
				try {
					packetListeners[element].Add(listener);
				} catch (Exception ex) {
					packetListeners[element] = new List<PacketListener>();
					packetListeners[element].Add(listener);
				}
				return true;
			}

		}

		public void removePacketListener(PacketListener listener) {
			foreach (KeyValuePair<String, List<PacketListener>> kvp in packetListeners) {
				kvp.Value.Remove(listener);
			}

		}

		public override void run() {
			for (Packet packet = packetQueue.dequeue(); packet != null; packet = packetQueue.dequeue()) {
				Packet child;
				String matchString;
				JabberServer.output.WriteLine("Receiving packet: " + packet.ToString());
				if (packet.Element.Equals("iq", StringComparison.OrdinalIgnoreCase)) {
					child = packet.getFirstChild("query");
					if (child == null) { matchString = "jabber:iq:register"; } else { matchString = child.Namespace; }
				} else {
					matchString = packet.Element;
				}

				if (matchString.Equals("terminate")) {
					saveToFile();
                    JabberServer.output.Close();
					return;
				}

				try {
					lock (packetListeners) {
						foreach (PacketListener listener in packetListeners[matchString]) {
							listener.notify(packet);
						}
					}
				} catch (Exception ex) {
					JabberServer.output.WriteLine("Exception in QueueThread: " + ex.ToString());
				} // try/catch 1
				try {
					lock (packetListeners) {
						foreach (PacketListener listener in packetListeners[""]) {
							listener.notify(packet);
						}
					}
				} catch (Exception ex) {

				} // try/catch 2

			} // for

		}

		public static void saveToFile() {  //termination handler
			DataSet ds = createDataSet();
			fillDataSet(ds);
			ds.WriteXml(JabberServer.users_file_name, XmlWriteMode.WriteSchema);

            JabberServer.output.Flush();

		}

		private static DataSet createDataSet() {
			DataSet ds = new DataSet("UsersInformation");

			DataTable users = new DataTable("User");

			DataColumn username = new DataColumn("username");
			username.DataType = "".GetType();
			users.Columns.Add(username);
			DataColumn password = new DataColumn("password");
			password.DataType = "".GetType();
			users.Columns.Add(password);
			DataColumn hash = new DataColumn("hash");
			hash.DataType = "".GetType();
			users.Columns.Add(hash);
			DataColumn sequence = new DataColumn("sequence");
			sequence.DataType = "".GetType();
			users.Columns.Add(sequence);
			DataColumn token = new DataColumn("token");
			token.DataType = "".GetType();
			users.Columns.Add(token);

			ds.Tables.Add(users);

			DataTable items = new DataTable("Item");

			DataColumn userName = new DataColumn("username");
			userName.DataType = "".GetType();
			items.Columns.Add(userName);
			DataColumn jid = new DataColumn("jid");
			jid.DataType = "".GetType();
			items.Columns.Add(jid);
			DataColumn name = new DataColumn("name");
			name.DataType = "".GetType();
			items.Columns.Add(name);
			DataColumn group = new DataColumn("group");
			group.DataType = "".GetType();
			items.Columns.Add(group);

			ds.Tables.Add(items);

			DataRelation roster = new DataRelation("Roster", ds.Tables["User"].Columns["username"], ds.Tables["Item"].Columns["username"]);
			roster.Nested = true;

			ds.Relations.Add(roster);

			return ds;
		
		}

		private static void fillDataSet(DataSet ds) {
/*
			DataRow user = ds.Tables["User"].NewRow();
			user["username"] = "darko";
			user["password"] = "test";

			ds.Tables["User"].Rows.Add(user);

			DataRow item = ds.Tables["Item"].NewRow();
			item["username"] = "darko";
			item["jid"] = "bojan@localhost";
			item["name"] = "Bojan";
			item["group"] = "Grupa";
			ds.Tables["Item"].Rows.Add(item);
*/

			DataTable usersTable = ds.Tables["User"];
			DataRow newUser = null;
			Hashtable usersHash= JabberServer.getUserIndex().userIndex;
			foreach (User u in usersHash.Values) {
				newUser = usersTable.NewRow();
				newUser["username"] = u.getUsername();
				newUser["password"] = u.getPassword();
				newUser["hash"] = u.getHash();
				newUser["sequence"] = u.getSequence();
				newUser["token"] = u.getToken();
				usersTable.Rows.Add(newUser);
			}


			DataTable itemsTable = ds.Tables["Item"];
			DataRow newItem = null;
			Hashtable rosterHash = JabberServer.RosterHashtable;
			foreach (UserRoster ur in rosterHash.Values) {
				String user = ur.user;
				foreach (Packet p in ur.items.Values) {
					String jid = p["jid"];
					String subscription = p["subscription"];
					String name = p["name"];
					String group = p.getChildValue("group");

					if (subscription.Equals("both")) {
						newItem = itemsTable.NewRow();
						newItem["username"] = user;
						newItem["jid"] = jid;
						newItem["name"] = name;
						newItem["group"] = group;
						itemsTable.Rows.Add(newItem);
					}
				}

			}



		}


	}

}