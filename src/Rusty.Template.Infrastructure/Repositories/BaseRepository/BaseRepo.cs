using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Exceptions.Entity;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Repositories.Extensions;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepository;

public abstract partial class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<TEntity> DbSet;
    protected readonly Expression<Func<TEntity, object>> DefaultOrderBy;
    protected readonly OrderDirection DefaultOrderDirection;

    protected BaseRepo(AppDbContext context, Expression<Func<TEntity, object>> defaultOrderBy)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
        DefaultOrderBy = defaultOrderBy;
        DefaultOrderDirection = OrderDirection.Desc;
    }

    protected BaseRepo(AppDbContext context, Expression<Func<TEntity, object>> defaultOrderBy,
        OrderDirection defaultOrderDirection)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
        DefaultOrderBy = defaultOrderBy;
        DefaultOrderDirection = defaultOrderDirection;
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

    public virtual async Task<bool> ExistsAsync(int id)
    {
        return await GetByIdAsync(id) is not null;
    }

    public virtual async Task<bool> ExistsAsync(TEntity entity)
    {
        return await DbSet.FindAsync(entity) is not null;
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await DbSet.AnyAsync(expression);
    }

    public async Task<bool> IsEmptyAsync()
    {
        return !await DbSet.AnyAsync();
    }

    public async Task<bool> IsEmptyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return !await DbSet.AnyAsync(expression);
    }

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }

    public virtual void UpdateStateNoSave(TEntity entity)
    {
        DbSet.Entry(entity).State = EntityState.Modified;
    }

    public IQueryable<TEntity> IncludeIfNotNull(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        if (includes is null)
            return DbSet;
        return includes(DbSet);
    }

    public IQueryable<TEntity> OrderByOrPredefined(IQueryable<TEntity> query, OrderByData? data)
    {
        return data is null
            ? query.OrderByWithDirection(DefaultOrderBy, DefaultOrderDirection)
            : query.OrderByWithDirection(data.OrderBy, data.OrderDirection);
    }
}