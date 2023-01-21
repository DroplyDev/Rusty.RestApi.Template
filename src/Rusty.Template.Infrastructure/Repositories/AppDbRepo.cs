#region

using System.Linq.Expressions;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Repositories.Base;

#endregion

namespace Rusty.Template.Infrastructure.Repositories;

public abstract class AppDbRepo<TEntity> : BaseGenericRepo<AppDbContext, TEntity> where TEntity : class
{
	protected AppDbRepo(AppDbContext context, Expression<Func<TEntity, object>> defaultOrderBy) : base(context,
		defaultOrderBy)
	{
	}
}