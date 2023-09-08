namespace Domain.ValueObjects;

public record Hours : TimeUnity
{
    public Hours(string hours) : base(hours) { }
    public Hours(int hours) : base(hours) { }

    public static Hours Zero { get; } = new(ushort.MinValue);
    public static Hours One { get; } = new(1);
    public static Hours Max { get; } = new(ushort.MaxValue);
    public static Hours Number(ushort hours) => new(hours);

    public static explicit operator Hours(string hours) => new(hours);
    public override string ToString() => base.ToString();
}