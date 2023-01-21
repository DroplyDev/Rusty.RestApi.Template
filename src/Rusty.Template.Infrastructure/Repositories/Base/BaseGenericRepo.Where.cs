#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Infrastructure.Repositories.Extensions;

#endregion

namespace Rusty.Template.Infrastructure.Repositories.Base;

public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
	public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
	{
		return DbSet.Where(expression);
	}


	public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, string orderBy,
											 OrderDirection orderDirection)
	{
		return DbSet
			.Where(expression)
			.OrderByWithDirection(orderBy, orderDirection);
	}
}