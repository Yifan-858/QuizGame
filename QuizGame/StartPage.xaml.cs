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
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        public void Category_Click(Object sender, RoutedEventArgs e)
        {
            //var quizWindow = new QuizWindow() { Owner = this };
            //quizWindow.ShowDialog();

            //open a page instead
            Button button = sender as Button;
            string selectedCategory = button.Tag.ToString();

            this.NavigationService.Navigate(new QuizPage(selectedCategory));

        }

        public void EnterEditor_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new QuizEditorPage());
        }
    }
}
