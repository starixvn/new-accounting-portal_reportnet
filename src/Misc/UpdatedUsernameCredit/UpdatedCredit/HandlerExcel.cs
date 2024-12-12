using AutoMapper;
using MediatR;
using OfficeOpenXml;
using UpdatedUsernameCredit.Common;

namespace UpdatedUsernameCredit.UpdatedCredit
{
    public class HandlerExcel(
        UpdatedCreditService service,
        UpdatedCreditExcelSetting excelSetting,
        ExcelBuilder excelBuilder,
        IMapper mapper)
        : IRequestHandler<ExcelMessage, ExcelPackage>
    {
        public async Task<ExcelPackage> Handle(ExcelMessage request, CancellationToken cancellationToken)
        {
            var model = await service.GetUpdatedCredit(request);

            var excelViewModel = mapper.Map<IEnumerable<UpdatedCreditModel>, List<UpdatedCreditExcelModel>>(
                model,
                option =>
                   {
                       option.AfterMap((source, destination) =>
                       {
                           var index = 0;
                           foreach (var item in destination)
                           {
                               item.Index = index + 1;
                               index++;
                           }
                       });
                   });

            var excelSettings = excelSetting.CreateSetting(request);

            var excelPackage = excelBuilder.CreateExcelPackage(excelViewModel, excelSettings);

            return excelPackage;
        }
    }
}