using FluentValidation;

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>
///     The order by data
/// </summary>
public sealed record OrderByData(string OrderBy = null!, OrderDirection OrderDirection = OrderDirection.Desc);

/// <summary>
///     The order by data validator class
/// </summary>
/// <seealso cref="AbstractValidator{OrderByData}" />
public class OrderByDataValidator : AbstractValidator<OrderByData>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OrderByDataValidator" /> class
    /// </summary>
    public OrderByDataValidator()
    {
        RuleFor(d => d.OrderBy).NotNull().NotEmpty();
    }
}