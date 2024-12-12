using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ResultAnalysis.Models;
using System.Drawing;
using Utilities.Configurations;

namespace ResultAnalysis.Services
{
    public interface ExcelBuilderService
	{
		ExcelPackage Build(string title, ResultAnalysisExcelModel model, int sportId);
	}

	public class ExcelBuilderServiceImp(ConfigurationService configurationService) : ExcelBuilderService
    {
        private ExcelWorksheet sheet;

		private ExcelPackage ExcelPackage { get; set; }

		private const int StartSummaryRowIndex = 6;

		private const int StartDetailedRowIndex = 10;

		private const int Soccer = 1;

        public ExcelPackage Build(string title, ResultAnalysisExcelModel model, int sportId)
		{
			var isSoccer = sportId == Soccer;
			var fileExcel = isSoccer ? "ResultAnalysis.xlsx" : "SimpleResultAnalysis.xlsx";
			var cellAddressOfTitle = isSoccer ? "A1:P1" : "A1:M1";
			var cellAddressOfBreadCrumb = isSoccer ? "A3:P3" : "A3:M3";
			LoadTemplate(fileExcel);
			BuildTitle(title, cellAddressOfTitle);
			BuildBreadCrumb(title, cellAddressOfBreadCrumb);
			sheet.ApplyModel(sheet, model.Summary, StartSummaryRowIndex);
			sheet.ApplyVertical(model.Items, StartDetailedRowIndex);
			if (!isSoccer)
			{
				ResetValueForUnusedCell(model.Items.Count());
			}
            FormatSheet(model.Items.ToList(), isSoccer);
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

		private void ResetValueForUnusedCell(int totalOfDetailedItems)
		{
			sheet.Cells["M6"].Value = sheet.Cells["N6"].Value = sheet.Cells["O6"].Value = sheet.Cells["P6"].Value = "";
			sheet.Cells[$"N10:N{StartDetailedRowIndex + totalOfDetailedItems}"].Value = "";
		}

        private void FormatSheet(List<DetailedResultAnalysisExcelModel> data, bool isSoccer)
        {
            string[] separatingString = [" -vs- "];
            for (var i = 0; i < data.Count; i++)
            {
                FormatOddEvenBackground(isSoccer, i);
                HighlightFavoriteTeam(data[i], i, separatingString);
            }
        }

        private void FormatOddEvenBackground(bool isSoccer,int index)
        {
            var rowIndex = StartDetailedRowIndex + index;
            var cellAddress = isSoccer ? $"A{rowIndex}:N{rowIndex}" : $"A{rowIndex}:M{rowIndex}";
            var backgroundColor = index % 2 == 0 ? Color.White : Color.FromArgb(228, 242, 253);
            sheet.Cells[cellAddress].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[cellAddress].Style.Fill.BackgroundColor.SetColor(backgroundColor);
        }

        private void HighlightFavoriteTeam(DetailedResultAnalysisExcelModel model, int index, string[] separatingString)
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
    }
}
