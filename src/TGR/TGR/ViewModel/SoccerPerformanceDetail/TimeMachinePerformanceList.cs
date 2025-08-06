using TGR.Model.SoccerPerformanceDetail;

namespace TGR.ViewModel.SoccerPerformanceDetail;

[Serializable]
public class TimeMachinePerformanceList
{
    public List<TimeMachinePerformanceItem> ItemList { get; set; } = [];

    public TimeMachinePerformanceItem Total { get; set; } = new();

    public void AddItem(IEnumerable<TimeMachinePerformanceInfo> infos, short betType)
    {
        foreach (var info in infos)
        {
            if (info == null)
            {
                continue;
            }

            var item = new TimeMachinePerformanceItem(info, betType);

            ItemList.Add(item);

            CalculateTotal(item);
        }
    }

    private void CalculateTotal(TimeMachinePerformanceItem item)
    {
        Total.NoOfLeagues += item.NoOfLeagues;
        Total.NoOfMatches += item.NoOfMatches;
        Total.Turnover += item.Turnover;
        Total.WinLoss += item.WinLoss;
        Total.TurnoverReal += item.TurnoverReal;
        Total.WinLossReal += item.WinLossReal;
        Total.BetCount += item.BetCount;
    }
}