#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rusty.Template.Application.Repositories.BaseRepo;
using Rusty.Template.Domain.Exceptions.Entity;

#endregion

namespace Rusty.Template.Infrastructure.Repositories.Base;

public abstract partial class BaseGenericRepo<TContext, TEntity> : IBaseRepo<TEntity>
	where TEntity : class where TContext : DbContext
{
	protected readonly TContext Context;


	protected readonly DbSet<TEntity> DbSet;


	protected BaseGenericRepo(TContext context)
	{
		Context = context;
		DbSet = context.Set<TEntity>();
	}


	public async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken)
	{
		return await DbSet.FindAsync(new[] { id }, cancellationToken);
	}


	public TEntity? GetById(object id)
	{
		return DbSet.Find(id);
	}


	public async Task<TEntity> CreateAsync(TEntity entity)
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


	public async Task UpdateAsync(TEntity entity)
	{
		UpdateNoSave(entity);
		await SaveChangesAsync();
	}


	public virtual void UpdateNoSave(TEntity entity)
	{
		DbSet.Update(entity);
	}


	public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
	{
		UpdateRangeNoSave(entities);
		await SaveChangesAsync();
	}


	public virtual void UpdateRangeNoSave(IEnumerable<TEntity> entities)
	{
		DbSet.UpdateRange(entities);
	}


	public virtual void Attach(TEntity entity)
	{
		DbSet.Attach(entity);
	}


	public virtual void AttachRange(IEnumerable<TEntity> entities)
	{
		DbSet.AttachRange(entities);
	}


	public async Task DeleteAsync(TEntity entity)
	{
		DeleteNoSave(entity);
		await SaveChangesAsync();
	}


	public async Task DeleteAsync(object id)
	{
		var entity = await GetByIdAsync(id, CancellationToken.None)
					 ?? throw new EntityNotFoundByIdException<TEntity>(id);

		DeleteNoSave(entity);
		await SaveChangesAsync();
	}


	public virtual void DeleteNoSave(TEntity entity)
	{
		DbSet.Remove(entity);
	}


	public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
	{
		DeleteNoSaveRange(entities);
		await SaveChangesAsync();
	}


	public virtual void DeleteNoSaveRange(IEnumerable<TEntity> entities)
	{
		DbSet.RemoveRange(entities);
	}


	public virtual async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken)
	{
		return await GetByIdAsync(id, cancellationToken) is not null;
	}


	public virtual async Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken)
	{
		return await DbSet.FindAsync(new object[] { entity }, cancellationToken) is not null;
	}


	public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression,
												CancellationToken cancellationToken)
	{
		return await DbSet.AnyAsync(expression, cancellationToken);
	}


	public async Task<bool> IsEmptyAsync(CancellationToken cancellationToken)
	{
		return !await DbSet.AnyAsync(cancellationToken);
	}


	public async Task<bool> IsEmptyAsync(Expression<Func<TEntity, bool>> expression,
										 CancellationToken cancellationToken)
	{
		return !await DbSet.AnyAsync(expression, cancellationToken);
	}


	public async Task<int> SaveChangesAsync()
	{
		return await Context.SaveChangesAsync();
	}


	protected IQueryable<TEntity> IncludeIfNotNull(
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
	{
		if (includes is null)
			return DbSet;
		return includes(DbSet);
	}
}