namespace Rusty.Template.Contracts.SubTypes;

public record OrderByData
{
    public string OrderBy { get; set; } = null!;
    public OrderDirection OrderDirection { get; set; }
}