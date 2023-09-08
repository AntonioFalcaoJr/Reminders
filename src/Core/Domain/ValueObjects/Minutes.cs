namespace Domain.ValueObjects;

public record Minutes : TimeUnity
{
    public Minutes(string minutes) : base(minutes) { }
    public Minutes(int minutes) : base(minutes) { }

    public static Minutes Zero { get; } = new(ushort.MinValue);
    public static Minutes One { get; } = new(1);
    public static Minutes Max { get; } = new(ushort.MaxValue);
    public static Minutes Number(ushort minutes) => new(minutes);

    public static explicit operator Minutes(string minutes) => new(minutes);
    public override string ToString() => base.ToString();
}