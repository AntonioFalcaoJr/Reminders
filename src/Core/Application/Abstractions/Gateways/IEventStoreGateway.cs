using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Domain.Abstractions.Identities;
using Domain.Abstractions.Messages;
using Version = Domain.ValueObjects.Version;

namespace Application.Abstractions.Gateways;

public interface IEventStoreGateway
{
    Task AppendAsync<TAggregate, TId>(StoreEvent<TAggregate, TId> storeEvent, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new();

    Task<List<IDomainEvent>> GetStreamAsync<TAggregate, TId>(TId id, Version version, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new();
}