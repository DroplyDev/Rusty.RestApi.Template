using FluentValidation;

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>
///     The filter data
/// </summary>
public sealed record FilterData(DateTime DateFrom, DateTime DateTo);

/// <summary>
///     The filter data validator class
/// </summary>
/// <seealso cref="AbstractValidator{FilterData}" />
public sealed class FilterDataValidator : AbstractValidator<FilterData>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FilterDataValidator" /> class
    /// </summary>
    public FilterDataValidator()
    {
        RuleFor(d => d.DateFrom.Date)
            .LessThanOrEqualTo(DateTime.Now.Date)
            .LessThanOrEqualTo(d => d.DateTo.Date)
            .GreaterThanOrEqualTo(DateTime.Now.Date.AddYears(-10));
        RuleFor(d => d.DateTo.Date)
            .GreaterThanOrEqualTo(DateTime.Now.Date.AddYears(-10));
    }
}