using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Infrastructure.Repositories.Extensions;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepo;

/// <summary>
///     The base repo class
/// </summary>
public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    ///     Gets the all using the specified includes
    /// </summary>
    /// <param name="includes">The includes</param>
    /// <returns>A queryable of t entity</returns>
    public virtual IQueryable<TEntity> GetAll(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes);
    }

    /// <summary>
    ///     Gets the all using the specified order by
    /// </summary>
    /// <param name="orderBy">The order by</param>
    /// <param name="orderDirection">The order direction</param>
    /// <param name="includes">The includes</param>
    /// <returns>A queryable of t entity</returns>
    public virtual IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes)
            .OrderByWithDirection(orderBy, orderDirection);
    }
}