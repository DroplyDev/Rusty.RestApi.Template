namespace Rusty.Template.Tests.Integration;

public abstract class BaseTest
{
    protected readonly NSwagClient Client;

    protected BaseTest()
    {
        var appFactory = new WebApiFactory();
        Client = new NSwagClient(appFactory.CreateClient());
    }
}