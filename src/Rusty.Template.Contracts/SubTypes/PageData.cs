using FluentValidation;

namespace Rusty.Template.Contracts.SubTypes;

public sealed record PageData(int Offset = 0, int Limit = 0);

public sealed class PageDataValidator : AbstractValidator<PageData>
{
    public PageDataValidator()
    {
        RuleFor(d => d.Offset).GreaterThanOrEqualTo(0);
        RuleFor(d => d.Limit).GreaterThanOrEqualTo(0);
    }
}