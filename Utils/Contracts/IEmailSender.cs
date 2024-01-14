using Spotify.Utils.Models;
namespace Spotify.Utils.Contracts;

public interface IEmailSender
{
    void SendEmail(Email email);
}