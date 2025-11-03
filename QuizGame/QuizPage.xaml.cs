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
        //Constructor 1 for build-in category
        public QuizPage(string category = "Snack Quiz")
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

            LoadQuizView(quiz);
        }

        //Constructor 2 for customized category
        public QuizPage(Quiz quiz)
        {
            InitializeComponent();
            LoadQuizView(quiz);
        }

        public void LoadQuizView(Quiz quiz)
        {
            QuizContainer.Children.Clear();
            QuizContainer.Children.Add(new QuizView(quiz));
        }
    }
}
