using AdministrativeApp.Controllers.Abastractions;
using AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Streaming.Dto;
using Application.Streaming.Dto.Interfaces;

namespace LiteStreaming.AdministrativeApp.Controllers
{
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
                dto.UsuarioId = this.UserId.Value;
                flatService.Create(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };
                else
                    ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao salvar os dados." };
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
                dto.UsuarioId = this.UserId.Value;
                flatService.Update(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "danger", Message = argEx.Message };
                else
                    ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao atualizar os dados deste usuário." };
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
                    ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao editar os dados deste usuário." };
            }
            return View("Index", this.FindAll());
        }

        public IActionResult Delete(Guid IdUsuario)
        {
            try
            {
                var dto = this.flatService.FindById(IdUsuario);
                dto.UsuarioId = this.UserId.Value;
                var result = this.flatService.Delete(dto);
                if (result)
                    ViewBag.Alert = new AlertViewModel { Header = "Sucesso", Type = "success", Message = "Usuário inativado." };
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = new AlertViewModel { Header = "Informação", Type = "warning", Message = argEx.Message };

                else
                    ViewBag.Alert = new AlertViewModel { Header = "Erro", Type = "danger", Message = "Ocorreu um erro ao excluir os dados deste usuário." };
            }
            return View("Index", this.FindAll());
        }
    }
}