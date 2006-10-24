namespace Goodware.Jabber.Library {
    public interface StatusListener
    {
		void notify(Session.SessionStatus status);
	}

}