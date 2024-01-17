using Microsoft.AspNetCore.Mvc;
namespace Spotify.Utils;

public static class Extensions
{
    public static string? UrlGenerator(this IUrlHelper url, string action, string controller, object parameters)
    {
        return url.Action(
            action,
            controller,
            parameters,
            url.ActionContext.HttpContext.Request.Scheme,
            url.ActionContext.HttpContext.Request.Host.Value,
            null
        );
    }
}