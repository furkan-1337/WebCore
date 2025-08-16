namespace WebCore;

public class WebMessage
{
    public string Id { get; set; }
    public string Event { get; set; }
    public string Value { get; set; }
    public DateTime Timestamp { get; set; }
    
    public T GetValue<T>()
    {
        try
        {
            if (typeof(T) == typeof(string))
                return (T)(object)this.Value;

            if (typeof(T) == typeof(bool))
            {
                if (bool.TryParse(this.Value, out var b)) return (T)(object)b;
                if (this.Value == "1") return (T)(object)true;
                if (this.Value == "0") return (T)(object)false;
            }

            if (typeof(T) == typeof(int))
                return (T)(object)Convert.ToInt32(this.Value);

            if (typeof(T) == typeof(float))
                return (T)(object)Convert.ToSingle(this.Value);

            if (typeof(T) == typeof(double))
                return (T)(object)Convert.ToDouble(this.Value);
            
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), this.Value, ignoreCase: true);
            
            throw new InvalidCastException($"Cannot convert '{this.Value}' to {typeof(T).Name}");
        }
        catch (Exception ex)
        {
            throw new InvalidCastException($"Failed to convert '{this.Value}' to {typeof(T).Name}", ex);
        }
    }
}