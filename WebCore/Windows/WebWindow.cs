using System.Reflection;
using System.Text.Json;
using Microsoft.Web.WebView2.Core;
using WebCore.Events;

namespace WebCore.Windows;

public class WebWindow
{
    public static WebWindow Instance { get; private set; }
    public Form Window  { get; private set; }
    public Microsoft.Web.WebView2.WinForms.WebView2 WebView { get; private set; }
    public List<WebEventHandler> Handlers { get; set; } = new List<WebEventHandler>();
    public bool IsDebugEnabled { get; set; } = false;
    
    
    private string _navigateUrl = string.Empty;
    private bool _isWebViewInitialized = false;
    public WebWindow(string title, int width, int height)
    {
        Window = new Form();
        Window.Text = title;
        Window.Size = new Size(width, height);
        Window.StartPosition = FormStartPosition.CenterScreen;
        
        WebView = new Microsoft.Web.WebView2.WinForms.WebView2();
        WebView.Dock = DockStyle.Fill;
        Window.Controls.Add(WebView);
        WebView.CoreWebView2InitializationCompleted += (sender, args) =>
        {
            WebView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            _isWebViewInitialized = true;
            if(_navigateUrl != string.Empty)
                WebView.CoreWebView2.Navigate(_navigateUrl);
        };
        InitalizeAsync();
        RegisterWebEventHandlers();
        
        if(Instance == null)
            Instance = this;
        else
            throw new Exception("WebWindow is already initialized");
    }

    private async void InitalizeAsync()
    {
        if(WebView != null)
            await WebView.EnsureCoreWebView2Async(null);
        
        WebView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
    }
    
    public void Show()
    {
        if(Window == null)
            throw new Exception("Window is not initialized");
        
        Window.Show();
    }
    
    private void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        try
        {
            string messageJson = e.TryGetWebMessageAsString();
            var msg = JsonSerializer.Deserialize<WebMessage>(messageJson,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if(IsDebugEnabled)
                Console.WriteLine($"Message: {messageJson}");
            
            if (msg != null)
                HandleWebMessage(msg);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while processing the message: {ex.Message}", ex);
        }
    }

    public void NavigateByFile(string path)
    {
        string htmlPath = System.IO.Path.Combine(Application.StartupPath, path);
        Navigate($"file:///{htmlPath}");
    }

    public void Navigate(string url)
    {
        if(_isWebViewInitialized)
            WebView.CoreWebView2.Navigate(url);
        _navigateUrl = url;
    }
    
    private void HandleWebMessage(WebMessage message)
    {
        foreach (var eventHandler in Handlers.ToList())
        {
            if (eventHandler.Message.Id == message.Id)
            {
                if (eventHandler.Message.Event == message.Event)
                {
                    eventHandler.Action?.Invoke(message);
                    break;
                }
            }
        }
    }

    public async Task<string> ExecuteAsync(string script)
    {
        if (WebView != null && WebView.CoreWebView2 != null)
            return await WebView.ExecuteScriptAsync(script);
        else
            throw new Exception("WebView is not initialized");
    }
    
    private void RegisterWebEventHandlers()
    {
        var webUiTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(WebController)));

        foreach (var type in webUiTypes)
        {
            var instance = Activator.CreateInstance(type);

            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var method in methods)
            {
                var attrs = method.GetCustomAttributes<WebEventAttribute>();
                foreach (var attr in attrs)
                {
                    Handlers.Add(new WebEventHandler(
                        attr.Id,
                        attr.Event,
                        msg => method.Invoke(instance, new object[] { msg })
                    ));
                }
            }
        }   
    }
}

