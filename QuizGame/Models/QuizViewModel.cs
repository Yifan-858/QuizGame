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
        public string AnswerFeedback { get; private set; }
       
        public string ScoreText
        {
            get
            {
                int precentage = 0;
                if(TotalAnswered > 0)
                {
                    precentage = (int)((double)CorrectlyAnswered / TotalAnswered * 100);
                }
                return $"Your score: {CorrectlyAnswered}/{TotalAnswered} Correct Rate: {precentage}%";
            }

            set { }
        }

        public QuizViewModel(Quiz quiz)
        {
            Quiz = quiz;

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

        public void CheckAnswer(int selectedIndex)
        {
            TotalAnswered++;

            if(CurrentQuestion.CorrectAnswer == selectedIndex)
            {
                CorrectlyAnswered++;
                AnswerFeedback = "Correct Answer!";
            }
            else
            {
                AnswerFeedback = "Wrong Answer!";
            }

            OnPropertyChanged("ScoreText");
            OnPropertyChanged("AnswerFeedback");
        }

        public void NextQuestion()
        {
            AnswerFeedback = " ";
            CurrentQuestion = Quiz.GetRandomQuestion();
            OnPropertyChanged("CurrentQuestion");
            OnPropertyChanged("AnswerFeedback");
        }
    }
}
