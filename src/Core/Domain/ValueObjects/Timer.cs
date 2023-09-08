using static Domain.Exceptions;

namespace Domain.ValueObjects;

public record Timer
{
    public Timer(Hours hours, Minutes minutes, Seconds seconds)
    {
        if (hours.IsZero && minutes.IsZero && seconds.IsZero)
            TimerCannotBeZero.Throw();

        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }

    public Hours Hours { get; }
    public Minutes Minutes { get; }
    public Seconds Seconds { get; }

    public static Timer Initial { get; } = new(Hours.Zero, Minutes.One, Seconds.Zero);
    public static Timer Max { get; } = new(Hours.Max, Minutes.Max, Seconds.Max);

    public static explicit operator Timer(string time)
    {
        var parts = time.Split(':');
        return new(new(parts[0]), new(parts[1]), new(parts[2]));
    }

    public static implicit operator string(Timer time) => time.ToString();
    public override string ToString() => $"{Hours}:{Minutes}:{Seconds}";
}