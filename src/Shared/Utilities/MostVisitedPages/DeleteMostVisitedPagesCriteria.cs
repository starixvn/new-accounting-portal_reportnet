using Fanex.Data.Repository;

namespace Utilities.MostVisitedPages;

public class DeleteMostVisitedPagesCriteria : CriteriaBase, INonQueryCommand
{
    public int CustId { get; set; }

    public int MenuId { get; set; }

    public override string GetSettingKey()
    {
        return "Home_MostVisitedPage_DeleteMostvisitedPage";
    }

    public override bool IsValid()
    {
        return true;
    }
}