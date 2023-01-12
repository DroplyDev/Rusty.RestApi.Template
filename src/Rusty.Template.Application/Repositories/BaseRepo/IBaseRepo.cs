#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.SubTypes;

#endregion

namespace Rusty.Template.Application.Repositories.BaseRepo;

public partial interface IBaseRepo<TEntity> where TEntity : class
{
	( IQueryable<TEntity> Collection, int TotalCount) Paginate(int skipItems, int takeItems, string orderBy,
															   OrderDirection orderDirection,
															   Expression<Func<TEntity, bool>>? expression);

	Task<int> SaveChangesAsync();

	#region GetById

	Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken);
	TEntity? GetById(object id);

	#endregion

	#region Create

	Task<TEntity> CreateAsync(TEntity entity);
	Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);
	Task CreateNoSaveAsync(TEntity entity);

	#endregion

	#region Update

	Task UpdateAsync(TEntity entity);
	void UpdateNoSave(TEntity entity);
	Task UpdateRangeAsync(IEnumerable<TEntity> entities);
	void UpdateRangeNoSave(IEnumerable<TEntity> entities);
	void Attach(TEntity entity);
	void AttachRange(IEnumerable<TEntity> entities);

	#endregion

	#region Delete

	Task DeleteAsync(TEntity entity);
	Task DeleteAsync(int id);
	void DeleteNoSave(TEntity entity);
	Task DeleteRangeAsync(IEnumerable<TEntity> entities);
	void DeleteNoSaveRange(IEnumerable<TEntity> entities);

	#endregion


	#region Exists

	Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);

	Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken);

	Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

	#endregion

	#region IsEmpty

	Task<bool> IsEmptyAsync(CancellationToken cancellationToken);

	Task<bool> IsEmptyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

	#endregion

	#region First

	TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
							Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

	Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken,
									   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes =
										   null);

	TEntity First(Expression<Func<TEntity, bool>> expression,
				  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

	Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken,
							 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

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