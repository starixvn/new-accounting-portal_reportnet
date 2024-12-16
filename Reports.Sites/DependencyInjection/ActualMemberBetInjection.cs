using ActualMemberBet.Bettype;
using ActualMemberBet.Common;
using ActualMemberBet.Product;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class ActualMemberBetInjection
    {
        public static void RegisterActualMemberBetServices(this IServiceCollection service)
        {
            service.AddTransient<ActualMemberBetService, ActualMemberBetServiceImp>();
            service.AddTransient<ProductExcelBuilder, ProductExcelBuilderImp>();
            service.AddTransient<BettypeExcelBuilder, BettypeExcelBuilderImp>();
        }
    }
}
