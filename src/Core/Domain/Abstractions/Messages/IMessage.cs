using MassTransit;

namespace Domain.Abstractions.Messages;

[ExcludeFromTopology]
public interface IMessage
{
    DateTimeOffset Timestamp { get; }
}