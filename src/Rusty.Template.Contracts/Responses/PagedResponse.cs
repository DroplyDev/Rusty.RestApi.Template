namespace Rusty.Template.Contracts.Responses;

public sealed record PagedResponse<TEntity>(List<TEntity> Data, int TotalCount);