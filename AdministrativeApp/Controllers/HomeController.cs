using AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Application.Administrative.Interfaces;
using Application.Shared.Dto;

namespace AdministrativeApp.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAuthenticationService authenticationService;

    public HomeController(ILogger<HomeController> logger, IAuthenticationService authenticationService)
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
            var result = authenticationService.Authentication(dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.ErrorMessage = argEx.Message;
            else
                ViewBag.ErrorMessage = "Ocorreu um erro ao realizar login.";
            return View("Index");
        }
    }
}
