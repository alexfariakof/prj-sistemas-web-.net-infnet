using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Application.Administrative.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LiteStreaming.Application.Abstractions;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class UserController : BaseController<AdministrativeAccountDto>
{
    private readonly IService<AdministrativeAccountDto> administrativeAccountService;

    public UserController(IService<AdministrativeAccountDto> administrativeAccountService): base(administrativeAccountService)
    {
        this.administrativeAccountService = administrativeAccountService;
    }

    public override IActionResult Index()
    {
        return View(this.administrativeAccountService.FindAll());
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return CreateView();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Save(AdministrativeAccountDto dto)
    {
        if (ModelState is { IsValid: false })
            return CreateView();

        try
        {
            dto.UsuarioId = UserId;
            administrativeAccountService.Create(dto);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao salvar os dados do usuário.");
            return CreateView();
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Update(AdministrativeAccountDto dto)
    {
        if (ModelState is { IsValid: false })
            return EditView();

        try
        {
            dto.UsuarioId = UserId;
            administrativeAccountService.Update(dto);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao atualizar os dados do usuário { dto?.Name }.");
            return EditView();
        }
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Edit(Guid Id)
    {
        try
        {
            var result = this.administrativeAccountService.FindById(Id);
            return View(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);

            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao editar os dados deste usuário.");
        }
        return View(INDEX, this.administrativeAccountService.FindAll());
    }

    
    public IActionResult Delete(AdministrativeAccountDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.administrativeAccountService.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Success, $"Usuário { dto?.Name } excluído.");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao excluir o usuário { dto?.Name }.");
        }
        return View(INDEX, this.administrativeAccountService.FindAll());

    }
}
