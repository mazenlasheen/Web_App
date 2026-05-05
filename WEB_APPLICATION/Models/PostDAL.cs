using System;
using System.Collections.Generic;
using System.Data.SqlClient;

 

namespace WEB_APPLICATION.Models
{
    public class PostDAL
    {
      private SqlConnection conn = UtilityDAL.createConnection(); 

        public  bool  void CreatePost(Post post)
        {
            try 
            {
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Post (forumId, userId, title, textContent, imageUrl, postDate, postTime) VALUES (@forumId, @userId, @title, @textContent, @imageUrl, @postDate, @postTime)", conn))
                {
                    cmd.Parameters.AddWithValue("@forumId", post.forumId);
                    cmd.Parameters.AddWithValue("@userId", post.userId);
                    cmd.Parameters.AddWithValue("@title", post.title);
                    cmd.Parameters.AddWithValue("@textContent", post.textContent);
                    // casting ImageUrl to an object as ?? needs both sides to be compatible but if 
                    cmd.Parameters.AddWithValue("@imageUrl", (object)post.imageUrl ?? System.DBNull.Value); // a muhc shorter version of if else 
                    cmd.Parameters.AddWithValue("@postDate", post.postDate);
                    cmd.Parameters.AddWithValue("@postTime", post.postTime);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            } 
            catch (SqlException  e ) 
            {
                Console.WriteLine(e.Message) ;     
            }
            finally 
            {
                conn.Close() ; 
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
                        string content = UtilityDAL.returnString(reader, "textContent");
                        string imagePath = UtilityDAL.returnString(reader, "imageUrl");

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
