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
    /// Interaction logic for AddQuestionPage.xaml
    /// </summary>
    public partial class AddQuestionPage : Page
    {
        public AddQuestionPage()
        {
            InitializeComponent();
        }

        public void CreateNewQuiz_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Clear_Click(object sender, RoutedEventArgs e)
        {
            ExistQuizComboBox.SelectedIndex = -1;
            AddNewQuestionTextBox.Clear();
            Answer1TextBox.Clear();
            Answer2TextBox.Clear();
            Answer3TextBox.Clear();
            Answer4TextBox.Clear();

            CorrectIndexButton1.IsChecked = false;
            CorrectIndexButton2.IsChecked = false;
            CorrectIndexButton3.IsChecked = false;
            CorrectIndexButton4.IsChecked = false;
        }

        public void Return_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new QuizEditorPage());
        }
    }
}
