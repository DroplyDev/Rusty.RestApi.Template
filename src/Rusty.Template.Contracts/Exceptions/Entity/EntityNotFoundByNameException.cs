using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

/// <summary>
///     The entity not found by name exception class
/// </summary>
/// <seealso cref="BaseEntityException{TEntity}" />
public class EntityNotFoundByNameException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Initializes a new instance of the
    ///     <see>
    ///         <cref>EntityNotFoundByNameException</cref>
    ///     </see>
    ///     class
    /// </summary>
    /// <param name="name">The name</param>
    public EntityNotFoundByNameException(string name) : base($"{typeof(TEntity).Name} with name {name} was not found",
        404)
    {
        Name = name;
    }

    /// <summary>
    ///     Gets the value of the name
    /// </summary>
    public string Name { get; }
}