#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace Rusty.Template.Infrastructure.Database;

public abstract partial class ScaffoldedDbContext
{
	protected ScaffoldedDbContext(DbContextOptions options)
		: base(options)
	{
	}
}