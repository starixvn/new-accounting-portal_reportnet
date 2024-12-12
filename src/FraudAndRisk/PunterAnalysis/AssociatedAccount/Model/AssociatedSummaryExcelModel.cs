using Epplus.Mapper.Annotations;

namespace PunterAnalysis.AssociatedAccount.Model
{
    public class AssociatedSummaryExcelModel
    {
        private const string MemberRole = "Member";

        private decimal winLoss;

        private decimal turnover;

        private decimal betCount;

        [Cell("A8")]
        public string Username { get; set; }

        [Cell("B8")]
        public string RoleName { get; set; }

        [Cell("C8")]
        public object WinLoss
        {
            get => IsMember() ? winLoss : string.Empty;
            set => winLoss = (decimal)value;
        }

        [Cell("D8")]
        public object Turnover
        {
            get => IsMember() ? turnover : string.Empty;
            set => turnover = (decimal)value;
        }

        [Cell("E8")]
        public object Margin
        {
            get
            {
                if (IsMember())
                {
                    return turnover == 0
                   ? "N/A" :
                   winLoss / turnover;
                }

                return string.Empty;
            }
        }

        [Cell("F8")]
        public object BetCount
        {
            get => IsMember() ? betCount : string.Empty;
            set => betCount = (int)value;
        }

        [Cell("G8")]
        public string Category { get; set; }

        private bool IsMember()
        {
            return RoleName == string.Empty || RoleName == MemberRole;
        }
    }
}