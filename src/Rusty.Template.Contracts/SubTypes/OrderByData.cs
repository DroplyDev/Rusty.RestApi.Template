#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

public sealed class OrderByData
{
	public string OrderBy { get; set; } = null!;
	public OrderDirection OrderDirection { get; set; }
}

public class OrderByDataValidator : AbstractValidator<OrderByData>
{
	public OrderByDataValidator()
	{
		RuleFor(d => d.OrderBy).NotEmpty();
		RuleFor(d => d.OrderDirection).IsInEnum();
	}
}