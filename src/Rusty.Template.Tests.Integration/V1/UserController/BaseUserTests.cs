namespace Rusty.Template.Tests.Integration.V1.UserController;

[TestCaseOrderer("Integration.Tests.PriorityOrderer", "CompanyName.ProjectName.Integration.Tests")]
[Collection("StandardIntegrationTests")]
[Trait("Category", "Get")]
public class BaseUserTests : BaseTests
{
    public BaseUserTests(WebApiFactory apiFactory) : base(apiFactory)
    {
    }
}