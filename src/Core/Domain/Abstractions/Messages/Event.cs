using MassTransit;

namespace Domain.Abstractions.Messages;

[ExcludeFromTopology]
public abstract record Event : Message, IEvent;