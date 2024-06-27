﻿using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using LiteStreaming.Application.Abstractions;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class GenreController : BaseController<GenreDto>
{
    private readonly IService<GenreDto> genreService;
    public GenreController(IService<GenreDto> genreService): base(genreService)
    {
        this.genreService = genreService;
    }

    public override IActionResult Index()
    {
        return View(this.genreService.FindAll());
    }

    public IActionResult Create()
    {
        return CreateView();
    }

    [HttpPost]
    public IActionResult Save(GenreDto dto)
    {
        if (ModelState is { IsValid: false })
            return CreateView();

        try
        {
            dto.UsuarioId = UserId;
            genreService.Create(dto);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao salvar os dados do gênero.");
            return CreateView();
        }
    }

    [HttpPost]
    public IActionResult Update(GenreDto dto)
    {
        if (ModelState is { IsValid: false })
            return EditView();

        try
        {
            dto.UsuarioId = UserId;
            genreService.Update(dto);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao atualizar o gênero {dto?.Name}.");
            return EditView();
        }
    }

    public IActionResult Edit(Guid Id)
    {
        try
        {
            var result = this.genreService.FindById(Id);
            return View(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);

            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao editar os dados deste gênero.");
        }
        return View(INDEX, this.genreService.FindAll());
    }

    public IActionResult Delete(GenreDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.genreService.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Success, $"Gênero {dto?.Name} excluído.");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);

            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao excluir o gênero {dto?.Name}.");
        }
        return View(INDEX, this.genreService.FindAll());
    }
} 