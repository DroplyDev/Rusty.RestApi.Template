#region

using Serilog.Events;

#endregion

namespace Rusty.Template.Domain.Exceptions.Entity;

public class EntityWitNameAlreadyExistsException<TEntity> : BaseEntityException<TEntity> where TEntity : class
{
	public EntityWitNameAlreadyExistsException(string name) : base(
		$@"{typeof(TEntity).Name} with name: {name} already exists", 409, LogEventLevel.Warning)
	{
		Name = name;
	}


	public string Name { get; }
}