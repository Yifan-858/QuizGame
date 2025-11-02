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
       
        
        public Question(string statement, int correctAnswer, string imagePath = "Image/quizDefault.jpg", params string[] answers )
        {
            Statement = statement;
            CorrectAnswer = correctAnswer;
            ImagePath = imagePath;
            Answers = answers;

            CombineFullImagePath(imagePath);
        }

        public void CombineFullImagePath(string imagePath)
        {
            if (Path.IsPathRooted(imagePath)) 
            {
                //user image, save the absolute path
                FullImagePath = imagePath;
            }
            else
            {
                //built-in img, complete the in-app path
                FullImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
            }
        }

    }
}
