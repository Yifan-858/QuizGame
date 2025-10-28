using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Question
    {
        public string Statement { get; set; }
        public int CorrectAnswer { get; set; }
        public string ImagePath { get; set; }
        public string[] Answers { get; set; }
        public string FullImagePath { get; private set; }
        
        public Question(string statement, int correctAnswer, string imagePath, params string[] answers )
        {
            Statement = statement;
            CorrectAnswer = correctAnswer;
            ImagePath = imagePath;
            Answers = answers;
        }

        public void CombineFullImagePath(string imagePath)
        {
            FullImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
        }
    }
}
