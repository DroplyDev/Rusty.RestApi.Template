using FluentValidation;

namespace Rusty.Template.Contracts.Dtos;

/// <summary>
///     The base validator class
/// </summary>
/// <seealso cref="AbstractValidator{T}" />
public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
{
    /// <summary>
    ///     Initializes a new instance of the
    ///     <see>
    ///         <cref>BaseValidator</cref>
    ///     </see>
    ///     class
    /// </summary>
    protected BaseValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Continue;
    }
}