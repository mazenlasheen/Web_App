using System;

namespace WEB_APPLICATION.Models
{
    public class Assessment
    {
        public int AssessmentID { get; set; }
        public int LessonID { get; set; }
        public int AttemptNumber { get; set; }

        public Assessment(int assessmentID, int lessonID)
        {
            AssessmentID = assessmentID;
            LessonID = lessonID;
            AttemptNumber = 0;
        }
    }
}
