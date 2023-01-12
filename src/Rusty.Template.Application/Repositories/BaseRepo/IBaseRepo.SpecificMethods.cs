#region

using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Contracts.Responses;

#endregion

namespace Rusty.Template.Application.Repositories.BaseRepo;

public partial interface IBaseRepo<TEntity> where TEntity : class
{
	#region Pagination

	Task<PagedResponse<TEntity>> PaginateAsync(OrderedPagedRequest request, CancellationToken cancellationToken,
											   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
												   includes = null);

	Task<PagedResponse<TResult>> PaginateAsync<TResult>(OrderedPagedRequest request,
														CancellationToken cancellationToken,
														Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>
															? includes = null) where TResult : class;

	#endregion
}