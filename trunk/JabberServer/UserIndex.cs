using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Goodware.Jabber.Library;

namespace Goodware.Jabber.Server {
    public class UserIndex {
        //User table, key: username (String), value: User
        public Hashtable userIndex = new Hashtable();

        //Session table, key: Session, value: User
        public Hashtable sessionIndex = new Hashtable();

        public User addUser(String name) {
            User user = getUser(name);
            if (user == null) {
                user = new User(name);
            }
            user.setNewRoster(name);
            userIndex.Add(name, user);
            return user;
        }
        public User getUser(String name) {
            return (User)userIndex[name];
        }
        public User getUser(Session session) {
            return (User)sessionIndex[session];
        }
        public void removeUser(String name) {
            userIndex.Remove(name);
        }

        public System.IO.StreamWriter getWriter(String jabberID) {
            return getWriter(new JabberID(jabberID));
        }
        public System.IO.StreamWriter getWriter(JabberID jabberID) {
//			String resource = jabberID.Resource;
//			User user = getUser(jabberID.User);
            return getUser(jabberID.getUser()).getWriter(jabberID.getResource());
        }

        public void addSession(Session session) {
            User user = getUser(session.getJID().getUser());
            user.addSession(session);
            sessionIndex.Add(session, user);
        }

        public bool containsSession(Session session)
        {
            return sessionIndex.Contains(session);
        }
        public bool containsUser(String user)
        {
            return userIndex.Contains(new JabberID(user).User);
        }


        public void removeSession(Session session) {
            sessionIndex.Remove(session);
            if (session.getJID() == null) {
                return;
            }
            getUser(session.getJID().getUser()).removeSession(session);
        }
    }
}
