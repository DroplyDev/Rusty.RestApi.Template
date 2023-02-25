#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>
/// FilterDataValidator
/// </summary>
public sealed class FilterDataValidator : AbstractValidator<FilterData>
{
	/// <summary>Initializes a new instance of the <see cref="FilterDataValidator"/> class.</summary>
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