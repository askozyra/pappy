using Core.Platforms.Windows;
using Microsoft.UI.Windowing;

namespace Core.Views.Desktop.Components.Window;

public partial class Titlebar : ContentView
{
    private const ushort TitlebarHeight = 24;

    public Titlebar()
    {
        InitializeComponent();

        SetDragRegion();
    }

    private void SetDragRegion()
    {
        var titlebarRect = new Windows.Graphics.RectInt32
        {
            X = 0,
            Y = 0,
            Width = WindowConfigurator.AppWindow.Size.Width,
            Height = TitlebarHeight
        };

        WindowConfigurator.AppWindow.TitleBar.SetDragRectangles(new[] { titlebarRect });
    }

    private void CloseWindow(object sender, EventArgs e)
    {
        var window = Application.Current?.Windows[0].Handler.PlatformView
            as Microsoft.UI.Xaml.Window;

        window?.Close();
    }

    private void MaximizeRestoreWindow(object sender, EventArgs e)
    {
        var appWindow = WindowConfigurator.AppWindow;
        if (appWindow?.Presenter is OverlappedPresenter presenter)
        {
            if (presenter.State == OverlappedPresenterState.Maximized)
            {
                presenter.Restore();
            }
            else
            {
                presenter.Maximize();
            }
        }
    }

    private void MinimizeWindow(object sender, EventArgs e)
    {
        var appWindow = WindowConfigurator.AppWindow;
        if (appWindow is not null && appWindow?.Presenter is OverlappedPresenter presenter)
        {
            presenter.Minimize();
        }
    }
}