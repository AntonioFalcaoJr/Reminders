using static Domain.Exceptions;

namespace Domain.Abstractions.Identities;

public interface IIdentifier;

public abstract record GuidIdentifier : IIdentifier
{
    public Guid Id { get; init; }
    
    protected GuidIdentifier()
    {
        Id = Guid.NewGuid();
    }

    protected GuidIdentifier(string value)
    {
        InvalidIdentifier.ThrowIf(
            Guid.TryParse(value, out var result) is false);

        Id = result;
    }

    public static implicit operator string(GuidIdentifier id) => id.Id.ToString();
    public static implicit operator Guid(GuidIdentifier id) => id.Id;
    public static bool operator ==(GuidIdentifier id, string value) => id.Id.CompareTo(value) is 0;
    public static bool operator !=(GuidIdentifier id, string value) => id.Id.CompareTo(value) is not 0;
    public override string ToString() => Id.ToString();
}