using static Domain.Exceptions;

namespace Domain.ValueObjects;

public abstract record TimeUnity
{
    private readonly ushort _value;

    protected TimeUnity(string time)
    {
        time = time.Trim();

        if (ushort.TryParse(time, out var value) is false)
            TimeUnityMustBeAPositiveNumber.Throw();

        _value = value;
    }

    protected TimeUnity(int time)
    {
        if (time is < ushort.MinValue or > ushort.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(time), "Must be a valid number");

        _value = (ushort)time;
    }

    public bool IsZero => _value == 0;

    public static implicit operator ushort(TimeUnity timeUnity) => timeUnity._value;
    public static implicit operator string(TimeUnity timeUnity) => timeUnity._value.ToString();

    public static bool operator <(TimeUnity left, TimeUnity right) => left._value < right._value;
    public static bool operator >(TimeUnity left, TimeUnity right) => left._value > right._value;
    public static bool operator <=(TimeUnity left, TimeUnity right) => left._value <= right._value;
    public static bool operator >=(TimeUnity left, TimeUnity right) => left._value >= right._value;

    public override string ToString() => _value.ToString();
}