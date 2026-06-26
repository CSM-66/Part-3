using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChatBOT.ChatBot
{
    public class TaskForm : Form
    {
        private DatabaseManager _db = new DatabaseManager();
        private ListBox lstTasks = new ListBox();
        private TextBox txtTitle = new TextBox();
        private TextBox txtDescription = new TextBox();
        private TextBox txtReminder = new TextBox();
        private Button btnAdd = new Button();
        private Button btnComplete = new Button();
        private Button btnDelete = new Button();
        private Button btnRefresh = new Button();
        private List<TaskItem> _tasks = new List<TaskItem>();

        public TaskForm()
        {
            BuildUI();
            LoadTasks();
        }

        private void BuildUI()
        {
            this.Text = "Task Assistant";
            this.Size = new Size(700, 550);
            this.BackColor = Color.FromArgb(13, 17, 23);
            this.ForeColor = Color.White;
            this.Font = new Font("Consolas", 9.5f);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Title
            Label lblHeader = new Label();
            lblHeader.Text = "🗒 Cybersecurity Task Assistant";
            lblHeader.Font = new Font("Consolas", 14f, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(0, 200, 120);
            lblHeader.Location = new Point(10, 10);
            lblHeader.Size = new Size(500, 30);

            // Input labels and fields
            Label lblTitle = new Label() { Text = "Task Title:", Location = new Point(10, 55), Size = new Size(100, 20), ForeColor = Color.White };
            txtTitle.Location = new Point(120, 52);
            txtTitle.Size = new Size(540, 24);
            txtTitle.BackColor = Color.FromArgb(30, 38, 50);
            txtTitle.ForeColor = Color.White;
            txtTitle.BorderStyle = BorderStyle.FixedSingle;

            Label lblDesc = new Label() { Text = "Description:", Location = new Point(10, 85), Size = new Size(100, 20), ForeColor = Color.White };
            txtDescription.Location = new Point(120, 82);
            txtDescription.Size = new Size(540, 24);
            txtDescription.BackColor = Color.FromArgb(30, 38, 50);
            txtDescription.ForeColor = Color.White;
            txtDescription.BorderStyle = BorderStyle.FixedSingle;

            Label lblReminder = new Label() { Text = "Reminder:", Location = new Point(10, 115), Size = new Size(100, 20), ForeColor = Color.White };
            txtReminder.Location = new Point(120, 112);
            txtReminder.Size = new Size(540, 24);
            txtReminder.BackColor = Color.FromArgb(30, 38, 50);
            txtReminder.ForeColor = Color.White;
            txtReminder.BorderStyle = BorderStyle.FixedSingle;
            txtReminder.PlaceholderText = "e.g. Remind me in 7 days";

            // Buttons
            btnAdd.Text = "Add Task";
            btnAdd.Location = new Point(10, 148);
            btnAdd.Size = new Size(100, 30);
            btnAdd.BackColor = Color.FromArgb(0, 180, 100);
            btnAdd.ForeColor = Color.Black;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;

            btnComplete.Text = "✔ Complete";
            btnComplete.Location = new Point(120, 148);
            btnComplete.Size = new Size(100, 30);
            btnComplete.BackColor = Color.FromArgb(0, 100, 180);
            btnComplete.ForeColor = Color.White;
            btnComplete.FlatStyle = FlatStyle.Flat;
            btnComplete.FlatAppearance.BorderSize = 0;
            btnComplete.Click += BtnComplete_Click;

            btnDelete.Text = "🗑 Delete";
            btnDelete.Location = new Point(230, 148);
            btnDelete.Size = new Size(100, 30);
            btnDelete.BackColor = Color.FromArgb(180, 30, 30);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += BtnDelete_Click;

            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.Location = new Point(340, 148);
            btnRefresh.Size = new Size(100, 30);
            btnRefresh.BackColor = Color.FromArgb(80, 80, 80);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => LoadTasks();

            // Task list
            Label lblTasks = new Label() { Text = "Your Tasks:", Location = new Point(10, 190), Size = new Size(200, 20), ForeColor = Color.FromArgb(0, 200, 120) };
            lstTasks.Location = new Point(10, 212);
            lstTasks.Size = new Size(660, 280);
            lstTasks.BackColor = Color.FromArgb(20, 26, 34);
            lstTasks.ForeColor = Color.White;
            lstTasks.Font = new Font("Consolas", 9f);
            lstTasks.BorderStyle = BorderStyle.None;

            this.Controls.AddRange(new Control[] {
                lblHeader, lblTitle, txtTitle, lblDesc, txtDescription,
                lblReminder, txtReminder, btnAdd, btnComplete, btnDelete,
                btnRefresh, lblTasks, lstTasks
            });
        }

        private void LoadTasks()
        {
            _tasks = _db.GetAllTasks();
            lstTasks.Items.Clear();
            foreach (var task in _tasks)
            {
                string status = task.IsCompleted ? "✔" : "○";
                string reminder = string.IsNullOrEmpty(task.Reminder) ? "" : $" | ⏰ {task.Reminder}";
                lstTasks.Items.Add($"{status} [{task.Id}] {task.Title}{reminder} - {task.CreatedAt:dd/MM/yyyy}");
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a task title.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = _db.AddTask(txtTitle.Text, txtDescription.Text, txtReminder.Text);
            if (success)
            {
                MessageBox.Show("Task added successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTitle.Text = "";
                txtDescription.Text = "";
                txtReminder.Text = "";
                LoadTasks();
            }
            else
            {
                MessageBox.Show("Failed to add task. Check your database connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnComplete_Click(object? sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex < 0) { MessageBox.Show("Please select a task first."); return; }
            var task = _tasks[lstTasks.SelectedIndex];
            _db.CompleteTask(task.Id);
            LoadTasks();
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex < 0) { MessageBox.Show("Please select a task first."); return; }
            var task = _tasks[lstTasks.SelectedIndex];
            var confirm = MessageBox.Show($"Delete task '{task.Title}'?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                _db.DeleteTask(task.Id);
                LoadTasks();
            }
        }
    }
}