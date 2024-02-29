using Application;
using Application.Account.Dto;
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

    [HttpGet("{id}")]
    public IActionResult FindById(Guid id)
    {
        var result = this._merchantService.FindById(id);

        if (result == null)
            return NotFound();

        return Ok(result);

    }

    [HttpPost]
    public IActionResult Create([FromBody] MerchantDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        var result = this._merchantService.Create(dto);

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(MerchantDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        var result = this._merchantService.Update(dto);

        return Ok(result);
    }

    [HttpDelete]
    public IActionResult Delete(MerchantDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        var result = this._merchantService.Delete(dto);

        return Ok(result);
    }
}
