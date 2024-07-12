namespace TreeWinLoss.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System;

    public class TreeWinLossByComCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public bool IsNormalMode { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int Currency { get; set; }

        [SpParam(Ignored = true)]
        public string ProductSelection { get; set; }

        public string Products { get; set; }

        public string SportType { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "WinLoss_TreeWinLoss_GetDataByCom";
        }
    }
}
