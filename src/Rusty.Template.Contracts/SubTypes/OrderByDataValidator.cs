#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>
/// OrderByDataValidator
/// </summary>
public sealed class OrderByDataValidator : AbstractValidator<OrderByData>
{
	/// <summary>Initializes a new instance of the <see cref="OrderByDataValidator"/> class.</summary>
	public OrderByDataValidator()
	{
		RuleFor(d => d.OrderBy).NotEmpty();
		RuleFor(d => d.OrderDirection).IsInEnum();
	}
}