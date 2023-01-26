#region

using AutoBogus;

#endregion

namespace Rusty.Template.Tests.Integration;

public static class MoqDataGenerator
{
	public static IEnumerable<UserCreateDto> UserCreateDto(int count = 1)
	{
		return new AutoFaker<UserCreateDto>()
			.RuleFor(o => o.Email, f => f.Person.Email)
			.RuleFor(o => o.UserName, f => f.Person.UserName)
			.RuleFor(o => o.Password, f => f.PickRandom<string>()).Generate(count);
	}
}