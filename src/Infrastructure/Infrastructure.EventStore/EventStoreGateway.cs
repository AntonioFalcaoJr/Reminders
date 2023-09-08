using Application.Abstractions.Gateways;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Domain.Abstractions.Identities;
using Domain.Abstractions.Messages;
using Microsoft.EntityFrameworkCore;
using Version = Domain.ValueObjects.Version;

namespace Infrastructure.EventStore;

public class EventStoreGateway(DbContext dbContext) : IEventStoreGateway
{
    public async Task AppendAsync<TAggregate, TId>(StoreEvent<TAggregate, TId> storeEvent, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
    {
        await dbContext.Set<StoreEvent<TAggregate, TId>>().AddAsync(storeEvent, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<IDomainEvent>> GetStreamAsync<TAggregate, TId>(TId id, Version version, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot<TId>
        where TId : IIdentifier, new()
        => dbContext.Set<StoreEvent<TAggregate, TId>>()
            .AsNoTracking()
            .Where(@event => @event.AggregateId.Equals(id))
            .Where(@event => @event.Version > version)
            .Select(@event => @event.Event)
            .ToListAsync(cancellationToken);
}