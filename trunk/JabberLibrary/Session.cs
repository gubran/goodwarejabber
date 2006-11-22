using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Goodware.Jabber.Library {
    public class Session
    {
		//Od Marko
    Presence presence = new Presence();
    //cannot use c# style property
    public Presence getPresence()
    {
       return this.presence;
    }

		JabberID jid;

		public JabberID JID {
			get {
				return this.jid;
			}
			set {
				this.jid = value;
			}

		}
		[System.Obsolete("use property instead")]
		public JabberID getJID() {
			return this.jid;
		}
		[System.Obsolete("use property instead")]
		public void setJID(JabberID jid) {
			this.jid = jid;
		}

		String sid;
		public String StreamID {
			get {
				return this.sid;
			}
			set {
				this.sid = value;
			}


		}

		[System.Obsolete("use property instead")]
		public String getStreamID() {
			return this.sid;
		}
		[System.Obsolete("use property instead")]
		public void setStreamID(String streamID) {
			this.sid = streamID;
		}

		Socket sock;
		public Socket Socket {
			get {
				return this.sock;
			}
			set {
				this.sock = value;
			}


		}

		[System.Obsolete("use property instead")]
		public Socket getSocket() {
			return sock;
		}
		[System.Obsolete("use property instead")]
		public void setSocket(Socket sock) {
			this.sock = sock;
		}


		public Session() {
		}
		public Session(Socket socket) {
			Socket = (socket);
		}

        //додадено од Дарко Алексовски
        int priority;
        public int getPriority() { return priority; }
        public void setPriority(int level) { priority = level; }
        //крај додадено

		StreamWriter output;
		public StreamWriter Writer {
			get {
				if (output == null) {
					output = new StreamWriter(new NetworkStream(this.sock), Encoding.UTF8);

				}
				return output;
			}

		}

		[System.Obsolete("use property instead")]
		public StreamWriter getWriter() {
			if (output == null) {
				output = new StreamWriter(new NetworkStream(this.sock), Encoding.UTF8);
			}
			return output;
		}
		StreamReader input;
		public StreamReader Reader {
			get {
				if (input == null) {
					input = new StreamReader(new NetworkStream(this.sock), Encoding.UTF8);
				}
				return input;
			}

		}

		[System.Obsolete("use property instead")]
		public StreamReader getReader() {
			if (input == null) {
				input = new StreamReader(new NetworkStream(this.sock), Encoding.UTF8);
			}
			return input;
		}
		List<StatusListener> statusListeners = new List<StatusListener>();

		public void addStatusListener(StatusListener listener) {
			statusListeners.Add(listener);
		}
		public Boolean removeStatusListener(StatusListener listener) {
			return statusListeners.Remove(listener);
		}
		public enum SessionStatus {
			disconnected,
			connected,
			streaming,
			authenticated
		}
		SessionStatus status;
		public SessionStatus Status {
			get {
				return this.status;
			}
			set {
				this.status = value;
				foreach (StatusListener listener in statusListeners) {
					listener.notify(status);
				}

			}

		}

		[System.Obsolete("use property instead")]
		public SessionStatus getStatus() {
			return this.status;
		}
		[System.Obsolete("use property instead")]
		public void setStatus(SessionStatus status) {
			this.status = status;
			foreach (StatusListener listener in statusListeners) {
				listener.notify(status);
			}
		}

	}

}