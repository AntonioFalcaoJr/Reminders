namespace Application.Abstractions;

public interface IInteractor<in TCommand>
    where TCommand : notnull
{
    Task InteractAsync(TCommand cmd, CancellationToken cancellationToken);
}

public interface IInteractor<in TCommand, TResult>
    where TCommand : notnull
{
    Task<TResult> InteractAsync(TCommand cmd, CancellationToken cancellationToken);
}