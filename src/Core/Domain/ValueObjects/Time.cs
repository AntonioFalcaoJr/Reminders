namespace Domain.ValueObjects;

public record Time
{
    private DateTimeOffset _time;

    public Time(string time) => _time = DateTimeOffset.Parse(time);
    public Time(DateTimeOffset time) => _time = time;

    public static Time Zero { get; } = new("00:00:00");
    public static Time Now => new(DateTimeOffset.Now);

    public static implicit operator string(Time time) => time._time.ToString();
    public static implicit operator DateTimeOffset(Time time) => time._time;
    public static explicit operator Time(string time) => new(time);

    public Time Add(Hours hours, Minutes minutes, Seconds seconds)
    {
        _time = _time.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
        return this;
    }

    public static TimeSpan operator -(Time left, Time right) => left._time - right._time;

    public override string ToString() => _time.ToString();
}