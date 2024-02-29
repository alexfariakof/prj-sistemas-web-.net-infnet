using Application;
using Application.Account.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private IService<CustomerDto> _customerService;

    public CustomerController(IService<CustomerDto> customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{id}")]
    public IActionResult FindById(Guid id)
    {
        var result = this._customerService.FindById(id);

        if (result == null)
            return NotFound();

        return Ok(result);

    }

    [HttpPost]
    public IActionResult Create([FromBody] CustomerDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        var result = this._customerService.Create(dto);

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(CustomerDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        var result = this._customerService.Update(dto);

        return Ok(result);
    }

    [HttpDelete]
    public IActionResult Delete(CustomerDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        var result = this._customerService.Delete(dto);

        return Ok(result);
    }
}
