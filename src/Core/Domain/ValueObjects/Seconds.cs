namespace Domain.ValueObjects;

public record Seconds : TimeUnity
{
    public Seconds(string time) : base(time) { }
    public Seconds(int quantity) : base(quantity) { }

    public static Seconds Zero { get; } = new(ushort.MinValue);
    public static Seconds One { get; } = new(1);
    public static Seconds Max { get; } = new(ushort.MaxValue);
    public static Seconds Number(ushort quantity) => new(quantity);
    
    public static explicit operator Seconds(string seconds) => new(seconds);
    public override string ToString() => base.ToString();
}