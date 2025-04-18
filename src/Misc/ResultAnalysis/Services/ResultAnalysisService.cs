namespace ResultAnalysis.Services;

using Criteria;
using Fanex.Data.Repository;
using Models;

public interface ResultAnalysisService
{
    Task<IEnumerable<LeagueModel>> GetLeagues(LeagueCriteria criteria);

    Task<ResultAnalysisModel> GetResultAnalysis(ResultAnalysisCriteria criteria);

    Task<ResultAnalysisOfQuarterModel> GetResultAnalysisOfQuarter(ResultAnalysisCriteria criteria);
}

public class ResultAnalysisServiceImp(IDynamicRepository repository) : ResultAnalysisService
{
    public Task<IEnumerable<LeagueModel>> GetLeagues(LeagueCriteria criteria)
    {
        return repository.FetchAsync<LeagueModel>(criteria);
    }

    public async Task<ResultAnalysisModel> GetResultAnalysis(ResultAnalysisCriteria criteria)
    {
        var items = (await repository.FetchAsync<DetailedResultAnalysisModel>(criteria)).ToList();
        var result = new ResultAnalysisModel
        {
            Items = items,
            Summary = GetSummaryModel(items)
        };
        return await Task.FromResult(result);
    }

    public async Task<ResultAnalysisOfQuarterModel> GetResultAnalysisOfQuarter(ResultAnalysisCriteria criteria)
    {
        var items = (await repository.FetchAsync<DetailedResultAnalysisOfQuarterModel>(criteria)).ToList();
        var result = new ResultAnalysisOfQuarterModel
        {
            Items = items,
            Summary = GetSummaryModel(items)
        };
        return await Task.FromResult(result);
    }

    private static ResultAnalysisSummaryModel GetSummaryModel(List<DetailedResultAnalysisModel> data)
    {
        var result = new ResultAnalysisSummaryModel
        {
            TotalLeague = data.Select(item => item.League).Distinct().Count(),
            TotalMatch = data.Select(item => item.MatchID).Distinct().Count(),
            FavHdpResult = data.Count(item => item.ResultHDP == "Fav"),
            UnderdogHdpResult = data.Count(item => item.ResultHDP == "Underdog"),
            DrawHdpResult = data.Count(item => item.ResultHDP == "Draw"),
            OverOuResult = data.Count(item => item.ResultOU == "Over"),
            UnderOuResult = data.Count(item => item.ResultOU == "Under"),
            DrawOuResult = data.Count(item => item.ResultOU == "Draw"),
            Fav1x2Result = data.Count(item => item.Result1x2 == "Fav"),
            Underdog1x2Result = data.Count(item => item.Result1x2 == "Underdog"),
            Draw1x2Result = data.Count(item => item.Result1x2 == "Draw"),
            TgResult01 = data.Count(item => CheckResultTg(item.ResultTG, 0, 1)),
            TgResult23 = data.Count(item => CheckResultTg(item.ResultTG, 2, 3)),
            TgResult46 = data.Count(item => CheckResultTg(item.ResultTG, 4, 6)),
            TgResult7 = data.Count(item => CheckResultTg(item.ResultTG, 7, -1))
        };
        return result;
    }

    private static ResultAnalysisSummaryModel GetSummaryModel(List<DetailedResultAnalysisOfQuarterModel> data)
    {
        var result = new ResultAnalysisSummaryModel
        {
            TotalLeague = data.Select(item => item.League).Distinct().Count(),
            TotalMatch = data.Select(item => item.MatchID).Distinct().Count(),
            FavHdpResult = data.Count(item => item.ResultHDP == "Fav"),
            UnderdogHdpResult = data.Count(item => item.ResultHDP == "Underdog"),
            DrawHdpResult = data.Count(item => item.ResultHDP == "Draw"),
            OverOuResult = data.Count(item => item.ResultOU == "Over"),
            UnderOuResult = data.Count(item => item.ResultOU == "Under"),
            DrawOuResult = data.Count(item => item.ResultOU == "Draw"),
            Fav1x2Result = data.Count(item => item.Result1x2 == "Fav"),
            Underdog1x2Result = data.Count(item => item.Result1x2 == "Underdog"),
            Draw1x2Result = data.Count(item => item.Result1x2 == "Draw")
        };
        return result;
    }

    private static bool CheckResultTg(string data, int first, int second)
    {
        if (data == "-") return false;
        var number = int.Parse(data);
        if (second > 0) return number >= first && number <= second;
        return number >= first;
    }
}