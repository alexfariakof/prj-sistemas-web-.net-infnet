using Application.Administrative.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrativeApp.Controllers;
public class UserController : Controller
{
    private IAdministrativeAccountService administrativeAccountService;

    public UserController(IAdministrativeAccountService administrativeAccountService)
    {
        this.administrativeAccountService = administrativeAccountService;
    }

    public IActionResult Index()
    {
        var result = this.administrativeAccountService.FindAll();
        return View(result);
    }

    public IActionResult Create()
    {
        return View();
    }
}
