using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using Microsoft.AspNetCore.Authorization;
using LiteStreaming.Application.Abstractions;
using LiteStreaming.Application.Core.Interfaces.Query;

namespace LiteStreaming.AdministrativeApp.Controllers;

public class AlbumController : BaseController<AlbumDto>
{
    private readonly IFindAll<BandDto> bandService;
    private readonly IFindAll<GenreDto> genreService;
    public AlbumController(IService<AlbumDto> albumService, IFindAll<BandDto> bandFindAllService, IFindAll<GenreDto> genreFindAllService) : base(albumService)
    {
        this.bandService = bandFindAllService;
        this.genreService = genreFindAllService;
    }

    [Authorize]
    public override IActionResult Create()
    {
        var genres = genreService.FindAll();
        var bands = bandService.FindAll();
        var viewModel = new AlbumViewModel()
        {
            Album = new AlbumDto(),
            Bands = bands,
            Genres = genres
        };
        return View(viewModel);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Save(AlbumViewModel viewModel)
    {
        if (ModelState is { IsValid: false })
            return CreateView();

        try
        {
            viewModel.Album.UsuarioId = UserId;
            this.Services.Create(viewModel.Album);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argSaveEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argSaveEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao salvar os dados do álbum.");
            return CreateView();
        }
    }

    [HttpPost]
    [Authorize]
    public IActionResult Update(AlbumViewModel viewModel)
    {
        if (ModelState is { IsValid: false })
            return EditView();

        try
        {
            viewModel.Album.UsuarioId = UserId;
            this.Services.Update(viewModel.Album);
            return this.RedirectToIndexView();
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEditEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEditEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao atualizar o álbum {viewModel.Album?.Name }.");
            return EditView();
        }
    }

    [Authorize]
    public IActionResult Edit(Guid Id)
    {
        try
        {
            var genres = genreService.FindAll();
            var bands = bandService.FindAll();
            var viewModel = new AlbumViewModel
            {
                Album = this.Services.FindById(Id),
                Bands = bands,
                Genres = genres
            };
            return View(viewModel);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argEditEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argEditEx.Message);
            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, "Ocorreu um erro ao editar os dados deste álbum.");
        }
        return IndexView();
    }

    [Authorize]
    public IActionResult Delete(AlbumDto dto)
    {
        try
        {
            dto.UsuarioId = UserId;
            var result = this.Services.Delete(dto);
            if (result)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Success, $"Álbum { dto?.Name } excluído.");
        }
        catch (Exception ex)
        {
            if (ex is ArgumentException argDeleteEx)
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Warning, argDeleteEx.Message);

            else
                ViewBag.Alert = new AlertViewModel(AlertViewModel.AlertType.Danger, $"Ocorreu um erro ao excluir o álbum { dto?.Name }.");
        }
        return IndexView();
    }
}