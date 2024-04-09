using AbnormalBet.Criteria;
using AbnormalBet.HttpParameter;
using AbnormalBet.Model;
using AutoMapper;
using MediatR;
using OfficeOpenXml;

namespace AbnormalBet.Report
{
    public class ReportHandlerExcel : IRequestHandler<LicenseeHttpParameterExcel, ExcelPackage>,
        IRequestHandler<MemberHttpParameterExcel, ExcelPackage>
    {
        private readonly ReportService service;

        private readonly ReportExcelBuilder excelBuilder;

        private readonly IMapper mapper;

        public ReportHandlerExcel(ReportService service, ReportExcelBuilder excelBuilder, IMapper mapper)
        {
            this.service = service;
            this.excelBuilder = excelBuilder;
            this.mapper = mapper;
        }

        public async Task<ExcelPackage> Handle(MemberHttpParameterExcel request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<MemberQueryCriteria>(request);
            var memberModel = await service.QueryMember(criteria);
            var memberExcelModel = mapper.Map<IEnumerable<MemberExcelModel>>(memberModel.Members);
            var excelPackage = excelBuilder.Build(request.Title, memberExcelModel);
            return excelPackage;
        }

        public async Task<ExcelPackage> Handle(LicenseeHttpParameterExcel request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<LicenseeQueryCriteria>(request);
            var licenseeModel = await service.QueryLicensee(criteria);
            var licenseeExcelModel = mapper.Map<IEnumerable<LicenseeExcelModel>>(licenseeModel.Licensees);
            var excelPackage = excelBuilder.Build(request.Title, licenseeExcelModel);
            return excelPackage;
        }
    }
}