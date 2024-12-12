using TraderPerformance.Criteria;
using TraderPerformance.Model;
using TraderPerformance.Repositories;
using Utilities.Sorting;

namespace TraderPerformance.Services
{
    public interface TraderPerformanceOtherSportsService
    {
        Task<TraderPerformanceOtherSportsModel> GetReport(ReportCriteria criteria);

        Task<TraderPerformanceOtherSportsModel> SortReportData(ReportCriteria criteria);

        Task<TraderPerformanceOtherSportsDetailModel> GetDetailReport(DetailReportCriteria criteria);

        Task<TraderPerformanceOtherSportsDetailModel> SortDetailReportData(DetailReportCriteria criteria);
    }

    public class TraderPerformanceOtherSportsServiceImp(
        TraderPerformanceOtherSportsRepository repository) : 
        TraderPerformanceOtherSportsService
    {
        public async Task<TraderPerformanceOtherSportsModel> GetReport(ReportCriteria criteria)
        {
            var result = new TraderPerformanceOtherSportsModel();

            result = await repository.GetTraderPerformanceOtherSportsReport(criteria);

            return result;
        }

        public async Task<TraderPerformanceOtherSportsModel> SortReportData(ReportCriteria criteria)
        {
            var report = await GetReport(criteria);

            SortReportData(report, criteria);

            return report;
        }

        
        public async Task<TraderPerformanceOtherSportsDetailModel> GetDetailReport(DetailReportCriteria criteria)
        {
            var result = new TraderPerformanceOtherSportsDetailModel
            {
                GrandTotal = new TraderPerformanceOtherSportsDetailModel()
            };

            var startIndex = criteria.StartIndex;


            var reportDetailData = await repository.GetTraderPerformanceOtherSportsDetailReport(criteria);

            var traderGroups = reportDetailData.ReportRows.GroupBy(data => new
            {
                data.Id,
                data.Name
            });

            foreach (var traderGroup in traderGroups)
            {
                var winLossTotal = new TraderPerformanceOtherSportsDetailModel();
                var turnOverTotal = new TraderPerformanceOtherSportsDetailModel();
                var liveBetCountTotal = new TraderPerformanceOtherSportsDetailModel();
                var marginTotal = new TraderPerformanceOtherSportsDetailModel();
                var commissionTotal = new TraderPerformanceOtherSportsDetailModel();

                var traderGroupInfo = new TraderPerformanceOtherSportsDetailModel
                {
                    Id = traderGroup.Key.Id,
                    Name = traderGroup.Key.Name,
                    GrandTotal = new TraderPerformanceOtherSportsDetailModel()
                };

                foreach (var data in traderGroup)
                {
                    var traderInfo = new TraderPerformanceOtherSportsDetailModel
                    {
                        EventDate = data.EventDate,
                        LeagueName = data.LeagueName,
                        HomeName = data.HomeName,
                        AwayName = data.AwayName,
                        HTHomeScore = data.HTHomeScore,
                        HTAwayScore = data.HTAwayScore,
                        FTHomeScore = data.FTHomeScore,
                        FTAwayScore = data.FTAwayScore,
                        Maxbet = data.Maxbet,
                        ChildInfo = data.ChildInfo
                    };

                    winLossTotal.CalculateTotal(traderInfo.ChildInfo[0]);
                    turnOverTotal.CalculateTotal(traderInfo.ChildInfo[1]);
                    liveBetCountTotal.CalculateTotal(traderInfo.ChildInfo[2]);
                    commissionTotal.CalculateTotal(traderInfo.ChildInfo[4]);

                    traderInfo.ChildInfo[0].FormatTwoDecimalNumberData(traderInfo.ChildInfo[0]);
                    traderInfo.ChildInfo[1].FormatTwoDecimalNumberData(traderInfo.ChildInfo[1]);
                    traderInfo.ChildInfo[3].FormatTwoDecimalNumberData(traderInfo.ChildInfo[3]);
                    traderInfo.ChildInfo[4].FormatTwoDecimalNumberData(traderInfo.ChildInfo[4]);

                    traderGroupInfo.ReportRows.Add(traderInfo);
                }

                marginTotal.CalculateMarginTotal(winLossTotal, turnOverTotal);

                winLossTotal.FormatTwoDecimalNumberData(winLossTotal);
                turnOverTotal.FormatTwoDecimalNumberData(turnOverTotal);
                marginTotal.FormatTwoDecimalNumberData(marginTotal);
                commissionTotal.FormatTwoDecimalNumberData(commissionTotal);

                traderGroupInfo.GrandTotal.ChildInfo.Add(winLossTotal);
                traderGroupInfo.GrandTotal.ChildInfo.Add(turnOverTotal);
                traderGroupInfo.GrandTotal.ChildInfo.Add(liveBetCountTotal);
                traderGroupInfo.GrandTotal.ChildInfo.Add(marginTotal);
                traderGroupInfo.GrandTotal.ChildInfo.Add(commissionTotal);

                result.ReportRows.Add(traderGroupInfo);
            }

            result.GrandTotal = reportDetailData.GrandTotal;
            result.ReportLength = result.ReportRows.Count;



            SortSecondSubLevelReport(result, criteria);

            var reportPagingData = new TraderPerformanceOtherSportsDetailModel
            {
                ReportRows = criteria.PageIndex > 0 ? result.ReportRows
                    .Skip(startIndex)
                    .Take(criteria.PageSize)
                    .ToList() : result.ReportRows.ToList(),
                GrandTotal = result.GrandTotal,
                ReportLength = result.ReportLength
            };

            return reportPagingData;
        }

        public async Task<TraderPerformanceOtherSportsDetailModel> SortDetailReportData(DetailReportCriteria criteria)
        {
            var report = await GetDetailReport(criteria);

            var pagingReport = new TraderPerformanceOtherSportsDetailModel
            {
                ReportRows = report.ReportRows
                .Skip(criteria.StartIndex)
                .Take(criteria.PageSize)
                .ToList(),
                GrandTotal = report.GrandTotal,
                ReportLength = report.ReportLength
            };

            SortDetailReportData(pagingReport, criteria);

            return pagingReport;
        }
        
        private TraderPerformanceOtherSportsModel SortReportData(TraderPerformanceOtherSportsModel report, ReportCriteria criteria)
        {
            var isAsc = string.Equals(criteria.SortDirection, SortDirectionConstants.Asc, StringComparison.OrdinalIgnoreCase);

            report.ReportRows = criteria.SortColumnName switch
            {
                "Trader" => isAsc
                    ? report.ReportRows.OrderBy(traderInfo => traderInfo.Name).ToList()
                    : report.ReportRows.OrderByDescending(traderInfo => traderInfo.Name).ToList(),
                "WinLossOverall" => isAsc
                    ? report.ReportRows.OrderBy(traderInfo => traderInfo.ChildInfo[0].Overall).ToList()
                    : report.ReportRows.OrderByDescending(traderInfo => traderInfo.ChildInfo[0].Overall).ToList(),
                "TurnOverOverall" => isAsc
                    ? report.ReportRows.OrderBy(traderInfo => traderInfo.ChildInfo[1].Overall).ToList()
                    : report.ReportRows.OrderByDescending(traderInfo => traderInfo.ChildInfo[1].Overall).ToList(),
                "LiveBetCountOverall" => isAsc
                    ? report.ReportRows.OrderBy(traderInfo => traderInfo.ChildInfo[2].Overall).ToList()
                    : report.ReportRows.OrderByDescending(traderInfo => traderInfo.ChildInfo[2].Overall).ToList(),
                "MarginOverall" => isAsc
                    ? report.ReportRows.OrderBy(traderInfo => traderInfo.ChildInfo[3].Overall).ToList()
                    : report.ReportRows.OrderByDescending(traderInfo => traderInfo.ChildInfo[3].Overall).ToList(),
                "CommOverall" => isAsc
                    ? report.ReportRows.OrderBy(traderInfo => traderInfo.ChildInfo[4].Overall).ToList()
                    : report.ReportRows.OrderByDescending(traderInfo => traderInfo.ChildInfo[4].Overall).ToList(),
                _ => report.ReportRows
            };

            return report;
        }

        private void SortDetailReportData(TraderPerformanceOtherSportsDetailModel report,
            DetailReportCriteria criteria)
        {
            SortSecondSubLevelReport(report, criteria);
        }

        
        private void SortSecondSubLevelReport(TraderPerformanceOtherSportsDetailModel report,
            DetailReportCriteria criteria)
        {
            var isAsc = string.Equals(criteria.SortDirection, SortDirectionConstants.Asc, StringComparison.OrdinalIgnoreCase);

            foreach (var traderInfo in report.ReportRows)
            {
                traderInfo.ReportRows = criteria.SortColumnName switch
                {
                    "WinLossOverall" => isAsc
                        ? traderInfo.ReportRows
                            .OrderBy(traderChildGroupInfo => traderChildGroupInfo.ChildInfo[0].Overall)
                            .ToList()
                        : traderInfo.ReportRows
                            .OrderByDescending(traderChildGroupInfo => traderChildGroupInfo.ChildInfo[0].Overall)
                            .ToList(),
                    "TurnOverOverall" => isAsc
                        ? traderInfo.ReportRows
                            .OrderBy(traderChildGroupInfo => traderChildGroupInfo.ChildInfo[1].Overall)
                            .ToList()
                        : traderInfo.ReportRows
                            .OrderByDescending(traderChildGroupInfo => traderChildGroupInfo.ChildInfo[1].Overall)
                            .ToList(),
                    "LiveBetCountOverall" => isAsc
                        ? traderInfo.ReportRows
                            .OrderBy(traderChildGroupInfo => traderChildGroupInfo.ChildInfo[2].Overall)
                            .ToList()
                        : traderInfo.ReportRows
                            .OrderByDescending(traderChildGroupInfo => traderChildGroupInfo.ChildInfo[2].Overall)
                            .ToList(),
                    "MarginOverall" => isAsc
                        ? traderInfo.ReportRows
                            .OrderBy(traderChildGroupInfo => traderChildGroupInfo.ChildInfo[3].Overall)
                            .ToList()
                        : traderInfo.ReportRows
                            .OrderByDescending(traderChildGroupInfo => traderChildGroupInfo.ChildInfo[3].Overall)
                            .ToList(),
                    "CommOverall" => isAsc
                        ? traderInfo.ReportRows
                            .OrderBy(traderChildGroupInfo => traderChildGroupInfo.ChildInfo[4].Overall)
                            .ToList()
                        : traderInfo.ReportRows
                            .OrderByDescending(traderChildGroupInfo => traderChildGroupInfo.ChildInfo[4].Overall)
                            .ToList(),
                    _ => traderInfo.ReportRows
                };
            }
        }
    }
}
