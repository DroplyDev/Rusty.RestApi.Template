using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

/// <summary>
///     The entity wit name already exists exception class
/// </summary>
/// <seealso cref="BaseEntityException{TEntity}" />
public class EntityWitNameAlreadyExistsException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Initializes a new instance of the
    ///     <see>
    ///         <cref>EntityWitNameAlreadyExistsException</cref>
    ///     </see>
    ///     class
    /// </summary>
    /// <param name="name">The name</param>
    public EntityWitNameAlreadyExistsException(string name) : base(
        $"{typeof(TEntity).Name} with name: {name} already exists", 409)
    {
        Name = name;
    }

    /// <summary>
    ///     Gets the value of the name
    /// </summary>
    public string Name { get; }
}