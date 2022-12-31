using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.Group;

public sealed record GroupUpdateDto(string Name);

public sealed class GroupUpdateDtoValidator : BaseValidator<GroupUpdateDto>
{
    public GroupUpdateDtoValidator()
    {
        RuleFor(item => item.Name).MaximumLength(32);
    }
}