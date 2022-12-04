using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Domain;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepository;

public partial class BaseRepo<TEntity> where TEntity : BaseEntity
{
    public virtual IQueryable<TEntity> Paginate(int skipItems, int takeItems,
        Expression<Func<TEntity, bool>>? expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        var query = IncludeIfNotNull(includes);
        if (expression is not null)
            query = query.Where(expression);
        return query.SkipWithValidation(skipItems)
            .TakeWithValidation(takeItems);
    }

    public virtual IQueryable<TEntity> Paginate(int skipItems, int takeItems,
        Expression<Func<TEntity, bool>>? expression,
        string orderBy, OrderDirection orderDirection,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return Paginate(skipItems, takeItems, expression, includes).OrderByWithDirection(orderBy, orderDirection);
    }

    public virtual IQueryable<TEntity> Paginate(PagedInfoRequest request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        var query = IncludeIfNotNull(includes);
        if (request.OrderByData is not null)
            query = query.OrderByWithDirection(request.OrderByData.OrderBy, request.OrderByData.OrderDirection);
        if (request.PageData is not null)
            query = query.SkipWithValidation(request.PageData.Offset).TakeWithValidation(request.PageData.Limit);
        return query;
    }
}