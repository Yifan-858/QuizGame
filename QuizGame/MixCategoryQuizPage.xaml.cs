using QuizGame.Loader;
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
using System.Windows.Shapes;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for MixCategoryQuizPage.xaml
    /// </summary>
    public partial class MixCategoryQuizPage : Page
    {
        public string statusMessage;
        public List<Quiz>? allQuizzes;
        public List<Quiz> selectedQuizzes = new List<Quiz>();
        public Quiz quiz;

        public MixCategoryQuizPage()
        {
            InitializeComponent();
            allQuizzes = QuizDataLoader.FindLocalQuizzes(out string localStatusMessage);
            statusMessage = localStatusMessage;
            GetCategoryCheckbox();
        }

        public void GetCategoryCheckbox()
        {
            if (allQuizzes == null) return;

            for (int i = 0; i < allQuizzes.Count; i++)
            {
                CheckBox cb = new CheckBox
                {
                    Content = allQuizzes[i].Title,
                    Tag = i,
                    FontSize = 15,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                cb.Checked += CheckBox_Changed;
                cb.Unchecked += CheckBox_Changed;

                CategoryCheckboxContainer.Children.Add(cb);
            }
        }

        public void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag is int index)
            {
                bool isChecked = cb.IsChecked ?? false;
                if(allQuizzes != null)
                {
                    if (isChecked)
                    {
                        if (!selectedQuizzes.Contains(allQuizzes[index]))
                        {
                            selectedQuizzes.Add(allQuizzes[index]);
                        }
                    }
                    else
                    {
                        selectedQuizzes.Remove(allQuizzes[index]);
                    }
                }
               
            }
        }

         public void LoadMixedQues_Click(object sender, RoutedEventArgs e)
        {
            Quiz mixedQuiz = new Quiz
            {
                Title = "Shuffle the quiz",
            };

            foreach(Quiz q in selectedQuizzes)
            {
                mixedQuiz.Questions.AddRange(q.Questions);
            }
            
            this.NavigationService.Navigate(new QuizPage( mixedQuiz));
        }

        public void Return_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StartPage());
        }
    }
}
