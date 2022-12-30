using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepository;

/// <summary>
///     The base repo class
/// </summary>
public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    ///     Firsts the or default using the specified expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>The entity</returns>
    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes).FirstOrDefault(expression);
    }

    /// <summary>
    ///     Firsts the or default using the specified expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>A task containing the entity</returns>
    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return await IncludeIfNotNull(includes).FirstOrDefaultAsync(expression);
    }

    /// <summary>
    ///     Firsts the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>The entity</returns>
    public virtual TEntity First(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes).First(expression);
    }

    /// <summary>
    ///     Firsts the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>A task containing the entity</returns>
    public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return await IncludeIfNotNull(includes).FirstAsync(expression);
    }
}