using Spotify.Models;
using Microsoft.AspNetCore.Mvc;
using Spotify.Data.Repositories.Contracts;
using Spotify.Models.DTOs;
namespace Spotify.Controllers;

public class HomeController : Controller
{
    private readonly IMusicRepository _musicRepository;

    public HomeController(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;    
    }

    public async Task<IActionResult> Index()
    {
        IReadOnlyList<Music> musics = await _musicRepository.GetAllMusicsWithArtist();
        ShowMusicDto smd = new() { Musics = musics };
        return View(smd);
    }

    public IActionResult Error()
    {
        return View("_Error");
    }
}
