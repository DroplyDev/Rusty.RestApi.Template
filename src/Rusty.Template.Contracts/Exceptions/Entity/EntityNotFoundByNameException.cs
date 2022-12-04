using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

public class EntityNotFoundByNameException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    public EntityNotFoundByNameException(string name) : base($"{typeof(TEntity).Name} with name {name} was not found",
        404)
    {
        Name = name;
    }

    public string Name { get; }
}