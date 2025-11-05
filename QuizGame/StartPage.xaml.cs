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
using QuizGame.Loader;
using QuizGame.Models;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            LoadCustomizedQuiz();
        }

        public void Category_Click(Object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string selectedCategory = button.Tag.ToString();

            this.NavigationService.Navigate(new QuizPage(selectedCategory));
        }

        public void EnterEditor_Click(Object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new QuizEditorPage());
        }

        public void LoadCustomizedQuiz()
        {
            string statusMessage;
            List<Quiz>? quizzes = QuizDataLoader.FindLocalQuizzes(out statusMessage);

            CustomizedQuizContainer.Children.Clear();

            if(quizzes.Count == 0)
            {
                CustomizedQuizContainer.Children.Add(new TextBlock
                {
                    Text = statusMessage,
                    FontSize = 12,
                });

                return;
            }

            foreach(Quiz q in quizzes)
            {
                Button button = new Button
                {
                    Content = q.Title,
                    FontSize=20,
                    Margin= new Thickness(0,5,0,5),
                    FontWeight= FontWeights.Bold,
                    Foreground=Brushes.White,
                    Background=(Brush)new BrushConverter().ConvertFrom("#6f86d6"), 
                    BorderBrush=(Brush)new BrushConverter().ConvertFrom("#6f86d6"),
                    Tag = q  //store the whole quiz object in the tag
                };

                button.Click += LocalQuizCategory_Click;

                CustomizedQuizContainer.Children.Add(button);
            }
        }

        public void LocalQuizCategory_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Quiz quiz = button.Tag as Quiz;

            if (quiz.Questions.Count > 0)
            {
                this.NavigationService.Navigate(new QuizPage(quiz));
            }
            else
            {
                CustomizedQuizFeedbackTextBlock.Text = "No question in the quiz yet. Add more in Quiz Editor.";
            }
            
        }

        public void MixCategory_Click(object sender, RoutedEventArgs e)
        {
             this.NavigationService.Navigate(new MixCategoryQuizPage());
        }
    }
}
