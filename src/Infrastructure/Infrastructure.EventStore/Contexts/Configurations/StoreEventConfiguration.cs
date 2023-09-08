using Domain.Abstractions.EventStore;
using Domain.Aggregates;
using Infrastructure.EventStore.Contexts.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EventStore.Contexts.Configurations;

public class StoreEventConfiguration : IEntityTypeConfiguration<StoreEvent<Reminder, ReminderId>>
{
    public void Configure(EntityTypeBuilder<StoreEvent<Reminder, ReminderId>> builder)
    {
        builder.ToTable($"{nameof(Reminder)}StoreEvents");

        builder.HasKey(@event => new { @event.Version, @event.AggregateId });

        builder
            .Property(@event => @event.AggregateId)
            .HasConversion<IdentifierConverter<ReminderId>>()
            .IsRequired();

        builder
            .Property(@event => @event.Event)
            .HasConversion<EventConverter>()
            .IsRequired();

        builder
            .Property(@event => @event.EventType)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder
            .Property(@event => @event.Timestamp)
            .IsRequired();

        builder
            .Property(@event => @event.Version)
            .HasConversion<VersionConverter>()
            .IsRequired();
    }
}