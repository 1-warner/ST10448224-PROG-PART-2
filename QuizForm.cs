using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CybersecurityAwarenessChatbot
{
    public partial class QuizForm : Form
    {
        private List<QuizQuestion> currentQuestions;
        private List<int> userAnswers;
        private int currentQuestionIndex = 0;
        private bool quizCompleted = false;

        public QuizForm()
        {
            InitializeComponent();
            InitializeQuiz();
        }

        private void InitializeQuiz()
        {
            currentQuestions = QuizSystem.GetRandomQuestions(5);
            userAnswers = new List<int>();
            currentQuestionIndex = 0;
            quizCompleted = false;

            // Initialize user answers with -1 (not answered)
            for (int i = 0; i < currentQuestions.Count; i++)
            {
                userAnswers.Add(-1);
            }

            DisplayCurrentQuestion();
            UpdateProgressBar();
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex >= currentQuestions.Count)
            {
                ShowResults();
                return;
            }

            var question = currentQuestions[currentQuestionIndex];
            lblQuestion.Text = $"Question {currentQuestionIndex + 1} of {currentQuestions.Count}";
            txtQuestion.Text = question.Question;

            // Clear previous radio buttons
            panelOptions.Controls.Clear();

            // Create radio buttons for options
            for (int i = 0; i < question.Options.Count; i++)
            {
                RadioButton rb = new RadioButton
                {
                    Text = $"{GetOptionLetter(i)}. {question.Options[i]}",
                    Tag = i,
                    AutoSize = true,
                    Location = new Point(10, i * 30 + 10),
                    Width = panelOptions.Width - 20
                };

                // Check if this option was previously selected
                if (userAnswers[currentQuestionIndex] == i)
                {
                    rb.Checked = true;
                }

                panelOptions.Controls.Add(rb);
            }

            // Update navigation buttons
            btnPrevious.Enabled = currentQuestionIndex > 0;
            btnNext.Text = currentQuestionIndex == currentQuestions.Count - 1 ? "Finish Quiz" : "Next";
        }

        private string GetOptionLetter(int index)
        {
            return ((char)('A' + index)).ToString();
        }

        private void UpdateProgressBar()
        {
            progressBar.Value = (int)((double)(currentQuestionIndex + 1) / currentQuestions.Count * 100);
            lblProgress.Text = $"{currentQuestionIndex + 1} / {currentQuestions.Count}";
        }

        private void SaveCurrentAnswer()
        {
            if (currentQuestionIndex >= currentQuestions.Count) return;

            // Find the selected radio button
            foreach (Control control in panelOptions.Controls)
            {
                if (control is RadioButton rb && rb.Checked)
                {
                    userAnswers[currentQuestionIndex] = (int)rb.Tag;
                    break;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();

            if (currentQuestionIndex == currentQuestions.Count - 1)
            {
                ShowResults();
            }
            else
            {
                currentQuestionIndex++;
                DisplayCurrentQuestion();
                UpdateProgressBar();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();

            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                DisplayCurrentQuestion();
                UpdateProgressBar();
            }
        }

        private void ShowResults()
        {
            quizCompleted = true;
            panelQuiz.Visible = false;
            panelResults.Visible = true;

            var result = QuizSystem.GradeQuiz(currentQuestions, userAnswers);
            QuizSystem.LogQuizAttempt(result);

            lblFinalScore.Text = $"Final Score: {result.CorrectAnswers} out of {result.TotalQuestions} ({result.ScorePercentage:F1}%)";
            lblScoreMessage.Text = QuizSystem.GetScoreMessage(result.ScorePercentage);

            // Display detailed results
            txtResults.Clear();
            txtResults.AppendText($"QUIZ RESULTS\n");
            txtResults.AppendText($"============\n\n");

            for (int i = 0; i < currentQuestions.Count; i++)
            {
                var question = currentQuestions[i];
                var userAnswer = userAnswers[i];
                var isCorrect = result.IsCorrect[i];

                txtResults.AppendText($"Question {i + 1}: {question.Question}\n");
                txtResults.AppendText($"Your Answer: {GetOptionLetter(userAnswer)}. {question.Options[userAnswer]}\n");
                txtResults.AppendText($"Correct Answer: {GetOptionLetter(question.CorrectAnswerIndex)}. {question.Options[question.CorrectAnswerIndex]}\n");
                txtResults.AppendText($"Result: {(isCorrect ? "✓ CORRECT" : "✗ INCORRECT")}\n");
                txtResults.AppendText($"Explanation: {question.Explanation}\n");
                txtResults.AppendText("\n");
            }

            txtResults.AppendText($"Overall Score: {result.ScorePercentage:F1}%\n");
            txtResults.AppendText($"Message: {QuizSystem.GetScoreMessage(result.ScorePercentage)}");
        }

        private void btnNewQuiz_Click(object sender, EventArgs e)
        {
            panelResults.Visible = false;
            panelQuiz.Visible = true;
            InitializeQuiz();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QuizForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!quizCompleted)
            {
                var result = MessageBox.Show("Are you sure you want to exit? Your quiz progress will be lost.", 
                    "Exit Quiz", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
} 