namespace Rusty.Template.Contracts.Responses;

public record PagedResponse<TEntity>(List<TEntity> Data, int TotalCount);