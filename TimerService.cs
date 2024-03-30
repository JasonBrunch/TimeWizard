using System;
using System.Timers;

namespace TimeWizard
{
    public class TimerService
    {
        public event Action? TimerElapsed;
        private readonly System.Timers.Timer _timer;
        public TimeSpan CurrentTime { get; private set; }
       

        public TimerService()
        {
            _timer = new System.Timers.Timer(1000); // 1-second interval
            _timer.Elapsed += OnTimerElapsed;
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            CurrentTime = CurrentTime.Add(TimeSpan.FromSeconds(1));
            if (App.SessionService.CurrentSession != null)
            {
                // Update the total time of the current session
                App.SessionService.CurrentSession.TotalTime = App.SessionService.CurrentSession.TotalTime.Add(TimeSpan.FromSeconds(1));
                TimerElapsed?.Invoke();
            }
        }

        public void StartTimer()
        {
            if (!_timer.Enabled)
            {
                App.SessionService.StartSession();
                _timer.Start();
            }
        }

        public void StopTimer()
        {
            _timer.Stop();
            CurrentTime = TimeSpan.Zero;
            TimerElapsed?.Invoke();
        }

        public void Reset()
        {
            StopTimer();
            if(App.SessionService.CurrentSession != null)
            {
                App.SessionService.CurrentSession.TotalTime = TimeSpan.Zero; 
            }
            
            TimerElapsed?.Invoke();
        }
         public void ResetCurrentTime()
        {
            
        }
    }
   
}