#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace Rusty.Template.Infrastructure.Database;

public class AppDbContext : ScaffoldedDbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyGlobalFilters("IsDeleted", false);
		modelBuilder.ApplyGlobalFilters<DateTime?>("DeleteDate", null);
		base.OnModelCreating(modelBuilder);
	}


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


	public override int SaveChanges()
	{
		UpdateDefaultActionStatuses();
		return base.SaveChanges();
	}


	public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
											   CancellationToken cancellationToken = default)
	{
		UpdateDefaultActionStatuses();
		return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}

	// ReSharper disable once CognitiveComplexity


	private void UpdateDefaultActionStatuses()
	{
		foreach (var entry in ChangeTracker.Entries())
		{
			var entryPropertyNames = entry.CurrentValues.Properties.Select(item => item.Name).ToList();
			switch (entry.State)
			{
				case EntityState.Added:
					if (entryPropertyNames.Contains("IsDeleted"))
						entry.CurrentValues["IsDeleted"] = false;
					if (entryPropertyNames.Contains("CreateDate"))
						entry.CurrentValues["CreateDate"] = DateTime.Now;
					break;
				case EntityState.Modified:
					if (entryPropertyNames.Contains("UpdateDate"))
						entry.CurrentValues["UpdateDate"] = DateTime.Now;
					break;
				case EntityState.Deleted:
					if (entryPropertyNames.Contains("IsDeleted"))
					{
						entry.State = EntityState.Modified;
						entry.CurrentValues["IsDeleted"] = true;
					}
					else if (entryPropertyNames.Contains("DeleteDate"))
					{
						entry.State = EntityState.Modified;
						entry.CurrentValues["DeleteDate"] = DateTime.Now;
					}

					break;
			}
		}
	}
}