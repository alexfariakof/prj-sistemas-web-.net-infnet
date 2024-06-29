﻿using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Application.Administrative.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LiteStreaming.Application.Abstractions;

namespace LiteStreaming.AdministrativeApp.Controllers;

[Authorize]
public class UserController : BaseController<AdministrativeAccountDto>
{
    public UserController(IService<AdministrativeAccountDto> administrativeAccountService): base(administrativeAccountService)  { }

    [Authorize(Roles = "Admin")]
    public override IActionResult Create()
    {
        return View(CREATE);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Save(AdministrativeAccountDto dto)
    {
        if (ModelState is { IsValid: false })
            return this.Create();

        try
        {
            dto.UsuarioId = UserId;
            this.Services.Create(dto);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argSaveEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argSaveEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao salvar os dados do usuário.");
            return this.Create();
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
            this.Services.Update(dto);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argUpdateEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argUpdateEx.Message);
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
            var result = this.Services.FindById(Id);
            return View(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEditEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEditEx.Message);

            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao editar os dados deste usuário.");
        }
        return IndexView();
    }

    
    public IActionResult Delete(AdministrativeAccountDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.Services.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Success, $"Usuário { dto?.Name } excluído.");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argDeleteEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argDeleteEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao excluir o usuário { dto?.Name }.");
        }
        return IndexView();
    }
}