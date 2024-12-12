using Fanex.Data;
using Fanex.Data.Repository;

namespace CompanyWinLoss.Criteria
{
    public class CompanyWinLossCriteria : CriteriaBase
    {
        public int LicenseeId { get; set; }

        public int CurrencyId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate
        {
            get
            {
                var endOfMonth = new DateTime(
                    FromDate.Year,
                    FromDate.Month,
                    DateTime.DaysInMonth(FromDate.Year, FromDate.Month));
                if (endOfMonth > DateTime.Now)
                {
                    endOfMonth = DateTime.Now;
                }

                return endOfMonth;
            }
        }

        public string ProductPrefix { get; set; }

        public DateTime MoveBetsDay
        {
            get;
            set;
        }

        [SpParam(Ignored = true)]
        public bool IsPagingCallBack { get; set; }

        [SpParam(Ignored = true)]
        public int PageIndex { get; set; }

        [SpParam(Ignored = true)]
        public int PageSize { get; set; }

        public override string GetSettingKey() => "Acc_Rpt_CompanyWinloss";

        public override bool IsValid() => true;
    }
}
