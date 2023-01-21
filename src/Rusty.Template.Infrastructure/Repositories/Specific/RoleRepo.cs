#region

using Rusty.Template.Application.Repositories;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;

#endregion

namespace Rusty.Template.Infrastructure.Repositories.Specific;

public class RoleRepo : AppDbRepo<Role>, IRoleRepo
{
	public RoleRepo(AppDbContext context) : base(context, item => item.Name)
	{
	}


	public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
	{
		return await FirstOrDefaultAsync(item => item.Name == name, cancellationToken);
	}
}