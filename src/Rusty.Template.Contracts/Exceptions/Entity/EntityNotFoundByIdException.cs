using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

/// <summary>
///     The entity not found by id exception class
/// </summary>
/// <seealso cref="BaseEntityException{TEntity}" />
public class EntityNotFoundByIdException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Initializes a new instance of the
    ///     <see>
    ///         <cref>EntityNotFoundByIdException</cref>
    ///     </see>
    ///     class
    /// </summary>
    /// <param name="identifier">The identifier</param>
    public EntityNotFoundByIdException(int identifier) : base(
        $"{typeof(TEntity).Name} with id {identifier} was not found", 404)
    {
        Identifier = identifier;
    }

    /// <summary>
    ///     Gets the value of the identifier
    /// </summary>
    public int Identifier { get; }
}