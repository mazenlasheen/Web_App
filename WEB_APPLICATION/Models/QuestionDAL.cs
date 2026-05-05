using System.Collections.Generic;
using System.Data.SqlClient;

namespace WEB_APPLICATION.Models
{
    public class QuestionDAL
    {
        private SqlConnection conn = UtilityDAL.createConnection() ; 
        public void CreateQuestion(Question question)
        {

            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Question (assessmentId, questionType, questionText, correctAnswer, questionAnswer) VALUES (@assessmentId, @questionType, @questionText, @correctAnswer, @questionAnswer)", conn))
            {
                cmd.Parameters.AddWithValue("@assessmentId", question.AssessmentID);
                cmd.Parameters.AddWithValue("@questionType", question.QuestionType);
                cmd.Parameters.AddWithValue("@questionText", question.QuestionText);
                cmd.Parameters.AddWithValue("@correctAnswer", question.CorrectAnswer);
                cmd.Parameters.AddWithValue("@questionAnswer", (object)question.QuestionAnswer ?? System.DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Question> GetQuestionsByAssessment(int assessmentId)
        {
            List<Question> questions = new List<Question>();

            using (SqlCommand cmd = new SqlCommand(
                "SELECT questionId, assessmentId, questionType, questionText, correctAnswer, questionAnswer FROM Question WHERE assessmentId = @assessmentId", conn))
            {
                cmd.Parameters.AddWithValue("@assessmentId", assessmentId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Question q = new Question(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                        q.QuestionAnswer = reader.IsDBNull(5) ? null : reader.GetString(5);
                        questions.Add(q);
                    }
                }
            }
            return questions;
        }

        public void UpdateQuestion(Question question)
        {

            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Question SET questionType = @questionType, questionText = @questionText, correctAnswer = @correctAnswer, questionAnswer = @questionAnswer WHERE questionId = @questionId", conn))
            {
                cmd.Parameters.AddWithValue("@questionType", question.QuestionType);
                cmd.Parameters.AddWithValue("@questionText", question.QuestionText);
                cmd.Parameters.AddWithValue("@correctAnswer", question.CorrectAnswer);
                cmd.Parameters.AddWithValue("@questionAnswer", (object)question.QuestionAnswer ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@questionId", question.QuestionID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteQuestion(int questionId)
        {

            using (SqlCommand cmd = new SqlCommand("DELETE FROM Question WHERE questionId = @questionId", conn))
            {
                cmd.Parameters.AddWithValue("@questionId", questionId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool CheckAnswer(int questionId, string answer)
        {

            using (SqlCommand cmd = new SqlCommand(
                "SELECT correctAnswer FROM Question WHERE questionId = @questionId", conn))
            {
                cmd.Parameters.AddWithValue("@questionId", questionId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null && result.ToString() == answer;
            }
        }
    }
}
