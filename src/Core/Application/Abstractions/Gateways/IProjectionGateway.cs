using System.Linq.Expressions;
using Domain.Abstractions.Identities;

namespace Application.Abstractions.Gateways;

public interface IProjectionGateway<TProjection>
    where TProjection : IProjection
{
    Task<TProjection?> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<TProjection?> GetAsync<TId>(TId id, CancellationToken cancellationToken) where TId : IIdentifier;
    ValueTask ReplaceInsertAsync(TProjection replacement, CancellationToken cancellationToken);
    ValueTask RebuildInsertAsync(TProjection replacement, CancellationToken cancellationToken);
    Task DeleteAsync(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken);
    Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken) where TId : struct;
    Task UpdateFieldAsync<TField, TId>(TId id, ulong version, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken) where TId : struct;
}