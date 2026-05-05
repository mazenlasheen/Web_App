using System.Collections.Generic;
using System.Data.SqlClient;

namespace WEB_APPLICATION.Models
{
    public class PostDAL
    {
      private SqlConnection conn = UtilityDAL.createConnection(); 

        public void CreatePost(Post post)
        {
 
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Post (forumId, userId, title, textContent, imageUrl, postDate, postTime) VALUES (@forumId, @userId, @title, @textContent, @imageUrl, @postDate, @postTime)", conn))
            {
                cmd.Parameters.AddWithValue("@forumId", post.ForumID);
                cmd.Parameters.AddWithValue("@userId", post.UserID);
                cmd.Parameters.AddWithValue("@title", post.Title);
                cmd.Parameters.AddWithValue("@textContent", post.TextContent);
                cmd.Parameters.AddWithValue("@imageUrl", (object)post.ImageUrl ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@postDate", post.PostDate);
                cmd.Parameters.AddWithValue("@postTime", post.PostTime);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Post> GetPostsByForum(int forumId) // this method returns a list of pots objects that belong to a specific forum 
        {
            List<Post> posts = new List<Post>();
   
            using (SqlCommand cmd = new SqlCommand(
                "SELECT postId, forumId, userId, title, textContent, imageUrl FROM Post WHERE forumId = @forumId", conn))
            {
                cmd.Parameters.AddWithValue("@forumId", forumId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int postId = UtilityDAL.returnInt(reader, "postId");
                        int userId = UtilityDAL.returnInt(reader, "userId");
                        int forumId = UtilityDAL.returnInt(reader, "forumId");
                        string title = UtilityDAL.returnString(reader, "title");
                        string content = UtilityDAL.returnString(reader, "content");
                        string imagePath = UtilityDAL.returnString(reader, "imagePath");

                        Post post = new Post(postId, userId, forumId, title, content, imagePath);

                        posts.Add(post);
                    }
                }
            }
            return posts;
        }

        public void UpdatePost(Post post)
        {

            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Post SET title = @title, textContent = @textContent, imageUrl = @imageUrl WHERE postId = @postId", conn))
            {
                cmd.Parameters.AddWithValue("@title", post.Title);
                cmd.Parameters.AddWithValue("@textContent", post.TextContent);
                cmd.Parameters.AddWithValue("@imageUrl", (object)post.ImageUrl ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@postId", post.PostID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePost(int postId)
        {

            using (SqlCommand cmd = new SqlCommand("DELETE FROM Post WHERE postId = @postId", conn))
            {
                cmd.Parameters.AddWithValue("@postId", postId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
