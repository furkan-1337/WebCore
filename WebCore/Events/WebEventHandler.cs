namespace WebCore.Events;

public struct WebEventHandler
{
    public WebMessage Message { get; private set; }
    public Action<WebMessage> Action { get; set; }

    public WebEventHandler(string id, string webEvent, Action<WebMessage> action)
    {
        Message = new WebMessage();
        Message.Id = id;
        Message.Event = webEvent;
        Action = action;
    }
    
    public WebEventHandler(string elementId, WebEvent webEvent, Action<WebMessage> action) : this(elementId, (char.ToLower(webEvent.ToString()[0]) + webEvent.ToString().Substring(1)), action) { }
}