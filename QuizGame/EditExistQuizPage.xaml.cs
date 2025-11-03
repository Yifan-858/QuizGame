using QuizGame.Controls;
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


namespace QuizGame
{
    /// <summary>
    /// Interaction logic for EditExistQuizPage.xaml
    /// </summary>
    public partial class EditExistQuizPage : Page
    {
        public EditExistQuizPage()
        {
            InitializeComponent();
        }

        public void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[]? jsonFilePath = QuizDataLoader.GetLocalJsonFiles();
                int selectedQuizIndex = CustomizedQuizComboBox.SelectedIndex;

                string selectedQuizPath = jsonFilePath[selectedQuizIndex];

                string jsonString = File.ReadAllText(selectedQuizPath);
                JSONOutputTextBox.Text = jsonString;
            }
            catch (Exception ex)
            {
                LoadJsonFeedbackTextBox.Text = $"Something went wrong with loading. {ex.Message}";
            }
        }

        public void AddFromJsonButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.Filter = "JSON Files (*.json)|*.json";

                if (dialog.ShowDialog() == true)
                {
                    string jsonFilePath = dialog.FileName;
                    string jsonString = File.ReadAllText(jsonFilePath);
                    JSONOutputTextBox.Text = jsonString;
                }
            }
            catch (Exception ex)
            {
                 LoadJsonFeedbackTextBox.Text = $"Something went wrong with loading. {ex.Message}";
            }
        }

        public async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string jsonString = JSONOutputTextBox.Text;

            //check if JSONOutputTextBox.Text holds the jsonString
            if(string.IsNullOrWhiteSpace(jsonString))
            {
                SaveFeedbackTextBox.Text = "Choose a category or load a JSON file first";
                return;
            }

            try
            {
                //validate the json format is correct 
                using JsonDocument doc = JsonDocument.Parse(jsonString);

                //get the quiz title
                JsonElement root = JsonDocument.Parse(jsonString).RootElement;
                string title = root.GetProperty("Title").GetString() ?? "NewQuiz";
                string jsonFileName = title.Replace(" ", "_") + ".json";

                //get full save path
                string dataFolder = QuizDataLoader.GetLocalDataFolder();
                string fullSavePath = Path.Combine(dataFolder, jsonFileName);

                //save quiz json
                File.WriteAllText(fullSavePath, jsonString);

                SaveFeedbackTextBox.Text = $"Saved to {fullSavePath}";
                SaveFeedbackTextBox.Foreground = Brushes.Green;

                await Task.Delay(1000);
                ClearInputField();
            }
            catch (Exception ex)
            {
                SaveFeedbackTextBox.Text = $"Error: {ex.Message}";
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
            JSONOutputTextBox.Text = string.Empty;
            SaveFeedbackTextBox.Text = string.Empty;
        }
    }
}
