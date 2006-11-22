using System;
using System.Threading;
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
				Console.WriteLine("Receiving packet: " + packet.ToString());
                if (packet.Element.Equals("iq", StringComparison.OrdinalIgnoreCase)) {
                    child = packet.getFirstChild("query");
                    //if (child == null) { matchString = "iq"; } else { matchString = child.getNamespace(); }
                    if (child == null) { matchString = "jabber:iq:register"; } else { matchString = child.Namespace; }
                } else {
                    matchString = packet.Element;
                }

				try {
					lock (packetListeners) {
						//String element = packet.Element;
						foreach (PacketListener listener in packetListeners[matchString]) {
                            //if (listener == null) { Console.WriteLine("Null Reference"); }
//							Console.WriteLine(packet.ToString());
							listener.notify(packet);
						}
					}
                } catch (Exception ex) {
                    Console.WriteLine("Exception in QueueThread: " + ex.ToString());
				} // try/catch 1
				try {
					lock (packetListeners) {
						foreach (PacketListener listener in packetListeners[""]) {
							listener.notify(packet);
						}
					}

				} catch (Exception ex) {
                  //  Console.WriteLine("Exception2 in QueueThread: " /*+ ex.ToString()*/);
                } // try/catch 2

			} // for

		}
	}

}