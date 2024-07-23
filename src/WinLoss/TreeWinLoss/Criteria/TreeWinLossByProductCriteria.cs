using Fanex.Data;
using Fanex.Data.Repository;

namespace TreeWinLoss.Criteria
{
    public class TreeWinLossByProductCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public bool IsNormalMode { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        [SpParam(Ignored = true)]
        public string ProductSelection { get; set; }

        public string Products { get; set; }

        public decimal Turnover_8RM { get; set; }

        public decimal Total_8RM { get; set; }

        public decimal VirtualTotal_8RM { get; set; }

        public decimal Turnover_BARM { get; set; }

        public decimal Total_BARM { get; set; }

        public decimal VirtualTotal_BARM { get; set; }

        public string SportType { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "WinLoss_TreeWinLoss_GetDataByProduct";
        }
    }
}
