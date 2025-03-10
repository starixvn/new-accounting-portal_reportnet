namespace SaveBetReport.Models;

using Microsoft.AspNetCore.Mvc;

public class ApiResult
{
    public bool Success { get; set; }

    public string Error { get; set; }

    public FileContentResult Data { get; set; }
}