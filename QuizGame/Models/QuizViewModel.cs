using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace QuizGame.Models
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        public Quiz Quiz { get; private set; }
        public Question CurrentQuestion { get; private set; }
        public int SelectedAnswerIndex { get; private set; }
        public int CorrectlyAnswered { get; private set; }
        public int TotalAnswered { get; private set; }
        public string ScoreText
        {
            get
            {
                return $"{CorrectlyAnswered} / {TotalAnswered}";
            }

            set { }
        }

        public QuizViewModel()
        {
            Quiz = new Quiz("Nut Quiz");

            Quiz.AddQuestion("Which nut do you like the most?", 1, "Pistashu", "Peanut", "Walnut","Almond");

            CurrentQuestion = Quiz.GetRandomQuestion();
            SelectedAnswerIndex = -1;

            OnPropertyChanged("CurrentQuestion");

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
