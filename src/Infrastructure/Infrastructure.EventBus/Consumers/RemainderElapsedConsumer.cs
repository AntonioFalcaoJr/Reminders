using Application.UseCases.Events;
using Domain.Aggregates.Events;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class RemainderElapsedConsumer(ICallbackWhenReminderElapsedInteractor interactor)
    : Consumer<DelayedEvent.ReminderElapsed>(interactor);