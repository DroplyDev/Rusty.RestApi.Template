using FluentValidation;

namespace Rusty.Template.Contracts.Dtos.Group;

/// <summary>
///     The group create dto
/// </summary>
public sealed class GroupCreateDto
{
    /// <summary>
    ///     Gets or sets the value of the name
    /// </summary>
    public string Name { get; set; } = null!;
}

/// <summary>
///     The group create dto validator class
/// </summary>
/// <seealso cref="BaseValidator{GroupCreateDto}" />
public sealed class GroupCreateDtoValidator : BaseValidator<GroupCreateDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="GroupCreateDtoValidator" /> class
    /// </summary>
    public GroupCreateDtoValidator()
    {
        RuleFor(w => w.Name)
            .NotNull()
            .MaximumLength(32);
    }
}