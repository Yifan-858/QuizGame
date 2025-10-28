using QuizGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using QuizGame.Loader;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for QuizPage.xaml
    /// </summary>
    public partial class QuizPage : Page
    {
        public QuizPage(string category)
        {
            InitializeComponent();
            string dataPath = " ";

            switch (category)
            {
                case "Snack Quiz":
                    dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "quizData1.json");
                    break;
                case "Game Quiz":
                    dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "quizData2.json");
                    break;
                default:
                    dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "quizData1.json");
                    break;
            }

            Quiz quiz = QuizDataLoader.LoadJSON(dataPath);

            QuizContainer.Children.Clear();
            QuizContainer.Children.Add(new QuizView(quiz));
        }
    }
}
