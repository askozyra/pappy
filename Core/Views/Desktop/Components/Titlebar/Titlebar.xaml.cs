namespace Core.Views.Desktop.Components.Titlebar;

public partial class Titlebar : ContentView
{
    public Titlebar()
    {
        InitializeComponent();
    }

    private void CloseWindow(object sender, EventArgs e)
    {
#if WINDOWS
        var window = Application.Current?.Windows[0].Handler.PlatformView as Microsoft.UI.Xaml.Window;
        window?.Close();
#endif
    }
}