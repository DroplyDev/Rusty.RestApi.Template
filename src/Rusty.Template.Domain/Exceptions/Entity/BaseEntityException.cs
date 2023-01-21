#region

using Serilog.Events;

#endregion

namespace Rusty.Template.Domain.Exceptions.Entity;

public abstract class BaseEntityException<TEntity> : ApiException where TEntity : class
{
	protected BaseEntityException(string message, int statusCode, LogEventLevel logEventLevel) : base(message,
		statusCode, logEventLevel)
	{
	}
}