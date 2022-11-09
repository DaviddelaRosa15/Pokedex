using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database;
using Database.Models;
using Application.Services;
using Application.ViewModels.Tipo;

namespace PokemonWorld.Controllers
{
    public class TipoController : Controller
    {
        private readonly TipoService _tipoService;

        public TipoController(ApplicationContext context)
        {
            _tipoService = new(context);
        }

        public async Task<IActionResult> Index()
        {
            var list = await _tipoService.GetAllViewModel();
            return View(list);
        }

        public IActionResult Create()
        {
            return View("SaveTipo", new SaveTipoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveTipoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTipo", vm);
            }

            await _tipoService.Add(vm);
            return RedirectToRoute(new { controller = "Tipo", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveTipo", await _tipoService.GetByIdSaveTipoViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveTipoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTipo", vm);
            }

            await _tipoService.Update(vm);
            return RedirectToRoute(new { controller = "Tipo", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _tipoService.GetByIdSaveTipoViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _tipoService.Delete(id);
            return RedirectToRoute(new { controller = "Tipo", action = "Index" });
        }
    }
}
