namespace TimeWizard
{
    public partial class App : Application
    {
        public static SessionService SessionService { get; private set; }
        public App()
        {
            InitializeComponent();

            // Initialize and load sessions
            SessionService = new SessionService();
            SessionService.LoadSessions();

            MainPage = new AppShell();
        }
    }
}
