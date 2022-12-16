using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Repositories.Extensions;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepository;

/// <summary>
///     The base repo class
/// </summary>
public partial class BaseRepo<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Wheres the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>A queryable of t entity</returns>
    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes).Where(expression);
    }

    /// <summary>
    ///     Wheres the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="orderBy">The order by</param>
    /// <param name="orderDirection">The order direction</param>
    /// <param name="includes">The includes</param>
    /// <returns>A queryable of t entity</returns>
    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, string orderBy,
        OrderDirection orderDirection,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes)
            .Where(expression)
            .OrderByWithDirection(orderBy, orderDirection);
    }
}