using System;
using System.Windows.Forms;
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

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChatbotForm());
        }
    }
}