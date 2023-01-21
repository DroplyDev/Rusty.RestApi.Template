#region

using FluentValidation;
using Rusty.Template.Contracts.SubTypes;

#endregion

namespace Rusty.Template.Contracts.Requests.Pagination;

public sealed class OrderByPagedRequestValidator : AbstractValidator<OrderedPagedRequest>
{
	public OrderByPagedRequestValidator()
	{
		RuleFor(w => w.PageData)
			.SetValidator(new PageDataValidator()!)
			.When(item => item.PageData is not null);
		RuleFor(w => w.OrderByData)
			.SetValidator(new OrderByDataValidator());
	}
}