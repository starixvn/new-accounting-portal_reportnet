namespace UpdatedUsernameCredit.UpdatedUsername;

using AutoMapper;
using Common;
using MediatR;
using OfficeOpenXml;

public class HandlerExcel(
    UpdatedUsernameService service,
    UpdatedUsernameExcelSetting excelSetting,
    ExcelBuilder excelBuilder,
    IMapper mapper)
    : IRequestHandler<ExcelMessage, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(ExcelMessage request, CancellationToken cancellationToken)
    {
        var model = await service.GetUpdatedUsername(request);

        var excelViewModel = mapper.Map<IEnumerable<UpdatedUsernameModel>, List<UpdatedUsernameExcelModel>>(
            model,
            option =>
            {
                option.AfterMap((_, destination) =>
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