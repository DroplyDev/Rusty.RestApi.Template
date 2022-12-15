namespace Rusty.Template.Contracts.Responses;

/// <summary>
///     The paged response
/// </summary>
public sealed record PagedResponse<TEntity>(List<TEntity> Data, int TotalCount);