using QuizGame.Loader;
using QuizGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for AddQuestionPage.xaml
    /// </summary>
    public partial class AddQuestionPage : Page
    {

        public string pictureAbsolutePath;
        public AddQuestionPage()
        {
            InitializeComponent();
        }

        public void Picture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;)|*.png;*.jpg;*.jpeg;";

            if (dialog.ShowDialog() == true)
            {
                pictureAbsolutePath = dialog.FileName.Replace("\\", "/");; 
                PictureFeedback.Text = $"Add {pictureAbsolutePath}"; 
            }
        }

        public async void SubmitNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //find the save path from the ComboBox index
                int dataFileIndex = CustomizedQuizComboBox.SelectedIndex;

                if(dataFileIndex <= -1)
                {
                    SubmitFeedbckTextBlock.Text = "Invalid quiz. Please select a category.";
                    return;
                }

                string[]? jsonFilePaths = QuizDataLoader.GetLocalJsonFiles();

                if(jsonFilePaths == null || jsonFilePaths.Length == 0 || dataFileIndex >= jsonFilePaths.Length)
                {
                    SubmitFeedbckTextBlock.Text = "Quiz File not found";
                    return;
                }

                string selectedQuizFilePath = jsonFilePaths[dataFileIndex];

                //load the selectQuiz
                string jsonString = File.ReadAllText(selectedQuizFilePath);
                Quiz selectedQuiz = JsonSerializer.Deserialize<Quiz>(jsonString)?? throw new Exception("Failed to load quiz from JSON.");;

                string[] answers = { Answer1TextBox.Text, Answer2TextBox.Text, Answer3TextBox.Text, Answer4TextBox.Text };

                //add in the new question
                if(pictureAbsolutePath == null)
                {
                    string defaultPicturePath = "Image/quizDefault.jpg";
                    selectedQuiz.AddQuestion(NewQuestionStatementTextBox.Text,CorrectIndexComboBox.SelectedIndex,defaultPicturePath,answers);
                }
                else
                {
                    selectedQuiz.AddQuestion(NewQuestionStatementTextBox.Text,CorrectIndexComboBox.SelectedIndex,pictureAbsolutePath,answers);
                }

                //submit the form
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string updatedQuizJSON = JsonSerializer.Serialize(selectedQuiz, options);
                File.WriteAllText(selectedQuizFilePath, updatedQuizJSON);
                SubmitFeedbckTextBlock.Text = $"Saved to {selectedQuiz}";
                SubmitFeedbckTextBlock.Foreground = Brushes.Green;

                await Task.Delay(1000);
                ClearInputField();
            }
            catch (Exception ex)
            {
                SubmitFeedbckTextBlock.Text = $"Something went wrong. {ex.Message}";
            }
        }

        public void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputField();
        }

        public void Return_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new QuizEditorPage());
        }

        public void ClearInputField()
        {
            CustomizedQuizComboBox.SelectedIndex = -1;
            NewQuestionStatementTextBox.Clear();
            Answer1TextBox.Clear();
            Answer2TextBox.Clear();
            Answer3TextBox.Clear();
            Answer4TextBox.Clear();
            SubmitFeedbckTextBlock.Text = string.Empty;
            PictureFeedback.Text = string.Empty;
            CorrectIndexComboBox.SelectedIndex = -1;
        }
    }
}
