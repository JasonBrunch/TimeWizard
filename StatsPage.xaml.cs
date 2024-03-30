using System.Linq;
using System.Linq;

namespace TimeWizard;

public partial class StatsPage : ContentPage
{
    private readonly StatsPageViewModel _viewModel;

    public StatsPage()
    {
        InitializeComponent();
        _viewModel = new StatsPageViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadSessions();
        DisplayTotalHours();
        App.TimerService.TimerElapsed += OnTimerElapsed; // Subscribe to the event
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        App.TimerService.TimerElapsed -= OnTimerElapsed; // Unsubscribe from the event
    }

    private void OnTimerElapsed()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            _viewModel.UpdateCurrentSession(); // Update the current session display
        });
    }

    private async void OnSwipedRight(object sender, SwipedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
    private void DisplayTotalHours()
    {
        TimeSpan totalDuration = TimeSpan.Zero;
        foreach (Session session in App.SessionService.Sessions)
        {
            totalDuration += session.TotalTime;
        }

        // Format totalDuration to a readable string, e.g., "Total Hours: 123:45:67"
        totalHoursLabel.Text = $"Total Hours: {totalDuration:hh\\:mm\\:ss}";
    }
    private void OnResetButtonClicked(object sender, EventArgs e)
    {
        //erase the list and make a new one
        //save the list
        App.SessionService.ClearAllSessions();
        _viewModel.LoadSessions();
        DisplayTotalHours();

    }
}