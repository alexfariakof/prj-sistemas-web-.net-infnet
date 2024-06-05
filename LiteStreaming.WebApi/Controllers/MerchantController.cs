using Application;
using Application.Streaming.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Abstractions;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MerchantController : ControllerBaseTokensProps
{
    private readonly IService<MerchantDto> _merchantService;
    public MerchantController(IService<MerchantDto> merchantService)
    {
        _merchantService = merchantService;
    }

    [HttpGet]
    [ProducesResponseType((200), Type = typeof(MerchantDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((404), Type = null)]
    [ProducesResponseType((403))]
    [Authorize("Bearer", Roles = "Admin, Normal, Merchant")]
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
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType((200), Type = typeof(MerchantDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((403))]
    [Authorize("Bearer", Roles = "Admin, Normal")]
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
            return BadRequest(ex.Message);
        }

    }

    [HttpPut]
    [ProducesResponseType((200), Type = typeof(MerchantDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((403))]
    [Authorize("Bearer", Roles = "Admin, Normal")]
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
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((403))]
    [Authorize("Bearer", Roles = "Admin, Normal")]
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
            return BadRequest(ex.Message);
        }

    }
}
