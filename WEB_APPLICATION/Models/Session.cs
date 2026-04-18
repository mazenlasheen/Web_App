using System;

namespace WEB_APPLICATION.Models
{
    public class Session
    {
        public int SessionID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }

        public Session(int sessionID, int userID)
        {
            SessionID = sessionID;
            UserID = userID;
            Date = DateTime.UtcNow;
            LoginTime = DateTime.UtcNow;
        }
    }
}
