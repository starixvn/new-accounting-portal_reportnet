using Fanex.Data.Repository;
using PunterAnalysis.BetCount.Model;
using PunterAnalysis.Shared;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.BetCount
{
    public interface BetCountService
    {
        Task<SummaryEntity<BetCountModel>> Query(Criteria criteria);
    }

    public class BetCountServiceImp(IDynamicRepository repository, DateTimeService dateTimeService)
        : BetCountService
    {
        public async Task<SummaryEntity<BetCountModel>> Query(Criteria criteria)
        {
            return (await repository.FetchAsync<BetCountRawModel>(criteria))
                           .Select(e => Map(e, criteria.PeriodType))
                           .CalculateSummaryEntity();
        }

        private BetCountModel Map(BetCountRawModel rawModel, int periodType)
        {
            return new BetCountModel
            {
                BetCount = rawModel.BetCount,
                DrawCount = rawModel.DrawCount,
                WinCount = rawModel.WinCount,
                WinLoss = rawModel.WinLoss,
                Date = dateTimeService.FormatDateTimeByPeriodType(periodType, rawModel.Date)
            };
        }
    }
}