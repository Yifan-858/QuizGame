using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Quiz
    {
        public string Title { get; private set; }
        public List<Question> Questions { get; private set; }
        public Random Randomizer { get; private set; }
        public Quiz(string title = " ")
        {
            Title = title;
            Questions = new List<Question>();
            Randomizer = new Random();
        }

        public Question GetRandomQuestion()
        {
            int i = Randomizer.Next(0, Questions.Count);
            return Questions[i];
        }

        public void AddQuestion(string statement, int correctAnswer, params string[] answers)
        {
            Question newQuestion = new Question(statement, correctAnswer, answers);
            Questions.Add(newQuestion);
        }

        public void RemoveQuestion(int index)
        {
            Questions.Remove(Questions[index]);
        }
    }
}
