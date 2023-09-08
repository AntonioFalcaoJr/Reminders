using MassTransit;

namespace Domain.Abstractions.Messages;

[ExcludeFromTopology]
public abstract record Message
{
    public DateTimeOffset Timestamp { get; private init; } = DateTimeOffset.Now;
}