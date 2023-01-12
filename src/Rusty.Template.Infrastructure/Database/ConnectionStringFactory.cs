#region

using Rusty.Template.Domain.Exceptions;

#endregion

namespace Rusty.Template.Infrastructure.Database;

public class ConnectionStringFactory
{
	public ConnectionStringFactory(string? connectionString)
	{
		ConnectionString = connectionString ?? throw new ConnectionStringIsNullException();
	}


	public string ConnectionString { get; }
}