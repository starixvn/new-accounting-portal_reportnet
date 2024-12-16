using Fanex.Data.Repository;
using Margin.Criteria;
using Margin.Entities;
using Margin.Model;
using Utilities.RemotingRef;

namespace Margin.Services;

public interface MarginWinLossService
{
    Task<List<MarginWinLossModel>> GetMarginWinLoss(MarginWinLossCriteria criteria);
}

public class MarginWinLossServiceImp(IDynamicRepository repository, RemotingRefService remotingRefService) 
    : MarginWinLossService
{
    public async Task<List<MarginWinLossModel>> GetMarginWinLoss(MarginWinLossCriteria criteria)
    {
        var result = new List<MarginWinLossModel>();

        var rawData = await repository.FetchAsync<MarginWinLossItem>(criteria);

        if (!rawData.Any())
        {
            return result;
        }

        foreach (var item in rawData)
        {
            var bettypeId = item.BettypeId;
            var bettypeName = remotingRefService.GetBetTypeName(bettypeId);

            var winLossItem = new MarginWinLossModel
            {
                UserName = item.UserName,
                BettypeId = bettypeId,
                BettypeName = bettypeName,
                WinningAmount = item.WinningAmount,
                Turnover = item.Turnover,
                Ratio = item.Ratio,
                TotalBet = item.TotalBet
            };
               
            result.Add(winLossItem);
        }

        return result;
    }
}