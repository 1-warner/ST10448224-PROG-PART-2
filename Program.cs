using System;
using System.IO;
using System.Media;
using System.Collections.Generic;
using System.Linq;

// ChatGPT was used to manage code structure, add sound playback, and apply console color enhancements
// Reference: https://chatgpt.com
// For conditional if-else logic syntax: https://www.w3schools.com/cs/cs_conditions_elseif.php

namespace CybersecurityAwarenessChatbot
{
    // A delegate that defines a structure for methods that generate responses
    public delegate string ResponseHandler(string input, UserState state);

    // This class holds user-specific data to personalize and manage the conversation
    public class UserState
    {
        public bool HasUnlockedCybersecurityTopics { get; set; } = false;  // Tracks if the user has unlocked full topic list
        public List<string> ConversationHistory { get; set; } = new List<string>(); // Stores user messages
        public int PositiveSentimentCount { get; set; } = 0; // Tracks positive sentiment
        public int NegativeSentimentCount { get; set; } = 0; // Tracks negative sentiment
        public string LastTopic { get; set; } = string.Empty; // Remembers last topic for repeat detection
    }

    class Program
    {
        // Dictionary mapping keywords to response methods
        private static Dictionary<string, ResponseHandler> responseHandlers;

        // Keywords used for basic sentiment analysis
        private static readonly string[] positiveWords = { "good", "great", "excellent", "thanks", "helpful", "happy", "appreciate", "like", "love" };
        private static readonly string[] negativeWords = { "bad", "poor", "terrible", "frustrated", "annoyed", "unhelpful", "hate", "dislike", "confusing" };

        // Predefined response sets for randomization (makes bot feel more human)
        private static readonly Dictionary<string, List<string>> randomizedResponses = new Dictionary<string, List<string>>
        {
            // Different greetings for variety
            ["greeting"] = new List<string>
            {
                "Hello! I'm AchA, your Cybersecurity Awareness Assistant.",
                "Hi there! I'm AchA, ready to help with cybersecurity awareness.",
                "Greetings! I'm AchA, your guide to staying safe online."
            },
            // Each category below has 3+ phrasings to keep chatbot replies dynamic
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
            }
        };

        static void Main()
        {
            Console.WriteLine("DEBUG: Starting application");

            // Load topic-based handlers into dictionary
            InitializeResponseHandlers();

            UserState userState = new UserState();

            // Attempt to play welcome sound from file
            try
            {
                string filePath = @"C:\Users\suton\Desktop\st10448224 prog part 2 final\output.wav"; // Voice file from Kokoro TTS

                if (File.Exists(filePath))
                {
                    Console.WriteLine("DEBUG: Audio file found, attempting to play");
                    try
                    {
                        SoundPlayer player = new SoundPlayer(filePath);
                        player.Play();
                        Console.WriteLine("DEBUG: Audio played successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"DEBUG: Audio error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("DEBUG: Audio file not found at path: " + filePath);
                    Console.WriteLine("\u001b[31mWarning: Voice file not found. Skipping sound playback.\u001b[0m");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: Exception in audio initialization: {ex.Message}");
            }

            // Set terminal title and display bot welcome in ASCII art
            Console.Title = "AchA - Cybersecurity Awareness Bot";

            Console.WriteLine("\u001b[31m");
            Console.WriteLine(@"
     █████╗  ██████╗██╗  ██╗ █████╗ 
    ██╔══██╗██╔════╝██║  ██║██╔══██╗
    ███████║██║     ███████║███████║
    ██╔══██║██║     ██╔══██║██╔══██║
    ██║  ██║╚██████╗██║  ██║██║  ██║
    ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝  
        Welcome to AchA chatbot - Your Cybersecurity Awareness bot!
");
            Console.WriteLine("\u001b[0m");

            // Get system username for a more personal greeting
            string desktopUsername = GetDesktopUsername();
            Console.WriteLine($"\u001b[31mChatbot: {GetRandomResponse("greeting")} Nice to meet you, {desktopUsername}!\u001b[0m");

            // Starter suggestions to guide the user
            Console.WriteLine("\n\u001b[31mYou can ask me:\u001b[0m");
            Console.WriteLine("A. What is your purpose?");
            Console.WriteLine("B. How are you?");
            Console.WriteLine("C. What can I ask about?");
            Console.WriteLine();

            Console.WriteLine("DEBUG: Entering main conversation loop");

            // Begin main chat loop
            while (true)
            {
                Console.Write("\u001b[32mYou: \u001b[0m");
                string userInput = Console.ReadLine()?.ToLower().Trim() ?? string.Empty;

                Console.WriteLine($"DEBUG: Received user input: '{userInput}'");

                userState.ConversationHistory.Add(userInput); // Log message

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("\u001b[31mChatbot: I didn't catch that. Could you please try again?\u001b[0m");
                    continue;
                }

                if (userInput == "exit")
                {
                    Console.WriteLine("\u001b[31mChatbot: Thank you for chatting! Stay safe online!\u001b[0m");
                    break;
                }

                // Unlocks additional questions if user asks what they can ask
                if (!userState.HasUnlockedCybersecurityTopics && (userInput == "c" || userInput.Contains("what can i ask")))
                {
                    Console.WriteLine("DEBUG: Unlocking cybersecurity topics");
                    userState.HasUnlockedCybersecurityTopics = true;
                    Console.WriteLine("\n\u001b[31mChatbot: You can now ask me about the following cybersecurity topics:\u001b[0m");
                    Console.WriteLine("1. What is Phishing?");
                    Console.WriteLine("2. Tell me about Password safety.");
                    Console.WriteLine("3. How do I identify Suspicious links?");
                    Console.WriteLine("4. What is Malware?");
                    Console.WriteLine("5. What is Identity theft?");
                    Console.WriteLine("6. End the chat.");
                    Console.WriteLine();
                }

                // Check sentiment based on keywords
                AnalyzeSentiment(userInput, userState);

                // Respond to user's input
                string response = RespondToUser(userInput, userState);
                Console.WriteLine($"\u001b[31mChatbot: {response}\u001b[0m");

                // Sentiment debug
                Console.WriteLine($"DEBUG: Positive sentiment count: {userState.PositiveSentimentCount}, Negative sentiment count: {userState.NegativeSentimentCount}");

                // Check if the user is repeating a question
                CheckForRepeatedQuestions(userState);
            }
        }

        // More comments are added after this line for functions like GetDesktopUsername, AnalyzeSentiment, etc.


        static string GetDesktopUsername()
        {
            try
            {
                string username = Environment.UserName;
                Console.WriteLine($"DEBUG: Retrieved username: {username}");
                return username;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: Error getting username: {ex.Message}");
                return "User";
            }
        }

        static void InitializeResponseHandlers()
        {
            Console.WriteLine("DEBUG: Initializing response handlers");
            responseHandlers = new Dictionary<string, ResponseHandler>
            {
                { "purpose", (input, state) => GetRandomResponse("purpose") },
                { "how are you", (input, state) => GetRandomResponse("how_are_you") },
                { "what can i ask", (input, state) => GetRandomResponse("topics") },
                { "phishing", (input, state) => { state.LastTopic = "phishing"; return GetRandomResponse("phishing"); } },
                { "password", (input, state) => { state.LastTopic = "password"; return GetRandomResponse("password"); } },
                { "link", (input, state) => { state.LastTopic = "links"; return GetRandomResponse("links"); } },
                { "malware", (input, state) => { state.LastTopic = "malware"; return GetRandomResponse("malware"); } },
                { "identity theft", (input, state) => { state.LastTopic = "identity"; return GetRandomResponse("identity"); } }
            };
            Console.WriteLine($"DEBUG: Initialized {responseHandlers.Count} response handlers");
        }

        static string GetRandomResponse(string category)
        {
            if (!randomizedResponses.ContainsKey(category))
            {
                Console.WriteLine($"DEBUG: Response category '{category}' not found");
                return "I don't have information on that topic.";
            }

            List<string> responses = randomizedResponses[category];
            int randomIndex = new Random().Next(responses.Count);
            string response = responses[randomIndex];

            Console.WriteLine($"DEBUG: Selected random response {randomIndex} from {category}");
            return response;
        }

        static void AnalyzeSentiment(string input, UserState state)
        {
            Console.WriteLine("DEBUG: Analyzing sentiment");

            // Check for positive words
            foreach (string word in positiveWords)
            {
                if (input.Contains(word))
                {
                    state.PositiveSentimentCount++;
                    Console.WriteLine($"DEBUG: Found positive word: {word}");
                    break;
                }
            }

            // Check for negative words
            foreach (string word in negativeWords)
            {
                if (input.Contains(word))
                {
                    state.NegativeSentimentCount++;
                    Console.WriteLine($"DEBUG: Found negative word: {word}");
                    break;
                }
            }

            // Respond to sentiment if strongly positive or negative
            if (state.PositiveSentimentCount >= 3 && state.PositiveSentimentCount > state.NegativeSentimentCount)
            {
                Console.WriteLine("DEBUG: Detected consistently positive sentiment");
                Console.WriteLine("\u001b[31mChatbot: I'm glad you're finding this conversation helpful!\u001b[0m");
            }
            else if (state.NegativeSentimentCount >= 3 && state.NegativeSentimentCount > state.PositiveSentimentCount)
            {
                Console.WriteLine("DEBUG: Detected consistently negative sentiment");
                Console.WriteLine("\u001b[31mChatbot: I notice you seem frustrated. Can I help clarify something?\u001b[0m");
            }
        }

        static void CheckForRepeatedQuestions(UserState state)
        {
            if (state.ConversationHistory.Count < 3) return;

            string lastInput = state.ConversationHistory.Last();
            string previousSimilarInput = state.ConversationHistory
                .Take(state.ConversationHistory.Count - 1)
                .FirstOrDefault(input => input.Contains(lastInput) || lastInput.Contains(input));

            if (previousSimilarInput != null)
            {
                Console.WriteLine($"DEBUG: Detected repeated question: {lastInput}");
                Console.WriteLine("\u001b[31mChatbot: I notice you're asking about this topic again. Was my previous answer unclear?\u001b[0m");
            }
        }

        static string RespondToUser(string input, UserState state) //the "if" function allows the user to ask questions by their number or by their text give to the user  
        {
            Console.WriteLine($"DEBUG: Processing input: '{input}', HasUnlockedTopics: {state.HasUnlockedCybersecurityTopics}");

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
                else if (input == "6" || input.Contains("end the chat") || input.Contains("close"))
                {
                    Console.WriteLine("\u001b[31mChatbot: Thank you for chatting! Stay safe online!\u001b[0m");
                    Environment.Exit(0); // Close the console immediately
                    return ""; // This won't be reached but is needed for compilation
                }
                else
                {
                    // Advanced keyword detection
                    Console.WriteLine("DEBUG: No direct match, trying keyword detection");
                    foreach (var handler in responseHandlers)
                    {
                        if (input.Contains(handler.Key.Split(' ')[0])) // Check first word of multi-word keys
                        {
                            Console.WriteLine($"DEBUG: Found partial keyword match: {handler.Key}");
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
    }
}