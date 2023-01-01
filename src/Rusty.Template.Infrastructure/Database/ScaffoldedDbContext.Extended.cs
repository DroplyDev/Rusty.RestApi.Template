using Microsoft.EntityFrameworkCore;

namespace Rusty.Template.Infrastructure.Database;

public abstract partial class ScaffoldedDbContext
{
    public ScaffoldedDbContext(DbContextOptions options)
        : base(options)
    {
    }
}