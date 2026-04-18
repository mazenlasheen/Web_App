using System;

namespace WEB_APPLICATION.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public int ForumID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string TextContent { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime PostTime { get; set; }

        public Post(int postID, int forumID, int userID, string title, string textContent, string imageUrl)
        {
            PostID = postID;
            ForumID = forumID;
            UserID = userID;
            Title = title;
            TextContent = textContent;
            ImageUrl = imageUrl;
            PostDate = DateTime.UtcNow;
            PostTime = DateTime.UtcNow;
        }
    }
}
