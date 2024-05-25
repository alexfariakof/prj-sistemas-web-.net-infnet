using AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Application.Administrative.Interfaces;
using Application.Shared.Dto;
using AdministrativeApp.Controllers.Abastractions;

namespace AdministrativeApp.Controllers;
public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAuthenticationService authenticationService;

    public HomeController(ILogger<HomeController> logger, IAuthenticationService authenticationService) : base()
    {
        _logger = logger;
        this.authenticationService = authenticationService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult SingIn(LoginDto dto)
    {
        if (ModelState is { IsValid: false })
            return View("Index");

        try
        {
            var accountDto = authenticationService.Authentication(dto);
            HttpContext.Session.SetString("UserId", accountDto.Id.ToString());
            HttpContext.Session.SetString("UserName", accountDto.Name);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };
            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao realizar login." };
            return View("Index");
        }
    }

    public IActionResult LogOut()
    {
        ClaerSession();
        return RedirectToAction("Index");
    }

}
