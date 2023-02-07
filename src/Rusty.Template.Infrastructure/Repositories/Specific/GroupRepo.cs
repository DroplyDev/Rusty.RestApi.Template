#region

using Rusty.Template.Application.Repositories;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;

#endregion

namespace Rusty.Template.Infrastructure.Repositories.Specific;

public sealed class GroupRepo : AppDbRepo<Group>, IGroupRepo
{
	public GroupRepo(AppDbContext context) : base(context)
	{
	}


	public async Task<Group?> GetByNameAsync(string name,  CancellationToken cancellationToken = default)
	{
		return await FirstOrDefaultAsync(item => item.Name == name, cancellationToken);
	}
}