using Spotify.Utils.Models;
namespace Spotify.Utils.Contracts;

public interface IEmailSender
{
    Task<bool> SendEmail(Email email);
}