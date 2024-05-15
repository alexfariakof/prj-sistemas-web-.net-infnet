using Application.Administrative.Dto;
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

    [HttpPost]
    public IActionResult Save(AdministrativeAccountDto dto)
    {
        if (ModelState is { IsValid: false })
            return View("Create");

        try
        {
            administrativeAccountService.Create(dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.ErrorMessage = argEx.Message;
            else
                ViewBag.ErrorMessage = "Ocorreu um erro ao salvar os dados.";
            return View("Create");
        }
    }

}
