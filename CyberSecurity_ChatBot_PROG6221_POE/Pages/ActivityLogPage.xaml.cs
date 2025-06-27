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

namespace CyberSecurity_ChatBot_PROG6221_POE.Pages
{
    /// <summary>
    /// Interaction logic for ActivityLogPage.xaml
    /// </summary>
    public partial class ActivityLogPage : Page
    {
        private static List<string> activityLog = new List<string>();

        public ActivityLogPage()
        {
            InitializeComponent();
            LoadLog();
        }

        public static void Log(string action)
        {
            string entry = DateTime.Now.ToString("HH:mm") + " - " + action;
            activityLog.Add(entry);
            if (activityLog.Count > 10)
                activityLog.RemoveAt(0);
        }

        private void LoadLog()
        {
            LogListBox.Items.Clear();
            foreach (var item in activityLog)
            {
                LogListBox.Items.Add(new ListBoxItem { Content = item });
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadLog();
        }
    }
}