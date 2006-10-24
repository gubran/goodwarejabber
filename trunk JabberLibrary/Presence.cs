using System;
using System.Collections.Generic;
using System.Text;

namespace Goodware.Jabber.Library
{
    public class Presence
    {
        Boolean availible;
        public Boolean Availible{
            get
            {
                return this.availible;
            }
            set
            {
                this.availible = value;
            }
        }
        [System.Obsolete("use property instead")]
        public Boolean isAvailible()
        {
            return this.availible;
        }
        [System.Obsolete("use property instead")]
        public void setAvailible(Boolean isAvailible)
        {
            this.availible = isAvailible;
        }

        //Sealed may not be needed, equals final in Java
        static public String SHOW_CHAT = "chat";
        static public String SHOW_AWAY = "away";
        static public String SHOW_XA = "xa";
        static public String SHOW_DND = "dnd";

        String show;
        public String Show{
            get
            {
                return this.show;
            }
            set
            {
                this.show = value;
            }
        }
        [System.Obsolete("use property instead")]
        public String getShow()
        {
            return this.show;
        }
        [System.Obsolete("use property instead")]
        public void setShow(String newShow)
        {
            this.show = newShow;
        }
        String status;
        public String Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }
        [System.Obsolete("use property instead")]
        public String getStatus()
        {
            return this.status;
        }
        [System.Obsolete("use property instead")]
        public void setStatus(String newStatus)
        {
            this.status = newStatus;
        }
        String priority;
        public String Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                this.priority = value;
            }
        }

        [System.Obsolete("use property instead")]
        public String getPriority()
        {
            return this.priority;
        }
        [System.Obsolete("use property instead")]
        public void setPriority(String newPriority)
        {
            this.priority = newPriority;
        }

    }
}
