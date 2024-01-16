using Spotify.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Spotify.Services.Contracts;
using Spotify.Data.Repositories.Contracts;
using Spotify.Models;
namespace Spotify.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "AdminPolicy")]
public class HomeController : Controller
{
    private readonly IAws3Services _aws3Services;
    private readonly IMusicRepository _musicRepository;
    private readonly IArtistRepository _artistRepository;

    public HomeController(IAws3Services aws3Services, IMusicRepository musicRepository, IArtistRepository artistRepository)
    {
        _aws3Services = aws3Services;
        _musicRepository = musicRepository;
        _artistRepository = artistRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AddMusic()
    {
        ViewBag.Success = false;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddMusic(AddMusicDto musicDto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Success = false;
            return View(model: musicDto);
        }

        Artist? artist = null;
        if (!await _artistRepository.IsExistedArtistByName(musicDto.Artist))
        {
            artist = new() { Name = musicDto.Artist };
            await _artistRepository.Add(artist);
        }
        else
        {
            artist = await _artistRepository.GetArtistByName(musicDto.Artist);
        }


        string? result = await _aws3Services.UploadFile(musicDto.Music);
        Music music = new()
        {
            Title = musicDto.Title,
            Link = result,
            ArtistId = artist.Id
        };

        await _musicRepository.Add(music);
        ViewBag.Success = true;
        return View();
    }
}
