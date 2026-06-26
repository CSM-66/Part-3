using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChatBOT.ChatBot
{
    public class QuizForm : Form
    {
        private List<QuizQuestion> _questions = new List<QuizQuestion>();
        private int _currentIndex = 0;
        private int _score = 0;

        private Label lblQuestion = new Label();
        private Label lblProgress = new Label();
        private Button btnA = new Button();
        private Button btnB = new Button();
        private Button btnC = new Button();
        private Button btnD = new Button();
        private Label lblFeedback = new Label();
        private Button btnNext = new Button();

        public QuizForm()
        {
            LoadQuestions();
            BuildUI();
            ShowQuestion();
        }

        private void LoadQuestions()
        {
            _questions = new List<QuizQuestion>
            {
                new QuizQuestion("What should you do if you receive an email asking for your password?",
                    new[] { "A) Reply with your password", "B) Delete the email", "C) Report it as phishing", "D) Ignore it" }, "C",
                    "Reporting phishing emails helps prevent scams and protects others."),

                new QuizQuestion("How long should a strong password be?",
                    new[] { "A) 4 characters", "B) 6 characters", "C) 8 characters", "D) At least 12 characters" }, "D",
                    "Longer passwords are harder to crack. Aim for at least 12 characters."),

                new QuizQuestion("What does 2FA stand for?",
                    new[] { "A) Two Factor Authentication", "B) Two File Access", "C) Transfer File Authorization", "D) Two Firewall Activation" }, "A",
                    "Two Factor Authentication adds an extra layer of security beyond just a password."),

                new QuizQuestion("Is it safe to use the same password for multiple accounts?",
                    new[] { "A) True", "B) False" }, "B",
                    "Using the same password everywhere means if one account is hacked, all are at risk."),

                new QuizQuestion("What does HTTPS mean in a website URL?",
                    new[] { "A) The site is fast", "B) The site is secure", "C) The site is popular", "D) The site is free" }, "B",
                    "HTTPS means the connection between your browser and the website is encrypted."),

                new QuizQuestion("What is phishing?",
                    new[] { "A) A type of malware", "B) A trick to steal your info via fake emails", "C) A firewall setting", "D) A password manager" }, "B",
                    "Phishing uses fake emails or websites to trick you into giving personal information."),

                new QuizQuestion("Should you connect to public Wi-Fi without a VPN?",
                    new[] { "A) True", "B) False" }, "B",
                    "Public Wi-Fi is unsecured. A VPN encrypts your traffic and keeps you safe."),

                new QuizQuestion("What is malware?",
                    new[] { "A) A type of antivirus", "B) Malicious software designed to harm your device", "C) A secure browser", "D) A type of firewall" }, "B",
                    "Malware includes viruses, ransomware, and spyware that can damage or steal from your device."),

                new QuizQuestion("Which of these is the safest password?",
                    new[] { "A) password123", "B) john1990", "C) Purple$Horse!Mountain9", "D) 12345678" }, "C",
                    "A strong password mixes uppercase, lowercase, numbers, and symbols with no personal info."),

                new QuizQuestion("What should you do before clicking a link in an email?",
                    new[] { "A) Click it immediately", "B) Hover over it to check the URL", "C) Forward it to friends", "D) Reply to the sender" }, "B",
                    "Hovering over a link shows the real URL so you can check if it looks suspicious."),

                new QuizQuestion("What is a VPN used for?",
                    new[] { "A) Speed up your internet", "B) Block ads", "C) Encrypt your internet traffic", "D) Download files faster" }, "C",
                    "A VPN encrypts your connection and hides your online activity from snoopers."),

                new QuizQuestion("Is it okay to share your password with a trusted friend?",
                    new[] { "A) True", "B) False" }, "B",
                    "Never share your password with anyone — even trusted people can accidentally expose it.")
            };
        }

        private void BuildUI()
        {
            this.Text = "Cybersecurity Quiz";
            this.Size = new Size(680, 480);
            this.BackColor = Color.FromArgb(13, 17, 23);
            this.ForeColor = Color.White;
            this.Font = new Font("Consolas", 9.5f);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblHeader = new Label();
            lblHeader.Text = "🎮 Cybersecurity Quiz";
            lblHeader.Font = new Font("Consolas", 14f, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(0, 200, 120);
            lblHeader.Location = new Point(10, 10);
            lblHeader.Size = new Size(400, 30);

            lblProgress.Location = new Point(10, 45);
            lblProgress.Size = new Size(640, 20);
            lblProgress.ForeColor = Color.FromArgb(100, 160, 130);

            lblQuestion.Location = new Point(10, 75);
            lblQuestion.Size = new Size(640, 60);
            lblQuestion.ForeColor = Color.White;
            lblQuestion.Font = new Font("Consolas", 10f);

            Button[] answerBtns = { btnA, btnB, btnC, btnD };
            int[] yPos = { 148, 193, 238, 283 };

            for (int i = 0; i < 4; i++)
            {
                answerBtns[i].Location = new Point(10, yPos[i]);
                answerBtns[i].Size = new Size(640, 36);
                answerBtns[i].BackColor = Color.FromArgb(30, 38, 50);
                answerBtns[i].ForeColor = Color.White;
                answerBtns[i].FlatStyle = FlatStyle.Flat;
                answerBtns[i].FlatAppearance.BorderColor = Color.FromArgb(0, 100, 80);
                answerBtns[i].TextAlign = ContentAlignment.MiddleLeft;
                answerBtns[i].Padding = new Padding(5, 0, 0, 0);
                string tag = new[] { "A", "B", "C", "D" }[i];
                answerBtns[i].Tag = tag;
                answerBtns[i].Click += AnswerBtn_Click;
            }

            lblFeedback.Location = new Point(10, 332);
            lblFeedback.Size = new Size(640, 50);
            lblFeedback.ForeColor = Color.FromArgb(255, 200, 80);
            lblFeedback.Font = new Font("Consolas", 9f);

            btnNext.Text = "Next Question ▶";
            btnNext.Location = new Point(10, 390);
            btnNext.Size = new Size(160, 34);
            btnNext.BackColor = Color.FromArgb(0, 180, 100);
            btnNext.ForeColor = Color.Black;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Font = new Font("Consolas", 9f, FontStyle.Bold);
            btnNext.Visible = false;
            btnNext.Click += BtnNext_Click;

            this.Controls.AddRange(new Control[] {
                lblHeader, lblProgress, lblQuestion,
                btnA, btnB, btnC, btnD,
                lblFeedback, btnNext
            });
        }

        private void ShowQuestion()
        {
            if (_currentIndex >= _questions.Count)
            {
                ShowFinalScore();
                return;
            }

            var q = _questions[_currentIndex];
            lblProgress.Text = $"Question {_currentIndex + 1} of {_questions.Count}  |  Score: {_score}";
            lblQuestion.Text = q.Question;
            lblFeedback.Text = "";
            btnNext.Visible = false;

            Button[] btns = { btnA, btnB, btnC, btnD };
            for (int i = 0; i < btns.Length; i++)
            {
                if (i < q.Options.Length)
                {
                    btns[i].Text = q.Options[i];
                    btns[i].Visible = true;
                    btns[i].Enabled = true;
                    btns[i].BackColor = Color.FromArgb(30, 38, 50);
                }
                else
                {
                    btns[i].Visible = false;
                }
            }
        }

        private void AnswerBtn_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            string selected = btn.Tag?.ToString() ?? "";
            var q = _questions[_currentIndex];

            Button[] btns = { btnA, btnB, btnC, btnD };
            foreach (var b in btns) b.Enabled = false;

            if (selected == q.CorrectAnswer)
            {
                _score++;
                btn.BackColor = Color.FromArgb(0, 140, 70);
                lblFeedback.ForeColor = Color.FromArgb(0, 220, 100);
                lblFeedback.Text = "✔ Correct! " + q.Explanation;
            }
            else
            {
                btn.BackColor = Color.FromArgb(160, 30, 30);
                lblFeedback.ForeColor = Color.FromArgb(255, 100, 100);
                lblFeedback.Text = $"✘ Wrong! The correct answer was {q.CorrectAnswer}. {q.Explanation}";
            }

            btnNext.Visible = true;
            lblProgress.Text = $"Question {_currentIndex + 1} of {_questions.Count}  |  Score: {_score}";
        }

        private void BtnNext_Click(object? sender, EventArgs e)
        {
            _currentIndex++;
            ShowQuestion();
        }

        private void ShowFinalScore()
        {
            string feedback;
            if (_score >= 10) feedback = "🏆 Amazing! You're a cybersecurity pro!";
            else if (_score >= 7) feedback = "👍 Great job! Keep learning to stay safe!";
            else if (_score >= 5) feedback = "📚 Not bad! Review the topics to improve.";
            else feedback = "💡 Keep learning to stay safe online!";

            lblQuestion.Text = $"Quiz Complete!\n\nYour Score: {_score} / {_questions.Count}\n\n{feedback}";
            lblFeedback.Text = "";
            btnA.Visible = false;
            btnB.Visible = false;
            btnC.Visible = false;
            btnD.Visible = false;
            btnNext.Visible = false;
            lblProgress.Text = $"Final Score: {_score}/{_questions.Count}";
        }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }

        public QuizQuestion(string question, string[] options, string correctAnswer, string explanation)
        {
            Question = question;
            Options = options;
            CorrectAnswer = correctAnswer;
            Explanation = explanation;
        }
    }
}