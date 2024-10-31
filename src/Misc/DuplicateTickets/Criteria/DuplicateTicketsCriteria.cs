using Fanex.Data.Repository;

namespace DuplicateTickets.Criteria
{
    public class DuplicateTicketsCriteria : CriteriaBase
    {
        public DateTime Date { get; set; }

        public short CheckTime { get; set; }

        public override string GetSettingKey()
        {
            return "Misc_DuplicateTickets_GetReport";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
