using FluentValidation;
using Rusty.Template.Contracts.SubTypes;

// ReSharper disable All

namespace Rusty.Template.Contracts.Requests;

public sealed record OrderByPagedRequest
{
    public PageData? PageData { get; set; }
    public OrderByData? OrderByData { get; set; }
}

public sealed class OrderByPagedRequestValidator : AbstractValidator<OrderByPagedRequest>
{
    public OrderByPagedRequestValidator()
    {
        RuleFor(w => w.PageData).SetValidator(new PageDataValidator()!).When(item => item.PageData is not null);
        RuleFor(w => w.OrderByData).SetValidator(new OrderByDataValidator()!)
            .When(item => item.OrderByData is not null);
    }
}