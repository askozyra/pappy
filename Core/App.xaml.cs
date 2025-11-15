namespace Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell())
            {
                Title = AppInfo.Current.Name,
#if WINDOWS
                TitleBar = new TitleBar()
                {
                    Icon = "appicon.ico",
                    Title = AppInfo.Current.Name,
                    BackgroundColor = new Color(0.2f) // #333
                }
#endif
            };
        }
    }
}