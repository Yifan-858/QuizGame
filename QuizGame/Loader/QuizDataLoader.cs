using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizGame.Models;
using System.Text.Json;
using System.IO;

namespace QuizGame.Loader
{
    public static class QuizDataLoader
    {
        public static Quiz LoadJSON(string dataPath)
        {
            string json = File.ReadAllText(dataPath);
            Quiz quiz = JsonSerializer.Deserialize<Quiz>(json);

            if(quiz != null)
            {
                quiz.Randomizer = new Random();
                foreach(Question q in quiz.Questions)
                {
                    q.CombineFullImagePath(q.ImagePath);
                }
            }

            return quiz;
        }
    }
}
