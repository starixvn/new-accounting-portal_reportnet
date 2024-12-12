using AutoMapper;
using HouseWinLoss.Common.Services;

namespace HouseWinLoss.ProductBetType
{
    public interface Service
    {
        Task<ProductBetTypeWinLoss> Query(QueryModel queryModel);
    }

    public class ServiceImp(RawWinLossService rawWinLossService, CalculationService calculationService, IMapper mapper)
        : Service
    {
        public async Task<ProductBetTypeWinLoss> Query(QueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);

            var rawBetTypes = await rawWinLossService.Query(criteria);
           
            var betTypes = calculationService.CalculateBetTypes(rawBetTypes);

            var grandTotal = calculationService.CalculateSumWinLoss("Grand Total", rawBetTypes);

            return new ProductBetTypeWinLoss
            {
                BetTypes = betTypes,
                GrandTotal = grandTotal
            };
        }
    }
}
