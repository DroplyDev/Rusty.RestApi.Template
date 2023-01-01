using Rusty.Template.Application.Repositories;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;

namespace Rusty.Template.Infrastructure.Repositories.AppDbRepo;

/// <summary>
///     The role repo class
/// </summary>
/// <seealso cref="AppDbRepo{Role}" />
/// <seealso cref="IRoleRepo" />
public class RoleRepo : AppDbRepo<Role>, IRoleRepo
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RoleRepo" /> class
    /// </summary>
    /// <param name="context">The context</param>
    public RoleRepo(AppDbContext context) : base(context, item => item.Name)
    {
    }

    /// <summary>
    ///     Gets the by name using the specified name
    /// </summary>
    /// <param name="name">The name</param>
    /// <returns>A task containing the role</returns>
    public async Task<Role?> GetByNameAsync(string name)
    {
        return await FirstOrDefaultAsync(item => item.Name == name);
    }
}