using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using SoccerPerformanceDetail.Constants;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Utilities;
using SoccerPerformanceDetail.ViewModel;
using Utilities.Configurations;

namespace SoccerPerformanceDetail.Services;

public interface TeamExcelService
{
    Task<ExcelPackage> ExportExcelTeamPerformance(TeamPerformanceList data,
        GetTeamPerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes);
}

public class TeamExcelServiceImp(
    ConfigurationService configurationService,
    IMapper mapper)
    : BaseExcelService(configurationService), TeamExcelService
{
    public Task<ExcelPackage> ExportExcelTeamPerformance(TeamPerformanceList data, GetTeamPerformanceCriteria criteria, IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes)
    {
        LoadTemplate("Team", "Team.xlsx", ExcelConstants.TeamPerformance);

        var rowIndex = BuildFilterBlock(criteria, leagueGroups, betTypes);
        var currentRowIndex = rowIndex + 4;

        if (data.ItemLiveList.Any())
        {
            BuildBodyRows(data.ItemLiveList, currentRowIndex, ExcelConstants.LiveText);
            currentRowIndex += data.ItemLiveList.Count;
        }

        if (data.ItemNonLiveList.Any())
        {
            BuildBodyRows(data.ItemNonLiveList, currentRowIndex + 1, ExcelConstants.NonLiveText);
            currentRowIndex += data.ItemNonLiveList.Count + 1;
        }

        BuildTotalRow(data.Total, currentRowIndex + 1);
        ClearTemplate(ExcelConstants.TraderPerformance);
        return Task.FromResult(ExcelPackage!);
    }

    private void BuildBodyRows(IEnumerable<TeamPerformanceItem> list, int startRowIndex, string title)
    {
        var index = 1;
        var rowIndex = startRowIndex;

        var titleExcelItem = new TeamPerformanceExcelTitleItem
        {
            Title = title
        };
        sheet!.ApplyModel(titleSheet, titleExcelItem, rowIndex++);

        var excelModels = mapper.Map<IEnumerable<TeamPerformanceExcelItem>>(list);
        foreach (var item in excelModels)
        {
            item.No = index++;
            sheet!.ApplyModel(bodySheet, item, rowIndex);
            UpdateValueSpecificColumns(rowIndex++, item);
        }
    }

    private void BuildTotalRow(TeamPerformanceItem data, int startIndex)
    {
        var excelModel = mapper.Map<TeamPerformanceExcelItem>(data);
        sheet!.ApplyModel(totalSheet, excelModel, startIndex);
        UpdateValueSpecificColumns(startIndex, excelModel);
        var cellTotalAddress = $"A{startIndex}:B{startIndex}";
        sheet!.Cells[cellTotalAddress].Merge = true;
        sheet!.Cells[cellTotalAddress].Value = ExcelConstants.GrandTotalText;
    }

    private void UpdateValueSpecificColumns(int startIndex, TeamPerformanceExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"G{startIndex}"], model.Score);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"K{startIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"O{startIndex}"], model.MarginReal);
        sheet!.Cells[$"I{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.Turnover / model.NoOfMatches : ExcelConstants.NotAvailableText;
        sheet!.Cells[$"M{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.TurnoverReal / model.NoOfMatches : ExcelConstants.NotAvailableText;
    }
}