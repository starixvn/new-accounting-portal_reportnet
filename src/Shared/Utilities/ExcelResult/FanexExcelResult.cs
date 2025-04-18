namespace Utilities.ExcelResult;

using System.Net;
using System.Net.Http.Headers;
using Fanex.Excel;
using Fanex.Excel.Models;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;

public class FanexExcelResult : IActionResult
{
    public FanexExcelResult(GridOptions gridOptions, string sheetName)
    {
        var excel = new FanexExcel(gridOptions ?? new GridOptions());
        Workbook = excel.ToExcel2003(sheetName);
    }

    public FanexExcelResult(List<GridOptions> listGridOptions, string sheetName)
    {
        var excel = new FanexExcel(listGridOptions ?? []);
        Workbook = excel.ToExcel2003(sheetName);
    }

    public IWorkbook Workbook { get; }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        using var memoryStream = new MemoryStream();
        Workbook.Write(memoryStream);
        using var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new ByteArrayContent(memoryStream.ToArray());
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = "undefined.xls"
        };
        await response.Content.CopyToAsync(context.HttpContext.Response.Body);
    }
}