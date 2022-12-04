using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Exceptions.Entity;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepository;

public abstract partial class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepo(AppDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        await CreateNoSaveAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        var rangeAsync = entities.ToList();
        await DbSet.AddRangeAsync(rangeAsync);
        await SaveChangesAsync();
        return rangeAsync;
    }

    public virtual async Task CreateNoSaveAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        UpdateNoSave(entity);
        await SaveChangesAsync();
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
        await SaveChangesAsync();
    }

    public virtual void UpdateNoSave(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual void UpdateStateNoSave(TEntity entity)
    {
        DbSet.Entry(entity).State = EntityState.Modified;
    }

    public virtual async Task UpdateStateAsync(TEntity entity)
    {
        UpdateStateNoSave(entity);
        await SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        DeleteNoSave(entity);
        await SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null)
            throw new EntityNotFoundByIdException<TEntity>(id);
        DeleteNoSave(entity);
        await SaveChangesAsync();
    }

    public virtual void DeleteNoSave(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        DeleteNoSaveRange(entities);
        await SaveChangesAsync();
    }

    public virtual void DeleteNoSaveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await GetByIdAsync(id) is not null;
    }

    public async Task<bool> ExistsAsync(TEntity entity)
    {
        return await DbSet.FindAsync(entity) is not null;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await DbSet.AnyAsync(expression);
    }

    public async Task<bool> IsEmptyAsync()
    {
        return !await DbSet.AnyAsync();
    }

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }

    private IQueryable<TEntity> IncludeIfNotNull(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        if (includes is null)
            return DbSet;
        return includes(DbSet);
    }
}