using WebCore.Events;
using WebCore.Extensions;
using WebCore.Windows;

namespace WebCore.Sandbox.Settings;

public class Window : WebController
{
    [WebEvent("window", WebEvent.Close)]
    private void Window_OnClose(WebMessage msg)
    {
        Environment.Exit(0);
    }
}