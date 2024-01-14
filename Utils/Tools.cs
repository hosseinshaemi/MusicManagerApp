namespace Spotify.Utils;

public static class Tools
{
    public static string TokenGenerator()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }
}