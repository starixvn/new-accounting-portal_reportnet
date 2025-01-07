namespace Utilities.ExcelResult;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

public class EpplusExcelResult(ExcelPackage excelPackage, string password = "") : IActionResult
{
    public ExcelPackage ExcelPackage { get; } = excelPackage;

    public string FileName { get; set; } = "Excel.xlsx";

    public async Task ExecuteResultAsync(ActionContext context)
    {
        byte[] bytes;

        using (ExcelPackage)
        {
            bytes = await ExcelPackage.GetAsByteArrayAsync(password);
        }

        using var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new ByteArrayContent(bytes);

        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = FileName
        };

        await response.Content.CopyToAsync(context.HttpContext.Response.Body);
    }
}