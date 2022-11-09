using Application.Services;
using Application.ViewModels.Pokemon;
using Database;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PokemonWorld.Controllers
{
    public class PokemonController : Controller
    {
        private readonly PokemonService _pokemonService;
        private readonly RegionService _regionService;
        private readonly TipoService _tipoService;

        public PokemonController(ApplicationContext dbContext)
        {
            _pokemonService = new(dbContext);
            _regionService = new(dbContext);
            _tipoService = new(dbContext);
        }
        public async Task<IActionResult> Index()
        {
            var list = await _pokemonService.GetAllViewModel();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            SavePokemonViewModel vm = new();
            vm.Regions = await _regionService.GetAllViewModel();
            vm.Types = await _tipoService.GetAllViewModel();
            return View("SavePokemon", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePokemonViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Regions = await _regionService.GetAllViewModel();
                vm.Types = await _tipoService.GetAllViewModel();
                return View("SavePokemon", vm);
            }

            await _pokemonService.Add(vm);
            return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            SavePokemonViewModel vm = await _pokemonService.GetByIdSavePokemonViewModel(id);
            vm.Regions = await _regionService.GetAllViewModel();
            vm.Types = await _tipoService.GetAllViewModel();
            return View("SavePokemon", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePokemonViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Regions = await _regionService.GetAllViewModel();
                vm.Types = await _tipoService.GetAllViewModel();
                return View("SavePokemon", vm);
            }

            await _pokemonService.Update(vm);
            return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _pokemonService.GetByIdSavePokemonViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _pokemonService.Delete(id);
            return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
        }
    }
}
