using Fanex.Data;
using Fanex.Data.Repository;

namespace Utilities.MostVisitedPages
{
    public class SelectMostVisitedPageCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public static int MaxPages => 9;

        public string Environment { get; set; }

        public int UserId { get; set; }
       

        public int SiteId { get; set; }

        public override string GetSettingKey() => "Home_MostVisitedPage_GetMostvisitedPage";

        public override bool IsValid()
        {
            return true;
        }
    }
}
