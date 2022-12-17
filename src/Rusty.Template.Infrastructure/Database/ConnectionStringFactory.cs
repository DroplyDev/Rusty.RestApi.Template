namespace Rusty.Template.Infrastructure.Database;

public class ConnectionStringFactory
{
    public string ConnectionString { get; }

    public ConnectionStringFactory(string connectionString)
    {
        ConnectionString = connectionString;
    }
}