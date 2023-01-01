using Microsoft.EntityFrameworkCore;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Contracts.SubTypes;

namespace Rusty.Template.Infrastructure.Repositories.Extensions;

/// <summary>
///     The pagination extensions class
/// </summary>
public static class PaginationExtensions
{
    /// <summary>
    ///     Paginates the with total count using the specified query
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    /// <param name="query">The query</param>
    /// <param name="skipItems">The skip items</param>
    /// <param name="takeItems">The take items</param>
    /// <returns>A queryable of t entity collection and int total count</returns>
    public static ( IQueryable<TEntity> Collection, int TotalCount) PaginateWithTotalCount<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (query.Paginate(skipItems, takeItems), query.Count());
    }

    /// <summary>
    ///     Paginates the with total count using the specified query
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    /// <param name="query">The query</param>
    /// <param name="skipItems">The skip items</param>
    /// <param name="takeItems">The take items</param>
    /// <returns>A task containing a queryable of t entity collection and int total count</returns>
    public static async Task<( IQueryable<TEntity> Collection, int TotalCount)> PaginateWithTotalCountAsync<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (query.Paginate(skipItems, takeItems), await query.CountAsync());
    }

    /// <summary>
    ///     Paginates the with total count as list using the specified query
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    /// <param name="query">The query</param>
    /// <param name="skipItems">The skip items</param>
    /// <param name="takeItems">The take items</param>
    /// <returns>A task containing a list of t entity collection and int total count</returns>
    public static async Task<(List<TEntity> Collection, int TotalCount)> PaginateWithTotalCountAsListAsync<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (await query.Paginate(skipItems, takeItems).ToListAsync(), await query.CountAsync());
    }

    /// <summary>
    ///     Paginates the with total count as list using the specified query
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    /// <param name="query">The query</param>
    /// <param name="pageData">The page data</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task containing a paged response of t result</returns>
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

    /// <summary>
    ///     Paginates the query
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    /// <param name="query">The query</param>
    /// <param name="skipItems">The skip items</param>
    /// <param name="takeItems">The take items</param>
    /// <returns>A queryable of t entity</returns>
    public static IQueryable<TEntity> Paginate<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return query.Skip(skipItems).Take(takeItems);
    }
}