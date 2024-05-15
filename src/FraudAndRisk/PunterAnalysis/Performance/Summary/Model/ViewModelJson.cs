namespace PunterAnalysis.Performance.Summary.Model
{
    public class ViewModelJson
    {
        public SummaryStatistic SummaryStatistic { get; set; }

        public List<MonthStatistic> MonthStatistics { get; set; }

        public List<ProductTurnoverStatistic> ProductTurnoverStatistics { get; set; }

        public List<SportStatistic> SportStatistics { get; set; }

        public List<BetTypeStatistic> BetTypeStatistics { get; set; }

        public List<EventStatistic> EventStatistics { get; set; }
    }
}