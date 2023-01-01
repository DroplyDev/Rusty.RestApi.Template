using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.Group;

/// <summary>
///     The group update dto
/// </summary>
public sealed record GroupUpdateDto(string Name);

/// <summary>
///     The group update dto validator class
/// </summary>
/// <seealso cref="BaseValidator{GroupUpdateDto}" />
public sealed class GroupUpdateDtoValidator : BaseValidator<GroupUpdateDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="GroupUpdateDtoValidator" /> class
    /// </summary>
    public GroupUpdateDtoValidator()
    {
        RuleFor(item => item.Name).MaximumLength(32);
    }
}