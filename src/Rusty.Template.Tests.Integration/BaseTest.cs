namespace Rusty.Template.Tests.Integration;

public abstract class BaseTest : IClassFixture<WebApiFactory>
{
    protected readonly NSwagClient Client;

    protected BaseTest(WebApiFactory apiFactory)
    {
        Client = new NSwagClient(apiFactory.CreateClient());
    }
}