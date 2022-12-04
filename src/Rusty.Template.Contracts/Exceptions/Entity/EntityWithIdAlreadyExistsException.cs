using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

public class EntityWithIdAlreadyExistsException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    public EntityWithIdAlreadyExistsException(int id) : base($"{typeof(TEntity).Name} with id: {id} already exists",
        409)
    {
        Id = id;
    }

    public int Id { get; }
}