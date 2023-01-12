#region

using Rusty.Template.Application.Repositories.BaseRepo;
using Rusty.Template.Domain;

#endregion

namespace Rusty.Template.Application.Repositories;

public interface IRoleRepo : IBaseRepo<Role>
{
	Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
}