using MassTransit.Courier.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
using CyberSecurity_ChatBot_PROG6221_POE.Pages;

namespace CyberSecurity_ChatBot_PROG6221_POE.Pages
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        Random rnd = new Random();
        string userName = "";
        string favoriteTopic = "";
        string lastTopic = "";
        bool chatbotUsed = false;

        List<string> passwordResponses = new List<string> {
            "Use a mix of letters (capital and lowercase), numbers, and symbols.",
            "Avoid using personal info. Make it 12+ characters.",
            "Use a password manager to store strong passwords securely."
        };

        List<string> phishingResponses = new List<string> {
            "Phishing tricks you by pretending to be trusted sources.",
            "Never click suspicious links or give your credentials.",
            "Be cautious of unexpected emails asking for info."
        };

        List<string> malwareResponses = new List<string> {
            "Malware includes viruses, spyware, and ransomware.",
            "Keep your system updated and use antivirus software.",
            "Avoid unknown downloads or suspicious links."
        };

        List<string> browsingResponses = new List<string> {
            "Browse safely using HTTPS sites.",
            "Avoid sketchy links and keep your browser updated.",
            "Don’t download files from untrusted websites."
        };

        List<string> purposeResponses = new List<string> {
            "I'm here to help with cybersecurity information and safety tips.",
            "My job is to help you stay safe online!"
        };

        List<string> askResponses = new List<string> {
            "You can ask me about passwords, phishing, malware, or safe browsing.",
            "Feel free to ask anything about cybersecurity."
        };

        List<string> feelingResponses = new List<string> {
            "I'm just a bot, but I’m always ready to help!",
            "Thanks for asking! Let’s stay secure together."
        };

        public ChatPage()
        {
            InitializeComponent();
            PlayGreetingAudio("cyber_bot.wav");
            ShowAsciiArt();
            BotReply("Hello! I’m your Cybersecurity Assistant. What’s your name?");
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string input = ChatInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            if (!chatbotUsed)
            {
                ActivityLogPage.Log("User used the chatbot.");
                chatbotUsed = true;
            }

            ChatListBox.Items.Add(new ListBoxItem { Content = $"You: {input}" });
            ChatInput.Text = "";

            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                BotReply($"Nice to meet you, {userName}! You can ask me about passwords, phishing, malware, or browsing.");
                return;
            }

            HandleUserQuery(input.ToLower());
        }

        private void HandleUserQuery(string input)
        {
            string sentiment = DetectSentiment(input);

            if (input.Contains("favorite") && input.Contains("topic"))
            {
                BotReply("What's your favorite cybersecurity topic?");
                favoriteTopic = input;
                return;
            }

            if (Match(input, new[] { "password", "pass" }))
            {
                lastTopic = "password";
                ActivityLogPage.Log("User asked about passwords.");
                Respond(passwordResponses, sentiment);
            }
            else if (Match(input, new[] { "phishing", "scam", "link" }))
            {
                lastTopic = "phishing";
                ActivityLogPage.Log("User asked about phishing.");
                Respond(phishingResponses, sentiment);
            }
            else if (Match(input, new[] { "malware", "virus", "spyware" }))
            {
                lastTopic = "malware";
                ActivityLogPage.Log("User asked about malware.");
                Respond(malwareResponses, sentiment);
            }
            else if (Match(input, new[] { "browse", "https", "web", "site" }))
            {
                lastTopic = "browsing";
                ActivityLogPage.Log("User asked about safe browsing.");
                Respond(browsingResponses, sentiment);
            }
            else if (Match(input, new[] { "purpose", "why", "function" }))
            {
                lastTopic = "purpose";
                ActivityLogPage.Log("User asked about the chatbot's purpose.");
                Respond(purposeResponses, sentiment);
            }
            else if (Match(input, new[] { "what can i ask", "ask you", "topics" }))
            {
                lastTopic = "ask";
                ActivityLogPage.Log("User asked what they can ask the bot.");
                Respond(askResponses, sentiment);
            }
            else if (Match(input, new[] { "how are you", "how do you feel" }))
            {
                ActivityLogPage.Log("User asked how the chatbot feels.");
                Respond(feelingResponses, sentiment);
            }
            else if (input.Contains("more") || input.Contains("explain") || input.Contains("confused"))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    BotReply($"Here’s more about {lastTopic}:");
                    ActivityLogPage.Log($"User asked for more info about {lastTopic}.");
                    GiveTips(lastTopic);
                }
                else
                {
                    BotReply("Could you tell me what topic you'd like more info on?");
                }
            }
            else if (input.Contains("tip") || input == "yes")
            {
                ActivityLogPage.Log("User asked for general tips.");
                GiveGeneralTips();
            }
            else
            {
                BotReply("Hmm... I’m not sure I understand. Would you like some cybersecurity tips? (yes/no)");
            }
        }

        private void Respond(List<string> list, string sentiment)
        {
            string message = list[rnd.Next(list.Count)];
            if (sentiment == "worried") message += " Don’t worry, you’re doing great!";
            if (sentiment == "curious") message += " Feel free to ask more questions!";
            BotReply(message);
        }

        private void BotReply(string message)
        {
            ChatListBox.Items.Add(new ListBoxItem { Content = $"Bot: {message}" });
        }

        private void GiveTips(string topic)
        {
            if (topic == "password")
                BotReply("Use long passwords with symbols and never reuse them.");
            else if (topic == "phishing")
                BotReply("Always check the sender’s email before clicking links.");
            else if (topic == "malware")
                BotReply("Keep antivirus software up to date.");
            else if (topic == "browsing")
                BotReply("Use HTTPS and don’t click suspicious links.");
            else
                BotReply("I'm here to help with anything related to cybersecurity.");
        }

        private void GiveGeneralTips()
        {
            BotReply("Here are some general cybersecurity tips:");
            BotReply("1. Use strong and unique passwords.");
            BotReply("2. Enable two-factor authentication.");
            BotReply("3. Don’t click suspicious links.");
            BotReply("4. Keep your system and antivirus updated.");
        }

        private bool Match(string input, string[] keywords)
        {
            foreach (string k in keywords)
                if (input.Contains(k)) return true;
            return false;
        }

        private string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("confused")) return "worried";
            if (input.Contains("curious") || input.Contains("learn") || input.Contains("interesting")) return "curious";
            return "neutral";
        }

        private void PlayGreetingAudio(string fileName)
        {
            try
            {
                string fullPath = fileName;
                if (File.Exists(fullPath))
                {
                    SoundPlayer player = new SoundPlayer(fullPath);
                    player.Play();
                }
                else
                {
                    BotReply("Greeting audio not found.");
                }
            }
            catch
            {
                BotReply("Could not play audio.");
            }
        }

        private void ShowAsciiArt()
        {
            AsciiArtBlock.Text = @"
_________        ___.               __________        __   
\_   ___ \___.__.\_ |__   __________\______   \ _____/  |_ 
/    \  \<   |  | | __ \_/ __ \_  __ \    |  _//  _ \   __\
\     \___\___  | | \_\ \  ___/|  | \/    |   (  <_> )  |  
 \______  / ____| |___  /\___  >__|  |______  /\____/|__|  
        \/\/          \/     \/             \/             
";
        }
    }
}