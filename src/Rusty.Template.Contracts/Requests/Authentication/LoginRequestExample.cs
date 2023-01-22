#region

using Swashbuckle.AspNetCore.Filters;

#endregion

namespace Rusty.Template.Contracts.Requests.Authentication;

public class LoginRequestExample : IExamplesProvider<LoginRequest>
{
	public LoginRequest GetExamples()
	{
		return new LoginRequest
		{
			Username = "John",
			Password = "Aa@123456"
		};
	}
}