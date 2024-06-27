using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using Application.Streaming.Dto.Interfaces;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class BandController : BaseController
{
    private readonly IBandService bandService;
    public BandController(IBandService bandService)
    {
        this.bandService = bandService;
    }

    public IActionResult Index()
    {
        return View(this.bandService.FindAll());
    }

    public IActionResult Create()
    {
        return CreateView();
    }

    [HttpPost]
    public IActionResult Save(BandDto dto)
    {
        if (ModelState is { IsValid: false })
            return CreateView();

        try
        {
            dto.UsuarioId = UserId;
            bandService.Create(dto);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao salvar os dados da banda.");
            return CreateView();
        }
    }

    [HttpPost]
    public IActionResult Update(BandDto dto)
    {
        if (ModelState is { IsValid: false })
            return EditView();

        try
        {
            dto.UsuarioId = UserId;
            bandService.Update(dto);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao atualizar a banda { dto?.Name }.");
            return EditView();
        }
    }

    public IActionResult Edit(Guid Id)
    {
        try
        {
            var result = this.bandService.FindById(Id);
            return View(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao editar os dados desta banda.");
        }
        return View(INDEX, this.bandService.FindAll());
    }

    public IActionResult Delete(BandDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.bandService.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Success, $"Banda { dto?.Name } excluída.");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);

            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao excluir a banda { dto?.Name }.");
        }
        return View(INDEX, this.bandService.FindAll());
    }
}