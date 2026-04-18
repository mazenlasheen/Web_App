using System;

namespace WEB_APPLICATION.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int AssessmentID { get; set; }
        public string QuestionType { get; set; }
        public string QuestionText { get; set; }
        public string QuestionAnswer { get; set; }
        public string CorrectAnswer { get; set; }

        public Question(int questionID, int assessmentID, string questionType, string questionText, string correctAnswer)
        {
            QuestionID = questionID;
            AssessmentID = assessmentID;
            QuestionType = questionType;
            QuestionText = questionText;
            CorrectAnswer = correctAnswer;
            QuestionAnswer = null;
        }
    }
}
