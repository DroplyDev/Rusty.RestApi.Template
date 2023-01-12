#region

using Rusty.Template.Application.Repositories.BaseRepo;
using Rusty.Template.Domain;

#endregion

namespace Rusty.Template.Application.Repositories;

public interface IGroupRepo : IBaseRepo<Group>
{
	Task<Group?> GetByNameAsync(string name, CancellationToken cancellationToken);
}