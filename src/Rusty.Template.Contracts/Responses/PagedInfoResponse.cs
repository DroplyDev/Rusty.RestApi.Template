namespace Rusty.Template.Contracts.Responses;

public record PagedInfoResponse<TEntity>
{
    public IEnumerable<TEntity> Data { get; set; } = Enumerable.Empty<TEntity>();
    public int TotalCount { get; set; }
}