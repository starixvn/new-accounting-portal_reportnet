using Fanex.Data;
using Fanex.Data.Repository;

namespace ParlayWon.Criteria
{
    public class ParlayWonReportCriteria : CriteriaBase
    {
        public DateTime WinlostDate { get; set; }

        public int Bettype { get; set; }

        [SpParam(Ignored = true)]
        public string BetTypeName { get; set; }

        public bool IsSoccer { get; set; }

        [SpParam(Ignored = true)]
        public string SportName { get; set; }

        public DateTime MoveBetsDay { get; set; }   

        public DateTime BalanceUpDay { get; set; }

        [SpParam(Ignored = true)]
        public string SortBy { get; set; }

        [SpParam(Ignored = true)]
        public string SortDirection { get; set; }


        [SpParam(Ignored = true)]
        public bool IsGetAll { get; set; }

        [SpParam(Ignored = true)]
        public int ItemsPerPage { get; set; }

        [SpParam(Ignored = true)]
        public int TotalItem { get; set; }

        [SpParam(Ignored = true)]
        public int CurrentPage { get; set; }

        [SpParam(Ignored = true)]
        public int StartIndex => CurrentPage <= 0 ? 1 : (CurrentPage - 1) * ItemsPerPage;

        [SpParam(Ignored = true)]
        public int EndIndex => StartIndex + ItemsPerPage - 1;

        [SpParam(Ignored = true)]
        public bool IsPagingCallBack { get; set; }

        public override string GetSettingKey()
        {
            return "ParlayWon_GetReport";
        }
        
        public override bool IsValid()
        {
            return true;
        }
    }
}
