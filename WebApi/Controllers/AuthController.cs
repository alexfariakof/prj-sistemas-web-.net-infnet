using Application.Account.Dto;
using Application.Account.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly ICustomerService _customerService;
    private readonly IMerchantService _merchantService;
    public AuthController(ICustomerService customerService, IMerchantService merchantService)
    {
        _customerService = customerService;
        _merchantService = merchantService;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType((200), Type = typeof(AuthenticationDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult SignIn([FromBody] LoginDto loginDto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        try
        {
            if (loginDto.Type == UserType.Customer)
            {
                var result = _customerService.Authentication(loginDto);

                if (result == null)
                    return BadRequest(new { message = "Erro ao realizar login!" });

                return new OkObjectResult(result);
            }
            else if (loginDto.Type == UserType.Merchant)
            {
                var result = _merchantService.Authentication(loginDto);

                if (result == null)
                    return BadRequest(new { message = "Erro ao realizar login!" });

                return new OkObjectResult(result);
            }

            return BadRequest(new { message = "Erro ao realizar login!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
