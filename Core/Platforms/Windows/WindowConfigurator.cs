#if WINDOWS
using System.Runtime.InteropServices;

namespace Core.Platforms.Windows
{
    /// <summary>
    /// Contains WinUI 3 window appearance configurator logic.
    /// </summary>
    public static class WindowConfigurator
    {
        // Index for function GetWindowLong / SetWindowLong to get or set window style (appearance).
        const int GWL_STYLE = -16;

        // Default window style definition.
        const int WS_OVERLAPPEDWINDOW = 0x00CF0000;

        // Apply window visibility after it was created.
        const int WS_VISIBLE = 0x10000000;

        // Change window parameters.
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        // Read current window parameters.
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Configure window chrome appearance.
        /// </summary>
        public static void Configure(Microsoft.UI.Xaml.Window window)
        {
            if (window is not MauiWinUIWindow mauiWindow)
                return;

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(mauiWindow);

            // Change current window style:
            int style = GetWindowLong(hwnd, GWL_STYLE);
            style &= ~WS_OVERLAPPEDWINDOW;  // remove borders & titlebar;
            style |= WS_VISIBLE;            // make window visible.
            SetWindowLong(hwnd, GWL_STYLE, style);

            mauiWindow.ExtendsContentIntoTitleBar = true;

            // Change current window style on MauiWindow layer.
            //var handle = WinRT.Interop.WindowNative.GetWindowHandle(mauiWindow);
            //var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
            //var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
            //if (appWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter pre)
            //{
            //    pre.IsMaximizable = false;
            //    pre.IsResizable = false;
            //    pre.IsMinimizable = false;
            //    pre.SetBorderAndTitleBar(false, false);
            //}
        }
    }
}
#endif