namespace Spotify.Services.Contracts;

public interface IViewRenderService
{
    Task<string> RenderToStringAsync(string viewName, object model);
}