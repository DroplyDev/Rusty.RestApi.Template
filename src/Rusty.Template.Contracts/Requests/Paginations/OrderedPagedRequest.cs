#region

using FluentValidation;
using Rusty.Template.Contracts.SubTypes;

#endregion

// ReSharper disable All

namespace Rusty.Template.Contracts.Requests;

public sealed record OrderedPagedRequest
{
	public PageData? PageData { get; set; }


	public OrderByData OrderByData { get; set; } = null!;
}

public sealed class OrderByPagedRequestValidator : AbstractValidator<OrderedPagedRequest>
{
	public OrderByPagedRequestValidator()
	{
		RuleFor(w => w.PageData).SetValidator(new PageDataValidator()!).When(item => item.PageData is not null);
		RuleFor(w => w.OrderByData).SetValidator(new OrderByDataValidator()!);
	}
}