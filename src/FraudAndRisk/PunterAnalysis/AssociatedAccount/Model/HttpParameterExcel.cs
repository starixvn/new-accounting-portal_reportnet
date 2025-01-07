using MediatR;
using OfficeOpenXml;

namespace PunterAnalysis.AssociatedAccount.Model;

public class HttpParameterExcel : HttpParameterJson, IRequest<ExcelPackage>
{
    public string Title { get; set; }

    public string Filter { get; set; }
}