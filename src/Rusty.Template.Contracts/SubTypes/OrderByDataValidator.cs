#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

public sealed class OrderByDataValidator : AbstractValidator<OrderByData>
{
	public OrderByDataValidator()
	{
		RuleFor(d => d.OrderBy).NotEmpty();
		RuleFor(d => d.OrderDirection).IsInEnum();
	}
}