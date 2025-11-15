using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Quiz
    {

        public string Title { get; set; } = string.Empty;
        public List<Question> Questions { get; set; }

        [JsonIgnore]
        public Random Randomizer { get; set; }
        public Quiz()
        {
            Questions = new List<Question>();
            Randomizer = new Random();
        }
        private int previousIndex = -1;

        public Question? GetRandomQuestion()
        {
            int i = -1;

            if (Questions.Count == 0)
            {
                throw new InvalidOperationException("The quiz has no questions. Add one in quiz editor");
            }

            if (Questions.Count == 1)
            {
                previousIndex = 0;
                return Questions[0]; 
            }
          
            do
            {
                i = Randomizer.Next(0, Questions.Count);
            } while (previousIndex == i);

            previousIndex = i;

            return Questions[i];    
        }

        public void AddQuestion(string statement, int correctAnswer, string imagePath, params string[] answers)
        {
            Question newQuestion = new Question(statement, correctAnswer,imagePath, answers);
            Questions.Add(newQuestion);
        }

        public void RemoveQuestion(int index)
        {
            Questions.Remove(Questions[index]);
        }
    }
}
