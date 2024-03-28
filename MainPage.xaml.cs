using System;

namespace TimeWizard
{
    public partial class MainPage : ContentPage
    {
        private bool _isTimerRunning = false;
        private string _currentButtonImage = "play.png";

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            App.TimerService.TimerElapsed += UpdateTimerLabel;
            // Initialize the current button image for the start/stop button
            CurrentButtonImage = "play.png";
            InitializeTimerLabel();
        }

        public string CurrentButtonImage
        {
            get => _currentButtonImage;
            set
            {
                if (_currentButtonImage != value)
                {
                    _currentButtonImage = value;
                    OnPropertyChanged(nameof(CurrentButtonImage));
                }
            }
        }

        protected override void OnDisappearing()
        {
            // Save sessions when app is going into the background
            App.SessionService.SaveSessions();
            base.OnDisappearing();
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
                App.TimerService.StopTimer();
                _isTimerRunning = false;
                CurrentButtonImage = "play.png";
                App.SessionService.SaveSessions();
            }
            else
            {
                App.TimerService.StartTimer();
                _isTimerRunning = true;
                CurrentButtonImage = "square.png";
            }
        }

        private void OnResetButtonClicked(object sender, EventArgs e)
        {
            App.TimerService.Reset();
            _isTimerRunning = false;
            CurrentButtonImage = "play.png";
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            // Handle the cancel button click
        }

        private void UpdateTimerLabel()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                currentTimerLabel.Text = App.TimerService.CurrentTime.ToString("hh\\:mm\\:ss");
                if (App.SessionService.CurrentSession != null)
                {
                    totalTimeTodayLabel.Text = App.SessionService.CurrentSession.TotalTime.ToString("hh\\:mm\\:ss");
                }
                else
                {
                    totalTimeTodayLabel.Text = "Current Ses Null";
                }
            });
        }

        public void InitializeTimerLabel()
        {
            var today = DateTime.Today;
            Session? currentSession = App.SessionService.Sessions.FirstOrDefault(s => s.Date.Date == today);
            if (currentSession != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                        totalTimeTodayLabel.Text = currentSession.TotalTime.ToString("hh\\:mm\\:ss");                
                });
            }

        }
    }
}