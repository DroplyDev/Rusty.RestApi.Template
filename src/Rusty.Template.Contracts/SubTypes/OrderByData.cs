using FluentValidation;

namespace Rusty.Template.Contracts.SubTypes;

public sealed record OrderByData(string OrderBy = null!, OrderDirection OrderDirection = OrderDirection.Desc);

public class OrderByDataValidator : AbstractValidator<OrderByData>
{
    public OrderByDataValidator()
    {
        RuleFor(d => d.OrderBy).NotNull().NotEmpty();
    }
}