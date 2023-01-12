#region

using Rusty.Template.Application.Repositories.BaseRepo;
using Rusty.Template.Domain;

#endregion

namespace Rusty.Template.Application.Repositories;

public interface IUserRepo : IBaseRepo<User>
{
	Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}