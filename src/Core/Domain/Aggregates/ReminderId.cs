using Domain.Abstractions.Identities;

namespace Domain.Aggregates;

public record ReminderId : GuidIdentifier
{
    public ReminderId() { }
    public ReminderId(string value) : base(value) { }

    public static ReminderId New => new();
    public static readonly ReminderId Undefined = new() { Id = Guid.Empty };

    public static explicit operator ReminderId(string value) => new(value);
    public static implicit operator ReminderId(Guid value) => new() { Id = value };
    public override string ToString() => base.ToString();
}