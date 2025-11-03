namespace Core.Platforms.Windows
{
    internal class CustomWindow : Window
    {
        private VerticalStackLayout _rootContainer;
        private ContentPage _mainPage;

        public CustomWindow(Page page)
            : base(page)
        {
        }
    }
}
