using System.Linq.Expressions;

namespace Rusty.Template.Infrastructure.Repositories.Extensions;

/// <summary>
///     The repository extensions class
/// </summary>
public static class RepositoryExtensions
{
    /// <summary>
    ///     Wheres the nullable using the specified query
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    /// <param name="query">The query</param>
    /// <param name="expression">The expression</param>
    /// <returns>A queryable of t entity</returns>
    public static IQueryable<TEntity> WhereNullable<TEntity>(this IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>>? expression)
    {
        return expression is null ? query : query.Where(expression);
    }
}