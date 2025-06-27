using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityAwarenessChatbot
{
    public partial class ChatbotForm : Form
    {
        private UserState userState;
        private Dictionary<string, ResponseHandler> responseHandlers;
        private bool isProcessingInput = false;

        // Keywords used for basic sentiment analysis
        private static readonly string[] positiveWords = { "good", "great", "excellent", "thanks", "helpful", "happy", "appreciate", "like", "love" };
        private static readonly string[] negativeWords = { "bad", "poor", "terrible", "frustrated", "annoyed", "unhelpful", "hate", "dislike", "confusing" };

        // Predefined response sets for randomization
        private static readonly Dictionary<string, List<string>> randomizedResponses = new Dictionary<string, List<string>>
        {
            ["greeting"] = new List<string>
            {
                "Hello! I'm AchA, your Cybersecurity Awareness Assistant.",
                "Hi there! I'm AchA, ready to help with cybersecurity awareness.",
                "Greetings! I'm AchA, your guide to staying safe online."
            },
            ["phishing"] = new List<string>
            {
                "Phishing is a cyber attack where attackers impersonate legitimate organizations to steal sensitive information.",
                "Phishing attacks try to trick you into revealing personal info by pretending to be trustworthy sources.",
                "Be careful of phishing - it's when criminals send fake emails that look like they're from real companies to steal your data."
            },
            ["password"] = new List<string>
            {
                "It's important to use strong passwords. A good password should be at least 12 characters long and include a mix of letters, numbers, and symbols.",
                "Strong passwords are your first defense! Use unique combinations of uppercase, lowercase, numbers and symbols.",
                "For password safety, avoid using the same password on multiple sites and consider using a password manager."
            },
            ["links"] = new List<string>
            {
                "Be cautious with links in emails or messages. Hover over the link to see the actual URL before clicking.",
                "Suspicious links often have misspellings or unusual domains. Always verify before clicking.",
                "To identify suspicious links, check if the URL matches the expected website and look for https:// at the beginning."
            },
            ["malware"] = new List<string>
            {
                "Malware is malicious software designed to harm your computer or steal information.",
                "Malware can include viruses, ransomware, spyware, and other harmful programs that compromise your security.",
                "Protect against malware by keeping your software updated and using trusted antivirus programs."
            },
            ["identity"] = new List<string>
            {
                "Identity theft occurs when someone uses your personal information without your permission.",
                "Protect yourself from identity theft by monitoring your accounts and being careful about sharing personal information.",
                "Identity thieves may use your personal data for financial fraud or to impersonate you online."
            },
            ["purpose"] = new List<string>
            {
                "My purpose is to help raise awareness about cybersecurity and keep you informed on how to stay safe online.",
                "I'm designed to educate you about online security threats and protective measures.",
                "I'm here to share cybersecurity best practices and help you stay protected in the digital world."
            },
            ["how_are_you"] = new List<string>
            {
                "I'm just a bot, but I'm always ready to help you stay secure!",
                "As a virtual assistant, I'm operational and ready to assist with your cybersecurity questions!",
                "I'm functioning perfectly and eager to discuss online safety with you!"
            },
            ["topics"] = new List<string>
            {
                "You can now ask me about phishing, password safety, suspicious links, malware, or identity theft.",
                "Feel free to inquire about topics like phishing attacks, password best practices, recognizing suspicious links, malware protection, or preventing identity theft.",
                "I can provide information on several cybersecurity topics including phishing, secure passwords, link safety, malware, and identity protection."
            },
            ["error"] = new List<string>
            {
                "I'm sorry, I don't have information on that topic. Try asking about phishing, passwords, links, malware, or identity theft.",
                "That topic isn't in my knowledge base. I can help with phishing, passwords, suspicious links, malware, or identity theft.",
                "I don't understand that request. Please ask about one of my supported topics: phishing, passwords, links, malware, or identity theft."
            },
            ["quiz"] = new List<string>
            {
                "Great! You can test your cybersecurity knowledge by clicking the 'Take Quiz' button at the top of the window. The quiz includes multiple choice questions about various cybersecurity topics.",
                "I have an interactive quiz feature! Click the purple 'Take Quiz' button in the top-right corner to test your cybersecurity knowledge with multiple choice questions.",
                "Want to test your cybersecurity knowledge? Use the 'Take Quiz' button to take an interactive quiz with questions about phishing, passwords, malware, and more!"
            }
        };

        public ChatbotForm()
        {
            InitializeComponent();
            InitializeChatbot();
        }

        private void InitializeChatbot()
        {
            userState = new UserState();
            InitializeResponseHandlers();
            
            // Log application start
            ActivityLogger.LogApplicationStart();
            
            // Display welcome message
            string desktopUsername = Environment.UserName;
            string welcomeMessage = $"{GetRandomResponse("greeting")} Nice to meet you, {desktopUsername}!";
            
            AppendToChat("AchA", welcomeMessage, Color.DarkRed);
            ActivityLogger.LogBotResponse(welcomeMessage);
            
            // Display initial options
            AppendToChat("AchA", "You can ask me:\nA. What is your purpose?\nB. How are you?\nC. What can I ask about?", Color.DarkRed);
            
            // Try to play welcome sound
            PlayWelcomeSound();
        }

        private void InitializeResponseHandlers()
        {
            responseHandlers = new Dictionary<string, ResponseHandler>
            {
                { "purpose", (input, state) => GetRandomResponse("purpose") },
                { "how are you", (input, state) => GetRandomResponse("how_are_you") },
                { "what can i ask", (input, state) => GetRandomResponse("topics") },
                { "phishing", (input, state) => { state.LastTopic = "phishing"; return GetRandomResponse("phishing"); } },
                { "password", (input, state) => { state.LastTopic = "password"; return GetRandomResponse("password"); } },
                { "link", (input, state) => { state.LastTopic = "links"; return GetRandomResponse("links"); } },
                { "malware", (input, state) => { state.LastTopic = "malware"; return GetRandomResponse("malware"); } },
                { "identity theft", (input, state) => { state.LastTopic = "identity"; return GetRandomResponse("identity"); } },
                { "quiz", (input, state) => { state.LastTopic = "quiz"; return GetRandomResponse("quiz"); } }
            };
        }

        private void PlayWelcomeSound()
        {
            try
            {
                string filePath = @"C:\Users\suton\Desktop\st10448224 prog part 2 final\output.wav";
                if (File.Exists(filePath))
                {
                    SoundPlayer player = new SoundPlayer(filePath);
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                // Silently handle audio errors
                System.Diagnostics.Debug.WriteLine($"Audio error: {ex.Message}");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            ProcessUserInput();
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                ProcessUserInput();
            }
        }

        private void ProcessUserInput()
        {
            if (isProcessingInput) return;

            string userInput = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput)) return;

            isProcessingInput = true;
            
            // Display user input
            AppendToChat("You", userInput, Color.DarkBlue);
            ActivityLogger.LogUserInput(userInput);
            
            // Clear input field
            txtInput.Clear();
            
            // Process the input
            string response = ProcessInput(userInput);
            
            // Display bot response
            AppendToChat("AchA", response, Color.DarkRed);
            ActivityLogger.LogBotResponse(response);
            
            isProcessingInput = false;
            
            // Scroll to bottom
            txtChat.SelectionStart = txtChat.TextLength;
            txtChat.ScrollToCaret();
        }

        private string ProcessInput(string input)
        {
            input = input.ToLower().Trim();
            userState.ConversationHistory.Add(input);

            if (input == "exit")
            {
                ActivityLogger.LogApplicationExit();
                Application.Exit();
                return "";
            }

            // Unlock cybersecurity topics if user asks what they can ask
            if (!userState.HasUnlockedCybersecurityTopics && (input == "c" || input.Contains("what can i ask")))
            {
                userState.HasUnlockedCybersecurityTopics = true;
                ActivityLogger.LogTopicUnlocked();
                
                string topicsMessage = "You can now ask me about the following cybersecurity topics:\n" +
                    "1. What is Phishing?\n" +
                    "2. Tell me about Password safety.\n" +
                    "3. How do I identify Suspicious links?\n" +
                    "4. What is Malware?\n" +
                    "5. What is Identity theft?\n" +
                    "6. Take a Cybersecurity Quiz (click the purple 'Take Quiz' button)\n" +
                    "7. End the chat.";
                
                return topicsMessage;
            }

            // Analyze sentiment
            AnalyzeSentiment(input, userState);

            // Check for repeated questions
            CheckForRepeatedQuestions(userState);

            // Generate response
            return RespondToUser(input, userState);
        }

        private void AnalyzeSentiment(string input, UserState state)
        {
            foreach (string word in positiveWords)
            {
                if (input.Contains(word))
                {
                    state.PositiveSentimentCount++;
                    break;
                }
            }

            foreach (string word in negativeWords)
            {
                if (input.Contains(word))
                {
                    state.NegativeSentimentCount++;
                    break;
                }
            }

            // Log sentiment analysis
            string sentiment = state.PositiveSentimentCount > state.NegativeSentimentCount ? "Positive" : 
                             state.NegativeSentimentCount > state.PositiveSentimentCount ? "Negative" : "Neutral";
            ActivityLogger.LogSentimentAnalysis(sentiment, state.PositiveSentimentCount, state.NegativeSentimentCount);

            // Respond to strong sentiment
            if (state.PositiveSentimentCount >= 3 && state.PositiveSentimentCount > state.NegativeSentimentCount)
            {
                AppendToChat("AchA", "I'm glad you're finding this conversation helpful!", Color.DarkRed);
            }
            else if (state.NegativeSentimentCount >= 3 && state.NegativeSentimentCount > state.PositiveSentimentCount)
            {
                AppendToChat("AchA", "I notice you seem frustrated. Can I help clarify something?", Color.DarkRed);
            }
        }

        private void CheckForRepeatedQuestions(UserState state)
        {
            if (state.ConversationHistory.Count < 3) return;

            string lastInput = state.ConversationHistory.Last();
            string previousSimilarInput = state.ConversationHistory
                .Take(state.ConversationHistory.Count - 1)
                .FirstOrDefault(input => input.Contains(lastInput) || lastInput.Contains(input));

            if (previousSimilarInput != null)
            {
                ActivityLogger.LogRepeatedQuestion(lastInput);
                AppendToChat("AchA", "I notice you're asking about this topic again. Was my previous answer unclear?", Color.DarkRed);
            }
        }

        private string RespondToUser(string input, UserState state)
        {
            // Handle initial menu options
            if (input == "a" || input.Contains("your purpose") || input.Contains("what is your purpose"))
            {
                return responseHandlers["purpose"](input, state);
            }
            else if (input == "b" || input.Contains("how are you"))
            {
                return responseHandlers["how are you"](input, state);
            }
            else if (input == "c" || input.Contains("what can i ask"))
            {
                return responseHandlers["what can i ask"](input, state);
            }
            else if (state.HasUnlockedCybersecurityTopics)
            {
                // Process numbered menu options
                if (input == "1" || input.Contains("phishing"))
                {
                    return responseHandlers["phishing"](input, state);
                }
                else if (input == "2" || input.Contains("password"))
                {
                    return responseHandlers["password"](input, state);
                }
                else if (input == "3" || input.Contains("link"))
                {
                    return responseHandlers["link"](input, state);
                }
                else if (input == "4" || input.Contains("malware"))
                {
                    return responseHandlers["malware"](input, state);
                }
                else if (input == "5" || input.Contains("identity theft"))
                {
                    return responseHandlers["identity theft"](input, state);
                }
                else if (input == "6" || input.Contains("quiz") || input.Contains("test"))
                {
                    return responseHandlers["quiz"](input, state);
                }
                else if (input == "7" || input.Contains("end the chat") || input.Contains("close"))
                {
                    ActivityLogger.LogApplicationExit();
                    Application.Exit();
                    return "";
                }
                else
                {
                    // Advanced keyword detection
                    foreach (var handler in responseHandlers)
                    {
                        if (input.Contains(handler.Key.Split(' ')[0]))
                        {
                            return handler.Value(input, state);
                        }
                    }

                    return GetRandomResponse("error");
                }
            }
            else
            {
                return "Please choose from the available options: A, B, or C.";
            }
        }

        private string GetRandomResponse(string category)
        {
            if (!randomizedResponses.ContainsKey(category))
            {
                return "I don't have information on that topic.";
            }

            List<string> responses = randomizedResponses[category];
            int randomIndex = new Random().Next(responses.Count);
            return responses[randomIndex];
        }

        private void AppendToChat(string sender, string message, Color color)
        {
            if (txtChat.InvokeRequired)
            {
                txtChat.Invoke(new Action(() => AppendToChat(sender, message, color)));
                return;
            }

            txtChat.SelectionStart = txtChat.TextLength;
            txtChat.SelectionLength = 0;

            // Add timestamp
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            txtChat.AppendText($"[{timestamp}] ");
            txtChat.SelectionColor = Color.Gray;

            // Add sender name
            txtChat.AppendText($"{sender}: ");
            txtChat.SelectionColor = color;

            // Add message
            txtChat.AppendText(message + Environment.NewLine);
            txtChat.SelectionColor = txtChat.ForeColor;
        }

        private void btnViewLogs_Click(object sender, EventArgs e)
        {
            string recentLogs = ActivityLogger.GetRecentLogs(100);
            MessageBox.Show(recentLogs, "Recent Activity Logs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOpenLogFile_Click(object sender, EventArgs e)
        {
            string logFilePath = ActivityLogger.GetLogFilePath();
            if (File.Exists(logFilePath))
            {
                System.Diagnostics.Process.Start("notepad.exe", logFilePath);
            }
            else
            {
                MessageBox.Show("No log file found yet.", "Log File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ChatbotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ActivityLogger.LogApplicationExit();
        }

        private void btnQuiz_Click(object sender, EventArgs e)
        {
            ActivityLogger.LogActivity("Quiz Launched", "User clicked Take Quiz button");
            QuizForm quizForm = new QuizForm();
            quizForm.ShowDialog(this);
        }
    }
} 