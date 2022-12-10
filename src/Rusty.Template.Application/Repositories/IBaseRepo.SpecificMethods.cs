using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Domain;

namespace Rusty.Template.Application.Repositories;

public partial interface IBaseRepo<TEntity> where TEntity : BaseEntity
{
    Task<PagedResponse<TEntity>> PaginateAsync(OrderByPagedRequest request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    Task<PagedResponse<TResult>> PaginateAsync<TResult>(OrderByPagedRequest request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);
}