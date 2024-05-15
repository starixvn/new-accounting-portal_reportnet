using AutoMapper;
using Fanex.Data.Repository;
using MediatR;
using OfficeOpenXml;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Shared
{
    public class HandlerExcelBase
    {
        private readonly PunterAnalysisService punterAnalysisService;

        private readonly PunterAnalysisExcelBuilder excelBuilder;

        private readonly IMapper mapper;    

        protected HandlerExcelBase(
            PunterAnalysisService punterAnalysisService,
            PunterAnalysisExcelBuilder excelBuilder,
            IMapper mapper)
        {
            this.punterAnalysisService = punterAnalysisService;
            this.excelBuilder = excelBuilder;
            this.mapper = mapper;
        }

        protected async Task<ExcelPackage> Handle<TCriteria, TModel, TExcelModel>(IRequest<ExcelPackage> message, ExcelSetting excelSetting)
            where TCriteria : ICriteria
            where TModel : class, new()
            where TExcelModel : class, new()
        {
            var criteria = mapper.Map<TCriteria>(message);
            var model = await punterAnalysisService.Query<TModel>(criteria);
            var excelViewModel = mapper.Map<SummaryEntity<TExcelModel>>(model);
            var excelPackage = excelBuilder.CreateExcelPackage(excelViewModel, excelSetting);
            return excelPackage;
        }
    }
}