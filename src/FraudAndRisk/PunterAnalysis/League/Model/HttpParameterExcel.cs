using MediatR;
using OfficeOpenXml;

namespace PunterAnalysis.League.Model
{
    public class HttpParameterExcel : IRequest<ExcelPackage>
    {
        public int PeriodType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int CustId { get; set; }

        public string ListCombinationId { get; set; }

        public string LeagueId { get; set; }

        public int SportId { get; set; }

        public int BetTypeId { get; set; }

        public int LiveStatusId { get; set; }

        public string Title { get; set; }

        public string Filter { get; set; }

        public string ListLeagueGroupId { get; set; }

        public int LeagueGroupId { get; set; }
    }
}