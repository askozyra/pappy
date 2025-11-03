using Core.Platforms.Windows;

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
            return new CustomWindow(new AppShell());
        }
    }
}