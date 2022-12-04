using System.ComponentModel.DataAnnotations;

namespace Rusty.Template.Contracts.SubTypes;

public record PageData
{
    [Range(0, int.MaxValue, ErrorMessage = "Offset should be non negative")]
    public int Offset { get; set; } = 0;

    [Range(0, int.MaxValue, ErrorMessage = "Limit should be non negative")]
    public int Limit { get; set; }
}