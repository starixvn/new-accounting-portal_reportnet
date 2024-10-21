using Microsoft.AspNetCore.Mvc;

namespace SaveBetReport.Models;

public class ApiResult
{
    public bool Success { get; set; }

    public string Error { get; set; }

    public FileContentResult Data { get; set; }
}