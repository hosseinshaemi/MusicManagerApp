using Spotify.Utils.Contracts;
using Spotify.Utils.Models;
namespace Spotify.Utils;

public class EmailSender : IEmailSender
{
    public async Task<bool> SendEmail(Email email)
    {
        return true;
    }
}