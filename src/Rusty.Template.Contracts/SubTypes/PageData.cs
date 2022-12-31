using FluentValidation;
using Rusty.Template.Contracts.Dtos;

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>
///     The page data
/// </summary>
public sealed record PageData(int Offset = 0, int Limit = 0);

/// <summary>
///     The page data validator class
/// </summary>
/// <seealso cref="BaseValidator{T}" />
public sealed class PageDataValidator : BaseValidator<PageData>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PageDataValidator" /> class
    /// </summary>
    public PageDataValidator()
    {
        RuleFor(d => d.Offset).GreaterThanOrEqualTo(0);
        RuleFor(d => d.Limit).GreaterThanOrEqualTo(0);
    }
}