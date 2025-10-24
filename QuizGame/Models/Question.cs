using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Question
    {
        public string Statement { get; private set; }
        public string[] Answers { get; private set; }
        public int CorrectAnswer { get; private set; }
        public Question(string statement, int correctAnswer, params string[] answers )
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }
    }
}
