using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server {
	class ListenerThread : AThread {

		Socket serverSocket;

		public ListenerThread(int port) {
			serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
			serverSocket.Bind(ep);
			serverSocket.Listen(20);

		}

		public override void run() {
			while (true) {
				Socket newSock = serverSocket.Accept();
				Session session = new Session(newSock);

				ProcessThread processor = new ProcessThread(JabberServer.packetQueue, session);
				processor.setDaemon(true);
				processor.start();
			}

		}

	}
}
