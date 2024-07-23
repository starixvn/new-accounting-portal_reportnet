using Fanex.Data;
using Fanex.Data.Repository;

namespace TreeWinLoss.Criteria
{
    public class TreeWinLossBySuperCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public bool IsNormalMode { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        [SpParam(Ignored = true)]
        public string ProductSelection { get; set; }

        public string Products { get; set; }

        public int SRecommend { get; set; }

        public string SportType { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "WinLoss_TreeWinLoss_GetDataBySuper";
        }
    }
}
