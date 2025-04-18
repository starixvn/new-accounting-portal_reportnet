namespace Margin.Services;

using Criteria;
using Entities;
using Fanex.Data.Repository;
using Model;
using Utilities.RemotingRef;

public interface MarginWinLossService
{
    Task<List<MarginWinLossModel>> GetMarginWinLoss(MarginWinLossCriteria criteria);
}

public class MarginWinLossServiceImp(IDynamicRepository repository, RemotingRefService remotingRefService) 
    : MarginWinLossService
{
    public async Task<List<MarginWinLossModel>> GetMarginWinLoss(MarginWinLossCriteria criteria)
    {
        var rawData = await repository.FetchAsync<MarginWinLossItem>(criteria);

        return !rawData.Any() ? [] : rawData.Select(CreateMarginWinLossModel).ToList();

        MarginWinLossModel CreateMarginWinLossModel(MarginWinLossItem item)
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
            return winLossItem;
        }
    }
}