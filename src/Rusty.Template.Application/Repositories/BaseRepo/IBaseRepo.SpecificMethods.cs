#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Contracts.Responses;

#endregion

namespace Rusty.Template.Application.Repositories.BaseRepo;

public partial interface IBaseRepo<TEntity> where TEntity : class
{
	#region Pagination

	Task<PagedResponse<TEntity>> PaginateAsync(OrderedPagedRequest request, CancellationToken cancellationToken,
											   Func<IQueryable<TEntity>, IQueryable<TEntity>>?
												   includes = null);

	Task<PagedResponse<TResult>> PaginateAsync<TResult>(OrderedPagedRequest request,
														CancellationToken cancellationToken,
														Func<IQueryable<TEntity>, IQueryable<TEntity>>
															? includes = null) where TResult : class;

	Task<PagedResponse<TResult>> PaginateAsync<TResult>(
		OrderedPagedRequest request, Expression<Func<TEntity, bool>> expression,
		CancellationToken cancellationToken,
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null) where TResult : class;

	#endregion
}