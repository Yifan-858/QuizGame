﻿using QuizGame.Models;
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
            ViewModel = new QuizViewModel();
            DataContext = ViewModel;
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedIndex = int.Parse(button.Tag.ToString());
            //ViewModel.NextQuestion
        }
    }
}
