using WebCore.Windows;

namespace WebCore.Sandbox;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Windows.WebWindow webWindow = new Windows.WebWindow("WebCore", 800, 600);
        
        webWindow.Window.FormBorderStyle = FormBorderStyle.None;
        Win32.SetCorners(webWindow, true);
        
        webWindow.NavigateByFile("menu.html");
        Application.Run(webWindow.Window);
    }
}