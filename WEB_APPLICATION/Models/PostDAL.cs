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

        public List<Post> GetPostsByForum(int forumId)
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
                        posts.Add(new Post(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetInt32(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.IsDBNull(5) ? null : reader.GetString(5)
                        ));
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
