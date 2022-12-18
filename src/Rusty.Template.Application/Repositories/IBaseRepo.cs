using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.SubTypes;

namespace Rusty.Template.Application.Repositories;

/// <summary>
///     The base repo interface
/// </summary>
public partial interface IBaseRepo<TEntity> where TEntity : class
{
    /// <summary>
    ///     Gets the by id using the specified id
    /// </summary>
    /// <param name="id">The id</param>
    /// <returns>A task containing the entity</returns>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    ///     Gets the by id with exception using the specified id
    /// </summary>
    /// <param name="id">The id</param>
    /// <returns>A task containing the entity</returns>
    Task<TEntity> GetByIdWithExceptionAsync(int id);

    /// <summary>
    ///     Creates the entity
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>A task containing the entity</returns>
    Task<TEntity> CreateAsync(TEntity entity);

    /// <summary>
    ///     Creates the range using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    /// <returns>A task containing an enumerable of t entity</returns>
    Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Creates the no save using the specified entity
    /// </summary>
    /// <param name="entity">The entity</param>
    Task CreateNoSaveAsync(TEntity entity);

    /// <summary>
    ///     Updates the full using the specified entity
    /// </summary>
    /// <param name="entity">The entity</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    ///     Updates the full no save using the specified entity
    /// </summary>
    /// <param name="entity">The entity</param>
    void UpdateNoSave(TEntity entity);

    /// <summary>
    ///     Updates the full range using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Updates the full range no save using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    void UpdateRangeNoSave(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Updates the attach no save using the specified entity
    /// </summary>
    /// <param name="entity">The entity</param>
    void Attach(TEntity entity);

    /// <summary>
    ///     Updates the attach range using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    void AttachRange(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Deletes the entity
    /// </summary>
    /// <param name="entity">The entity</param>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    ///     Deletes the id
    /// </summary>
    /// <param name="id">The id</param>
    Task DeleteAsync(int id);

    /// <summary>
    ///     Deletes the no save using the specified entity
    /// </summary>
    /// <param name="entity">The entity</param>
    void DeleteNoSave(TEntity entity);

    /// <summary>
    ///     Deletes the range using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Deletes the no save range using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    void DeleteNoSaveRange(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Existses the id
    /// </summary>
    /// <param name="id">The id</param>
    /// <returns>A task containing the bool</returns>
    Task<bool> ExistsAsync(int id);

    /// <summary>
    ///     Existses the entity
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>A task containing the bool</returns>
    Task<bool> ExistsAsync(TEntity entity);

    /// <summary>
    ///     Existses the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <returns>A task containing the bool</returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    ///     Ises the empty
    /// </summary>
    /// <returns>A task containing the bool</returns>
    Task<bool> IsEmptyAsync();

    /// <summary>
    ///     Ises the empty using the specified expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <returns>A task containing the bool</returns>
    Task<bool> IsEmptyAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    ///     Saves the changes
    /// </summary>
    /// <returns>A task containing the int</returns>
    Task<int> SaveChangesAsync();

    //First
    /// <summary>
    ///     Firsts the or default using the specified expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>The entity</returns>
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    /// <summary>
    ///     Firsts the or default using the specified expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>A task containing the entity</returns>
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    /// <summary>
    ///     Firsts the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>The entity</returns>
    TEntity First(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    /// <summary>
    ///     Firsts the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>A task containing the entity</returns>
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    //Get All
    /// <summary>
    ///     Gets the all using the specified includes
    /// </summary>
    /// <param name="includes">The includes</param>
    /// <returns>A queryable of t entity</returns>
    IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    /// <summary>
    ///     Gets the all using the specified order by
    /// </summary>
    /// <param name="orderBy">The order by</param>
    /// <param name="orderDirection">The order direction</param>
    /// <param name="includes">The includes</param>
    /// <returns>A queryable of t entity</returns>
    IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    //Pagination
    /// <summary>
    ///     Paginates the skip items
    /// </summary>
    /// <param name="skipItems">The skip items</param>
    /// <param name="takeItems">The take items</param>
    /// <param name="orderBy">The order by</param>
    /// <param name="orderDirection">The order direction</param>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>A queryable of t entity collection and int total count</returns>
    ( IQueryable<TEntity> Collection, int TotalCount) Paginate(int skipItems, int takeItems, string orderBy,
        OrderDirection orderDirection,
        Expression<Func<TEntity, bool>>? expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    //Where
    /// <summary>
    ///     Wheres the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="includes">The includes</param>
    /// <returns>A queryable of t entity</returns>
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    /// <summary>
    ///     Wheres the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="orderBy">The order by</param>
    /// <param name="orderDirection">The order direction</param>
    /// <param name="includes">The includes</param>
    /// <returns>A queryable of t entity</returns>
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, string orderBy,
        OrderDirection orderDirection,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);
}