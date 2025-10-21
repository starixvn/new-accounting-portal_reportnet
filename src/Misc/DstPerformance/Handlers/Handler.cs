using AutoMapper;
using DstPerformance.Criteria;
using DstPerformance.Models;
using DstPerformance.Requests;
using DstPerformance.Services;
using MediatR;

namespace DstPerformance.Handlers;

public class Handler(DstPerformanceService service, IMapper mapper) : IRequestHandler<SportRequest, SportModel>
{
    public Task<SportModel> Handle(SportRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<ReportCriteria>(request);
        return service.GetSports(criteria);
    }
}