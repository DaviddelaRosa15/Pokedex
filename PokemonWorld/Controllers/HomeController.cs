using Application.Services;
using Application.ViewModels.Pokemon;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokemonWorld.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly PokemonService _pokemonService;
        private readonly RegionService _regionService;

        public HomeController(ApplicationContext dbContext)
        {
            _pokemonService = new(dbContext);
            _regionService = new(dbContext);
        }

        public async Task<IActionResult> Index(FilterPokemonViewModel vm)
        {
            ViewBag.Regions = await _regionService.GetAllViewModel();
            return View(await _pokemonService.GetAllViewModelWithFilters(vm));
        }
    }
}
