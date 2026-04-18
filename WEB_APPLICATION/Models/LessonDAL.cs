using System.Collections.Generic;
using System.Data.SqlClient;

namespace WEB_APPLICATION.Models
{
    public class LessonDAL
    {
        private string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["LearningPlatformDB"].ConnectionString;

        public void CreateLesson(Lesson lesson)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Lesson (courseId, lessonTitle, lessonContent) VALUES (@courseId, @lessonTitle, @lessonContent)", conn))
            {
                cmd.Parameters.AddWithValue("@courseId", lesson.CourseID);
                cmd.Parameters.AddWithValue("@lessonTitle", lesson.LessonTitle);
                cmd.Parameters.AddWithValue("@lessonContent", lesson.LessonContent);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Lesson> GetLessonsByCourse(int courseId)
        {
            List<Lesson> lessons = new List<Lesson>();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT lessonId, courseId, lessonTitle, lessonContent FROM Lesson WHERE courseId = @courseId", conn))
            {
                cmd.Parameters.AddWithValue("@courseId", courseId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lessons.Add(new Lesson(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetString(2),
                            reader.GetString(3)
                        ));
                    }
                }
            }
            return lessons;
        }

        public Lesson GetLessonById(int lessonId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT lessonId, courseId, lessonTitle, lessonContent FROM Lesson WHERE lessonId = @lessonId", conn))
            {
                cmd.Parameters.AddWithValue("@lessonId", lessonId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Lesson(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3));
                }
            }
            return null;
        }

        public void UpdateLesson(Lesson lesson)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Lesson SET lessonTitle = @lessonTitle, lessonContent = @lessonContent WHERE lessonId = @lessonId", conn))
            {
                cmd.Parameters.AddWithValue("@lessonTitle", lesson.LessonTitle);
                cmd.Parameters.AddWithValue("@lessonContent", lesson.LessonContent);
                cmd.Parameters.AddWithValue("@lessonId", lesson.LessonID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteLesson(int lessonId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Lesson WHERE lessonId = @lessonId", conn))
            {
                cmd.Parameters.AddWithValue("@lessonId", lessonId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
