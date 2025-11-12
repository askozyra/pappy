#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using System.Runtime.InteropServices;
using WinRT.Interop;

namespace Core.Platforms.Windows
{
    /// <summary>
    /// Contains WinUI 3 window appearance configurator logic.
    /// </summary>
    public static class WindowConfigurator
    {
        #region WinAPI definitions
        private const int SW_SHOW = 5;

        // Index to get/set window style.
        private const int GWL_STYLE = -16;
        // Index to get/set window procedure.
        private const int GWL_WNDPROC = -4;

        // Title bar style flag.
        private const int WS_CAPTION = 0x00C00000;
        // Apply window visibility after it was created.
        private const int WS_VISIBLE = 0x10000000;

        // Message for erasing the bg (triggered before painting).
        private const int WM_ERASEBKGND = 0x0014;

        // Keep a reference of delegate to prevent GC dispose it.
        private static WndProcDelegate _procDelegate;

        // Message for erasing the bg (triggered before painting).
        private static IntPtr _prevProc = IntPtr.Zero;

        // Delegate type for windows procedure callback.
        private delegate IntPtr WndProcDelegate(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int left, top, right, bottom; }
        #endregion

        private static UInt32 WindowBgColor = 0x00333333;

        public static AppWindow AppWindow { get; set; }

        static WindowConfigurator()
        {
            InitAppWindow();
        }

        /// <summary>
        /// Configure window chrome appearance.
        /// </summary>
        public static void Configure()
        {
            var window = Application.Current?.Windows[0];

            if (window?.Handler.PlatformView is not MauiWinUIWindow mauiWindow)
                return;

            var hwnd = WindowNative.GetWindowHandle(mauiWindow);

            // Change current window style:
            int style = GetWindowLong(hwnd, GWL_STYLE);
            style &= ~WS_CAPTION;   // remove title bar (caption);
            style |= WS_VISIBLE;    // ensure window is visible.
            SetWindowLong(hwnd, GWL_STYLE, style);

            // Set up custom window bg processing.
            _procDelegate = new WndProcDelegate(WndProc);
            _prevProc = SetWindowLongPtr(hwnd, GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(_procDelegate));

            mauiWindow.ExtendsContentIntoTitleBar = true;

            ShowWindow(hwnd, SW_SHOW);
        }

        private static void InitAppWindow()
        {
            var wnd = Application.Current?.Windows[0];

            var nativeWindow = wnd?.Handler.PlatformView as Microsoft.UI.Xaml.Window;
            var hwnd = WindowNative.GetWindowHandle(nativeWindow);

            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            AppWindow = AppWindow.GetFromWindowId(windowId);
        }

        // Custom window procedure for handling messages.
        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == WM_ERASEBKGND)
            {
                GetClientRect(hwnd, out RECT rc);
                IntPtr brush = CreateSolidBrush(WindowBgColor);
                FillRect(wParam, ref rc, brush);

                return IntPtr.Zero;
            }

            // Call original WndProc.
            return CallPrev(hwnd, msg, wParam, lParam);
        }
        private static IntPtr CallPrev(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
            => CallWindowProc(_prevProc, hwnd, msg, wParam, lParam);

        #region WinAPI imports
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // Set/Get window parameters.
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // Get/Set pointer to window procedure.
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr newProc);

        // Bg painting related methods.
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateSolidBrush(uint color);
        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT rect);
        [DllImport("user32.dll")]
        private static extern IntPtr FillRect(IntPtr hdc, ref RECT rect, IntPtr hbr);

        [DllImport("user32.dll")]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc,
            IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        #endregion
    }
}
#endif