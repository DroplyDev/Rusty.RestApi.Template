#region

using FluentValidation;
using Rusty.Template.Contracts.SubTypes;

#endregion

namespace Rusty.Template.Contracts.Requests.Pagination;

/// <summary>
/// FilteredOrderedPagedRequestValidator
/// </summary>
public sealed class FilteredOrderedPagedRequestValidator : AbstractValidator<FilterOrderPageRequest>
{
	/// <summary>Initializes a new instance of the <see cref="FilteredOrderedPagedRequestValidator"/> class.</summary>
	public FilteredOrderedPagedRequestValidator()
	{
		RuleFor(w => w.FilterData)
			.SetValidator(new FilterDataValidator()!)
			.When(item => item.FilterData is not null);
		RuleFor(w => w.PageData)
			.SetValidator(new PageDataValidator()!)
			.When(item => item.PageData is not null);
		RuleFor(w => w.OrderByData)
			.SetValidator(new OrderByDataValidator());
	}
}