using System;

namespace TimeWizard
{
    public partial class MainPage : ContentPage
    {
        private bool _isTimerRunning = false;
        private string _currentButtonImage = "play.png";
        private List<string> _categories = new List<string> { "Work", "Study", "Exercise", "Leisure", "Other" };
        private string _selectedCategory;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            App.TimerService.TimerElapsed += UpdateTimerLabel;

            // Assuming _categories is not empty, set the first category as the default
            _selectedCategory = _categories.FirstOrDefault();
            categoryPicker.ItemsSource = _categories;
            categoryPicker.SelectedItem = _selectedCategory; // Set the default category here

            categoryPicker.SelectedIndexChanged += OnCategorySelected;
        }


        public string SelectedCategory
        {
            get => _selectedCategory;
            set => _selectedCategory = value;
        }
        public string CurrentButtonImage
        {
            get => _currentButtonImage;
            set
            {
                _currentButtonImage = value;
                OnPropertyChanged(nameof(CurrentButtonImage)); // Notify the UI of the change
            }
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
                CurrentButtonImage = "play.png"; // Change the button image
            }
            else
            {
                App.TimerService.StartTimer();
                _isTimerRunning = true;
                CurrentButtonImage = "square.png"; // Change the button image
            }
        }
        private void OnCategorySelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            _selectedCategory = (string)picker.SelectedItem;
            // Add any additional logic you want to execute when a new category is selected
        }

        private void OnResetButtonClicked(object sender, EventArgs e)
        {
            App.TimerService.Reset();
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

                // Check if CurrentSession is not null before accessing TotalTime
                if (App.SessionService.CurrentSession != null)
                {
                    totalTimeTodayLabel.Text = App.SessionService.CurrentSession.TotalTime.ToString("hh\\:mm\\:ss");
                }
                else
                {
                    totalTimeTodayLabel.Text = "00:00:00"; // Or some default value
                }
            });
        }
    }
}