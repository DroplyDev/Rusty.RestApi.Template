#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.Responses;

/// <summary>
/// Paged payload response
/// </summary>
public record PagedResponse<TEntity>
{
	/// <summary>Gets the data.</summary>
	public IEnumerable<TEntity> Data { get; init; } = null!;
	/// <summary>Gets the total count.</summary>
	/// <example>0</example>
	public int TotalCount { get; init; }

	/// <summary>Initializes a new instance of the <see cref="PagedResponse{TEntity}"/> class.</summary>
	/// <param name="data">The data.</param>
	/// <param name="totalCount">The total count.</param>
	public PagedResponse(IEnumerable<TEntity> data, int totalCount)
	{
		Data = data;
		TotalCount = totalCount;
	}
};