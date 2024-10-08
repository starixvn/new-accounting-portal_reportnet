namespace Utilities.Controller;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class BaseController: Controller
{
    public override JsonResult Json(object data)
    {
        var settings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        };

        return new JsonResult(data, settings);
    }
}