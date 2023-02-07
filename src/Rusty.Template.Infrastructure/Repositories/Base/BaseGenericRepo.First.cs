#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

#endregion

namespace Rusty.Template.Infrastructure.Repositories.Base;

public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
	public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
										   Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
	{
		return IncludeIfNotNull(includes).FirstOrDefault(expression);
	}

	public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
															 CancellationToken cancellationToken = default,
															Func<IQueryable<TEntity>,
																IIncludableQueryable<TEntity, object>>? includes = null)
	{
		return await IncludeIfNotNull(includes).FirstOrDefaultAsync(expression, cancellationToken);
	}


	public virtual TEntity First(Expression<Func<TEntity, bool>> expression,
								 Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
	{
		return IncludeIfNotNull(includes).First(expression);
	}

	public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression,
												   CancellationToken cancellationToken = default,
												  Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
	{
		return await IncludeIfNotNull(includes).FirstAsync(expression, cancellationToken);
	}
}