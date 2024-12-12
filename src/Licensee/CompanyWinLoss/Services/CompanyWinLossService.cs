using CompanyWinLoss.Criteria;
using CompanyWinLoss.Model;
using Fanex.Caching;
using Fanex.Data.Repository;
using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using Fanex.NAP.Client;
using Utilities.DateTimes;
using Utilities.BalanceUpServices;

namespace CompanyWinLoss.Services
{
    public interface CompanyWinLossService
    {
        Task<List<Currency>> GetCurrenciesByLicensee(CurrenciesByLicenseeCriteria criteria);

        Task<List<Product>> GetProductListByLicense(ProductByLicenseCriteria criteria);

        Task<GridOptions> GetCompanyWinLoss(CompanyWinLossCriteria criteria);

        Task<GridOptions> GetFullReportFromCache(CompanyWinLossCriteria criteria);

        Task<List<Licensee>> GetLicensee();
    }

    public class CompanyWinLossServiceImp(
        IDynamicRepository repository,
        ICacheService cacheService,
        INapSession napSession,
        BalanceUpService balanceUpService) : CompanyWinLossService
    {
        private readonly string dataCachedKey = $"Fanex.AccountingReports.Licensee.CompanyWinLoss.{napSession.UserName}";

        public async Task<List<Currency>> GetCurrenciesByLicensee(CurrenciesByLicenseeCriteria criteria)
        {
            var result = await repository.FetchAsync<Currency>(criteria);

            return result.ToList();
        }

        public async Task<List<Product>> GetProductListByLicense(ProductByLicenseCriteria criteria)
        {
            var result = await repository.FetchAsync<Product>(criteria);

            return result.ToList();
        }

        public async Task<GridOptions> GetCompanyWinLoss(CompanyWinLossCriteria criteria)
        {
            GridOptions gridOptions = null;
            var pagingGridOptions = InitGridOptions(criteria);

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

        public async Task<List<Licensee>> GetLicensee()
        {
            var criteria = new LicenseeCriteria();

            var result = await repository.FetchAsync<Licensee>(criteria);

            return result.ToList();
        }

        private void GetPagingReport(GridOptions gridOptions, ref GridOptions pagingGridOptions, CompanyWinLossCriteria criteria)
        {
            var pageSize = criteria.PageSize;
            var pageIndex = criteria.PageIndex;
            pagingGridOptions.BodyRows = gridOptions.BodyRows.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        private async Task<GridOptions> GetReportData(CompanyWinLossCriteria criteria)
        {
            var balanceUp = balanceUpService.Query();

            criteria.MoveBetsDay = balanceUp.MoveBetsDate;
            var gridOptions = InitGridOptions(criteria);

            var rawData = await repository.FetchAsync<CompanyWinLossItem>(criteria);

            gridOptions.BodyRows = MapReportDataRows(rawData);

            await cacheService.SetAsync(dataCachedKey, gridOptions);

            return gridOptions;
        }

        public async Task<GridOptions> GetFullReportFromCache(CompanyWinLossCriteria criteria)
        {
            GridOptions gridOptions = null;
            gridOptions = await cacheService.GetAsync<GridOptions>(dataCachedKey);

            if (gridOptions == null)
            {
                criteria.IsPagingCallBack = false;
                gridOptions =  await GetCompanyWinLoss(criteria);
            }

            return gridOptions;
        }

        private GridOptions InitGridOptions(CompanyWinLossCriteria criteria)
        {
            return new GridOptions
            {
                CustomizeHeader = true,
                Columns = CreateHeaderColumns(),
                ShowFooter = false,
                ShowDistinctRow = false,
                ShowHighlightRow = false,
                ShowTitle = true,
                TitleType = TitleType.TwoLineBlue,
                TitleInfoText = $"Company Win Loss from {criteria.FromDate.ToShortFormatedDate()} to {criteria.ToDate.ToShortFormatedDate()}"
            };
        }

        private List<HeaderColumns> CreateHeaderColumns()
        {
            var header = new List<HeaderColumns>();
            header.AddRange(new List<HeaderColumns>
            {
                new() { HeaderText = "#", Name = "RowId", Width = 50, CustomCellType = ExcelCellType.Integer, CellAlignment = HAlignment.Center },
                new() { HeaderText = "Super", Name = "Username", Width = 250, CellAlignment = HAlignment.Left },
                new() { HeaderText = "Company Win Loss", Name = "CompanyWL", Width = 250, CustomCellType = ExcelCellType.DoubleRedNegative, CellAlignment = HAlignment.Right }
            });

            return header;
        }

        private List<GridDataRow> MapReportDataRows(IEnumerable<CompanyWinLossItem> rawData)
        {
            var dataColIndex = 0;
            List<GridDataRow> dataRows = [];
            foreach (var item in rawData)
            {
                var dataRow = new GridDataRow();
                dataRow.Columns.AddRange(new List<Columns>
                {
                    new(dataColIndex++) { Name = "RowId", Value = item.RowId },
                    new(dataColIndex++) { Name = "Username", Value = item.UserName },
                    new(dataColIndex++) { Name = "CompanyWL", Value = item.CompanyWinLoss }
                });

                dataRows.Add(dataRow);
            }

            return dataRows;
        }

        public string GetReportName(CompanyWinLossCriteria criteria)
        {
            var reportName = $"CompanyWinLoss_From_{criteria.FromDate.ToShortFormatedDateWithSlash().Replace("/", "_")}_To_{criteria.ToDate.ToShortFormatedDateWithSlash().Replace("/", "_")}";

            return reportName;
        }
    }
}
