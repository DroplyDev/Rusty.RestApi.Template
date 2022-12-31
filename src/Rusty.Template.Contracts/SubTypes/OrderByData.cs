using FluentValidation;
using Rusty.Template.Contracts.Dtos;

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>
///     The order by data
/// </summary>
public sealed record OrderByData(string OrderBy, OrderDirection OrderDirection = OrderDirection.Desc);

/// <summary>
///     The order by data validator class
/// </summary>
/// <seealso cref="BaseValidator{T}" />
public class OrderByDataValidator : BaseValidator<OrderByData>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OrderByDataValidator" /> class
    /// </summary>
    public OrderByDataValidator()
    {
        RuleFor(d => d.OrderBy).NotEmpty();
        RuleFor(d => d.OrderDirection).IsInEnum();

    }
}