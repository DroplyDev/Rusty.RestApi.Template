using Rusty.Template.Application.Repositories.BaseRepo;
using Rusty.Template.Domain;

namespace Rusty.Template.Application.Repositories;

/// <summary>
///     The role repo interface
/// </summary>
/// <seealso cref="IBaseRepo{Role}" />
public interface IRoleRepo : IBaseRepo<Role>
{
    /// <summary>
    ///     Gets the by name using the specified name
    /// </summary>
    /// <param name="name">The name</param>
    /// <returns>A task containing the role</returns>
    Task<Role?> GetByNameAsync(string name);
}