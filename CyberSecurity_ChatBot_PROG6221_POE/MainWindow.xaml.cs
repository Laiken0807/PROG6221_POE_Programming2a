using CyberSecurity_ChatBot_PROG6221_POE.Pages;
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

namespace CyberSecurity_ChatBot_PROG6221_POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ChatPage()); // Default page
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ChatPage());
        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TaskPage());
        }

        private void Quiz_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new QuizPage());
        }

        private void ActivityLog_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ActivityLogPage());
        }
    }
}

