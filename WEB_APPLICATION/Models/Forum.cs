using System;

namespace WEB_APPLICATION.Models
{
    public class Forum
    {
        public int ForumID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string PostFlair { get; set; }

        public Forum(int forumID, int courseID, string title, string postFlair)
        {
            ForumID = forumID;
            CourseID = courseID;
            Title = title;
            PostFlair = postFlair;
        }
    }
}
