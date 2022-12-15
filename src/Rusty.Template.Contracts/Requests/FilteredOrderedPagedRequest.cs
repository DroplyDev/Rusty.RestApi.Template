using FluentValidation;
using Rusty.Template.Contracts.SubTypes;

namespace Rusty.Template.Contracts.Requests;

/// <summary>
///     The filtered ordered paged request
/// </summary>
public sealed record FilteredOrderedPagedRequest
{
    /// <summary>
    ///     Gets or sets the value of the filter data
    /// </summary>
    public FilterData? FilterData { get; set; }

    /// <summary>
    ///     Gets or sets the value of the page data
    /// </summary>
    public PageData? PageData { get; set; }

    /// <summary>
    ///     Gets or sets the value of the order by data
    /// </summary>
    public OrderByData? OrderByData { get; set; }
}

/// <summary>
///     The filtered ordered paged request validator class
/// </summary>
/// <seealso cref="AbstractValidator{FilteredOrderedPagedRequest}" />
public sealed class FilteredOrderedPagedRequestValidator : AbstractValidator<FilteredOrderedPagedRequest>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FilteredOrderedPagedRequestValidator" /> class
    /// </summary>
    public FilteredOrderedPagedRequestValidator()
    {
        RuleFor(w => w.FilterData).SetValidator(new FilterDataValidator()!).When(item => item.FilterData is not null);
        RuleFor(w => w.PageData).SetValidator(new PageDataValidator()!).When(item => item.PageData is not null);
        RuleFor(w => w.OrderByData).SetValidator(new OrderByDataValidator()!)
            .When(item => item.OrderByData is not null);
    }
}