namespace BetConstructPerformance.Handlers;

using AutoMapper;
using Criteria;
using MediatR;
using Models;
using Requests;
using Services;

public class Handler(BetConstructPerformanceService service, IMapper mapper) : IRequestHandler<SportRequest, SportModel>
{
    public Task<SportModel> Handle(SportRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<SportCriteria>(request);
        return service.GetSports(criteria);
    }
}