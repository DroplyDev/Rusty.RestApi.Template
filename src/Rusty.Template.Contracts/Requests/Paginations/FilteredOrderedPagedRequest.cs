#region

using FluentValidation;
using Rusty.Template.Contracts.SubTypes;

#endregion

namespace Rusty.Template.Contracts.Requests.Paginations;

public sealed record FilteredOrderedPagedRequest
{
	public FilterData? FilterData { get; set; }


	public PageData? PageData { get; set; }


	public OrderByData? OrderByData { get; set; }
}

public sealed class FilteredOrderedPagedRequestValidator : AbstractValidator<FilteredOrderedPagedRequest>
{
	public FilteredOrderedPagedRequestValidator()
	{
		RuleFor(w => w.FilterData).SetValidator(new FilterDataValidator()!).When(item => item.FilterData is not null);
		RuleFor(w => w.PageData).SetValidator(new PageDataValidator()!).When(item => item.PageData is not null);
		RuleFor(w => w.OrderByData).SetValidator(new OrderByDataValidator()!)
								   .When(item => item.OrderByData is not null);
	}
}