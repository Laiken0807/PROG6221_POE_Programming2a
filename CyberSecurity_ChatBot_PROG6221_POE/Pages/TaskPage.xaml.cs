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
    /// Interaction logic for TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {
        public class TaskItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? Reminder { get; set; }
            public bool IsCompleted { get; set; }

            public override string ToString()
            {
                string status = IsCompleted ? "[Completed]" : "[Pending]";
                string reminderText = Reminder.HasValue ? $" (Reminder: {Reminder.Value:yyyy-MM-dd HH:mm})" : "";
                return $"{status} {Title} - {Description}{reminderText}";
            }
        }

        private List<TaskItem> taskList = new List<TaskItem>();

        public TaskPage()
        {
            InitializeComponent();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text.Trim();
            string description = TaskDescBox.Text.Trim();
            DateTime? reminder = null;

            if (ReminderDatePicker.SelectedDate.HasValue && TimeSpan.TryParse(ReminderTimeBox.Text, out TimeSpan timePart))
            {
                reminder = ReminderDatePicker.SelectedDate.Value.Date + timePart;
            }

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("⚠️ Please enter both a task title and description.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TaskItem task = new TaskItem
            {
                Title = title,
                Description = description,
                Reminder = reminder,
                IsCompleted = false
            };

            taskList.Add(task);
            TaskListBox.Items.Add(new ListBoxItem { Content = task.ToString() });

            TaskTitleBox.Text = "";
            TaskDescBox.Text = "";
            ReminderDatePicker.SelectedDate = null;
            ReminderTimeBox.Text = "";

            ActivityLogPage.Log("User added a task with reminder.");
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            int index = TaskListBox.SelectedIndex;

            if (index >= 0 && index < taskList.Count)
            {
                taskList[index].IsCompleted = true;

                TaskListBox.Items[index] = new ListBoxItem
                {
                    Content = taskList[index].ToString(),
                    Foreground = new SolidColorBrush(Colors.Teal)
                };

                ActivityLogPage.Log("User completed a task.");
            }
            else
            {
                MessageBox.Show("Please select a task to mark as complete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            int index = TaskListBox.SelectedIndex;

            if (index >= 0 && index < taskList.Count)
            {
                taskList.RemoveAt(index);
                TaskListBox.Items.RemoveAt(index);

                ActivityLogPage.Log("User deleted a task.");
            }
            else
            {
                MessageBox.Show("Please select a task to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}