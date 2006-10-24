using System;
using System.Threading;
using System.Collections.Generic;

namespace Goodware.Jabber.Library {
    public class PacketQueue
    {
		Queue<Packet> queue = new Queue<Packet>();

		Object sync = new Object();

		public void enqueue(Packet packet) {
			lock (sync) {
				queue.Enqueue(packet);
				Monitor.PulseAll(sync);  // Java notifyAll
			}
//			Console.WriteLine(packet.ToString());

		}

		public Packet dequeue() {
			lock (sync) {
				while (queue.Count == 0) {
					Monitor.Wait(sync);
				}
				return queue.Dequeue();
			}

		}


	}

}