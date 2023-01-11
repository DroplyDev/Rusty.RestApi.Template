using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Infrastructure.Repositories.Extensions;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepo;

/// <summary>
///     The base repo class
/// </summary>
public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    ///     Wheres the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <returns>A queryable of t entity</returns>
    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.Where(expression);
    }

    /// <summary>
    ///     Wheres the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="orderBy">The order by</param>
    /// <param name="orderDirection">The order direction</param>
    /// <returns>A queryable of t entity</returns>
    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, string orderBy,
                                             OrderDirection orderDirection)
    {
        return DbSet
               .Where(expression)
               .OrderByWithDirection(orderBy, orderDirection);
    }
}