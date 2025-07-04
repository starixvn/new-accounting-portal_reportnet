
namespace TGR.Model;

public class BetTypeInfo
{
    public short BetType { get; set; }

    public string TypeName { get; set; }

    public bool IsDefault { get; set; }

    public int GroupBettypeId { get; set; }

    public string GroupBettypeName { get; set; }
}