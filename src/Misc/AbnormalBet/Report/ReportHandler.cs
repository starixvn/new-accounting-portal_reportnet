using AbnormalBet.Criteria;
using AbnormalBet.HttpParameter;
using AbnormalBet.Model;
using AutoMapper;
using MediatR;

namespace AbnormalBet.Report
{
    public class ReportHandler : IRequestHandler<LicenseeHttpParameter, LicenseeModel>,
         IRequestHandler<MemberHttpParameter, MemberModel>
    {
        private readonly ReportService service;

        private readonly IMapper mapper;

        public ReportHandler(ReportService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<MemberModel> Handle(MemberHttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<MemberQueryCriteria>(request);
            return await service.QueryMember(criteria);
        }

        public async Task<LicenseeModel> Handle(LicenseeHttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<LicenseeQueryCriteria>(request);
            return await service.QueryLicensee(criteria);
        }
    }
}