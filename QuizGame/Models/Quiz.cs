using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Quiz
    {
        private string _title = string.Empty;
        public string Title => _title;

        private List<Question> _questions;
        public List<Question> Questions => _questions;
        public Random Randomizer { get; private set; }
        public Quiz(string title = " ")
        {
            _title = title;
            _questions = new List<Question>();
            Randomizer = new Random();
        }
        private int previousIndex = -1;

        public Question GetRandomQuestion()
        {
            int i = -1;

            do
            {
               i = Randomizer.Next(0, Questions.Count);
            } while (previousIndex == i);

            previousIndex = i;

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
