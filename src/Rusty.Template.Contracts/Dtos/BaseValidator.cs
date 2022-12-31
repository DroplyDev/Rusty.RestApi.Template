using FluentValidation;

namespace Rusty.Template.Contracts.Dtos;

public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
{
    protected BaseValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Continue;
    }
}