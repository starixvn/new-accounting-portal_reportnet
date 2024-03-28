using AutoMapper;
using Nap_Reports.HouseWinLoss.Common.Services;

namespace Nap_Reports.HouseWinLoss.ProductBetType
{
    public interface Service
    {
        Task<ProductBetTypeWinLoss> Query(QueryModel queryModel);
    }

    public class ServiceImp : Service
    {
        private readonly RawWinLossService rawWinLossService;

        private readonly CalculationService calculationService;

        private readonly IMapper mapper;

        public ServiceImp(RawWinLossService rawWinLossService, CalculationService calculationService, IMapper mapper)
        {
            this.rawWinLossService = rawWinLossService;
            this.calculationService = calculationService;
            this.mapper = mapper;
        }

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
