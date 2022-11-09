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
using Application.ViewModels.Region;

namespace PokemonWorld.Controllers
{
    public class RegionController : Controller
    {
        private readonly RegionService _regionService;

        public RegionController(ApplicationContext context)
        {
            _regionService = new(context);
        }

        public async Task<IActionResult> Index()
        {
            var list = await _regionService.GetAllViewModel();
            return View(list);
        }

        public IActionResult Create()
        {
            return View("SaveRegion", new SaveRegionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveRegionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveRegion", vm);
            }

            await _regionService.Add(vm);
            return RedirectToRoute(new { controller = "Region", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveRegion", await _regionService.GetByIdSaveRegionViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveRegionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveRegion", vm);
            }

            await _regionService.Update(vm);
            return RedirectToRoute(new { controller = "Region", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _regionService.GetByIdSaveRegionViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _regionService.Delete(id);
            return RedirectToRoute(new { controller = "Region", action = "Index" });
        }
    }
}
