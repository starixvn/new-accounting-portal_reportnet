namespace ResultAnalysis.Services;

using System.Drawing;
using AutoMapper;
using Epplus.Mapper.Extensions;
using Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Utilities.Configurations;

public interface ExcelBuilderService
{
    ExcelPackage Build(string title, ResultAnalysisExcelModel model);

    ExcelPackage BuildForQuarter(string title, ResultAnalysisOfQuarterModel model);
}

public class ExcelBuilderServiceImp(ConfigurationService configurationService, IMapper mapper) : ExcelBuilderService
{
    private ExcelWorksheet sheet;

    private ExcelPackage ExcelPackage { get; set; }

    private const int StartSummaryRowIndex = 6;

    private const int StartDetailedRowIndex = 10;

    private const int StartDetailedRowIndexOfQuarter = 9;

    private readonly string[] separatingString = [" -vs- "];

    public ExcelPackage Build(string title, ResultAnalysisExcelModel model)
    {
        LoadTemplate("ResultAnalysis.xlsx");
        BuildTitle(title, "A1:P1");
        BuildBreadCrumb(title, "A3:P3");
        sheet.ApplyModel(sheet, model.Summary, StartSummaryRowIndex);
        sheet.ApplyVertical(model.Items, StartDetailedRowIndex);
        FormatSheet(model.Items.ToList());
        return ExcelPackage;
    }

    public ExcelPackage BuildForQuarter(string title, ResultAnalysisOfQuarterModel model)
    {
        LoadTemplate("ResultAnalysisOfQuarter.xlsx");
        BuildTitle(title, "A1:S1");
        BuildBreadCrumb(title, "A3:S3");
        var summary = mapper.Map<ResultAnalysisSummaryOfQuarterExcelModel>(model.Summary);
        sheet.ApplyModel(sheet, summary, StartSummaryRowIndex);
        ExportDatRowForQuarter(model.Items, StartDetailedRowIndexOfQuarter);
        return ExcelPackage;
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/ResultAnalysis/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Results"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBreadCrumb(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void FormatSheet(List<DetailedResultAnalysisExcelModel> data)
    {
        for (var i = 0; i < data.Count; i++)
        {
            var rowIndex = StartDetailedRowIndex + i;
            var cellAddress = $"A{rowIndex}:N{rowIndex}";
            var backgroundColor = i % 2 == 0 ? Color.White : Color.FromArgb(228, 242, 253);
            FormatOddEvenBackground(backgroundColor, cellAddress);
            HighlightFavoriteTeam(data[i], i);
        }
    }

    private void FormatOddEvenBackground(Color backgroundColor, string cellAddress)
    {
        sheet.Cells[cellAddress].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[cellAddress].Style.Fill.BackgroundColor.SetColor(backgroundColor);
    }

    private void HighlightFavoriteTeam(DetailedResultAnalysisExcelModel model, int index)
    {
        if (string.IsNullOrEmpty(model.Match) || string.IsNullOrEmpty(model.Fav)) return;
        var match = model.Match.Split(separatingString, StringSplitOptions.None);
        var isFavHome = model.Fav.ToLower() == "home";
        var rowIndex = StartDetailedRowIndex + index;
        var cellAddress = $"C{rowIndex}";
        sheet.Cells[cellAddress].Value = string.Empty;
        sheet.Cells[cellAddress].IsRichText = true;
        var home = sheet.Cells[cellAddress].RichText.Add(match[0]);
        home.Color = isFavHome ? Color.Red : Color.Black;
        var vsText = sheet.Cells[cellAddress].RichText.Add(" -vs- ");
        vsText.Color = Color.Black;
        var away = sheet.Cells[cellAddress].RichText.Add(match[1]);
        away.Color = isFavHome ? Color.Black : Color.Red;
    }

    private void ExportDatRowForQuarter(IEnumerable<DetailedResultAnalysisOfQuarterModel> models, int startRowIndex)
    {
        var index = 0;
        foreach (var model in models)
        {
            var homeItem = mapper.Map<DetailedResultAnalysisOfHomeQuarterExcelModel>(model);
            var awayItem = mapper.Map<DetailedResultAnalysisOfAwayQuarterExcelModel>(model);
            sheet.ApplyModel(sheet, homeItem, startRowIndex++);
            sheet.ApplyModel(sheet, awayItem, startRowIndex++);
            MergeColumnsForQuarter(startRowIndex - 2);
            FormatMatchForQuarter(startRowIndex - 2, model.Fav, model.Match);
            var cellAddress = $"A{startRowIndex - 2}:S{startRowIndex - 1}";
            var backgroundColor = index % 2 == 0 ? Color.White : Color.FromArgb(228, 242, 253);
            FormatOddEvenBackground(backgroundColor, cellAddress);
            index++;
        }
    }

    private void MergeColumnsForQuarter(int rowIndex)
    {
        sheet.Cells[$"A{rowIndex}:A{rowIndex + 1}"].Merge = true;
        sheet.Cells[$"A{rowIndex}:A{rowIndex + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        sheet.Cells[$"B{rowIndex}:B{rowIndex + 1}"].Merge = true;
        sheet.Cells[$"B{rowIndex}:B{rowIndex + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        sheet.Cells[$"D{rowIndex}:D{rowIndex + 1}"].Merge = true;
        sheet.Cells[$"D{rowIndex}:D{rowIndex + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        sheet.Cells[$"E{rowIndex}:E{rowIndex + 1}"].Merge = true;
        sheet.Cells[$"E{rowIndex}:E{rowIndex + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        sheet.Cells[$"F{rowIndex}:F{rowIndex + 1}"].Merge = true;
        sheet.Cells[$"F{rowIndex}:F{rowIndex + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        sheet.Cells[$"O{rowIndex}:O{rowIndex + 1}"].Merge = true;
        sheet.Cells[$"O{rowIndex}:O{rowIndex + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        sheet.Cells[$"P{rowIndex}:P{rowIndex + 1}"].Merge = true;
        sheet.Cells[$"P{rowIndex}:P{rowIndex + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        sheet.Cells[$"Q{rowIndex}:Q{rowIndex + 1}"].Merge = true;
        sheet.Cells[$"Q{rowIndex}:Q{rowIndex + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        sheet.Cells[$"R{rowIndex}:R{rowIndex + 1}"].Merge = true;
        sheet.Cells[$"R{rowIndex}:R{rowIndex + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        sheet.Cells[$"S{rowIndex}:S{rowIndex + 1}"].Merge = true;
        sheet.Cells[$"S{rowIndex}:S{rowIndex + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
    }

    private void FormatMatchForQuarter(int rowIndex, string favTeam, string match)
    {
        if (string.IsNullOrEmpty(favTeam) || string.IsNullOrEmpty(match)) return;
        var teams = match.Split(separatingString, StringSplitOptions.None);
        if (favTeam.ToLower() == "home")
        {
            sheet.Cells[$"C{rowIndex}"].Value = teams[0];
            sheet.Cells[$"C{rowIndex}"].Style.Font.Color.SetColor(Color.Red);
            sheet.Cells[$"C{rowIndex + 1}"].Value = teams[1];
            sheet.Cells[$"C{rowIndex + 1}"].Style.Font.Color.SetColor(Color.Black);
        }
        else
        {
            sheet.Cells[$"C{rowIndex}"].Value = teams[0];
            sheet.Cells[$"C{rowIndex}"].Style.Font.Color.SetColor(Color.Black);
            sheet.Cells[$"C{rowIndex + 1}"].Value = teams[1];
            sheet.Cells[$"C{rowIndex + 1}"].Style.Font.Color.SetColor(Color.Red);
        }
    }
}