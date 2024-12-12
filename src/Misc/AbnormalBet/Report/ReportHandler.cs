using AbnormalBet.Criteria;
using AbnormalBet.HttpParameter;
using AbnormalBet.Model;
using AutoMapper;
using MediatR;

namespace AbnormalBet.Report
{
    public class ReportHandler(ReportService service, IMapper mapper)
        : IRequestHandler<LicenseeHttpParameter, LicenseeModel>,
            IRequestHandler<MemberHttpParameter, MemberModel>
    {
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