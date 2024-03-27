using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace TimeWizard
{
    public class SessionDisplay : INotifyPropertyChanged
    {
        public string? _displayText;
        public DateTime Date { get; set; }

        public string DisplayText
        {
            get => _displayText ?? string.Empty;
            set
            {
                _displayText = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class StatsPageViewModel : BindableObject
    {
        private ObservableCollection<SessionDisplay> _sessions = new ObservableCollection<SessionDisplay>();

        public ObservableCollection<SessionDisplay> Sessions
        {
            get => _sessions;
            set
            {
                _sessions = value;
                OnPropertyChanged(nameof(Sessions));
            }
        }

        public void LoadSessions()
        {
            Sessions.Clear();
            foreach (var session in App.SessionService.Sessions)
            {
                Sessions.Add(new SessionDisplay
                {
                    Date = session.Date,
                    DisplayText = $"{session.Date.ToString("dddd MMM dd")}: {session.TotalTime.ToString(@"hh\:mm\:ss")} hours"
                });
            }
        }

        public void UpdateCurrentSession()
        {
            var currentSession = App.SessionService.CurrentSession;
            if (currentSession != null)
            {
                // Find the display object for the current session
                var displaySession = Sessions.FirstOrDefault(s => s.Date.Date == currentSession.Date.Date);
                if (displaySession != null)
                {
                    // Update only the display text
                    displaySession.DisplayText = $"{currentSession.Date.ToString("dddd MMM dd")}: {currentSession.TotalTime.ToString(@"hh\:mm\:ss")} hours";
                }
            }
        }
    }

        
    }
