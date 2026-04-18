using System;

namespace WEB_APPLICATION.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public int UserID { get; set; }
        public string CourseDescription { get; set; }
        public string CourseName { get; set; }
        public bool ActiveStatus { get; set; }
        public string ImageUrl { get; set; }

        public Course(int courseID, int userID, string courseName, string courseDescription, string imageUrl)
        {
            CourseID = courseID;
            UserID = userID;
            CourseName = courseName;
            CourseDescription = courseDescription;
            ImageUrl = imageUrl;
            ActiveStatus = true;
        }
    }
}
