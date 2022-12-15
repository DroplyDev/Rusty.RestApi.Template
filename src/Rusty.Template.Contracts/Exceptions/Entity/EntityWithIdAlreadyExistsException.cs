using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

/// <summary>
///     The entity with id already exists exception class
/// </summary>
/// <seealso cref="BaseEntityException{TEntity}" />
public class EntityWithIdAlreadyExistsException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Initializes a new instance of the
    ///     <see>
    ///         <cref>EntityWithIdAlreadyExistsException</cref>
    ///     </see>
    ///     class
    /// </summary>
    /// <param name="id">The id</param>
    public EntityWithIdAlreadyExistsException(int id) : base($"{typeof(TEntity).Name} with id: {id} already exists",
        409)
    {
        Id = id;
    }

    /// <summary>
    ///     Gets the value of the id
    /// </summary>
    public int Id { get; }
}