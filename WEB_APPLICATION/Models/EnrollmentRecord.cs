using System;

namespace WEB_APPLICATION.Models
{
    public class EnrollmentRecord
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int UserID { get; set; }
        public float CompletionRate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool ActiveStatus { get; set; }

        public EnrollmentRecord(int enrollmentID, int courseID, int userID)
        {
            EnrollmentID = enrollmentID;
            CourseID = courseID;
            UserID = userID;
            CompletionRate = 0;
            EnrollmentDate = DateTime.UtcNow;
            ActiveStatus = true;
        }
    }
}
