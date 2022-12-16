using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rusty.Template.Infrastructure.Database;

/// <summary>
///     The app db context factory class
/// </summary>
/// <seealso cref="IDesignTimeDbContextFactory{AppDbContext}" />
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    /// <summary>
    ///     Creates the db context using the specified args
    /// </summary>
    /// <param name="args">The args</param>
    /// <returns>The app db context</returns>
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=EcommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new AppDbContext(optionsBuilder.Options);
    }
}