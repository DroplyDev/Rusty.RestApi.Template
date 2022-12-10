using Mapster;
using Microsoft.EntityFrameworkCore;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Contracts.SubTypes;

namespace Rusty.Template.Infrastructure.Repositories.Extensions;

public static class PaginationExtensions
{
    public static ( IQueryable<TEntity> Collection, int TotalCount) PaginateWithTotalCount<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (query.Paginate(skipItems, takeItems), query.Count());
    }

    public static (IQueryable<TResult> Collection, int TotalCount) PaginateWithTotalCount<TEntity, TResult>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (query.Paginate(skipItems, takeItems).ProjectToType<TResult>(), query.Count());
    }

    public static async Task<( IQueryable<TEntity> Collection, int TotalCount)> PaginateWithTotalCountAsync<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (query.Paginate(skipItems, takeItems), await query.CountAsync());
    }

    public static async Task<(IQueryable<TResult> Collection, int TotalCount)> PaginateWithTotalCountAsync<TEntity,
        TResult>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (query.Paginate(skipItems, takeItems).ProjectToType<TResult>(), await query.CountAsync());
    }

    public static async Task<(List<TEntity> Collection, int TotalCount)> PaginateWithTotalCountAsListAsync<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (await query.Paginate(skipItems, takeItems).ToListAsync(), await query.CountAsync());
    }

    public static async Task<PagedResponse<TEntity>> PaginateWithTotalCountAsListAsync<TEntity>(
        this IQueryable<TEntity> query, PageData? pageData)
    {
        if (pageData is null)
            return new PagedResponse<TEntity>(await query.ToListAsync(), await query.CountAsync());
        return new PagedResponse<TEntity>(await query.Paginate(pageData.Offset, pageData.Limit).ToListAsync(),
            await query.CountAsync());
    }

    public static async Task<(List<TResult> Collection, int TotalCount)> PaginateWithTotalCountAsListAsync<TEntity,
        TResult>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (await query.Paginate(skipItems, takeItems).ProjectToType<TResult>().ToListAsync(),
            await query.CountAsync());
    }

    public static async Task<PagedResponse<TResult>> PaginateWithTotalCountAsListAsync<TEntity, TResult>(
        this IQueryable<TEntity> query, PageData? pageData)
    {
        if (pageData is null)
            return new PagedResponse<TResult>(await query.ProjectToType<TResult>().ToListAsync(),
                await query.CountAsync());
        return new PagedResponse<TResult>(
            await query.Paginate(pageData.Offset, pageData.Limit).ProjectToType<TResult>().ToListAsync(),
            await query.CountAsync());
    }

    public static IQueryable<TEntity> Paginate<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return query.Skip(skipItems).Take(takeItems);
    }
}