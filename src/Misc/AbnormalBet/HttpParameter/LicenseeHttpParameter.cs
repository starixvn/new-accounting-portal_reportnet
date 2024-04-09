using AbnormalBet.Model;
using MediatR;

namespace AbnormalBet.HttpParameter
{
    public class LicenseeHttpParameter : IRequest<LicenseeModel>
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string CurrencyId { get; set; }

        public string Reason { get; set; }

        public string Sport { get; set; }

        public string BetType { get; set; }

        public string Licensee { get; set; }

        public bool IsTest { get; set; }

        public string LeagueId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}