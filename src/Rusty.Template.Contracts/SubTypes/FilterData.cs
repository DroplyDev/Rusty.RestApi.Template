namespace Rusty.Template.Contracts.SubTypes;

public record FilterData
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}