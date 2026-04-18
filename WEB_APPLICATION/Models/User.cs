using System;

namespace WEB_APPLICATION.Models
{
    public enum Role
    {
        Admin,
        Instructor,
        Student
    }

    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public User(int userID, string username, string password, Role role)
        {
            UserID = userID;
            Username = username;
            Password = password;
            Role = role;
        }
    }
}