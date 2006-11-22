using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using Goodware.Jabber.Library;


namespace Goodware.Jabber.Client {
    public class Program {		
		static void Main(string[] args) {
	/*		Console.WriteLine("Jabber Text Client");

            TestThreadMONOLOGUE clientThread = new TestThreadMONOLOGUE();
			JabberModel clientModel = new JabberModel(clientThread);

			clientModel.ServerName = "localhost";
			clientModel.ServerAddress = "127.0.0.1";
			clientModel.Port = "5222";
			clientModel.User = "misos";

			//Додадено од Милош/Васко
            clientModel.AuthMode = "plain";     // ??
            clientModel.Resource = "home";
            clientModel.Password = "test";
            //Крај додадено
			
			clientThread.Model = clientModel;
			clientModel.connect();


			
            //додадено од ДаркоА
            clientModel.register();
            clientModel.authenticate();
            //крај

            clientThread.start();

            Console.WriteLine("Press any key to exit");
           // Console.ReadLine();
            Console.WriteLine("Press any key to exit");
           // Console.ReadLine();*/
        }

	}
/*
    public class TestThreadMONOLOGUE : TestThread {

        public override void run() {
      try {

/*
        Console.WriteLine("== Sending message 1 ==");
        Model.sendMessage("misos@localhost", "hello", null, "normal", null, "Hello message from Misos!");
        Console.WriteLine("Message 1 sent!");
         
        Console.WriteLine("== Recieving message 1 ==");
        Packet packet = waitFor("message", null);
          
        Console.WriteLine("== Sending message 2 ==");
        Model.sendMessage("misos@localhost", "hello", null, "normal", null, "Hello AGAIN!");
        Console.WriteLine("Message 2 sent!");

        Console.WriteLine("== Recieving message 2 ==");
        packet = waitFor("message", null);
          
          //Iain Test Thread
          //Console.Write("waiting for connect");
          //Console.ReadLine();
        
        Console.WriteLine("== Sending my empty Presence update ==");
        Console.ReadLine();
        Model.sendPresence(null, null, null, null, null);
        Console.WriteLine("== Send request for subscription ==");
        Console.ReadLine();
        Model.sendPresence("misos2@" + Model.ServerName, "subscribe", null, null, null);
        
          Console.WriteLine("My new Roster:");
          //Console.WriteLine("== Sending my Presence update ==");
          Console.ReadLine();
         // Console.WriteLine("Now send remove!");
          Model.sendRosterGet();

          //Console.WriteLine("== Sending subscription/Presence confirmation ==");
         //Console.ReadLine();
          Console.WriteLine("Sending presence request refusal");
          Console.ReadLine();
          Model.sendPresence("misos2@" + Model.ServerName, "unsubscribe", null, null, null);

          //Console.WriteLine("Now update my roster with a name!");
          //Console.ReadLine();
        // Packet packet= waitFor("presence", "subscribe");
        //Model.sendPresence("misos@" + Model.ServerName, "subscribed", null, null, null);

         // Console.WriteLine("== Sending roster push! ==");
          //Console.WriteLine("Now send remove!");
          //Console.ReadLine();
         // Model.sendRosterGet();
         // Packet packet =waitFor("iq", "roster");
          //Hashtable groups=new Hashtable();
          //groups.Add("mykey","friends");
          //Model.sendRosterSet("misos2@" + Model.ServerName, "RunningMan", groups);
          //Model.sendRosterRemove("misos2@" + Model.ServerName);
          //Packet packet = waitFor("iq", "set");
          Console.WriteLine("Now check for updated roster!");
          Console.ReadLine();
          Model.sendRosterGet();
         

          //Console.WriteLine("Now try to remove this subscription!");
          //Console.ReadLine();
          //Model.sendPresence("misos2@" + Model.ServerName, "unsubscribe", null, null, null);
        //Packet packet = waitFor("presence", "subscribed");
          //Console.WriteLine("Now wait for opponent!");
         // Console.ReadLine();
         // Model.sendRosterGet();


      } catch (Exception ex){
          Console.WriteLine(ex.ToString());
      }
    } // run()
  }*/
 
}
