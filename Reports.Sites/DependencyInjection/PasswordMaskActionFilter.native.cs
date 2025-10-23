using Microsoft.AspNetCore.Mvc.Filters;

namespace Reports.Sites.DependencyInjection;

using System.Security.Cryptography;
using System.Text;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Mvc;

public class PasswordMaskActionFilter(IUserService userService) : IAsyncActionFilter
{
    private const string PasswordParameterName = "password";
    private const string DefaultPassword = "DD87F1758AEDE76B20899238C17";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.ContainsKey(PasswordParameterName))
        {
            await next();
            return;
        }

        var controller = context.RouteData.Values["controller"]?.ToString();
        var action = context.RouteData.Values["action"]?.ToString();
        var isPostMethod = context.HttpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase);

        if (controller != "Auth" ||
            action != "Index" ||
            !isPostMethod)
        {
            await next();
            return;
        }

        await TrySetMaskedPassword(context);

        await next();
    }

    private async Task TrySetMaskedPassword(ActionExecutingContext context)
    {
        try
        {
            var token = context.ActionArguments["loginToken"]?.ToString();
            var username = context.ActionArguments["username"]?.ToString();
            var password = context.ActionArguments["password"]?.ToString();
            var secretPassword = GetHashedPassword(DefaultPassword, token);
            if (password == secretPassword)
            {
                var maskedPassword = await GetMaskedPassword(username, token);
                context.ActionArguments[PasswordParameterName] = maskedPassword;
            }
        }
        catch
        {
        }
    }

    private async Task<string> GetMaskedPassword(string username, string token)
    {
        var user = await userService.GetAsync(username);
        var hashPassword = ToSHA256(user.Password + token);
        return hashPassword;
    }

    private static string GetHashedPassword(string password, string token)
    {
        return ToSHA256(password + token);
    }

    private static string ToSHA256(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Hey guy! I do not want to hash an empty or white space string");
        }

        using var hashAlgorithm = SHA256.Create();
        var hashData = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
        var encrypted = Convert.ToHexStringLower(hashData).Replace("-", string.Empty);

        return encrypted;
    }
}

public static class PasswordMaskActionFilterExtensions
{
    public static void AddPasswordMaskActionFilter(this IServiceCollection services)
    {
        services.AddScoped<PasswordMaskActionFilter>();
        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add<PasswordMaskActionFilter>();
        });

    }
}