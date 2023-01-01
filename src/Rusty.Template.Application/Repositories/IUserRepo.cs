using Rusty.Template.Application.Repositories.BaseRepo;
using Rusty.Template.Domain;

namespace Rusty.Template.Application.Repositories;

/// <summary>
///     The user repo interface
/// </summary>
/// <seealso cref="IBaseRepo{User}" />
public interface IUserRepo : IBaseRepo<User>
{
    /// <summary>
    ///     Gets the by username using the specified username
    /// </summary>
    /// <param name="username">The username</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task containing the user</returns>
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}