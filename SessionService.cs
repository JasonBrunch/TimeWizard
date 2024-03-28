using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;

namespace TimeWizard
{
    public class Session
    {
        public DateTime Date { get; set; }
        public TimeSpan TotalTime { get; set; }

        public bool IsValid()
        {
            // Add any specific validation logic for Session here
            return Date != default && TotalTime >= TimeSpan.Zero;
        }
    }

    public class SessionService
    {
        public List<Session> Sessions { get; private set; } = new List<Session>();
        public Session? CurrentSession { get; private set; }

        public void LoadSessions()
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, "sessions.json");
                if (File.Exists(filePath))
                {
                    var jsonData = File.ReadAllText(filePath);
                    var sessions = JsonConvert.DeserializeObject<List<Session>>(jsonData);
                    Sessions = sessions?.Where(session => session?.IsValid() == true).ToList() ?? new List<Session>();
                }
            }
            catch (Exception ex)
            {
                // Log error or handle it as needed
                Console.WriteLine($"Error loading sessions: {ex.Message}");
            }
        }

        public void StartSession()
        {
            var today = DateTime.Today;
            Session? currentSession = Sessions.FirstOrDefault(s => s.Date.Date == today);
            if (currentSession == null)
            {
                currentSession = new Session { Date = today, TotalTime = TimeSpan.Zero };
                Sessions.Add(currentSession);
            }
            CurrentSession = currentSession;
        }

        public void SaveSessions()
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, "sessions.json");
                var jsonData = JsonConvert.SerializeObject(Sessions);
                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                // Log error or handle it as needed
                Console.WriteLine($"Error saving sessions: {ex.Message}");
            }
        }

        public void UpdateSession(TimeSpan duration)
        {
            if (CurrentSession == null)
            {
                return;
            }

            if (duration < TimeSpan.Zero)
            {
                // Handle invalid duration
                return;
            }

            CurrentSession.TotalTime += duration;
            SaveSessions();
        }

        public void ResetSessions()
        {
            Sessions.Clear();
            SaveSessions();
        }
    }
}