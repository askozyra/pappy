using Core.Platforms.Windows;
using Microsoft.UI.Windowing;

namespace Core.Views.Desktop.Components.Titlebar;

public partial class Titlebar : ContentView
{
    public Titlebar()
    {
        InitializeComponent();
    }

    private void CloseWindow(object sender, EventArgs e)
    {
        var window = Application.Current?.Windows[0].Handler.PlatformView as Microsoft.UI.Xaml.Window;
        window?.Close();
    }

    private void MaximizeRestoreWindow(object sender, EventArgs e)
    {
        var appWindow = WindowConfigurator.AppWindow;
        if (appWindow?.Presenter is OverlappedPresenter presenter)
        {
            presenter.Maximize();

            // TODO: add Restore() logic
        }
    }

    private void MinimizeWindow(object sender, EventArgs e)
    {
        var appWindow = WindowConfigurator.AppWindow;
        if (appWindow is not null)
        {
            appWindow.Hide();
        }
    }
}