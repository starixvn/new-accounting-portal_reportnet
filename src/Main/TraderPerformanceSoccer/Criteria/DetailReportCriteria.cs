using Fanex.Data;
using Fanex.Data.Repository;

namespace TraderPerformance.Criteria
{
    public class DetailReportCriteria : CriteriaBase
    {
        public DateTime Fromdate { get; set; }

        public DateTime Todate { get; set; }

        public bool IsSoccer { get; set; }

        [SpParam(Ignored = true)]
        public string SortDirection { get; set; }

        [SpParam(Ignored = true)]
        public string SortColumnName { get; set; }

        [SpParam(Ignored = true)]
        public int PageSize { get; set; }

        [SpParam(Ignored = true)]
        public int PageIndex { get; set; }

        [SpParam(Ignored = true)]
        public int StartIndex => (PageIndex - 1) * PageSize;

        [SpParam(Ignored = true)]
        public int EndIndex => PageIndex * PageSize - 1;

        [SpParam(Ignored = true)]
        public bool IsPagingCallBack { get; set; }

        public override string GetSettingKey()
        {
            return "Main_TraderPerformance_GetDetailReport";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
