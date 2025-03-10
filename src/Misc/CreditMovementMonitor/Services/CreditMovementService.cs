namespace CreditMovementMonitor.Services;

using Criteria;
using Entities;
using Fanex.Caching;
using Fanex.Data.Repository;
using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using Fanex.NAP.Client;
using Model;
using Utilities.DateTimes;

public interface CreditMovementService
{
    Task<GridOptions> GetReport(CreditMovementCriteria criteria);

    Task<GridOptions> GetFullReportFromCache(CreditMovementCriteria criteria);

    Task<IEnumerable<SiteGroup>> GetSites();
}

public class CreditMovementServiceImp(
    IDynamicRepository repository,
    INapSession napSession,
    ICacheService cacheService) : CreditMovementService
{
    private readonly string creditMovementDataCachedKey = $"Fanex.AccountingReports.Misc.CreditMovement{napSession.UserName}"; 

    public async Task<GridOptions> GetReport(CreditMovementCriteria criteria)
    {
        GridOptions gridOptions;
        var pagingGridOptions = InitCreditMovementGridOptions(criteria);

        if (criteria.IsPagingCallBack)
        {
            gridOptions = await GetFullReportFromCache(criteria);
            pagingGridOptions.TotalItems = gridOptions.BodyRows.Count;
            GetPagingReport(gridOptions, ref pagingGridOptions, criteria);
            return pagingGridOptions;
        }

        gridOptions = await GetReportData(criteria);
        pagingGridOptions.TotalItems = gridOptions.BodyRows.Count;
        GetPagingReport(gridOptions, ref pagingGridOptions, criteria);
        return pagingGridOptions;
    }

    public async Task<GridOptions> GetFullReportFromCache(CreditMovementCriteria criteria)
    {
        var gridOptions = await cacheService.GetAsync<GridOptions>(creditMovementDataCachedKey);

        if (gridOptions != null)
        {
            return gridOptions;
        }

        criteria.IsPagingCallBack = false;
        gridOptions = await GetReportData(criteria);

        return gridOptions;
    }

    public string GetReportName(CreditMovementCriteria criteria)
    {
        var fromDate = criteria.FromDate.ToShortFormatedDateWithSlash().Replace("/", "_");
        var toDate = criteria.ToDate.ToShortFormatedDateWithSlash().Replace("/", "_");

        return $"Credit_Sites_Bet_Counts_Report_From_{fromDate}_To_{toDate}";
            
    }

    public async Task<IEnumerable<SiteGroup>> GetSites()
    {
        var criteria = new SiteCriteria();
        var sites = await repository.FetchAsync<Site>(criteria);

        var siteGroups = GetSiteGroup(sites);
        return siteGroups;
    }

    public IEnumerable<SiteGroup> GetSiteGroup(IEnumerable<Site> list)
    {
        var result = new List<SiteGroup>();
        var groups = list.GroupBy(x => x.SiteGroup).Select(x => x.Key);

        foreach (var group in groups)
        {
            var siteGroup = new SiteGroup
            {
                GroupName = group,
                Sites = []
            };

            var sites = list.Where(x => x.SiteGroup == group).ToList();
            siteGroup.Sites = sites;

            result.Add(siteGroup);
        }

        return result;
    }

    private async Task<GridOptions> GetReportData(CreditMovementCriteria criteria)
    {
        var gridOptions = InitCreditMovementGridOptions(criteria);

        var rawData = await repository.FetchAsync<CreditMovementEntity>(criteria);

        gridOptions.BodyRows = MapReportDataRows(rawData);

        await cacheService.SetAsync(creditMovementDataCachedKey, gridOptions);

        return gridOptions;
    }

    private GridOptions InitCreditMovementGridOptions(CreditMovementCriteria criteria)
    {
        return new GridOptions
        {
            GroupHeaderRows = GroupHeaderRows(),
            CustomizeHeader = true,
            Columns = HeaderColumns(),
            ShowFooter = false,
            ShowDistinctRow = false,
            ShowHighlightRow = false,
            ShowTitle = true,
            TitleType = TitleType.TwoLineBlue,
            TitleInfoText = $"Accounts which have been allocated with more than {criteria.Amount:0,0} RM credit for SMA + Member Line"
        };
    }
        
    private List<GroupHeaderRows> GroupHeaderRows()
    {
        var groupHeader = new List<GroupHeaderRows>();
        var groupHeaderRow = new GroupHeaderRows();
        groupHeaderRow.Columns.AddRange(new List<HeaderColumns>
        {
            new() { HeaderText = "#", Name = "No", MergeRowNumber = 2, Width = 50 },
            new() { HeaderText = "Date", Name = "Date", MergeRowNumber = 2, Width = 100 },
            new() { HeaderText = "Currency", Name = "Currency", MergeRowNumber = 2, Width = 100 },
            new() { HeaderText = "Site", Name = "Site", MergeRowNumber = 2, Width = 100 },
            new() { HeaderText = "Super/Master/Agent/Member", Name = "Super/Master/Agent/Member", MergeRowNumber = 2, Width = 300 },
            new() { HeaderText = "Credit Movement", Name = "Original Currency", MergeColNumber = 2 }
        });
        groupHeader.Add(groupHeaderRow);
        return groupHeader;
    }
        
    private List<HeaderColumns> HeaderColumns()
    {
        var header = new List<HeaderColumns>();
        header.AddRange(new List<HeaderColumns>
        {
            new() { HeaderText = "#", Name = "No", Width = 50, CustomCellType = ExcelCellType.Integer, CellAlignment = HAlignment.Center },
            new() { HeaderText = "Date", Name = "Date", Width = 100, CellAlignment = HAlignment.Center },
            new() { HeaderText = "Currency", Name = "Currency", Width = 100, CellAlignment = HAlignment.Center },
            new() { HeaderText = "Site", Name = "Site", Width = 100, CellAlignment = HAlignment.Center },
            new() { HeaderText = "Super/Master/Agent/Member", Name = "Super/Master/Agent/Member", Width = 300, CellAlignment = HAlignment.Center },
            new() { HeaderText = "Original Currency", Name = "Original Currency", Width = 150, CustomCellType = ExcelCellType.Double, CellAlignment = HAlignment.Right },
            new() { HeaderText = "Base Currency", Name = "Base Currency", Width = 150, CustomCellType = ExcelCellType.Double, CellAlignment = HAlignment.Right }
        });
        return header;
    }

    private List<GridDataRow> MapReportDataRows(IEnumerable<CreditMovementEntity> rawData)
    {
        var dataRowIndex = 0;
        var dataColIndex = 0;
        List<GridDataRow> dataRows = [];
        foreach (var item in rawData)
        {
            var downLineDetail = string.Empty;
            var superName = item.SuperName;
            downLineDetail = string.IsNullOrEmpty(superName) ? downLineDetail : superName;
            var masterName = item.MasterName;
            downLineDetail = string.IsNullOrEmpty(masterName) ? downLineDetail : $"{downLineDetail} / {masterName}";
            var agentName = item.AgentName;
            downLineDetail = string.IsNullOrEmpty(agentName) ? downLineDetail : $"{downLineDetail} / {agentName}";
            var memberName = item.CustomerName;
            downLineDetail = string.IsNullOrEmpty(memberName) ? downLineDetail : $"{downLineDetail} / {memberName}";
            var dataRow = new GridDataRow();
            dataRow.Columns.AddRange(new List<Columns>
            {
                new(dataColIndex++) { Name = "No", Value = dataRowIndex + 1 },
                new(dataColIndex++) { Name = "Date", Value = item.TransDate.ToShortFormatedDate() },
                new(dataColIndex++) { Name = "Currency", Value = item.Currency },
                new(dataColIndex++) { Name = "Site", Value = item.Site },
                new(dataColIndex++) { Name = "Super/Master/Agent/Member", Value = downLineDetail },
                new(dataColIndex++) { Name = "Original Currency", Value = item.Amount },
                new(dataColIndex++) { Name = "Base Currency", Value = item.BaseAmount }
            });

            dataRows.Add(dataRow);
            dataRowIndex++;
        }

        return dataRows;
    }

    private void GetPagingReport(GridOptions gridOptions, ref GridOptions pagingGridOptions, CreditMovementCriteria criteria)
    {
        var pageSize = criteria.PageSize;
        var pageIndex = criteria.PageIndex;
        pagingGridOptions.BodyRows = gridOptions.BodyRows.Skip(pageIndex * pageSize).Take(pageSize).ToList();
    }
}