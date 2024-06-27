using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using Application.Streaming.Dto.Interfaces;
using LiteStreaming.Application.Core.Interfaces.Query;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class MusicController : BaseController
{
    private readonly IMusicService musicService;
    private readonly IFindAll<BandDto> bandService;
    private readonly IFindAll<GenreDto> genreService;
    private readonly IFindAll<AlbumDto> albumService;
    public MusicController(IMusicService musicService, IFindAll<GenreDto> genreFindAllService, IFindAll<BandDto> bandFindAllService, IFindAll<AlbumDto> albumService)
    {
        this.musicService = musicService;
        this.bandService = bandFindAllService;
        this.genreService = genreFindAllService;
        this.albumService = albumService;
    }

    public IActionResult Index()
    {
        return View(this.musicService.FindAll());
    }

    public IActionResult Create()
    {
        var genres = genreService.FindAll();
        var bands = bandService.FindAll();
        var albums = albumService.FindAll();
        var viewModel = new MusicViewModel
        {
            Music = new MusicDto(),
            Bands = bands,
            Genres = genres,
            Albums = albums
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Save(MusicViewModel viewModel)
    {
        if (ModelState is { IsValid: false })
            return CreateView();

        try
        {
            viewModel.Music.UsuarioId = UserId;
            musicService.Create(viewModel.Music);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao salvar os dados da musica.");
            return CreateView();
        }
    }

    [HttpPost]
    public IActionResult Update(MusicViewModel viewModel)
    {
        if (ModelState is { IsValid: false })
            return EditView();

        try
        {
            viewModel.Music.UsuarioId = UserId;
            musicService.Update(viewModel.Music);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao atualizar a musica { viewModel.Music?.Name }.");
            return EditView();
        }
    }

    public IActionResult Edit(Guid Id)
    {
        try
        {
            var genres = genreService.FindAll();
            var bands = bandService.FindAll();
            var albums = albumService.FindAll();

            var viewModel = new MusicViewModel
            {
                Music = this.musicService.FindById(Id),
                Bands = bands,
                Genres = genres,
                Albums = albums
            };
            return View(viewModel);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao editar os dados desta musica.");
        }
        return View(INDEX, this.musicService.FindAll());
    }

    public IActionResult Delete(MusicDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.musicService.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Success, $"Musica { dto?.Name } excluída.");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEx.Message);

            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao excluir a musica {dto?.Name }.");
        }
        return View(INDEX, this.musicService.FindAll());
    }
}