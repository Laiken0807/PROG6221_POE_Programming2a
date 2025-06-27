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
    /// Interaction logic for QuizPage.xaml
    /// </summary>
    public partial class QuizPage : Page
    {
        private List<QuizQuestion> quizQuestions = new List<QuizQuestion>();
        private int currentQuestionIndex = 0;
        private int score = 0;

        public QuizPage()
        {
            InitializeComponent();
            LoadQuestions();
            LoadQuestion(currentQuestionIndex);
            ActivityLogPage.Log("User started the cybersecurity quiz.");
        }

        private void LoadQuestions()
        {
            quizQuestions.Add(new QuizQuestion(
                "Which of the following optiond best describes phishing?\n(a) A funn internet meme\n(b) A social engineering attack aiming to stealing sensitive information\n(c) A type of secure email protocol",
                "b",
                "Phishing is a cybercrime where attackers trick individuals into revealing sensitive data through fake emails or websites."));

            quizQuestions.Add(new QuizQuestion(
                "Why is it important to use a combination of upper/lowercase letters, numbers, and symbols in your password?\n(a) It looks fancy\n(b) It increases the complexity, making it harder to guess or crack\n(c) Some websites require it for visual consistency",
                "b",
                "Complex passwords reduce the chance of cyberattacks."));

            quizQuestions.Add(new QuizQuestion(
                "Which situation is most likely to indicate a phishing attempt?\n(a) An email from your bank asking to confirm your password via a link\n(b) A newsletter you subscribed to\n(c) A weekly software update alert from your installed app",
                "a",
                "Legitimate banks will never ask for sensitive information through a link in an unsolicited email."));

            quizQuestions.Add(new QuizQuestion(
                "What is the main purpose of Two-Factor Authentication (2FA)?\n(a) To slow down the login process\n(b) To double encrypt your data\n(c) To provide an additional security layer by requiring two forms of verification",
                "c",
                "2FA requires something you know (password) and something you have (like a code sent to your phone)."));

            quizQuestions.Add(new QuizQuestion(
                "Where is the safest place to store complex passwords for multiple accounts?\n(a) In a notebook under your bed\n(b) Saved in browser without encryption\n(c) In a reputable password manager with encryption",
                "c",
                "Password managers securely store and encrypt your passwords, reducing the chance of compromise."));

            quizQuestions.Add(new QuizQuestion(
                "True or False: Using public Wi-Fi without a VPN can expose your private data to attackers.",
                "true",
                "True. Public networks are often unsecured, and attackers can intercept your traffic."));

            quizQuestions.Add(new QuizQuestion(
                "True or False: Antivirus software is all you need to stay safe online.",
                "false",
                "False. Antivirus is helpful, but online safety requires cautious behavior and system updates."));

            quizQuestions.Add(new QuizQuestion(
                "True or False: Backing up your data is unnecessary if you have antivirus protection.",
                "false",
                "False. Antivirus doesn’t protect you from accidental deletion, hardware failure, or ransomware."));

            quizQuestions.Add(new QuizQuestion(
                "True or False: Clicking links from unknown sources is okay if you're using a secure browser.",
                "false",
                "False. Even secure browsers can’t protect you from phishing or malicious websites you willingly visit."));

            quizQuestions.Add(new QuizQuestion(
                "True or False: Strong passwords should be reused on multiple sites to make them easier to remember.",
                "false",
                "False. Reusing passwords across sites means one breach could compromise all your accounts."));
        }

        private void LoadQuestion(int index)
        {
            if (index < quizQuestions.Count)
            {
                QuestionTextBlock.Text = $"Q{index + 1}: {quizQuestions[index].Question}";
            }
            else
            {
                QuestionTextBlock.Text = "Quiz complete!";
            }
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuestionIndex >= quizQuestions.Count)
                return;

            string userAnswer = AnswerInput.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(userAnswer))
            {
                QuizListBox.Items.Add(new ListBoxItem { Content = "❗ Please enter an answer before submitting." });
                return;
            }

            var question = quizQuestions[currentQuestionIndex];
            string correct = question.Answer.ToLower();

            if (userAnswer == correct)
            {
                score++;
                QuizListBox.Items.Add(new ListBoxItem { Content = $" Correct! Answer: {correct}" });
            }
            else
            {
                QuizListBox.Items.Add(new ListBoxItem { Content = $" Incorrect. Correct answer: {correct}" });
            }

            QuizListBox.Items.Add(new ListBoxItem
            {
                Content = $"📘 Explanation: {question.Explanation}",
                Foreground = System.Windows.Media.Brushes.Navy
            });

            currentQuestionIndex++;
            AnswerInput.Text = "";

            if (currentQuestionIndex < quizQuestions.Count)
            {
                LoadQuestion(currentQuestionIndex);
            }
            else
            {
                QuizListBox.Items.Add(new ListBoxItem { Content = $"🎯 Final Score: {score} out of {quizQuestions.Count}" });

                if (score >= 8)
                    QuizListBox.Items.Add(new ListBoxItem { Content = " Excellent! You're a cybersecurity pro!" });
                else if (score >= 5)
                    QuizListBox.Items.Add(new ListBoxItem { Content = " Good job! A little more practice and you’ll master it." });
                else
                    QuizListBox.Items.Add(new ListBoxItem { Content = " Keep learning. Your cybersecurity journey is just beginning!" });

                ActivityLogPage.Log($"Quiz completed with score {score}/{quizQuestions.Count}.");
                SubmitButton.IsEnabled = false;
                AnswerInput.IsEnabled = false;
                TryAgainButton.Visibility = Visibility.Visible;
            }
        }

        private void TryAgain_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex = 0;
            score = 0;
            QuizListBox.Items.Clear();
            SubmitButton.IsEnabled = true;
            AnswerInput.IsEnabled = true;
            TryAgainButton.Visibility = Visibility.Collapsed;
            LoadQuestion(currentQuestionIndex);
            ActivityLogPage.Log("User restarted the quiz.");
        }

        private class QuizQuestion
        {
            public string Question;
            public string Answer;
            public string Explanation;

            public QuizQuestion(string question, string answer, string explanation)
            {
                Question = question;
                Answer = answer;
                Explanation = explanation;
            }
        }
    }
}