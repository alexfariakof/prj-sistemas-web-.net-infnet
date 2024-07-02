using Application.Streaming.Dto;
using LiteStreaming.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Abstractions;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaylistController : UnitControllerBase
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
    public IActionResult FindAll()
    {
        try
        {
            var result = this._playlistService.FindAll();
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
    [ProducesResponseType((403))]
    [Authorize]
    public IActionResult FindById([FromRoute] Guid playlistId)
    {
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
    [ProducesResponseType((403))]
    [Authorize]
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
    [ProducesResponseType((403))]
    [Authorize]
    public IActionResult Update(PlaylistDto dto)
    {
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
    [ProducesResponseType((403))]
    [Authorize]
    public IActionResult Delete(PlaylistDto dto)
    {
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
