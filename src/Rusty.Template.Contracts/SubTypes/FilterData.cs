#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>
/// Filter data subtype
/// </summary>
public sealed class FilterData
{
	/// <summary>Start date filter.</summary>
	/// <example>01-01-1900</example>
	public DateTime DateFrom { get; set; }

	/// <summary>End date filter.</summary>
	/// <example>01-01-2000</example>
	public DateTime DateTo { get; set; }
}