using System.Collections.Generic;
using System.Data.SqlClient;

namespace WEB_APPLICATION.Models
{
    public class ForumDAL
    {
        private string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["LearningPlatformDB"].ConnectionString;

        public void CreateForum(Forum forum)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Forum (courseId, title, postFlair) VALUES (@courseId, @title, @postFlair)", conn))
            {
                cmd.Parameters.AddWithValue("@courseId", forum.CourseID);
                cmd.Parameters.AddWithValue("@title", forum.Title);
                cmd.Parameters.AddWithValue("@postFlair", forum.PostFlair);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Forum> GetForumsByCourse(int courseId)
        {
            List<Forum> forums = new List<Forum>();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT forumId, courseId, title, postFlair FROM Forum WHERE courseId = @courseId", conn))
            {
                cmd.Parameters.AddWithValue("@courseId", courseId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        forums.Add(new Forum(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)));
                    }
                }
            }
            return forums;
        }

        public void DeleteForum(int forumId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Forum WHERE forumId = @forumId", conn))
            {
                cmd.Parameters.AddWithValue("@forumId", forumId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
