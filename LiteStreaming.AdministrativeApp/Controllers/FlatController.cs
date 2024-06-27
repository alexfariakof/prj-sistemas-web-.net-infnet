using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using Microsoft.AspNetCore.Authorization;
using LiteStreaming.Application.Abstractions;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class FlatController : BaseController<FlatDto>
{
    private readonly IService<FlatDto> flatService;
    public FlatController(IService<FlatDto> flatService): base(flatService)
    {
        this.flatService = flatService;
    }


    [Authorize]
    public IActionResult Create()
    {
        return CreateView();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Save(FlatDto dto)
    {
        if (ModelState is { IsValid: false })
            return CreateView();

        try
        {
            dto.UsuarioId = UserId;
            flatService.Create(dto);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao salvar os dados do plano.");
            return CreateView();
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
            flatService.Update(dto);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
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
            var result = this.flatService.FindById(Id);
            return View(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao editar os dados deste plano.");
        }
        return View(INDEX, this.flatService.FindAll());
    }

    [Authorize]
    public IActionResult Delete(FlatDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.flatService.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Success, $"Plano { dto?.Name } excluído.");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);

            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao excluir o plano { dto?.Name }.");
        }
        return View(INDEX, this.flatService.FindAll());
    }
}