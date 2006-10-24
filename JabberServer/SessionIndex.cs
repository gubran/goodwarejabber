using System;
using System.Collections.Generic;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server {
	public class SessionIndex {
		Dictionary<String, List<Session>> userIndex = new Dictionary<String, List<Session>>();
		Dictionary<String, Session> jidIndex = new Dictionary<String, Session>();

		public Session this[String jabberID] {
			get {
				return this[new JabberID(jabberID)];
			}

		}

		[System.Obsolete("user indexer instead")]
		public Session getSession(String jabberID) {
			return getSession(new JabberID(jabberID));
		}
		public Session this[JabberID jabberID] {
			get {
				String jidString = jabberID.ToString();
				Session session = null;
				try {
					session = jidIndex[jidString];
					return session;
				} catch (Exception ex) {
					try {
						List<Session> resources = userIndex[jabberID.User];
						return resources[0];
					} catch (Exception e) {
						return null;
					}
				}
			
			}

		}

		[System.Obsolete("user indexer instead")]
		public Session getSession(JabberID jabberID) {
			String jidString = jabberID.ToString();
			Session session = null;
			try {
				session = jidIndex[jidString];
				return session;
			} catch (Exception ex) {
				try {
					List<Session> resources = userIndex[jabberID.User];
					return resources[0];
				} catch (Exception e) {
					return null;
				}
			}
		}

		public void removeSession(Session session) {
			String jidString = session.JID.ToString();
			String user = session.JID.User;

			if (jidIndex.ContainsKey(jidString)) {
				jidIndex.Remove(jidString);
			}
			try {
				List<Session> resources = userIndex[user];
				if (resources.Count <= 1) {
					userIndex.Remove(user);
					return;
				}
				resources.Remove(session);
			} catch (Exception ex) {
				return;
			}
		}

		public void addSession(Session session) {
			jidIndex.Add(session.JID.ToString(), session);
			String user = session.JID.User;
			List<Session> resources;
			try {
				resources = userIndex[user];
			} catch (Exception ex) {
				resources = new List<Session>();
				userIndex[user] = resources;
			}
			resources.Add(session);
		}

	}


}