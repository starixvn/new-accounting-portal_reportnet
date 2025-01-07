using Fanex.Data.Repository;

namespace Utilities.MostVisitedPages;

public class UpdateMostVisitedPagePositionCriteria : CriteriaBase, INonQueryCommand
{
    public int CustId { set; get; }

    public int MenuId { get; set; }

    public int Position { get; set; }

    public override string GetSettingKey()
    {
        return "Home_MostVisitedPage_UpdateMostvisitedPagePosition";
    }

    public override bool IsValid()
    {
        return true;
    }
}