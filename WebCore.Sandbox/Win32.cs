using System.Runtime.InteropServices;
using WebCore.Windows;

namespace WebCore.Sandbox;

public class Win32
{
    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
    
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    
    public const int WM_NCLBUTTONDOWN = 0xA1;
    public const int HTCAPTION = 0x2;
    
    public static void SetCorners(WebWindow webWindow,bool enabled)
    {
        int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        int DWMWCP_ROUND = 2;
        int DWMWCP_DEFAULT = 0;

        int value = enabled ? DWMWCP_ROUND : DWMWCP_DEFAULT;
        Win32.DwmSetWindowAttribute(webWindow.Window.Handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref value, sizeof(int));
    }
}