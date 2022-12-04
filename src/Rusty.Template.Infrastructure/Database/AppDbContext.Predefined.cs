using Microsoft.EntityFrameworkCore;

namespace Rusty.Template.Infrastructure.Database;

public partial class AppDbContext
{
    [DbFunction("JSON_VALUE", IsBuiltIn = true, IsNullable = false)]
    public static string JsonValue(string expression, string path)
    {
        throw new NotSupportedException();
    }

    [DbFunction("JSON_QUERY", IsBuiltIn = true, IsNullable = false)]
    public static string JsonQuery(string expression, string path)
    {
        throw new NotSupportedException();
    }
}