using FluentValidation;

namespace Rusty.Template.Contracts.SubTypes;

public sealed record FilterData(DateTime DateFrom, DateTime DateTo);

public sealed class FilterDataValidator : AbstractValidator<FilterData>
{
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