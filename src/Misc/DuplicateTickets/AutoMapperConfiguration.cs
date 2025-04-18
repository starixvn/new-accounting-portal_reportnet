namespace DuplicateTickets;

using AutoMapper;
using Model;

public static class AutoMapperConfiguration
{
    public static void InitializeDuplicateTickets(this IProfileExpression cfg)
    {
        cfg.CreateMap<TicketModel, TicketExcelModel>();
        cfg.CreateMap<DuplicateTicketsByUserNameModel, DuplicateTicketsByUserNameExcelModel>();
        cfg.CreateMap<DuplicateTicketsModel, DuplicateTicketsExcelModel>();
    }
}