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
}