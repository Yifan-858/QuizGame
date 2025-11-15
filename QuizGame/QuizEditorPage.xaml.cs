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
using QuizGame.Models;
using System.Text.Json;
using QuizGame.Loader;
using System.Text.RegularExpressions;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for QuizEditorPage.xaml
    /// </summary>
    public partial class QuizEditorPage : Page
    {
        public string dataFolder;
        public string dataFilePath;

        public QuizEditorPage()
        {
            InitializeComponent();
            dataFolder = QuizDataLoader.GetLocalDataFolder();
        }

        public void CreateNewQuiz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string quizCategory = NewQuizCategory.Text.Trim();

                if (string.IsNullOrWhiteSpace(quizCategory))
                {
                    CreateStatusTextBlock.Text = "Please enter a quie category.";
                    CreateStatusTextBlock.Foreground = Brushes.Red;
                }

                Quiz newQuiz = new Quiz
                {
                    Title = quizCategory,
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string jsonString = JsonSerializer.Serialize(newQuiz, options);

                string fileName = quizCategory.Replace(" ", "_");

                dataFilePath = Path.Combine(dataFolder, $"{fileName}.json");

                File.WriteAllText(dataFilePath, jsonString);

                CreateStatusTextBlock.Text = $"Saved to {dataFilePath}";
                CreateStatusTextBlock.Foreground = Brushes.Green;
            }
            catch (Exception ex)
            {
                 CreateStatusTextBlock.Text = $"Something went wrong, {ex.Message}";
                 CreateStatusTextBlock.Foreground = Brushes.Red;
            }
            
        }

        public void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddQuestionPage());
        }

        public void EditExisitQuiz_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EditExistQuizPage());
        }

        public void Return_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StartPage());
        }
    }
}
