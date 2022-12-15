using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

/// <summary>
///     The entity validation exception class
/// </summary>
/// <seealso cref="BaseEntityException{TEntity}" />
public class EntityValidationException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Initializes a new instance of the
    ///     <see>
    ///         <cref>EntityValidationException</cref>
    ///     </see>
    ///     class
    /// </summary>
    public EntityValidationException() : base("Entity validation failed", 400)
    {
    }
}