using AutoMapper;
using CashOutPerformance.Constants;
using CashOutPerformance.Criteria;
using CashOutPerformance.Models;
using CashOutPerformance.Requests;
using CashOutPerformance.Services;
using CashOutPerformance.ViewModels;
using MediatR;
using OfficeOpenXml;
using System.Globalization;

namespace CashOutPerformance.Handlers;

public class HandlerExcel(
    TicketsExcelBuilder<SportLevelExcelModel> ticketsSportExcelBuilder,
    TicketsExcelBuilder<EventStatusLevelExcelModel> ticketsEventExcelBuilder,
    TicketsExcelBuilder<BetTypeLevelExcelModel> ticketsBetTypeExcelBuilder,
    TicketsExcelBuilder<MatchDetailLevelExcelModel> ticketsMatchExcelBuilder,
    CashOutPerformanceService service,
    IMapper mapper)
    : IRequestHandler<SportLevelExcelRequest, ExcelPackage>,
        IRequestHandler<TicketExcelRequest<EventStatusLevelModel>, ExcelPackage>,
        IRequestHandler<TicketExcelRequest<BetTypeLevelModel>, ExcelPackage>,
        IRequestHandler<TicketExcelRequest<MatchDetailLevelModel>, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(SportLevelExcelRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<SportLevelCriteria>(request);
        var reportData = await service.GetSportLevel(criteria);
        var excelModel = mapper.Map<SportLevelExcelModel>(reportData);
        var excelPackage = ticketsSportExcelBuilder.BuildSportLevel(request.Title, request.BreadCrumb, excelModel, true);
        return excelPackage;
    }

    public async Task<ExcelPackage> Handle(TicketExcelRequest<EventStatusLevelModel> request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<EventStatusLevelCriteria>(request);
        var reportData = await service.GetTickets<EventStatusLevelModel, EventStatusLevelCriteria>(criteria);
        var excelModel = mapper.Map<TicketViewExcelModel<EventStatusLevelExcelModel>>(reportData);
        var sportName = new CultureInfo("en-US").TextInfo.ToTitleCase(request.BreadCrumb.Last().ToLowerInvariant());
        foreach (var item in excelModel.TicketList)
        {
            var status = item.LiveStatus == "0" ? "Non-Live" : "Live";
            item.LiveStatus = $"{sportName} {status}";
        }
        var excelPackage = ticketsEventExcelBuilder.Build(request.Title, request.BreadCrumb, excelModel, ExcelType.Event, true);
        return excelPackage;
    }

    public async Task<ExcelPackage> Handle(TicketExcelRequest<BetTypeLevelModel> request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<BetTypeLevelCriteria>(request);
        var reportData = await service.GetTickets<BetTypeLevelModel, BetTypeLevelCriteria>(criteria);
        var excelModel = mapper.Map<TicketViewExcelModel<BetTypeLevelExcelModel>>(reportData);
        var excelPackage = ticketsBetTypeExcelBuilder.Build(request.Title, request.BreadCrumb, excelModel, ExcelType.BetType, true);
        return excelPackage;
    }

    public async Task<ExcelPackage> Handle(TicketExcelRequest<MatchDetailLevelModel> request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<MatchDetailLevelCriteria>(request);
        var reportData = await service.GetTickets<MatchDetailLevelModel, MatchDetailLevelCriteria>(criteria);
        var excelModel = mapper.Map<TicketViewExcelModel<MatchDetailLevelExcelModel>>(reportData);
        var excelPackage = ticketsMatchExcelBuilder.Build(request.Title, request.BreadCrumb, excelModel, ExcelType.Match, criteria.IsShowFullColumn);
        return excelPackage;
    }
}