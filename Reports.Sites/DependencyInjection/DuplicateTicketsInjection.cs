namespace Reports.Sites.DependencyInjection;

using DuplicateTickets.Services;

public static class DuplicateTicketsInjection
{
    public static void RegisterDuplicateTicketsServices(this IServiceCollection service)
    {
        service.AddTransient<DuplicateTicketsCashOutService, DuplicateTicketsCashOutServiceImp>();
        service.AddTransient<DuplicateTicketsUtility, DuplicateTicketsUtilityImp>();
        service.AddTransient<DuplicateTicketsService, DuplicateTicketsServiceImp>();
        service.AddTransient<DuplicateTicketsExcelService, DuplicateTicketsExcelServiceImp>();
    }
}