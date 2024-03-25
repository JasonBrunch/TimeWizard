using System;
using System.Timers;

namespace TimeWizard
{
    public class TimerService
    {
        public event Action<TimeSpan, TimeSpan>? TimerElapsed;
        private System.Timers.Timer _timer;
        public TimeSpan TotalTime { get; private set; }
        public TimeSpan CurrentTime { get; private set; }
        public DateTime StartDate { get; private set; }

        public TimerService()
        {
            _timer = new System.Timers.Timer(1000); // 1-second interval
            _timer.Elapsed += OnTimerElapsed;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            CurrentTime = CurrentTime.Add(TimeSpan.FromSeconds(1));
            TimerElapsed?.Invoke(CurrentTime, TotalTime);
        }

        public void StartTimer()
        {
            if (!_timer.Enabled)
            {
                StartDate = DateTime.Now.Date;
                _timer.Start();
            }
        }

        public void StopTimer()
        {
            _timer.Stop();
            TotalTime += CurrentTime; // Add the current time to the total time
            CurrentTime = TimeSpan.Zero;
            TimerElapsed?.Invoke(CurrentTime, TotalTime);
        }

        public void Reset()
        {
            StopTimer();
            TotalTime = TimeSpan.Zero; // Also reset the total time
            TimerElapsed?.Invoke(CurrentTime, TotalTime);
        }
    }
}