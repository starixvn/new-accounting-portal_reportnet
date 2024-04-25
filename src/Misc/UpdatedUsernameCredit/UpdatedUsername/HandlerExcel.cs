using AutoMapper;
using MediatR;
using OfficeOpenXml;
using UpdatedUsernameCredit.Common;

namespace UpdatedUsernameCredit.UpdatedUsername
{
    public class HandlerExcel : IRequestHandler<ExcelMessage, ExcelPackage>
    {
        private readonly UpdatedUsernameService service;
        private readonly UpdatedUsernameExcelSetting excelSetting;
        private readonly ExcelBuilder excelBuilder;
        private readonly IMapper mapper;

        public HandlerExcel(UpdatedUsernameService service, UpdatedUsernameExcelSetting excelSetting, ExcelBuilder excelBuilder, IMapper mapper)
        {
            this.service = service;
            this.excelSetting = excelSetting;
            this.excelBuilder = excelBuilder;
            this.mapper = mapper;
        }

        public async Task<ExcelPackage> Handle(ExcelMessage request, CancellationToken cancellationToken)
        {
            var model = await service.GetUpdatedUsername(request);

            var excelViewModel = mapper.Map<IEnumerable<UpdatedUsernameModel>, List<UpdatedUsernameExcelModel>>(
                model,
                option =>
                {
                    option.AfterMap((source, destination) =>
                    {
                        int index = 0;
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