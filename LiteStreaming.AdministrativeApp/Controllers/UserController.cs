using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Application.Administrative.Dto;
using Application.Administrative.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class UserController : BaseController
{
    private IAdministrativeAccountService administrativeAccountService;

    public UserController(IAdministrativeAccountService administrativeAccountService)
    {
        this.administrativeAccountService = administrativeAccountService;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View(this.FindAll());
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [Authorize]
    public IActionResult Save(AdministrativeAccountDto dto)
    {
        if (ModelState is { IsValid: false })
            return View("Create");

        try
        {
            dto.UsuarioId = UserId;
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
    [Authorize]
    public IActionResult Update(AdministrativeAccountDto dto)
    {
        if (ModelState is { IsValid: false })
            return View("Edit");

        try
        {
            dto.UsuarioId = UserId;
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

    [Authorize]
    private IEnumerable<AdministrativeAccountDto> FindAll()
    {
        return this.administrativeAccountService.FindAll();
    }

    [Authorize]
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
    
    [Authorize]
    public IActionResult Delete(Guid IdUsuario)
    {
        try
        {
            var dto = this.administrativeAccountService.FindById(IdUsuario);
            dto.UsuarioId = UserId;
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
