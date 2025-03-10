namespace ParlayWon.Services;

using Constant;
using Criteria;
using Entities;
using Fanex.Data.Repository;
using Model;
using Utilities.BalanceUpServices;
using Utilities.LinqDynamicSum;
using Utilities.Sorting;

public interface ParlayWonService
{
    Task<ParlayWonTransaction> GetReport(ParlayWonReportCriteria criteria);

    Task<ParlayWonTransaction> SortReport(ParlayWonReportCriteria criteria);
}

public class ParlayWonServiceImp(IDynamicRepository repository, BalanceUpService balanceUpService) : ParlayWonService
{
    public async Task<ParlayWonTransaction> GetReport(ParlayWonReportCriteria criteria)
    {
        var balanceUp = balanceUpService.Query();

        criteria.MoveBetsDay = balanceUp.MoveBetsDate;
        criteria.BalanceUpDay = balanceUp.BalanceUpDate;

        var startIndex = criteria.StartIndex;

        var fullReport = await GetFullReportFromDatabase(criteria);

        var pagingReport = new ParlayWonTransaction
        {
            Items = fullReport.Items.Skip(startIndex)
                .Take(criteria.ItemsPerPage)
                .ToList(),
            NumberItems = fullReport.NumberItems,
            Total = fullReport.Total
        };

        return pagingReport;
    }

    public async Task<ParlayWonTransaction> SortReport(ParlayWonReportCriteria criteria)
    {
        var report = await GetReport(criteria);

        if (report == null || !report.Items.Any())
        {
            return new ParlayWonTransaction();
        }

        var propertyName = GetSortedColumnName(criteria.SortBy);
        var startIndex = criteria.StartIndex;
        report.Items.SortByProperty(
            criteria.SortDirection,
            propertyName);


        var pagingReport = new ParlayWonTransaction
        {
            Items = report.Items.Skip(startIndex)
                .Take(criteria.ItemsPerPage)
                .ToList(),
            NumberItems = report.NumberItems,
            Total = report.Total
        };

        return pagingReport;

    }

    private async Task<ParlayWonTransaction> GetFullReportFromDatabase(ParlayWonReportCriteria criteria)
    {
        var rawData = await repository.FetchAsync<ParlayWonEntity>(criteria);
        var numberItems = rawData.Count();

        var fullReport = new ParlayWonTransaction
        {
            Items = [],
            NumberItems = numberItems,
            Total = new ParlayWonTransaction()
        };

        foreach (var row in rawData)
        {
            var transaction = new ParlayWonTransaction
            {
                RefNo = row.TransId,
                UserName = row.Username,
                Currency = row.Currency,
                Exchange = row.Exchange,
                OriginalWinLoss = row.WinLoss,
                BaseWinLoss = row.WinLossRM,
                CompanyWinLoss = row.CompanyWinLossRM,
                Selection = row.Selection,
                WinOdds = row.MOdds
            };

            switch (criteria.Bettype)
            {
                case BetTypeConstants.MixParlay:
                    transaction.OriginalStake = row.Stake;
                    transaction.BaseStake = row.StakeRM;
                    transaction.CompanyStake = row.CompanyStakeRM;
                    break;

                default:
                    transaction.OriginalStake = row.ActualStake;
                    transaction.BaseStake = row.ActualStakeRM;
                    transaction.CompanyStake = row.CompanyActualStakeRM;
                    break;
            }

            fullReport.Items.Add(transaction);
        }

        var total = fullReport.Items.DynamicSum();
        fullReport.Total = total;
            
        return fullReport;
    }

    private string GetSortedColumnName(string sortBy)
    {
        var columnName = sortBy switch
        {
            "BaseWinLoss" => "BaseWinLoss",
            "BaseCompanyWinLoss" => "CompanyWinLoss",
            _ => string.Empty
        };

        return columnName;
    }
}