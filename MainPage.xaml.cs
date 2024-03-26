using System;

namespace TimeWizard
{
    public partial class MainPage : ContentPage
    {
        private TimerService _timerService;
        private bool _isTimerRunning = false;

        public MainPage()
        {
            InitializeComponent();
            _timerService = new TimerService();
            _timerService.TimerElapsed += UpdateTimerLabel;
        }
        private async void OnSwipedToLeft(object sender, SwipedEventArgs e)
        {
            // Navigate to StatsPage
            await Shell.Current.GoToAsync(nameof(StatsPage));
        }

        private void OnStartStopButtonClicked(object sender, EventArgs e)
        {
            if (_isTimerRunning)
            {
                _timerService.StopTimer();
                playIcon.IsVisible = true;
                stopIcon.IsVisible = false;
                _isTimerRunning = false;
            }
            else
            {
                _timerService.StartTimer();
                playIcon.IsVisible = false;
                stopIcon.IsVisible = true;
                _isTimerRunning = true;
            }
        }

        private void OnResetButtonClicked(object sender, EventArgs e)
        {
            _timerService.Reset();
        }

        private void UpdateTimerLabel(TimeSpan currentTime, TimeSpan totalTime)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                currentTimerLabel.Text = currentTime.ToString("hh\\:mm\\:ss");
                totalTimeTodayLabel.Text = totalTime.ToString("hh\\:mm\\:ss");
            });
        }
    }
}