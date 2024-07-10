using AutoMapper;
using MediatR;
using OfficeOpenXml;
using ResultAnalysis.Criteria;
using ResultAnalysis.Models;
using ResultAnalysis.Requests;
using ResultAnalysis.Services;

namespace ResultAnalysis.Handlers
{
    public class ResultAnalysisExcelHandler: IRequestHandler<ResultAnalysisExcelRequest, ExcelPackage>
	{
		private readonly ResultAnalysisService service;
		private readonly ExcelBuilderService excelService;
		private readonly IMapper mapper;

		public ResultAnalysisExcelHandler(ResultAnalysisService service, ExcelBuilderService excelService, IMapper mapper)
		{
			this.service = service;
			this.excelService = excelService;
			this.mapper = mapper;
		}

		public async Task<ExcelPackage> Handle(ResultAnalysisExcelRequest request, CancellationToken cancellationToken)
		{
			var criteria = mapper.Map<ResultAnalysisCriteria>(request);
			var data = await service.GetResultAnalysis(criteria);
			var excelModel = mapper.Map<ResultAnalysisExcelModel>(data);
			var title = $"Result Analysis Report From {criteria.FromDate:MMM dd, yyyy} To {criteria.ToDate:MMM dd, yyyy}";
			return excelService.Build(title, excelModel, request.SportId);
		}
	}
}
