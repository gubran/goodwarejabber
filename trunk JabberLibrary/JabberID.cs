using System;
using System.Text;

namespace Goodware.Jabber.Library {
    public class JabberID
    {
		String user;

		public String User {
			get {
				return this.user;
			}
			set {
				this.user = value;
			}
		}
		[System.Obsolete("use property instead")]
		public String getUser() {
			return this.user;
		}
		[System.Obsolete("use property instead")]
		public void setUser(String user) {
			this.user = user;
		}

		String domain;
		public String Domain {
			get {
				return this.domain;
			}
			set {
				this.domain = value;
			}


		}

		[System.Obsolete("use property instead")]
		public String getDomain() {
			return this.domain;
		}
		[System.Obsolete("use property instead")]
		public void setDomain(String domain) {
			this.domain = domain;
		}

		String resource;
		public String Resource {
			get {
				return this.resource;
			}
			set {
				this.resource = value;
			}
		}

		[System.Obsolete("use property instead")]
		public String getResource() {
			return this.resource;
		}
		[System.Obsolete("use property instead")]
		public void setResource(String resource) {
			this.resource = resource;
		}

		public Boolean equalsDomain(String domain) {
			if (this.domain == null ^ domain == null) {
				return false;
			}
			return this.domain.Equals(domain, StringComparison.OrdinalIgnoreCase);
		}
		public Boolean equalsDomain(JabberID testJid) {
			return equalsDomain(testJid.domain);
		}
		public Boolean equalsUser(String user) {
			if (this.user == null ^ user == null) {
				return false;
			}
			return this.user.Equals(user, StringComparison.OrdinalIgnoreCase);
		}
		public Boolean equalsUser(JabberID testJid) {
			return equalsUser(testJid.user);
		}
		public Boolean equalsResource(String resource) {
			if (this.resource == null ^ resource == null) {
				return false;
			}
			return resource.Equals(resource, StringComparison.OrdinalIgnoreCase);
		}
		public Boolean equalsResource(JabberID testJid) {
			return equalsResource(testJid.resource);
		}
		public Boolean equals(JabberID jid) {
			return equalsUser(jid) && equalsDomain(jid) && equalsResource(jid);
		}
		public Boolean equals(String jid) {
			return equals(new JabberID(jid));
		}

		public void setJID(String jid) {
			if (jid == null) {
				user = null;
				domain = null;
				resource = null;
				return;
			}
			int atLoc = jid.IndexOf("@");
			if (atLoc == -1) {
				user = null;
			} else {
				user = jid.Substring(0, atLoc).ToLower();
				jid = jid.Substring(atLoc + 1);
			}
			atLoc = jid.IndexOf("/");
			if (atLoc == -1) {
				resource = null;
				domain = jid.ToLower();
			} else {
				domain = jid.Substring(0, atLoc).ToLower();
				resource = jid.Substring(atLoc + 1).ToLower();
			}
		}

		public JabberID(String user, String domain, String resource) {
			this.User = (user);
			this.Domain = (domain);
			this.Resource = (resource);
		}
		public JabberID(String jid) {
			setJID(jid);
		}
		public override String ToString() {
			StringBuilder jid = new StringBuilder();
			if (user != null) {
				jid.Append(user);
				jid.Append("@");
			}
			jid.Append(domain);
			if (resource != null) {
				jid.Append("/");
				jid.Append(resource);
			}
			return jid.ToString();
		}

	} 
}