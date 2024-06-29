using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using Microsoft.AspNetCore.Authorization;
using LiteStreaming.Application.Abstractions;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class BandController : BaseController<BandDto>
{
    public BandController(IService<BandDto> services): base(services) { }

    [HttpPost]
    [Authorize]
    public IActionResult Save(BandDto dto)
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
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao salvar os dados da banda.");
            return Create();
        }
    }

    [HttpPost]
    [Authorize]
    public IActionResult Update(BandDto dto)
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
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao atualizar a banda { dto?.Name }.");
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
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao editar os dados desta banda.");
        }
        return IndexView();
    }

    [Authorize]
    public IActionResult Delete(BandDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.Services.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Success, $"Banda { dto?.Name } excluída.");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argDeleteEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argDeleteEx.Message);

            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao excluir a banda { dto?.Name }.");
        }
        return IndexView();
    }
}