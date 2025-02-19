namespace SoccerPerformanceDetail.Services;

using Fanex.Data.Repository;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Model;
using SoccerPerformanceDetail.ViewModel;

public interface SoccerPerformanceDetailService
{
    Task<BetTypePerformanceList> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria);
}


public class DetailReportService(IDynamicRepository repository) : SoccerPerformanceDetailService
{
    public async Task<BetTypePerformanceList> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria)
    {
        var betTypeData = await repository.FetchAsync<BetTypePerformanceInfo>(criteria);
        BetTypePerformanceList betTypePerformance = new BetTypePerformanceList();

        if (betTypeData != null && betTypeData.Any())
        {
            if (criteria.MarketFilter == Market.All)
            {
                var data = SortBetTypePerformanceInfo(betTypeData);

                betTypePerformance.AddBetTypeList(data);
            }
            else
            {
                betTypePerformance.AddBetTypeList(betTypeData, criteria.MarketFilter == Market.Live);
            }

            betTypePerformance.CalculateTotalMargin();
        }

        return betTypePerformance;
    }

    private IEnumerable<BetTypePerformanceInfo> SortBetTypePerformanceInfo(IEnumerable<BetTypePerformanceInfo> betTypeData)
    {
        return betTypeData.AsEnumerable()
                          .OrderByDescending(c => c.BetType)
                          .ThenByDescending(c => c.IsLive)
                          .Select(c => new BetTypePerformanceInfo
                          {
                              BetType = c.BetType,
                              TypeName = c.TypeName,
                              TraderCount = c.TraderCount,
                              LeagueCount = c.LeagueCount,
                              MatchCount = c.MatchCount,
                              Turnover = c.Turnover,
                              WinLoss = c.WinLoss,
                              Margin = c.Margin,
                              TurnoverReal = c.TurnoverReal,
                              WinLossReal = c.WinLossReal,
                              MarginReal = c.MarginReal,
                              BetCount = c.BetCount,
                              IsLive = c.IsLive,
                              FlagForViewReport = betTypeData.Count(b => b.BetType == c.BetType)
                          });
    }
}