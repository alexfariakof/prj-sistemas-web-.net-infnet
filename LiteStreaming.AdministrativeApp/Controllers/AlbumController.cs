using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using Application.Streaming.Dto.Interfaces;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class AlbumController : BaseController
{
    private readonly IAlbumService albumService;
    public AlbumController(IAlbumService albumService)
    {
        this.albumService = albumService;
    }

    public IActionResult Index()
    {
        return View(this.albumService.FindAll());
    }

    public IActionResult Create()
    {
        return CreateView();
    }

    [HttpPost]
    public IActionResult Save(AlbumDto dto)
    {
        if (ModelState is { IsValid: false })
            return CreateView();

        try
        {
            dto.UsuarioId = UserId;
            albumService.Create(dto);
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
    public IActionResult Update(AlbumDto dto)
    {
        if (ModelState is { IsValid: false })
            return EditView();

        try
        {
            dto.UsuarioId = UserId;
            albumService.Update(dto);
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

    public IActionResult Edit(Guid Id)
    {
        try
        {
            var result = this.albumService.FindById(Id);
            return View(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao editar os dados deste plano.");
        }
        return View(INDEX, this.albumService.FindAll());
    }

    public IActionResult Delete(AlbumDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.albumService.Delete(dto);
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
        return View(INDEX, this.albumService.FindAll());
    }
}