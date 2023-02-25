#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>
/// PageDataValidator
/// </summary>
public sealed class PageDataValidator : AbstractValidator<PageData>
{
	/// <summary>Initializes a new instance of the <see cref="PageDataValidator"/> class.</summary>
	public PageDataValidator()
	{
		RuleFor(d => d.Offset).GreaterThanOrEqualTo(0);
		RuleFor(d => d.Limit).GreaterThanOrEqualTo(0);
	}
}