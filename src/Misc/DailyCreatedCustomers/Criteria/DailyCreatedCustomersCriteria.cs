using Fanex.Data.Repository;

namespace DailyCreatedCustomers.Criteria
{
    public class DailyCreatedCustomersCriteria : CriteriaBase
    {
        public DateTime Fromdate { get; set; }

        public DateTime Todate { get; set; }

        public override string GetSettingKey()
        {
            return "Misc_DailyCreatedCustomers_GetReport";
        }

        public override bool IsValid()
        {
            return this.Fromdate <= this.Todate;
        }
    }
}
