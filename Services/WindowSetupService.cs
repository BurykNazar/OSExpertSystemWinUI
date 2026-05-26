using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinRT.Interop;

namespace OSExpertSystemWinUI.Services;

public static class WindowSetupService
{
    public static void Configure(Window window, string title, string iconRelativePath)
    {
        try
        {
            var hWnd = WindowNative.GetWindowHandle(window);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            appWindow.Title = title;
            appWindow.Resize(new SizeInt32(1280, 860));

            var iconPath = Path.Combine(AppContext.BaseDirectory, iconRelativePath);
            if (File.Exists(iconPath))
            {
                appWindow.SetIcon(iconPath);
            }
        }
        catch
        {
            window.Title = title;
        }
    }
}
