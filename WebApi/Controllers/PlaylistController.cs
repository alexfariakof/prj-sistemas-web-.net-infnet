using Application;
using Application.Account.Dto;
using Application.Account.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class PlaylistController : ControllerBase
{
    private readonly IService<PlaylistDto> _playlistService;
    public PlaylistController(IService<PlaylistDto> playlistService)
    {
        _playlistService = playlistService;
    }

    [HttpGet]
    [ProducesResponseType((200), Type = typeof(List<PlaylistDto>))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((404), Type = null)]
    [Authorize("Bearer")]
    public IActionResult FindAll()
    {
        if (UserType != UserType.Customer) return Unauthorized();

        try
        {
            var result = this._playlistService.FindAll(UserIdentity);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{playlistId}")]
    [ProducesResponseType((200), Type = typeof(PlaylistDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((404), Type = null)]
    [Authorize("Bearer")]
    public IActionResult FindById([FromRoute] Guid playlistId)
    {
        if (UserType != UserType.Customer) return Unauthorized();

        try
        {
            var result = this._playlistService.FindById(playlistId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType((200), Type = typeof(PlaylistDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [Authorize("Bearer")]
    public IActionResult Create([FromBody] PlaylistDto dto)
    {

        if (ModelState is { IsValid: false })
            return BadRequest();
        try
        {
            var result = this._playlistService.Create(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPut]
    [ProducesResponseType((200), Type = typeof(PlaylistDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [Authorize("Bearer")]
    public IActionResult Update(PlaylistDto dto)
    {
        if (UserType != UserType.Customer) return Unauthorized();

        if (ModelState is { IsValid: false })
            return BadRequest();

        try
        {
            var result = this._playlistService.Update(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    [Authorize("Bearer")]
    public IActionResult Delete(PlaylistDto dto)
    {
        if (UserType != UserType.Customer) return Unauthorized();

        if (ModelState is { IsValid: false })
            return BadRequest();

        try
        {
            var result = this._playlistService.Delete(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}
