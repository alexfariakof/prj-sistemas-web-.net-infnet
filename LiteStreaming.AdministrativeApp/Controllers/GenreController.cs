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
        return this.FindAll();
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
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };
            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao salvar os dados do gênero." };
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
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "danger", Message = argEx.Message };
            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = $"Ocorreu um erro ao atualizar o gênero { dto?.Name }." };
            return EditView();
        }
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
        return this.FindAll();
    }

    public IActionResult Delete(GenreDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.genreService.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel { Header = "Sucesso", Type = "success", Message = $"Gênero { dto?.Name } excluído." };
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };

            else
                ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = $"Ocorreu um erro ao excluir o gênero { dto?.Name }." };
        }
        return this.FindAll();
    }

    private IActionResult FindAll()
    {
        return View(INDEX, this.genreService.FindAll());
    }
}