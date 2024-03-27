namespace TimeWizard
{
    public partial class App : Application
    {
        public static SessionService SessionService { get; private set; } = null!;
        public static TimerService TimerService { get; private set; } = null!;
        public App()
        {
            InitializeComponent();

            // Initialize and load sessions
            SessionService = new SessionService();
            TimerService = new TimerService();
            SessionService.LoadSessions();

            MainPage = new AppShell();
        }
    }
}
