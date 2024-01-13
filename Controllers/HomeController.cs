using Microsoft.AspNetCore.Mvc;
namespace Spotify.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Ok("This is home controller");
    }
}
