namespace CashOutPerformance.Services;

using AutoMapper;
using Constants;
using Criteria;
using Models;
using Utilities.LinqDynamicSum;
using ViewModels;

public interface ModelsHelperService
{
    TicketViewModel<BetTypeLevelModel> UpdateGameTimeLevelModel(List<BetTypeLevelModel> data,
        GameTimeCriteria criteria);

    TicketViewModel<BetTypeLevelModel> UpdateWinningProbabilityLevelModel(List<BetTypeLevelModel> data,
        GameTimeCriteria criteria);

    TicketViewModel<BetTypeLevelModel> UpdateDetailGameTimeLevelModel(List<BetTypeLevelModel> data,
        GameTimeCriteria criteria);

    TicketViewModel<BetTypeLevelModel> UpdateBetTypeViewModel(TicketViewModel<BetTypeLevelModel> data);
}

public class ModelsHelperServiceImp(IMapper mapper) : ModelsHelperService
{
    public TicketViewModel<BetTypeLevelModel> UpdateGameTimeLevelModel(List<BetTypeLevelModel> data, GameTimeCriteria criteria)
    {
        var result = new TicketViewModel<BetTypeLevelModel>();
        var rawData = data.Where(item => item.BetType == criteria.BetType && item.OutCome == criteria.OutCome);
        var ticketList = rawData.GroupBy(x => x.GameTime)
            .Select(ticket => ticket.DynamicSum())
            .OrderBy(x => x.GameTime).ToList();
        ticketList.ForEach(b =>
        {
            b.GameTime = b.GameTime.Replace(GameTimeConstants.HalftimeValue, GameTimeConstants.HalftimeText);
            b.BetTypeName = b.GameTime;
        });
        result.TicketList = ticketList;
        result.TotalTicket = rawData.DynamicSum();
        result.Total = ticketList.Count;
        return result;
    }

    public TicketViewModel<BetTypeLevelModel> UpdateWinningProbabilityLevelModel(List<BetTypeLevelModel> data, GameTimeCriteria criteria)
    {
        var result = new TicketViewModel<BetTypeLevelModel>();
        var rawData = data.Where(item => item.BetType == criteria.BetType);
        var ticketList = rawData.GroupBy(x => x.WinningProbabilityQuantile)
            .Select(ticket => ticket.DynamicSum())
            .OrderBy(x => x.WinningProbabilityQuantile).ToList();
        ticketList.ForEach(b => b.BetTypeName = b.WinningProbabilityQuantile);
        result.TicketList = ticketList;
        result.TotalTicket = rawData.DynamicSum();
        result.Total = ticketList.Count;
        return result;
    }

    public TicketViewModel<BetTypeLevelModel> UpdateDetailGameTimeLevelModel(List<BetTypeLevelModel> data, GameTimeCriteria criteria)
    {
        var result = new TicketViewModel<BetTypeLevelModel>();
        var rawData = data.Where(item => item.BetType == criteria.BetType).OrderBy(o => o.GameTime);
        var groups = rawData.GroupBy(g => g.GameTime);
        var ticketList = new List<BetTypeLevelModel>();
        foreach (var group in groups)
        {
            var gameTime = group.DynamicSum();
            gameTime.ChildItems = [];
            gameTime.GameTime = gameTime.GameTime.Replace(GameTimeConstants.HalftimeValue, GameTimeConstants.HalftimeText);
            gameTime.BetTypeName = gameTime.GameTime;
            var groupedOutComes = group.GroupBy(item => item.OutCome);
            foreach (var outCome in groupedOutComes)
            {
                var item = outCome.DynamicSum();
                var childItem = mapper.Map<ChildItemBetTypeModel>(item);
                childItem.BetTypeName = childItem.OutCome;
                gameTime.ChildItems.Add(childItem);
            }
            ticketList.Add(gameTime);
        }

        result.TicketList = ticketList;
        result.TotalTicket = rawData.DynamicSum();
        result.Total = rawData.Count();
        return result;
    }

    public TicketViewModel<BetTypeLevelModel> UpdateBetTypeViewModel(TicketViewModel<BetTypeLevelModel> data)
    {
        var result = new TicketViewModel<BetTypeLevelModel>();
        var ticketList = new List<BetTypeLevelModel>();
        var groups = data.TicketList.GroupBy(x => x.BetType);
        foreach (var group in groups)
        {
            var betType = group.DynamicSum();
            var groupedOutComes = group.GroupBy(item => item.OutCome);
            foreach (var outCome in groupedOutComes)
            {
                var item = outCome.DynamicSum();
                var childItem = mapper.Map<ChildItemBetTypeModel>(item);
                betType.ChildItems.Add(childItem);
            }
            ticketList.Add(betType);
        }

        result.TicketList = ticketList.OrderByDescending(s => s.TotalIncome_RM);
        result.Total = data.Total;
        result.TotalTicket = data.TotalTicket;
        return result;
    }
}