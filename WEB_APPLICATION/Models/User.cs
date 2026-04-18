using System;

namespace WEB_APPLICATION.Models
{
    public enum Role
    {
        Admin,
        Instructor,
        Student
    }
    public static string role_toString(Role role ) 
    {
        if (role == Role.Admin ) {return "admin" ;}
        else if (role == Role.Instructor ) {return "instructor" ;}
        else if (role == Role.Student ) {return "student" ;}
        return "" // in hte case it is unknown 
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