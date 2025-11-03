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

namespace QuizGame.Controls
{
    /// <summary>
    /// Interaction logic for CustomizedQuizComboBox.xaml
    /// </summary>
    public partial class CustomizedQuizComboBox : UserControl
    {
        public string StatusMessage { get; private set; }
        public int SelectedIndex
        {
            get
            {
                return InnerComboBox.SelectedIndex;
            }

            set
            {
                InnerComboBox.SelectedIndex = value;
            }
        }
          
        public CustomizedQuizComboBox()
        {
            InitializeComponent();
            this.Loaded += (s, e) => LoadAllCustomizedQuiz();
        }

        public void LoadAllCustomizedQuiz()
        {
            string statusMessage;
            List<Quiz>? quizzes = QuizDataLoader.FindLocalQuizzes(out statusMessage);

            if (quizzes.Count > 0)
            {
                foreach(Quiz q in quizzes)
                {
                    InnerComboBox.Items.Add(q.Title);
                }
            }
        }
    }
}
