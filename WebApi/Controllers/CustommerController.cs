using Application.Account;
using Application.Conta.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public IActionResult Criar(CustomerDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        var result = this._customerService.Create(dto);

        return Ok(result);
    }


    [HttpGet("{id}")]
    public IActionResult FindById(Guid id)
    {
        var result = this._customerService.FindById(id);

        if (result == null)
            return NotFound();

        return Ok(result);

    }

}
