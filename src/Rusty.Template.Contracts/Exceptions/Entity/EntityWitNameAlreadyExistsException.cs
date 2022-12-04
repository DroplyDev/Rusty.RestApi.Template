using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

public class EntityWitNameAlreadyExistsException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    public EntityWitNameAlreadyExistsException(string name) : base(
        $"{typeof(TEntity).Name} with name: {name} already exists", 409)
    {
        Name = name;
    }

    public string Name { get; }
}