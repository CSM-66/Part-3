# Part-3
CyberSecurity Awareness Chatbot
Overview
The CyberSecurity Awareness Chatbot is a Windows Forms application built in C#. It educates users about cybersecurity topics through interactive conversations, tasks, and a quiz game. This is the final version of the project covering Parts 1, 2, and 3.
Features
Part 1 — Console Foundation

ASCII art logo on startup
Voice greeting on launch
Basic keyword recognition for cybersecurity topics
Typing animation effect

Part 2 — GUI and Smart Responses

Full Windows Forms GUI with dark theme
Keyword recognition for 8 cybersecurity topics
Random responses so the bot never repeats itself
Conversation flow — handles follow-ups like "tell me more"
Memory and recall — remembers your name and favourite topic
Sentiment detection — detects worried, curious, frustrated and adjusts tone
Error handling for unrecognised inputs
Quick topic buttons for fast access

Part 3 — Advanced Features

Task Assistant with MySQL database integration
Cybersecurity Quiz with 12 questions and instant feedback
NLP Simulation — understands naturally worded questions
Activity Log with timestamps — type "show activity log" to view

Technologies Used

C# and .NET 10
Windows Forms
MySQL with MySql.Data package
Object-Oriented Programming

How to Run

Make sure MySQL is installed and running
Open the project in Visual Studio
Build the solution

Database Setup
-Run these commands in MySQL before running the app:
CREATE DATABASE chatbot_db;
USE chatbot_db;
CREATE TABLE tasks (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description VARCHAR(500),
    reminder VARCHAR(255),
    is_completed BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
Run the application
Enter your name when prompted
Start chatting or use the quick topic buttons
