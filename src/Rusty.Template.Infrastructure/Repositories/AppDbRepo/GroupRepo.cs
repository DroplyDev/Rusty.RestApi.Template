using Rusty.Template.Application.Repositories;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;

namespace Rusty.Template.Infrastructure.Repositories.AppDbRepo;

/// <summary>
///     The group repo class
/// </summary>
/// <seealso cref="AppDbRepo{Group}" />
/// <seealso cref="IGroupRepo" />
public class GroupRepo : AppDbRepo<Group>, IGroupRepo
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="GroupRepo" /> class
    /// </summary>
    /// <param name="context">The context</param>
    public GroupRepo(AppDbContext context) : base(context, item => item.Name)
    {
    }

    /// <summary>
    ///     Gets the by name using the specified name
    /// </summary>
    /// <param name="name">The name</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task containing the group</returns>
    public async Task<Group?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await FirstOrDefaultAsync(item => item.Name == name, cancellationToken);
    }
}