#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.SubTypes;

#endregion

namespace Rusty.Template.Application.Repositories.BaseRepo;

public partial interface IBaseRepo<TEntity> where TEntity : class
{
	(IQueryable<TEntity> Collection, int TotalCount) Paginate(int skipItems, int takeItems, string orderBy,
															   OrderDirection orderDirection,
															   Expression<Func<TEntity, bool>>? expression);

	Task<int> SaveChangesAsync();

	#region GetById

	Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
	TEntity? GetById(object id);

	#endregion

	#region Create

	Task<TEntity> CreateAsync(TEntity entity);
	Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);
	Task CreateNoSaveAsync(TEntity entity);

	#endregion

	#region Update

	Task ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> entities);
	Task UpdateAsync(TEntity entity);
	void UpdateNoSave(TEntity entity);
	Task UpdateRangeAsync(IEnumerable<TEntity> entities);
	void UpdateRangeNoSave(IEnumerable<TEntity> entities);
	void Attach(TEntity entity);
	void AttachRange(IEnumerable<TEntity> entities);

	#endregion

	#region Delete
	Task DeleteAsync(TEntity entity);
	Task DeleteAsync(object id);
	void DeleteNoSave(TEntity entity);
	Task DeleteRangeAsync(IEnumerable<TEntity> entities);
	void DeleteNoSaveRange(IEnumerable<TEntity> entities);

	#endregion


	#region Exists

	Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default);

	Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken = default);

	Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

	#endregion

	#region IsEmpty

	Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default);

	Task<bool> IsEmptyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

	#endregion

	#region First

	TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
							Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);

	Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
									   CancellationToken cancellationToken = default,
									   Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes =
										   null);

	TEntity First(Expression<Func<TEntity, bool>> expression,
				  Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);

	Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default,
							 Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);

	#endregion

	#region GetAll

	IQueryable<TEntity> GetAll();
	IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection);

	#endregion

	#region Where

	IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

	IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, string orderBy,
							  OrderDirection orderDirection);

	#endregion
}