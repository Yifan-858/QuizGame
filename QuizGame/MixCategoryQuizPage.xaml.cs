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
            if (allQuizzes == null ||  allQuizzes.Count == 0) 
            { 
                TextBlock text = new TextBlock
                {
                    Text = "You dont't have any quiz. Please add more in quiz editor.",
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 14,
                    Foreground = Brushes.Red, 
                    Margin = new Thickness(5)
                };

                CategoryCheckboxContainer.Children.Add(text);
            }

            if( allQuizzes.Count == 1)
            {
                TextBlock text = new TextBlock
                {
                    Text = "There are not enough quizzes to be mixed. Please add more in quiz editor.",
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 14,
                    Foreground = Brushes.Red, 
                    Margin = new Thickness(5)
                };

                CategoryCheckboxContainer.Children.Add(text);
            }

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

            if(selectedQuizzes == null)
            {
                ShuffleFeedback.Text = "Add more quiz in quiz editor";
                return;
            }

            if( selectedQuizzes.Count>=0 && selectedQuizzes.Count < 2)
            {
                ShuffleFeedback.Text = "Choose at least 2 quizzes";
                return;
            }

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
