#region

using Microsoft.EntityFrameworkCore;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Infrastructure.Repositories.Extensions;

#endregion

namespace Rusty.Template.Infrastructure.Repositories.BaseRepo;

public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
	public virtual IQueryable<TEntity> GetAll()
	{
		return DbSet;
	}


	public virtual IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection)
	{
		return DbSet.OrderByWithDirection(orderBy, orderDirection);
	}
}