using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;

namespace TimeWizard
{

    public class Session
    {
        public DateTime Date { get; set; }
        public TimeSpan TotalTime { get; set; }
    }

    public class SessionService
    {
    
        
            public List<Session> Sessions { get; private set; } = new List<Session>();

        // Methods to update this list will go here
        // For example, adding a new session, resetting sessions, etc.
        public void SaveSessions()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "sessions.json");
            var jsonData = JsonConvert.SerializeObject(Sessions);
            File.WriteAllText(filePath, jsonData);
        }
        public void LoadSessions()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "sessions.json");
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                Sessions = JsonConvert.DeserializeObject<List<Session>>(jsonData) ?? new List<Session>();
            }
        }
        public void StartSession()
        {
            var today = DateTime.Today;
            var currentSession = Sessions.FirstOrDefault(s => s.Date.Date == today);

            if (currentSession == null)
            {
                currentSession = new Session { Date = today, TotalTime = TimeSpan.Zero };
                Sessions.Add(currentSession);
            }

            // Assuming you have some timer logic to track the session duration
            // Start your timer here
        }
        public void UpdateSession(TimeSpan duration)
        {
            var today = DateTime.Today;
            var currentSession = Sessions.FirstOrDefault(s => s.Date.Date == today);

            if (currentSession != null)
            {
                currentSession.TotalTime += duration;
                SaveSessions(); // Consider when to call this
            }

            // Stop your timer here
        }
        public void ResetSessions()
        {
            Sessions.Clear();
            SaveSessions();
        }

    }

}
