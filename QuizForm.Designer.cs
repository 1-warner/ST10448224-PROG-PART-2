namespace CybersecurityAwarenessChatbot
{
    partial class QuizForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelQuiz = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.panelResults = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNewQuiz = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.RichTextBox();
            this.lblScoreMessage = new System.Windows.Forms.Label();
            this.lblFinalScore = new System.Windows.Forms.Label();
            this.lblQuizTitle = new System.Windows.Forms.Label();
            this.panelQuiz.SuspendLayout();
            this.panelResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelQuiz
            // 
            this.panelQuiz.Controls.Add(this.lblProgress);
            this.panelQuiz.Controls.Add(this.progressBar);
            this.panelQuiz.Controls.Add(this.btnNext);
            this.panelQuiz.Controls.Add(this.btnPrevious);
            this.panelQuiz.Controls.Add(this.panelOptions);
            this.panelQuiz.Controls.Add(this.txtQuestion);
            this.panelQuiz.Controls.Add(this.lblQuestion);
            this.panelQuiz.Controls.Add(this.lblQuizTitle);
            this.panelQuiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQuiz.Location = new System.Drawing.Point(0, 0);
            this.panelQuiz.Name = "panelQuiz";
            this.panelQuiz.Size = new System.Drawing.Size(800, 600);
            this.panelQuiz.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(12, 70);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(45, 17);
            this.lblProgress.TabIndex = 7;
            this.lblProgress.Text = "1 / 5";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 90);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(776, 23);
            this.progressBar.TabIndex = 6;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.DarkGreen;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(713, 550);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 35);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.DarkBlue;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.ForeColor = System.Drawing.Color.White;
            this.btnPrevious.Location = new System.Drawing.Point(632, 550);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 35);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // panelOptions
            // 
            this.panelOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOptions.Location = new System.Drawing.Point(12, 250);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(776, 200);
            this.panelOptions.TabIndex = 3;
            // 
            // txtQuestion
            // 
            this.txtQuestion.BackColor = System.Drawing.Color.White;
            this.txtQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuestion.Location = new System.Drawing.Point(12, 130);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.ReadOnly = true;
            this.txtQuestion.Size = new System.Drawing.Size(776, 100);
            this.txtQuestion.TabIndex = 2;
            this.txtQuestion.Text = "Question text will appear here...";
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(12, 110);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(120, 20);
            this.lblQuestion.TabIndex = 1;
            this.lblQuestion.Text = "Question 1 of 5";
            // 
            // panelResults
            // 
            this.panelResults.Controls.Add(this.btnClose);
            this.panelResults.Controls.Add(this.btnNewQuiz);
            this.panelResults.Controls.Add(this.txtResults);
            this.panelResults.Controls.Add(this.lblScoreMessage);
            this.panelResults.Controls.Add(this.lblFinalScore);
            this.panelResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResults.Location = new System.Drawing.Point(0, 0);
            this.panelResults.Name = "panelResults";
            this.panelResults.Size = new System.Drawing.Size(800, 600);
            this.panelResults.TabIndex = 1;
            this.panelResults.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkRed;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(713, 550);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 35);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNewQuiz
            // 
            this.btnNewQuiz.BackColor = System.Drawing.Color.DarkGreen;
            this.btnNewQuiz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewQuiz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewQuiz.ForeColor = System.Drawing.Color.White;
            this.btnNewQuiz.Location = new System.Drawing.Point(632, 550);
            this.btnNewQuiz.Name = "btnNewQuiz";
            this.btnNewQuiz.Size = new System.Drawing.Size(75, 35);
            this.btnNewQuiz.TabIndex = 3;
            this.btnNewQuiz.Text = "New Quiz";
            this.btnNewQuiz.UseVisualStyleBackColor = false;
            this.btnNewQuiz.Click += new System.EventHandler(this.btnNewQuiz_Click);
            // 
            // txtResults
            // 
            this.txtResults.BackColor = System.Drawing.Color.White;
            this.txtResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(12, 120);
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.Size = new System.Drawing.Size(776, 420);
            this.txtResults.TabIndex = 2;
            this.txtResults.Text = "";
            // 
            // lblScoreMessage
            // 
            this.lblScoreMessage.AutoSize = true;
            this.lblScoreMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScoreMessage.Location = new System.Drawing.Point(12, 90);
            this.lblScoreMessage.Name = "lblScoreMessage";
            this.lblScoreMessage.Size = new System.Drawing.Size(200, 20);
            this.lblScoreMessage.TabIndex = 1;
            this.lblScoreMessage.Text = "Score message will appear here";
            // 
            // lblFinalScore
            // 
            this.lblFinalScore.AutoSize = true;
            this.lblFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinalScore.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblFinalScore.Location = new System.Drawing.Point(12, 50);
            this.lblFinalScore.Name = "lblFinalScore";
            this.lblFinalScore.Size = new System.Drawing.Size(200, 26);
            this.lblFinalScore.TabIndex = 0;
            this.lblFinalScore.Text = "Final Score: X out of Y";
            // 
            // lblQuizTitle
            // 
            this.lblQuizTitle.AutoSize = true;
            this.lblQuizTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuizTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblQuizTitle.Location = new System.Drawing.Point(12, 15);
            this.lblQuizTitle.Name = "lblQuizTitle";
            this.lblQuizTitle.Size = new System.Drawing.Size(400, 29);
            this.lblQuizTitle.TabIndex = 8;
            this.lblQuizTitle.Text = "Cybersecurity Awareness Quiz";
            // 
            // QuizForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panelQuiz);
            this.Controls.Add(this.panelResults);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "QuizForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cybersecurity Quiz - AchA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuizForm_FormClosing);
            this.panelQuiz.ResumeLayout(false);
            this.panelQuiz.PerformLayout();
            this.panelResults.ResumeLayout(false);
            this.panelResults.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelQuiz;
        private System.Windows.Forms.Panel panelResults;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblFinalScore;
        private System.Windows.Forms.Label lblScoreMessage;
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.Button btnNewQuiz;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblQuizTitle;
    }
} 