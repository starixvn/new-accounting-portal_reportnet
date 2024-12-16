using AutoMapper;
using BetConstructPerformance.Criteria;
using BetConstructPerformance.Models;
using BetConstructPerformance.Requests;
using BetConstructPerformance.Services;
using MediatR;

namespace BetConstructPerformance.Handlers;

public class Handler(BetConstructPerformanceService service, IMapper mapper) : IRequestHandler<SportRequest, SportModel>
{
    public Task<SportModel> Handle(SportRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<SportCriteria>(request);
        return service.GetSports(criteria);
    }
}