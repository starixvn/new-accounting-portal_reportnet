//using CompanyWinLoss.Criteria;
//using CompanyWinLoss.Model;
//using Fanex.Data.Repository;
//using Fanex.Excel.Models;

//namespace CompanyWinLoss.Services
//{
//    public class CompanyWinLossService(IDynamicRepository repository)
//    {
//        private const int ExtendCachedTimeOut = 2;

//        private readonly string _dataCachedKey = "Fanex.AccountingReports.Misc.CompanyWinLoss."
//            + (AppSession.CurrentUser != null ? AppSession.CurrentUser.Username : string.Empty);

//        public async Task<List<Currency>> GetCurrenciesByLicensee(CurrenciesByLicenseeCriteria criteria)
//        {
//            var result = await repository.FetchAsync<Currency>(criteria);
           
//            return result.ToList();
//        }

//        public async Task<List<Product>> GetProductListByLicense(ProductByLicenseCriteria criteria)
//        {
//            var result = await repository.FetchAsync<Product>(criteria);

//            return result.ToList();
//        }

//        public GridOptions GetCompanyWinLoss(CompanyWinLossCriteria criteria)
//        {
//            GridOptions gridOptions = null;
//            GridOptions pagingGridOptions = InitGridOptions(criteria);

//            if (criteria.IsPagingCallBack)
//            {
//                gridOptions = GetFullReportFromCache(criteria);
//                pagingGridOptions.TotalItems = gridOptions.BodyRows.Count;
//                GetPagingReport(gridOptions, ref pagingGridOptions, criteria);
//                return pagingGridOptions;
//            }

//            gridOptions = GetReportData(criteria);
//            pagingGridOptions.TotalItems = gridOptions.BodyRows.Count;
//            GetPagingReport(gridOptions, ref pagingGridOptions, criteria);

//            return pagingGridOptions;
//        }

//        public async Task<List<Licensee>> GetLicensee()
//        {
//            var criteria = new LicenseeCriteria();

//            var result = await repository.FetchAsync<Licensee>(criteria);
           
//            return result.ToList();
//        }

//        private void GetPagingReport(GridOptions gridOptions, ref GridOptions pagingGridOptions, CompanyWinLossCriteria criteria)
//        {
//            int pageSize = criteria.PageSize;
//            int pageIndex = criteria.PageIndex;
//            pagingGridOptions.BodyRows = gridOptions.BodyRows.Skip(pageIndex * pageSize).Take(pageSize).ToList();
//        }

//        private GridOptions GetReportData(CompanyWinLossCriteria criteria)
//        {
//            criteria.MoveBetsDay = ReportService.BalanceUp.MoveBetsDay;
//            GridOptions gridOptions = InitGridOptions(criteria);

//            using (IDataReader reader = _repository.GetDataFromDB(criteria))
//            {
//                gridOptions.BodyRows = MapReportDataRows(reader);
//            }

//            CacheExtension.Set(_dataCachedKey, gridOptions);

//            return gridOptions;
//        }

//        public GridOptions GetFullReportFromCache(CompanyWinLossCriteria criteria)
//        {
//            GridOptions gridOptions = null;
//            gridOptions = CacheExtension.Get<GridOptions>(_dataCachedKey);

//            if (gridOptions == null)
//            {
//                criteria.IsPagingCallBack = false;
//                gridOptions = GetCompanyWinLoss(criteria);
//            }

//            return gridOptions;
//        }

//        private GridOptions InitGridOptions(CompanyWinLossCriteria criteria)
//        {
//            return new GridOptions
//            {
//                CustomizeHeader = true,
//                Columns = CreateHeaderColumns(),
//                ShowFooter = false,
//                ShowDistinctRow = false,
//                ShowHighlightRow = false,
//                ShowTitle = true,
//                TitleType = TitleType.TwoLineBlue,
//                TitleInfoText = Labels.Company_Win_Loss + " from " + criteria.FromDate.ToShortFormatedDate() + " to " + criteria.ToDate.ToShortFormatedDate()
//            };
//        }

//        private List<GroupHeaderRows> CreateGroupHeaderRows()
//        {
//            var groupHeader = new List<GroupHeaderRows>();
//            var groupHeaderRow = new GroupHeaderRows();
//            groupHeaderRow.Columns.AddRange(new List<HeaderColumns>()
//            {
//                new HeaderColumns() { HeaderText = "#", Name = Labels.RowId, Width = 50 },
//                new HeaderColumns() { HeaderText = Labels.Super, Name = Labels.UserName, Width = 250 },
//                new HeaderColumns() { HeaderText = Labels.Company_Win_Loss, Name = Labels.CompanyWL, Width = 250 },
//            });
//            groupHeader.Add(groupHeaderRow);

//            return groupHeader;
//        }

        
//        private List<HeaderColumns> CreateHeaderColumns()
//        {
//            var header = new List<HeaderColumns>();
//            header.AddRange(new List<HeaderColumns>()
//            {
//                new HeaderColumns() { HeaderText = "#", Name = Labels.RowId, Width = 50, CustomCellType = ExcelCellType.Integer, CellAlignment = HAlignment.Center },
//                new HeaderColumns() { HeaderText = Labels.Super, Name = Labels.UserName, Width = 250, CellAlignment = HAlignment.Left },
//                new HeaderColumns() { HeaderText = Labels.Company_Win_Loss, Name = Labels.CompanyWL, Width = 250, CustomCellType = ExcelCellType.DoubleRedNegative, CellAlignment = HAlignment.Right },
//            });

//            return header;
//        }

//        private List<GridDataRow> MapReportDataRows(IDataReader reader)
//        {
//            int dataColIndex = 0;
//            List<GridDataRow> dataRows = new List<GridDataRow>();
//            while (reader.Read())
//            {
//                GridDataRow dataRow = new GridDataRow();
//                dataRow.Columns.AddRange(new List<Columns>()
//                {
//                    new Columns(dataColIndex++) { Name = Labels.RowId, Value = Converter.To<int>(reader["RowId"]) },
//                    new Columns(dataColIndex++) { Name = Labels.UserName, Value = Converter.To<string>(reader["UserName"]) },
//                    new Columns(dataColIndex++) { Name = Labels.CompanyWL, Value = Converter.To<double>(reader["CompanyWL"]) },
//                });

//                dataRows.Add(dataRow);
//            }

//            return dataRows;
//        }

//        public string GetReportName(CompanyWinLossCriteria criteria)
//        {
//            string reportName = string.Format(string.Format(Labels.Company_Win_Loss.Replace(" ", string.Empty))
//                + "_From_" + string.Format(criteria.FromDate.ToShortFormatedDateWithSlash().Replace("/", "_"))
//                + "_To_" + string.Format(criteria.ToDate.ToShortFormatedDateWithSlash().Replace("/", "_")));

//            return reportName;
//        }
//    }
//}
