namespace PunterAnalysis.AssociatedAccount.Model;

using MediatR;
using OfficeOpenXml;

public class HttpParameterExcel : HttpParameterJson, IRequest<ExcelPackage>
{
    public string Title { get; set; }

    public string Filter { get; set; }
}