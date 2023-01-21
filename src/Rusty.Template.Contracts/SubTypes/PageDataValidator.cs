#region

using FluentValidation;

#endregion

namespace Rusty.Template.Contracts.SubTypes;

public sealed class PageDataValidator : AbstractValidator<PageData>
{
	public PageDataValidator()
	{
		RuleFor(d => d.Offset).GreaterThanOrEqualTo(0);
		RuleFor(d => d.Limit).GreaterThanOrEqualTo(0);
	}
}