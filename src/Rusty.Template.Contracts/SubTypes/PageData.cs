#region

using Swashbuckle.AspNetCore.Annotations;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>Page data subtype</summary>
public sealed class PageData
{
	/// <summary>Item offset.</summary>
	/// <example>0</example>
	public int Offset { get; set; }

	/// <summary>Item limit.</summary>
	/// <example>50</example>
	public int Limit { get; set; }
}