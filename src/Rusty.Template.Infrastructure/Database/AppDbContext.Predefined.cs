using Microsoft.EntityFrameworkCore;

namespace Rusty.Template.Infrastructure.Database;

/// <summary>
///     The app db context class
/// </summary>
public partial class AppDbContext
{
    /// <summary>
    ///     Jsons the value using the specified expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="path">The path</param>
    /// <exception cref="NotSupportedException"></exception>
    /// <returns>The string</returns>
    [DbFunction("JSON_VALUE", IsBuiltIn = true, IsNullable = false)]
    public static string JsonValue(string expression, string path)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    ///     Jsons the query using the specified expression
    /// </summary>
    /// <param name="expression">The expression</param>
    /// <param name="path">The path</param>
    /// <exception cref="NotSupportedException"></exception>
    /// <returns>The string</returns>
    [DbFunction("JSON_QUERY", IsBuiltIn = true, IsNullable = false)]
    public static string JsonQuery(string expression, string path)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    ///     Saves the changes
    /// </summary>
    /// <returns>The int</returns>
    public override int SaveChanges()
    {
        UpdateDefaultActionStatuses();
        return base.SaveChanges();
    }

    /// <summary>
    ///     Saves the changes using the specified accept all changes on success
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">The accept all changes on success</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A task containing the int</returns>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        UpdateDefaultActionStatuses();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    // ReSharper disable once CognitiveComplexity
    /// <summary>
    ///     Updates the default action statuses
    /// </summary>
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