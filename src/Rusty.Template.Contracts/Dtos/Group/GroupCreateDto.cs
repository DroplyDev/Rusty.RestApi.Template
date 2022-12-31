using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.Group;

public sealed record GroupCreateDto(string Name);

public sealed class GroupCreateDtoValidator : BaseValidator<GroupCreateDto>
{
    public GroupCreateDtoValidator()
    {
        RuleFor(w => w.Name).MaximumLength(32);
    }
}