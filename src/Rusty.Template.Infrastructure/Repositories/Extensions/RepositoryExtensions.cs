#region

using System.Linq.Expressions;

#endregion

namespace Rusty.Template.Infrastructure.Repositories.Extensions;

public static class RepositoryExtensions
{
	public static IQueryable<TEntity> WhereNullable<TEntity>(this IQueryable<TEntity> query,
															 Expression<Func<TEntity, bool>>? expression)
	{
		return expression is null ? query : query.Where(expression);
	}
}