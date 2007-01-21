using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;

// not finished: DARKOA

namespace Goodware.Jabber.Server {

    public class User {
    	
    		// Od Marko
    	  //added roster support by marko simple
        Roster roster;
        public Roster getRoster(){ 
            return this.roster;
        }
        
        public void setNewRoster(string username) {
            roster=new Roster(username);
            UserRoster myroster = new UserRoster();
            myroster.user = this.username;
        
            //when adding a user, create a corresponding item
            Server.JabberServer.RosterHashtable.Add(this.username, myroster);

        }

        String username;
        public User(String name) { 
			  this.username = name; 
		  }

		public String getUsername() {
			return this.username;
		}



        String pass;
        public void setPassword(String pass) { 
			  this.pass = pass; 
		  }
        public String getPassword() { return this.pass; }

        String hash;
        public void setHash(String value) {
			  hash = value; 
		  }
        public String getHash() { return hash; }

        String sequence;
        public void setSequence(String value) {
			  sequence = value; 
		  }
        public String getSequence() { return sequence; }

        String token;
        public void setToken(String value) {
			  token = value; 
		  }
        public String getToken() { return token; }

        LinkedList<Packet> messageStore = new LinkedList<Packet>();

        public void storeMessage(Packet p) {
            messageStore.AddLast(p);
        }

        public void deliverMessages() {
            while (messageStore.Count > 0) {
                LinkedListNode<Packet> temp = messageStore.Last;
                Packet storedMsg = (Packet)temp.Value;
                storedMsg.setSession(activeSession);
                //changed by marko
               // storedMsg.setTo(null);
                MessageHandler.deliverPacket(storedMsg);    // ???????
                //needed to remove the message from messagestore
                //bein
                messageStore.RemoveLast();
                //end change by marko
            }
        }

        Session activeSession;

        Hashtable resources = new Hashtable();

        public Hashtable getSessions() {       // dali moze popametno da se reshi ???
            return resources;
        }


        public void changePriority(Session session) {
            if (activeSession.getPriority() < session.getPriority()) {
                activeSession = session;
            }
        }

        public void addSession(Session session) {
            resources.Add(session.getJID().getResource(), session); // key, value
            if (activeSession == null) {
                activeSession = session;
            } else if (activeSession.getPriority() < session.getPriority()) {
                activeSession = session;
            }


			//isprakjanje na Roster (ne e spored stadard)

			Packet packet = new Packet("iq");
			packet["type"] = "set";
			JabberID jidTo = session.getJID();
			packet.To = jidTo.User + "@" + jidTo.Domain;

			getRoster().getPacket().setParent(packet);
			MessageHandler.deliverPacket(packet);


			// kraj na isprakjanje a roster





            deliverMessages();
        }


        public void removeSession(Session session) {
            resources.Remove(session.getJID().getResource());
            activeSession = null;

            foreach (Session sess in resources.Values) {
                if (sess.getPriority() > activeSession.getPriority()) {
                    activeSession = sess;
                }
            }
        }

        public System.IO.StreamWriter getWriter(String resource) {
            Session session;
            if (resource == null) {
                session = activeSession;
            } else if (resource.Length == 0) {
                session = activeSession;
            } else {
                session = (Session)resources[resource];
            }

            if (session == null) {
                return null;
            }
            return session.getWriter();
        }


    }


}
