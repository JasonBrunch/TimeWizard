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
        public Session? CurrentSession { get; private set; }

    //Sets up the session list upon loading the app
    public void LoadSessions()
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, "sessions.json");
        if (File.Exists(filePath))
        {
            var jsonData = File.ReadAllText(filePath);
            Sessions = JsonConvert.DeserializeObject<List<Session>>(jsonData) ?? new List<Session>();
        }
    }
    //Fires when timer starts. Sets up the current session
    public void StartSession()
    {
        //Get the days session, creates it if doesnt exist, and sets the current session variable
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
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "sessions.json");
            var jsonData = JsonConvert.SerializeObject(Sessions);
            File.WriteAllText(filePath, jsonData);
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
