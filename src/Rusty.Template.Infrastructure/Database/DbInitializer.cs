#region

using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Domain;

#endregion

namespace Rusty.Template.Infrastructure.Database;

public static class DbInitializer
{
	public static async Task InitializeDatabaseDataAsync(this IServiceProvider services)
	{
		await using var scope = services.CreateAsyncScope();
		var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepo>();
		const int userCount = 1000;
		int uUser = 1, uGroup = 0, Uinfo = 0;
		if (await userRepo.IsEmptyAsync(CancellationToken.None))
			for (var i = 0; i < 100; i++)
			{
				var users = new List<User>();
				for (var j = 1; j <= userCount; j++)
				{
					users.Add(new User
					{
						UserName = "TestUser" + uUser,
						Email = "TestEmail" + uUser + "@gmail.com",
						Password = "Ff@12345678"
					});
					uUser++;
				}

				for (var j = 0; j < userCount - 2;)
				{
					var group = new Group { Name = "Group" + uGroup++ };
					for (var k = 0; k < j + 1 && j < userCount - 2; k++)
						users[j++].Group = group;
				}

				for (var j = 0; j < userCount; j++)
					if (j % 2 == 0)
						users[j].UserInfo = new UserInfo
						{
							FirstName = "Firstname" + Uinfo++,
							LastName = "LastName" + Uinfo++
						};
				await userRepo.CreateRangeAsync(users);
				Console.WriteLine("1000 generated");
			}
	}


	public static async Task MigrateDatabaseAsync(this IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		await context.Database.MigrateAsync();
	}


	public static async Task CreateDatabaseFromContextIfNotExistsAsync(this IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		await context.Database.EnsureCreatedAsync();
	}

	private static string LoremIpsum(int minWords, int maxWords,
									 int minSentences, int maxSentences,
									 int numParagraphs)
	{
		var words = new[]
		{
			"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
			"adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
			"tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
		};

		var rand = new Random();
		var numSentences = rand.Next(maxSentences - minSentences)
						   + minSentences + 1;
		var numWords = rand.Next(maxWords - minWords) + minWords + 1;

		var result = new StringBuilder();

		for (var p = 0; p < numParagraphs; p++)
			for (var s = 0; s < numSentences; s++)
			{
				for (var w = 0; w < numWords; w++)
				{
					if (w > 0)
						result.Append(" ");
					result.Append(words[rand.Next(words.Length)]);
				}

				result.Append(". ");
			}

		return result.ToString();
	}
}