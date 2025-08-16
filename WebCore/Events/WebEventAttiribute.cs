namespace WebCore.Events;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class WebEventAttribute : Attribute
{
    public string Id { get; }
    public string Event { get; }

    public WebEventAttribute(string id, string event_name)
    {
        Id = id;
        Event = event_name;
    }
    
    public WebEventAttribute(string id, WebEvent webEvent) : this(id, (char.ToLower(webEvent.ToString()[0]) + webEvent.ToString().Substring(1))) { }
}