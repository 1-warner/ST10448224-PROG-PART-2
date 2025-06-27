using System;
using System.IO;
using System.Text;

namespace CybersecurityAwarenessChatbot
{
    public class ActivityLogger
    {
        private static readonly string LogFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "AchA_Activity_Log.txt");

        private static readonly object LogLock = new object();

        public static void LogActivity(string activity, string details = "")
        {
            try
            {
                lock (LogLock)
                {
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string logEntry = $"[{timestamp}] {activity}";
                    
                    if (!string.IsNullOrEmpty(details))
                    {
                        logEntry += $" - {details}";
                    }

                    // Ensure the log file exists and append the entry
                    File.AppendAllText(LogFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                // If logging fails, we don't want to crash the application
                System.Diagnostics.Debug.WriteLine($"Logging failed: {ex.Message}");
            }
        }

        public static void LogUserInput(string userInput)
        {
            LogActivity("User Input", userInput);
        }

        public static void LogBotResponse(string response)
        {
            LogActivity("Bot Response", response);
        }

        public static void LogSentimentAnalysis(string sentiment, int positiveCount, int negativeCount)
        {
            LogActivity("Sentiment Analysis", $"{sentiment} (Positive: {positiveCount}, Negative: {negativeCount})");
        }

        public static void LogTopicUnlocked()
        {
            LogActivity("Topics Unlocked", "User unlocked cybersecurity topics");
        }

        public static void LogRepeatedQuestion(string question)
        {
            LogActivity("Repeated Question Detected", question);
        }

        public static void LogApplicationStart()
        {
            LogActivity("Application Started", $"User: {Environment.UserName}");
        }

        public static void LogApplicationExit()
        {
            LogActivity("Application Exited");
        }

        public static string GetLogFilePath()
        {
            return LogFilePath;
        }

        public static string GetRecentLogs(int numberOfLines = 50)
        {
            try
            {
                if (!File.Exists(LogFilePath))
                {
                    return "No log file found.";
                }

                string[] lines = File.ReadAllLines(LogFilePath);
                int startIndex = Math.Max(0, lines.Length - numberOfLines);
                return string.Join(Environment.NewLine, lines, startIndex, lines.Length - startIndex);
            }
            catch (Exception ex)
            {
                return $"Error reading log file: {ex.Message}";
            }
        }
    }
} 