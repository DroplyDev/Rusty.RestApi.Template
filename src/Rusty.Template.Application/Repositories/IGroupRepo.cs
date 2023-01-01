using Rusty.Template.Application.Repositories.BaseRepo;
using Rusty.Template.Domain;

namespace Rusty.Template.Application.Repositories;

/// <summary>
///     The group repo interface
/// </summary>
/// <seealso cref="IBaseRepo{Group}" />
public interface IGroupRepo : IBaseRepo<Group>
{
    /// <summary>
    ///     Gets the by name using the specified name
    /// </summary>
    /// <param name="name">The name</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task containing the group</returns>
    Task<Group?> GetByNameAsync(string name, CancellationToken cancellationToken);
}