using MediatR;

namespace SMAWinLoss.Summary
{
    public class WinLossHttpParameter : IRequest<WinLossModel>
    {
        public int Year { get; set; }

        public int CustLevel { get; set; }

        public string Username { get; set; }

        public bool IsDownline { get; set; }

        public string Currencies { get; set; }

        public bool IsAppliedVR { get; set; }

        public string Sports { get; set; }

        public string LeagueIds { get; set; }

        public string Divisions { get; set; }

        public string AccountStatus { get; set; }

        public string VRSettings { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        
        public string LiveStatus { get; set; }

        public string BetFromIds { get; set; }

        public int PermissionProfileId { get; set; }
    }
}