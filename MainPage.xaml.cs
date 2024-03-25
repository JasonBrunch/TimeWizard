using System;

namespace TimeWizard
{
    public partial class MainPage : ContentPage
    {
        private TimerService _timerService;

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

        private void OnStartButtonClicked(object sender, EventArgs e)
        {
            _timerService.StartTimer();
            startStopButton.Text = "Stop"; // Change button text to "Stop"
        }

        private void OnStopButtonClicked(object sender, EventArgs e)
        {
            _timerService.StopTimer();
            startStopButton.Text = "Start"; // Change button text to "Start"
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