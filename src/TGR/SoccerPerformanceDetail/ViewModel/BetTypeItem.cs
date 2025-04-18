namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class BetTypeItem
{
    public BetTypeItem(BetTypeInfo betType)
    {
        if (betType == null)
        {
            return;
        }

        BetTypeId = betType.BetType;
        BetTypeName = betType.TypeName;
        IsDefaultCheck = betType.IsDefault;
    }

    public short BetTypeId { get; set; }

    public string BetTypeName { get; set; }

    public bool IsDefaultCheck { get; set; }
}