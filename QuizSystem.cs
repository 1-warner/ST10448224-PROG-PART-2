using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityAwarenessChatbot
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }

        public QuizQuestion(string question, List<string> options, int correctAnswerIndex, string explanation)
        {
            Question = question;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
            Explanation = explanation;
        }
    }

    public class QuizResult
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double ScorePercentage { get; set; }
        public List<int> UserAnswers { get; set; }
        public List<bool> IsCorrect { get; set; }

        public QuizResult()
        {
            UserAnswers = new List<int>();
            IsCorrect = new List<bool>();
        }
    }

    public static class QuizSystem
    {
        private static readonly List<QuizQuestion> cybersecurityQuestions = new List<QuizQuestion>
        {
            new QuizQuestion(
                "What is phishing?",
                new List<string>
                {
                    "A type of computer virus",
                    "A cyber attack where attackers impersonate legitimate organizations",
                    "A password management tool",
                    "A type of firewall"
                },
                1,
                "Phishing is a cyber attack where attackers impersonate legitimate organizations to steal sensitive information like passwords, credit card numbers, or personal data."
            ),
            new QuizQuestion(
                "What makes a strong password?",
                new List<string>
                {
                    "Using your name and birthdate",
                    "Using the same password for all accounts",
                    "At least 12 characters with letters, numbers, and symbols",
                    "Using common words like 'password' or '123456'"
                },
                2,
                "A strong password should be at least 12 characters long and include a mix of uppercase letters, lowercase letters, numbers, and special symbols."
            ),
            new QuizQuestion(
                "How can you identify a suspicious link?",
                new List<string>
                {
                    "Click on it immediately to check",
                    "Hover over it to see the actual URL",
                    "Ignore all links in emails",
                    "Trust links from friends automatically"
                },
                1,
                "Always hover over links to see the actual URL before clicking. Look for misspellings, unusual domains, or URLs that don't match the expected website."
            ),
            new QuizQuestion(
                "What should you do if you receive a suspicious email?",
                new List<string>
                {
                    "Reply to ask if it's legitimate",
                    "Click on all links to investigate",
                    "Delete it without opening attachments",
                    "Forward it to all your friends"
                },
                2,
                "If you receive a suspicious email, delete it without opening any attachments or clicking on links. Never reply to suspicious emails."
            ),
            new QuizQuestion(
                "What is two-factor authentication (2FA)?",
                new List<string>
                {
                    "Using two different passwords",
                    "A security method requiring two forms of verification",
                    "Having two email accounts",
                    "Using two different browsers"
                },
                1,
                "Two-factor authentication adds an extra layer of security by requiring both your password and a second form of verification (like a code sent to your phone)."
            ),
            new QuizQuestion(
                "What should you do with software updates?",
                new List<string>
                {
                    "Ignore them to save time",
                    "Install them immediately when available",
                    "Only update once a year",
                    "Only update if the computer is slow"
                },
                1,
                "Software updates often contain security patches that fix vulnerabilities. Install them immediately when available to keep your system secure."
            ),
            new QuizQuestion(
                "What is ransomware?",
                new List<string>
                {
                    "A type of antivirus software",
                    "Malicious software that encrypts your files and demands payment",
                    "A password manager",
                    "A type of firewall"
                },
                1,
                "Ransomware is malicious software that encrypts your files and demands payment (ransom) to restore access to your data."
            ),
            new QuizQuestion(
                "What should you do on public Wi-Fi networks?",
                new List<string>
                {
                    "Access your bank account",
                    "Avoid accessing sensitive information",
                    "Share your personal information",
                    "Download files from unknown sources"
                },
                1,
                "Public Wi-Fi networks are often unsecured. Avoid accessing sensitive information like bank accounts or entering passwords on public networks."
            ),
            new QuizQuestion(
                "What is a VPN?",
                new List<string>
                {
                    "A type of computer virus",
                    "A virtual private network that encrypts your internet connection",
                    "A password manager",
                    "A type of firewall"
                },
                1,
                "A VPN (Virtual Private Network) encrypts your internet connection, making it more secure and protecting your privacy online."
            ),
            new QuizQuestion(
                "What should you do if you suspect your account has been compromised?",
                new List<string>
                {
                    "Wait and see if anything happens",
                    "Change your password immediately and contact the service",
                    "Share the incident on social media",
                    "Ignore it and hope it goes away"
                },
                1,
                "If you suspect your account has been compromised, change your password immediately and contact the service provider for assistance."
            )
        };

        public static List<QuizQuestion> GetRandomQuestions(int count = 5)
        {
            var random = new Random();
            return cybersecurityQuestions.OrderBy(x => random.Next()).Take(count).ToList();
        }

        public static QuizResult GradeQuiz(List<QuizQuestion> questions, List<int> userAnswers)
        {
            var result = new QuizResult
            {
                TotalQuestions = questions.Count,
                UserAnswers = userAnswers
            };

            for (int i = 0; i < questions.Count; i++)
            {
                bool isCorrect = userAnswers[i] == questions[i].CorrectAnswerIndex;
                result.IsCorrect.Add(isCorrect);
                if (isCorrect)
                {
                    result.CorrectAnswers++;
                }
            }

            result.ScorePercentage = (double)result.CorrectAnswers / result.TotalQuestions * 100;
            return result;
        }

        public static string GetScoreMessage(double scorePercentage)
        {
            if (scorePercentage >= 90)
                return "Excellent! You have excellent cybersecurity knowledge!";
            else if (scorePercentage >= 80)
                return "Great job! You have good cybersecurity awareness!";
            else if (scorePercentage >= 70)
                return "Good work! You have decent cybersecurity knowledge, but there's room for improvement.";
            else if (scorePercentage >= 60)
                return "Not bad! You have some cybersecurity knowledge, but you should learn more about online safety.";
            else
                return "You need to improve your cybersecurity knowledge. Consider learning more about online safety practices.";
        }

        public static void LogQuizAttempt(QuizResult result)
        {
            ActivityLogger.LogActivity("Quiz Completed", 
                $"Score: {result.CorrectAnswers}/{result.TotalQuestions} ({result.ScorePercentage:F1}%)");
        }
    }
} 