using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Application.Repositories.BaseRepo;
using Rusty.Template.Contracts.Exceptions.Entity;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepo;

/// <summary>
///     The base repo class
/// </summary>
/// <seealso cref="IBaseRepo{TEntity}" />
public abstract partial class BaseGenericRepo<TContext, TEntity> : IBaseRepo<TEntity>
    where TEntity : class where TContext : DbContext
{
    /// <summary>
    ///     The context
    /// </summary>
    protected readonly TContext Context;

    /// <summary>
    ///     The db set
    /// </summary>
    protected readonly DbSet<TEntity> DbSet;


    /// <summary>
    ///     Initializes a new instance of the <see /> class
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="defaultOrderBy">The default order by</param>
    protected BaseGenericRepo(TContext context, Expression<Func<TEntity, object>> defaultOrderBy)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    /// <summary>
    ///     Gets the by id using the specified id
    /// </summary>
    /// <param name="id">The id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task containing the entity</returns>
    public async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(new[] { id }, cancellationToken);
    }

    /// <summary>
    ///     Gets the by id using the specified id
    /// </summary>
    /// <param name="id">The id</param>
    /// <returns>The entity</returns>
    public TEntity? GetById(object id)
    {
        return DbSet.Find(id);
    }

    /// <summary>
    ///     Creates the entity
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>The entity</returns>
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await CreateNoSaveAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    /// <summary>
    ///     Creates the range using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    /// <returns>The range</returns>
    public virtual async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        var rangeAsync = entities.ToList();
        await DbSet.AddRangeAsync(rangeAsync);
        await SaveChangesAsync();
        return rangeAsync;
    }

    /// <summary>
    ///     Creates the no save using the specified entity
    /// </summary>
    /// <param name="entity">The entity</param>
    public virtual async Task CreateNoSaveAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    /// <summary>
    ///     Updates the full using the specified entity
    /// </summary>
    /// <param name="entity">The entity</param>
    public async Task UpdateAsync(TEntity entity)
    {
        UpdateNoSave(entity);
        await SaveChangesAsync();
    }

    /// <summary>
    ///     Updates the full no save using the specified entity
    /// </summary>
    /// <param name="entity">The entity</param>
    public virtual void UpdateNoSave(TEntity entity)
    {
        DbSet.Update(entity);
    }

    /// <summary>
    ///     Updates the full range using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        UpdateRangeNoSave(entities);
        await SaveChangesAsync();
    }

    /// <summary>
    ///     Updates the full range no save using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    public virtual void UpdateRangeNoSave(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
    }

    /// <summary>
    ///     Attaches the entity
    /// </summary>
    /// <param name="entity">The entity</param>
    public virtual void Attach(TEntity entity)
    {
        DbSet.Attach(entity);
    }

    /// <summary>
    ///     Attaches the range using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    public virtual void AttachRange(IEnumerable<TEntity> entities)
    {
        DbSet.AttachRange(entities);
    }

    /// <summary>
    ///     Deletes the entity
    /// </summary>
    /// <param name="entity">The entity</param>
    public async Task DeleteAsync(TEntity entity)
    {
        DeleteNoSave(entity);
        await SaveChangesAsync();
    }

    /// <summary>
    ///     Deletes the id
    /// </summary>
    /// <param name="id">The id</param>
    /// <exception cref="EntityNotFoundByIdException{TEntity}"></exception>
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id, CancellationToken.None)
                     ?? throw new EntityNotFoundByIdException<TEntity>(id);

        DeleteNoSave(entity);
        await SaveChangesAsync();
    }

    /// <summary>
    ///     Deletes the no save using the specified entity
    /// </summary>
    /// <param name="entity">The entity</param>
    public virtual void DeleteNoSave(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    /// <summary>
    ///     Deletes the range using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        DeleteNoSaveRange(entities);
        await SaveChangesAsync();
    }

    /// <summary>
    ///     Deletes the no save range using the specified entities
    /// </summary>
    /// <param name="entities">The entities</param>
    public virtual void DeleteNoSaveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }

    /// <summary>
    ///     Exists the id
    /// </summary>
    /// <param name="id">The id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task containing the bool</returns>
    public virtual async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await GetByIdAsync(id, cancellationToken) is not null;
    }

    /// <summary>
    ///     Exists the entity
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task containing the bool</returns>
    public virtual async Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(new object[] { entity }, cancellationToken) is not null;
    }

    /// <summary>
    ///     Exists the expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task containing the bool</returns>
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken)
    {
        return await DbSet.AnyAsync(expression, cancellationToken);
    }

    /// <summary>
    ///     checks if collection is empty
    /// </summary>
    /// <returns>A task containing the bool</returns>
    public async Task<bool> IsEmptyAsync(CancellationToken cancellationToken)
    {
        return !await DbSet.AnyAsync(cancellationToken);
    }

    /// <summary>
    ///     Ises the empty using the specified expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task containing the bool</returns>
    public async Task<bool> IsEmptyAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken)
    {
        return !await DbSet.AnyAsync(expression, cancellationToken);
    }

    /// <summary>
    ///     Saves the changes
    /// </summary>
    /// <returns>A task containing the int</returns>
    public async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }

    /// <summary>
    ///     Includes the if not null using the specified includes
    /// </summary>
    /// <param name="includes">The includes</param>
    /// <returns>A queryable of t entity</returns>
    protected IQueryable<TEntity> IncludeIfNotNull(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        if (includes is null)
            return DbSet;
        return includes(DbSet);
    }
}