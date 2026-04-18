using System;

namespace WEB_APPLICATION.Models
{
    public class Lesson
    {
        public int LessonID { get; set; }
        public int CourseID { get; set; }
        public string LessonTitle { get; set; }
        public string LessonContent { get; set; }

        public Lesson(int lessonID, int courseID, string lessonTitle, string lessonContent)
        {
            LessonID = lessonID;
            CourseID = courseID;
            LessonTitle = lessonTitle;
            LessonContent = lessonContent;
        }
    }
}
