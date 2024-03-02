using Application;
using Application.Account.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class MerchantController : ControllerBase
{
    private readonly IService<MerchantDto> _merchantService;
    public MerchantController(IService<MerchantDto> merchantService)
    {
        _merchantService = merchantService;
    }

    [HttpGet]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(MerchantDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((404), Type = null)]
    public IActionResult FindById()
    {
        try
        {
            var result = this._merchantService.FindById(UserIdentity);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType((200), Type = typeof(MerchantDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult Create([FromBody] MerchantDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();
        try
        {
            var result = this._merchantService.Create(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }

    [HttpPut]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(MerchantDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult Update(MerchantDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        try
        {
            dto.Id = UserIdentity;
            var result = this._merchantService.Update(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }

    [HttpDelete]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult Delete(MerchantDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        try
        {

            dto.Id = UserIdentity;
            var result = this._merchantService.Delete(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }
}
