using AdministrativeApp.Controllers.Abastractions;
using AdministrativeApp.Models;
using Application.Administrative.Dto;
using Application.Administrative.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrativeApp.Controllers;
public class UserController : BaseController
{
    private IAdministrativeAccountService administrativeAccountService;

    public UserController(IAdministrativeAccountService administrativeAccountService)
    {
        this.administrativeAccountService = administrativeAccountService;
    }

    public IActionResult Index()
    {
        return View(this.FindAll());
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
            dto.UsuarioId = this.UserId.Value;
            administrativeAccountService.Create(dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };
            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao salvar os dados." };
            return View("Create");
        }
    }

    [HttpPost]
    public IActionResult Update(AdministrativeAccountDto dto)
    {
        if (ModelState is { IsValid: false })
            return View("Edit");

        try
        {
            dto.UsuarioId = this.UserId.Value;
            administrativeAccountService.Update(dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "danger", Message = argEx.Message };
            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao atualizar os dados deste usuário." };
            return View("Edit");
        }
    }

    private IEnumerable<AdministrativeAccountDto> FindAll()
    {
        return this.administrativeAccountService.FindAll();
    }

    public IActionResult Edit(Guid IdUsuario)
    {
        try
        {
            var result = this.administrativeAccountService.FindById(IdUsuario);
            return View(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };

            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao editar os dados deste usuário." };
        }
        return View("Index", this.FindAll());
    }

    public IActionResult Delete(Guid IdUsuario)
    {
        try
        {
            var dto = this.administrativeAccountService.FindById(IdUsuario);
            dto.UsuarioId = this.UserId.Value;
            var result = this.administrativeAccountService.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel {  Header = "Sucesso", Type= "success", Message = "Usuário inativado." };
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };

            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao excluir os dados deste usuário." };
        }
        return View("Index", this.FindAll());
    }
}
