using Xunit.Abstractions;
using Xunit.Sdk;

namespace Rusty.Template.Tests.Integration;

public class PriorityOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        var sortedMethods = new Dictionary<int, TTestCase>();

        foreach (var testCase in testCases)
        {
            var attributeInfo = testCase.TestMethod.Method
                .GetCustomAttributes(typeof(TestPriorityAttribute).AssemblyQualifiedName)
                .SingleOrDefault();
            if (attributeInfo != null)
            {
                var priority = attributeInfo.GetNamedArgument<int>("Priority");
                sortedMethods.Add(priority, testCase);
            }
        }

        return sortedMethods.OrderBy(x => x.Key).Select(x => x.Value);
    }
}