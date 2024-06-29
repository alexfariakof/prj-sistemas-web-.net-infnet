using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using Microsoft.AspNetCore.Authorization;
using LiteStreaming.Application.Abstractions;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class FlatController : BaseController<FlatDto>
{
    public FlatController(IService<FlatDto> services): base(services)
    {
    }

    [HttpPost]
    [Authorize]
    public IActionResult Save(FlatDto dto)
    {
        if (ModelState is { IsValid: false })
            return Create();

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
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao salvar os dados do plano.");
            return Create();
        }
    }

    [HttpPost]
    [Authorize]
    public IActionResult Update(FlatDto dto)
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
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao atualizar o plano { dto?.Name }.");
            return EditView();
        }
    }

    [Authorize]
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
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao editar os dados deste plano.");
        }
        return IndexView();
    }

    [Authorize]
    public IActionResult Delete(FlatDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.Services.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Success, $"Plano { dto?.Name } excluído.");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argDeleteEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argDeleteEx.Message);

            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao excluir o plano { dto?.Name }.");
        }
        return IndexView();
    }
}