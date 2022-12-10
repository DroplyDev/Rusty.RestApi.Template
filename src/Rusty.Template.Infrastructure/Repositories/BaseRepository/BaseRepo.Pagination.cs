using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Repositories.Extensions;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepository;

public partial class BaseRepo<TEntity> where TEntity : BaseEntity
{
    public virtual ( IQueryable<TEntity> Collection, int TotalCount) Paginate(int skipItems, int takeItems,
        string orderBy, OrderDirection orderDirection,
        Expression<Func<TEntity, bool>>? expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        var query = IncludeIfNotNull(includes);

        query = query.WhereNullable(expression);

        query = query.OrderByWithDirection(orderBy, orderDirection);

        return query.PaginateWithTotalCount(skipItems, takeItems);
    }

    public async Task<PagedResponse<TEntity>> PaginateAsync(OrderByPagedRequest request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        var query = IncludeIfNotNull(includes);
        query = OrderByOrPredefined(query, request.OrderByData);
        return await query.PaginateWithTotalCountAsListAsync(request.PageData);
    }


    public async Task<PagedResponse<TResult>> PaginateAsync<TResult>(OrderByPagedRequest request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        var query = IncludeIfNotNull(includes);
        query = OrderByOrPredefined(query, request.OrderByData);
        return await query.PaginateWithTotalCountAsListAsync<TEntity, TResult>(request.PageData);
    }
}