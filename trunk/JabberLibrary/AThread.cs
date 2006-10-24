using System;
using System.Threading;


namespace Goodware.Jabber.Library {
    public class AThread
    {
		Thread t;

		public void start() {
			t.Start();
		}

		public AThread() {
			t = new Thread(run);
		}

		public virtual void run() {
		}

		public Boolean isDaemon() {
			return t.IsBackground;
		}
		public void setDaemon(Boolean d) {
			t.IsBackground = d;
		}

	}

}