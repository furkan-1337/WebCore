using WebCore.Events;

namespace WebCore.Sandbox.Settings;

public class ESP : WebController
{
    public static bool Enabled { get; set; } = false;
    public static bool ShowNames { get; set; } = false;
    public static bool ShowHealth { get; set; } = false;
    public static bool ShowWeapons { get; set; } = false;
    public static bool ShowDistance { get; set; } = false;
    public static int MaxDistance { get; set; } = 200;
    public static ESPBoxType BoxType { get; set; } = ESPBoxType.Rect2D; 
    
    public enum ESPBoxType
    {
        Rect2D,
        Box3D,
        Corner
    }
    
    [WebEvent("espEnable", WebEvent.Change)]
    private void EspEnable_OnChange(WebMessage msg)
    {
        Enabled = msg.GetValue<bool>();
        Console.WriteLine($"ESP: {Enabled}");
    }
    
    [WebEvent("espNames", WebEvent.Change)]
    private void EspNames_OnChange(WebMessage msg)
    {
        ShowNames = msg.GetValue<bool>();
        Console.WriteLine($"Show Names: {ShowNames}");
    }
    
    [WebEvent("espHealth", WebEvent.Change)]
    private void EspHealth_OnChange(WebMessage msg)
    {
        ShowHealth = msg.GetValue<bool>();
        Console.WriteLine($"Show Health: {ShowHealth}");
    }
    
    [WebEvent("espWeapons", WebEvent.Change)]
    private void EspWeapons_OnChange(WebMessage msg)
    {
        ShowWeapons = msg.GetValue<bool>();
        Console.WriteLine($"Show Weapons: {ShowWeapons}");
    }
    
    [WebEvent("espDistance", WebEvent.Change)]
    private void EspDistance_OnChange(WebMessage msg)
    {
        ShowDistance = msg.GetValue<bool>();
        Console.WriteLine($"Show Distance: {ShowDistance}");
    }
    
    [WebEvent("espMaxDistance", WebEvent.Change)]
    private void EspMaxDistance_OnChange(WebMessage msg)
    {
        MaxDistance = msg.GetValue<int>();
        Console.WriteLine($"Distance: {MaxDistance}");
    }
    
    [WebEvent("espBoxType", WebEvent.Change)]
    private void EspBoxType_OnChange(WebMessage msg)
    {
        BoxType = msg.GetValue<ESPBoxType>();
        Console.WriteLine($"BoxType: {BoxType}");
    }
}