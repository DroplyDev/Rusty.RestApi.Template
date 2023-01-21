#region

using Serilog.Events;

#endregion

namespace Rusty.Template.Domain.Exceptions.Entity;

public class EntityNotFoundByNameException<TEntity> : BaseEntityException<TEntity> where TEntity : class
{
	public EntityNotFoundByNameException(string name) : base($@"{typeof(TEntity).Name} with name {name} was not found",
		404, LogEventLevel.Warning)
	{
		Name = name;
	}


	public string Name { get; }
}