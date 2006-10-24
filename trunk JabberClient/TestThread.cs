using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;


namespace Goodware.Jabber.Client {
    public class TestThread : AThread
    {
		private PacketQueue packetQueue;
		private JabberModel jModel;
		Dictionary<String, List<PacketListener>> packetListeners;

		public TestThread() {
			packetQueue = new PacketQueue();
			packetListeners = new Dictionary<string, List<PacketListener>>();
		}

		public JabberModel Model {
			set {
				jModel = value;
			}
			get {
				return jModel;
			}
		}

		public PacketQueue Queue {
			get {
				return packetQueue;
			}
		}

		public bool addListener(PacketListener listener, string element) {
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

		public bool removeListener(PacketListener listener) {
			foreach (KeyValuePair<String, List<PacketListener>> kvp in packetListeners) {
				kvp.Value.Remove(listener);
			}
			return true;
		}

		public override void run() {
		}

		public void notifyHandlers(Packet packet) {
			Console.WriteLine("In notifyHandlers: " + packet.ToString());
				try {
					lock (packetListeners) {
						String element = packet.Element;
						foreach (PacketListener listener in packetListeners[element]) {
							listener.notify(packet);
						}
					}
				} catch (Exception ex) {
					// Do nothing
				}
				try {
					lock (packetListeners) {
						foreach (PacketListener listener in packetListeners[""]) {
							listener.notify(packet);
						}
					}

				} catch (Exception ex) {
					// Do nothing
				}			
		}

		public Packet waitFor(string element, string type) {
            for (Packet packet = packetQueue.dequeue();
                 packet != null;
                 packet = packetQueue.dequeue() ) {
                    notifyHandlers(packet);
                    if (packet.Element.Equals(element, StringComparison.OrdinalIgnoreCase)){
                        if (type != null){
                            if (packet.Type.Equals(type,StringComparison.OrdinalIgnoreCase)){
                                return packet;
                            }
                        } else {
                            return packet;
                        }
                    }
                }
                return null;
        }

  }

}
