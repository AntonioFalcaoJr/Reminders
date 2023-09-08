using Domain.Abstractions.Entities;
using Domain.Abstractions.Identities;
using Domain.Abstractions.Messages;
using Newtonsoft.Json;
using Version = Domain.ValueObjects.Version;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
    where TId : IIdentifier, new()
{
    [JsonProperty]
    private readonly Queue<IDomainEvent> _events = new();

    public Version Version { get; private set; } = Version.Zero;

    public void LoadFromHistory(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            Apply(@event);
            Version = (Version)@event.Version;
        }
    }

    public bool TryDequeueEvent(out IDomainEvent? @event)
        => _events.TryDequeue(out @event);

    protected void RaiseEvent<TEvent>(Func<Version, TEvent> func) where TEvent : IDomainEvent
        => RaiseEvent((func as Func<Version, IDomainEvent>)!);

    protected void RaiseEvent(Func<Version, IDomainEvent> onRaise)
    {
        var @event = onRaise(++Version);
        Apply(@event);
        _events.Enqueue(@event);
    }

    protected abstract void Apply(IDomainEvent @event);
}