﻿using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using Application.Streaming.Dto.Interfaces;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class FlatController : BaseController
{
    private readonly IFlatService flatService;
    public FlatController(IFlatService flatService)
    {
        this.flatService = flatService;
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
    public IActionResult Save(FlatDto dto)
    {
        if (ModelState is { IsValid: false })
            return View("Create");

        try
        {
            dto.UsuarioId = UserId;
            flatService.Create(dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };
            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao salvar os dados do plano." };
            return View("Create");
        }
    }

    [HttpPost]
    public IActionResult Update(FlatDto dto)
    {
        if (ModelState is { IsValid: false })
            return View("Edit");

        try
        {
            dto.UsuarioId = UserId;
            flatService.Update(dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "danger", Message = argEx.Message };
            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = $"Ocorreu um erro ao atualizar o plano { dto?.Name }." };
            return View("Edit");
        }
    }

    private IEnumerable<FlatDto> FindAll()
    {
        return this.flatService.FindAll();            
    }

    public IActionResult Edit(Guid IdUsuario)
    {
        try
        {
            var result = this.flatService.FindById(IdUsuario);
            return View(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };

            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao editar os dados deste plano." };
        }
        return View("Index", this.FindAll());
    }

    public IActionResult Delete(FlatDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.flatService.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel { Header = "Sucesso", Type = "success", Message = $"Plano { dto?.Name } exclúido." };
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };

            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = $"Ocorreu um erro ao excluir o plano { dto?.Name }." };
        }
        return View("Index", this.FindAll());
    }
}