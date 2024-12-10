using OfficeOpenXml;
using PunterAnalysis.League.Model;
using PunterAnalysis.Shared;

namespace PunterAnalysis.League
{
    public interface LeagueExcelBuilder
    {
        ExcelPackage CreateExcelPackage(ReportModel model, ExcelSetting setting);
    }

    public class LeagueExcelBuilderImp : LeagueExcelBuilder
    {
        private const int WinLossColumnIndex = 2;

        private ExcelWorksheet headerTemplateSheet;

        private ExcelWorksheet statisticSheet;

        private ExcelWorksheet templateSheet;

        private ExcelWorksheet totalTemplateSheet;

        private int RowIndex { get; set; }

        private ExcelPackage ExcelPackage { get; set; }

        public ExcelPackage CreateExcelPackage(ReportModel model, ExcelSetting setting)
        {
            LoadTemplate(setting.TemplatePath);
            ApplySetting();
            SetRowIndex(setting.StartRowIndex);
            SetColumnWidth(model.GroupNames.Count);
            ApplyTitle(setting.Title, setting.TitleCellRange);
            ApplyFilter(setting.Filter, setting.FilterCellRange);
            CreateHeader(model.GroupNames);
            CreateBody(model.Entities);
            CreateFooter(model.Total);
            ClearTemplate();
            return ExcelPackage;
        }

        private void LoadTemplate(string templatePath)
        {
            using (var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read))
            {
                ExcelPackage = new ExcelPackage(stream);
                statisticSheet = ExcelPackage.Workbook.Worksheets.Add("Punter Analysis");
                templateSheet = ExcelPackage.Workbook.Worksheets["Template"];
                headerTemplateSheet = ExcelPackage.Workbook.Worksheets["HeaderTemplate"];
                totalTemplateSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
            }
        }

        private void ApplySetting()
        {
            statisticSheet.OutLineSummaryBelow = false;
        }

        private void ClearTemplate()
        {
            ExcelPackage.Workbook.Worksheets.Delete(templateSheet);
            ExcelPackage.Workbook.Worksheets.Delete(headerTemplateSheet);
            ExcelPackage.Workbook.Worksheets.Delete(totalTemplateSheet);
        }

        private void SetRowIndex(int rowIndex)
        {
            RowIndex = rowIndex;
        }

        private void SetColumnWidth(int numberGroup)
        {
            var numberColumns = (numberGroup * 4) + 1;
            for (var i = 0; i < numberColumns; i++)
            {
                statisticSheet.Column(i + 1).Width = 18;
            }

            statisticSheet.Column(1).Width = 50;
        }

        private void ApplyTitle(string title, string titleCellRange)
        {
            headerTemplateSheet.Cells["A1:E2"].Copy(statisticSheet.Cells[titleCellRange]);
            statisticSheet.Cells[titleCellRange].Value = title;
            statisticSheet.Cells[titleCellRange].Merge = true;
        }

        private void ApplyFilter(string filter, string filterCellRange)
        {
            headerTemplateSheet.Cells["A3:E8"].Copy(statisticSheet.Cells[filterCellRange]);
            statisticSheet.Cells[filterCellRange].Value = filter;
            statisticSheet.Cells[filterCellRange].Merge = true;
        }

        private void CreateHeader(List<string> groupNames)
        {
            var columnIndex = WinLossColumnIndex;
            const string leagueNameCellAddress = "A10:A11";
            headerTemplateSheet.Cells[leagueNameCellAddress].Copy(statisticSheet.Cells[leagueNameCellAddress]);
            statisticSheet.Cells[leagueNameCellAddress].Value = "League";
            statisticSheet.Cells[leagueNameCellAddress].Merge = true;
            groupNames.ForEach(groupName =>
            {
                headerTemplateSheet.Cells["B10:E10"].Copy(
                    statisticSheet.Cells[RowIndex, columnIndex, RowIndex, columnIndex + 3]);
                statisticSheet.Cells[RowIndex, columnIndex, RowIndex, columnIndex + 3].Value = groupName;
                statisticSheet.Cells[RowIndex, columnIndex, RowIndex, columnIndex + 3].Merge = true;
                headerTemplateSheet.Cells["B11:E11"].Copy(
                    statisticSheet.Cells[RowIndex + 1, columnIndex, RowIndex + 1, columnIndex + 3]);
                columnIndex += 4;
            });

            RowIndex += 2;
        }

        private void CreateBody(List<LeagueModel> model)
        {
            model.ForEach(league =>
            {
                var columnIndex = WinLossColumnIndex;
                templateSheet.Cells["A12:A12"].Copy(statisticSheet.Cells[$"A{RowIndex}"]);
                statisticSheet.Cells[$"A{RowIndex}"].Value = league.LeagueName;
                league.PeriodGroups.ForEach(group =>
                {
                    templateSheet.Cells["B12:E12"].Copy(
                        statisticSheet.Cells[RowIndex, columnIndex, RowIndex, columnIndex + 3]);
                    MapGroupValue(group, columnIndex);
                    columnIndex += 4;
                });

                RowIndex++;
            });
        }

        private void CreateFooter(LeagueModel model)
        {
            var columnIndex = WinLossColumnIndex;
            totalTemplateSheet.Cells["A12:A12"].Copy(statisticSheet.Cells[$"A{RowIndex}"]);
            statisticSheet.Cells[$"A{RowIndex}"].Value = "Total";
            model.PeriodGroups.ForEach(group =>
            {
                totalTemplateSheet.Cells["B12:E12"].Copy(
                    statisticSheet.Cells[RowIndex, columnIndex, RowIndex, columnIndex + 3]);
                MapGroupValue(group, columnIndex);
                columnIndex += 4;
            });
        }

        private void MapGroupValue(PeriodGroup group, int columnIndex)
        {
            statisticSheet.Cells[RowIndex, columnIndex].Value = group.WinLoss;
            statisticSheet.Cells[RowIndex, columnIndex + 1].Value = group.Turnover;
            statisticSheet.Cells[RowIndex, columnIndex + 2].Value = group.Turnover == 0 ? "N/A" : (object)group.Margin;
            statisticSheet.Cells[RowIndex, columnIndex + 3].Value = group.BetCount;
        }
    }
}