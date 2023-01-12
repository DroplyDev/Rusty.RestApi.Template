#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

public sealed record FilterData(DateTime DateFrom, DateTime DateTo);

public sealed class FilterDataValidator : AbstractValidator<FilterData>
{
	public FilterDataValidator()
	{
		RuleFor(d => d.DateFrom)
			.LessThanOrEqualTo(DateTime.Now)
			.LessThanOrEqualTo(d => d.DateTo.Date)
			.GreaterThanOrEqualTo(DateTime.MinValue);
		RuleFor(d => d.DateTo.Date)
			.GreaterThanOrEqualTo(DateTime.MinValue);
	}
}