using DuplicateTickets.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class DuplicateTicketsInjection
    {
        public static void RegisterDuplicateTicketsServices(this IServiceCollection service)
        {
            service.AddTransient<DuplicateTicketsCashOutService, DuplicateTicketsCashOutServiceImp>();
            service.AddTransient<DuplicateTicketsUtility, DuplicateTicketsUtilityImp>();
            service.AddTransient<DuplicateTicketsService, DuplicateTicketsServiceImp>();
        }
    }
}
