namespace Rusty.Template.Tests.Integration;

[AttributeUsage(AttributeTargets.Method)]
public class TestPriorityAttribute : Attribute
{
	public TestPriorityAttribute(int priority)
	{
		Priority = priority;
	}

	public int Priority { get; }
}