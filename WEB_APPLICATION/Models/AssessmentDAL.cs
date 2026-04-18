using System.Collections.Generic;
using System.Data.SqlClient;

namespace WEB_APPLICATION.Models
{
    public class AssessmentDAL
    {
        private string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["LearningPlatformDB"].ConnectionString;

        public void CreateAssessment(Assessment assessment)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Assessment (lessonId, attemptNumber) VALUES (@lessonId, @attemptNumber)", conn))
            {
                cmd.Parameters.AddWithValue("@lessonId", assessment.LessonID);
                cmd.Parameters.AddWithValue("@attemptNumber", assessment.AttemptNumber);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Assessment> GetAssessmentsByLesson(int lessonId)
        {
            List<Assessment> assessments = new List<Assessment>();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT assessmentId, lessonId, attemptNumber FROM Assessment WHERE lessonId = @lessonId", conn))
            {
                cmd.Parameters.AddWithValue("@lessonId", lessonId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Assessment a = new Assessment(reader.GetInt32(0), reader.GetInt32(1));
                        a.AttemptNumber = reader.GetInt32(2);
                        assessments.Add(a);
                    }
                }
            }
            return assessments;
        }

        public Assessment GetAssessmentById(int assessmentId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT assessmentId, lessonId, attemptNumber FROM Assessment WHERE assessmentId = @assessmentId", conn))
            {
                cmd.Parameters.AddWithValue("@assessmentId", assessmentId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Assessment a = new Assessment(reader.GetInt32(0), reader.GetInt32(1));
                        a.AttemptNumber = reader.GetInt32(2);
                        return a;
                    }
                }
            }
            return null;
        }

        public void DeleteAssessment(int assessmentId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Assessment WHERE assessmentId = @assessmentId", conn))
            {
                cmd.Parameters.AddWithValue("@assessmentId", assessmentId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
