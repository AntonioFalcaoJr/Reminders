namespace Infrastructure.EventStore.Exceptions;

public class AggregateNotFoundException() : EventStoreException<AggregateNotFoundException>("Aggregate not found");