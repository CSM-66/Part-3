using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ChatBOT.ChatBot
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Reminder { get; set; } = "";
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class DatabaseManager
    {
        private string _connectionString = "Server=localhost;Database=chatbot_db;Uid=root;Pwd=078291;";

        public bool AddTask(string title, string description, string reminder)
        {
            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();
                string query = "INSERT INTO tasks (title, description, reminder) VALUES (@title, @desc, @reminder)";
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@desc", description);
                cmd.Parameters.AddWithValue("@reminder", reminder);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB Error: " + ex.Message);
                return false;
            }
        }

        public List<TaskItem> GetAllTasks()
        {
            var tasks = new List<TaskItem>();
            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();
                string query = "SELECT * FROM tasks ORDER BY created_at DESC";
                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tasks.Add(new TaskItem
                    {
                        Id = reader.GetInt32("id"),
                        Title = reader.GetString("title"),
                        Description = reader.GetString("description"),
                        Reminder = reader.GetString("reminder"),
                        IsCompleted = reader.GetBoolean("is_completed"),
                        CreatedAt = reader.GetDateTime("created_at")
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB Error: " + ex.Message);
            }
            return tasks;
        }

        public bool CompleteTask(int id)
        {
            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();
                string query = "UPDATE tasks SET is_completed = TRUE WHERE id = @id";
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteTask(int id)
        {
            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();
                string query = "DELETE FROM tasks WHERE id = @id";
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch { return false; }
        }
    }
}