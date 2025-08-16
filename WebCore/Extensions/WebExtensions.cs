using WebCore.Windows;

namespace WebCore.Extensions;

public static class WebExtensions
{
    public static async Task SetValue(this WebWindow web, string elementId, string value)
    {
        await web.ExecuteAsync($@"document.getElementById('{elementId}').value = '{value}';");
    }
    
    public static async Task SetChecked(this WebWindow web, string elementId, bool value)
    {
        await web.ExecuteAsync($@"document.getElementById('{elementId}').checked = {value.ToString().ToLower()};");
    }
    
    public static async Task SetInnerText(this WebWindow web, string elementId, string value)
    {
        await web.ExecuteAsync($@"document.getElementById('{elementId}').innerText = '{value}';");
    }
    
    public static async Task SetInnerHtml(this WebWindow web, string elementId, string htmlContent)
    {
        await web.ExecuteAsync($@"document.getElementById('{elementId}').innerHTML = `{htmlContent}`;");
    }

    public static async Task<string> GetValue(this WebWindow web, string elementId)
    {
        var result = await web.ExecuteAsync($@"document.getElementById('{elementId}').value");
        return result?.Trim('"'); // input.value hep string döner
    }

    public static async Task<bool> GetChecked(this WebWindow web, string elementId)
    {
        var result = await web.ExecuteAsync($@"document.getElementById('{elementId}').checked");
        return bool.TryParse(result, out var val) && val;
    }

    public static async Task<string> GetInnerText(this WebWindow web, string elementId)
    {
        var result = await web.ExecuteAsync($@"document.getElementById('{elementId}').innerText");
        return result?.Trim('"');
    }

    public static async Task<string> GetInnerHtml(this WebWindow web, string elementId)
    {
        var result = await web.ExecuteAsync($@"document.getElementById('{elementId}').innerHTML");
        return result?.Trim('"');
    }
}