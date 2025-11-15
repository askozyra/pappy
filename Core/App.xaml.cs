namespace Core
{
    public partial class App : Application
    {
        private const string _iconName = "appicon.ico";
        private const float _titlebarColor = 0.2f; // #333
        private const int _minWidth = 600;
        private const int _minHeight = 200;

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var services = MauiWinUIApplication.Current.Services;
            var shell = services.GetRequiredService<AppShell>();

            return new Window(shell)
            {
                Title = AppInfo.Current.Name,
                MinimumWidth = _minWidth,
                MinimumHeight = _minHeight,
                TitleBar = new TitleBar()
                {
                    Icon = _iconName,
                    Title = AppInfo.Current.Name,
                    BackgroundColor = new Color(_titlebarColor)
                }
            };
        }
    }
}