using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using Application.Streaming.Dto.Interfaces;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class GenreController : BaseController
{
    private readonly IGenreService genreService;
    public GenreController(IGenreService genreService)
    {
        this.genreService = genreService;
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
    public IActionResult Save(GenreDto dto)
    {
        if (ModelState is { IsValid: false })
            return View("Create");

        try
        {
            dto.UsuarioId = UserId;
            genreService.Create(dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };
            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao salvar os dados do gênero." };
            return View("Create");
        }
    }

    [HttpPost]
    public IActionResult Update(GenreDto dto)
    {
        if (ModelState is { IsValid: false })
            return View("Edit");

        try
        {
            dto.UsuarioId = UserId;
            genreService.Update(dto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "danger", Message = argEx.Message };
            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao atualizar os dados deste gênero." };
            return View("Edit");
        }
    }

    private IEnumerable<GenreDto> FindAll()
    {
        return this.genreService.FindAll();
    }

    public IActionResult Edit(Guid IdUsuario)
    {
        try
        {
            var result = this.genreService.FindById(IdUsuario);
            return View(result);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };

            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao editar os dados deste gênero." };
        }
        return View("Index", this.FindAll());
    }

    public IActionResult Delete(Guid IdUsuario)
    {
        try
        {
            var dto = this.genreService.FindById(IdUsuario);
            dto.UsuarioId = UserId;
            var result = this.genreService.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel { Header = "Sucesso", Type = "success", Message = "Usuário inativado." };
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };

            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao excluir os dados deste gênero." };
        }
        return View("Index", this.FindAll());
    }
}