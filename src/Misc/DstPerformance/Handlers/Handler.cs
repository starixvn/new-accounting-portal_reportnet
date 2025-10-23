using AutoMapper;
using DstPerformance.Criteria;
using DstPerformance.Models;
using DstPerformance.Requests;
using DstPerformance.Services;
using MediatR;

namespace DstPerformance.Handlers;

public class Handler(DstPerformanceService service, IMapper mapper) : IRequestHandler<ReportRequest, ReportModel>
{
    public Task<ReportModel> Handle(ReportRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<ReportCriteria>(request);
        return service.GetData(criteria);
    }
}