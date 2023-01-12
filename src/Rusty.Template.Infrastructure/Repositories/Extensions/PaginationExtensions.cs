#region

using Microsoft.EntityFrameworkCore;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Contracts.SubTypes;

#endregion

namespace Rusty.Template.Infrastructure.Repositories.Extensions;

public static class PaginationExtensions
{
	public static ( IQueryable<TEntity> Collection, int TotalCount) PaginateWithTotalCount<TEntity>(
		this IQueryable<TEntity> query, int skipItems, int takeItems)
	{
		return (query.Paginate(skipItems, takeItems), query.Count());
	}


	public static async Task<( IQueryable<TEntity> Collection, int TotalCount)> PaginateWithTotalCountAsync<TEntity>(
		this IQueryable<TEntity> query, int skipItems, int takeItems)
	{
		return (query.Paginate(skipItems, takeItems), await query.CountAsync());
	}


	public static async Task<(List<TEntity> Collection, int TotalCount)> PaginateWithTotalCountAsListAsync<TEntity>(
		this IQueryable<TEntity> query, int skipItems, int takeItems)
	{
		return (await query.Paginate(skipItems, takeItems).ToListAsync(), await query.CountAsync());
	}


	public static async Task<PagedResponse<TEntity>> PaginateWithTotalCountAsListAsync<TEntity>(
		this IQueryable<TEntity> query, PageData? pageData, CancellationToken cancellationToken)
	{
		if (pageData is not null)
			return new PagedResponse<TEntity>(
				await query.Paginate(pageData.Offset, pageData.Limit).ToListAsync(cancellationToken),
				await query.CountAsync()
			);
		var data = await query.ToListAsync(cancellationToken);
		return new PagedResponse<TEntity>(
			data,
			data.Count
		);
	}


	public static IQueryable<TEntity> Paginate<TEntity>(
		this IQueryable<TEntity> query, int skipItems, int takeItems)
	{
		return query.Skip(skipItems).Take(takeItems);
	}
}