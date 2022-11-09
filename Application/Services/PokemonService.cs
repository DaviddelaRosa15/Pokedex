using Application.Repository;
using Application.ViewModels.Pokemon;
using Database;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PokemonService
    {
        private readonly PokemonRepository _pokemonRepository;

        public PokemonService(ApplicationContext dbContext)
        {
            _pokemonRepository = new(dbContext);
        }

        public async Task<List<PokemonViewModel>> GetAllViewModel()
        {
            var list = await _pokemonRepository.GetAllWithIncludeAsync(new List<string> { "Region", "FirstType", "SecondType" });
            var pokemon = list.Select(s => new PokemonViewModel
            {
                Name = s.Name,
                Description = s.Description,
                Id = s.Id,
                Url = s.UrlImage,
                RegionName = s.Region.Name,
                RegionId = s.Region.Id,
                FirstTypeName = s.FirstType.Name,
                SecondTypeName = s.SecondType == null ? " " : s.SecondType.Name
            }).OrderBy(pokemon => pokemon.Name).ToList();

            return pokemon;
        }

        public async Task Add(SavePokemonViewModel vm)
        {
            Pokemon pokemon = new();
            pokemon.Name = vm.Name;
            pokemon.Description = vm.Description;
            pokemon.UrlImage = vm.Url;
            pokemon.RegionId = vm.RegionId;
            pokemon.FirstTypeId = vm.FirstTypeId;
            pokemon.SecondTypeId = vm.SecondTypeId;

            await _pokemonRepository.AddAsync(pokemon);
        }

        public async Task Update(SavePokemonViewModel vm)
        {
            Pokemon pokemon = new()
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                UrlImage = vm.Url,
                RegionId = vm.RegionId,
                FirstTypeId = vm.FirstTypeId,
                SecondTypeId = vm.SecondTypeId
            };

            await _pokemonRepository.UpdateAsync(pokemon);
        }

        public async Task Delete(int id)
        {
            var pokemon = await _pokemonRepository.GetByIdAsync(id);
            await _pokemonRepository.DeleteAsync(pokemon);
        }

        public async Task<SavePokemonViewModel> GetByIdSavePokemonViewModel(int id)
        {
            var pokemon = await _pokemonRepository.GetByIdAsync(id);
            SavePokemonViewModel vm = new();
            vm.Id = pokemon.Id;
            vm.Name = pokemon.Name;
            vm.Description = pokemon.Description;
            vm.Url = pokemon.UrlImage;
            vm.RegionId = pokemon.RegionId;
            vm.FirstTypeId = pokemon.FirstTypeId;
            vm.SecondTypeId = pokemon.SecondTypeId;

            return vm;
        }

        public async Task<List<PokemonViewModel>> GetAllViewModelWithFilters(FilterPokemonViewModel filters)
        {
            var pokemonList = await _pokemonRepository.GetAllWithIncludeAsync(new List<string> { "Region", "FirstType", "SecondType" });

            var listViewModels = pokemonList.Select(pokemon => new PokemonViewModel
            {
                Name = pokemon.Name,
                Description = pokemon.Description,
                Id = pokemon.Id,
                Url = pokemon.UrlImage,
                RegionName = pokemon.Region.Name,
                RegionId = pokemon.Region.Id,
                FirstTypeName = pokemon.FirstType.Name,
                SecondTypeName = pokemon.SecondType == null ? " " : pokemon.SecondType.Name
            }).ToList();

            if (filters.RegionId != null)
            {
                listViewModels = listViewModels.Where(pokemon => pokemon.RegionId == filters.RegionId.Value).ToList();
            }
            else if(filters.PokemonName != null)
            {
                listViewModels = listViewModels.Where(pokemon => pokemon.Name.Contains(filters.PokemonName)).ToList();
            }

            listViewModels = listViewModels.OrderBy(pokemon => pokemon.Name).ToList();

            return listViewModels;
        }
    }
}
