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
    /// Interaction logic for QuizView.xaml
    /// </summary>
    public partial class QuizView : UserControl
    {
        public QuizViewModel ViewModel { get; set; }
        public QuizView()
        {
            InitializeComponent();

            Quiz nutQuiz = new Quiz("Nut Quiz");
            nutQuiz.AddQuestion("Which nut do you like the most?", 1, "Pistashu", "Peanut", "Walnut","Almond");
            nutQuiz.AddQuestion("Who is the best?", 2, "Picachu", "Ditto", "Kalakala","Miu2");
            nutQuiz.AddQuestion("Where do you live?", 0, "Gothenburg", "London", "Stockholm","Malmö");

            ViewModel = new QuizViewModel(nutQuiz);
            DataContext = ViewModel;
        }

        //private int currentQuestionIndex = 0;
        //private void GetQuestionElement()
        //{
        //    if(currentQuestionIndex >= 
        //}

        private async void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            int selectedIndex = int.Parse(button.Tag.ToString());

            ViewModel.CheckAnswer(selectedIndex);

            await Task.Delay(1000);

            ViewModel.NextQuestion();
        }
    }
}
