using Application.Repository;
using Application.ViewModels.Region;
using Database;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RegionService
    {
        private readonly RegionRepository _regionRepository;

        public RegionService(ApplicationContext dbContext)
        {
            _regionRepository = new(dbContext);
        }

        public async Task<List<RegionViewModel>> GetAllViewModel()
        {
            var list = await _regionRepository.GetAllAsync();
            return list.Select(s => new RegionViewModel
            {
                Name = s.Name,
                Description = s.Description,
                Id = s.Id,
            }).ToList();
        }

        public async Task Add(SaveRegionViewModel vm)
        {
            Region region = new();
            region.Name = vm.Name;
            region.Description = vm.Description;

            await _regionRepository.AddAsync(region);
        }

        public async Task Update(SaveRegionViewModel vm)
        {
            Region region = new()
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description
            };

            await _regionRepository.UpdateAsync(region);
        }

        public async Task Delete(int id)
        {
            var region = await _regionRepository.GetByIdAsync(id);
            await _regionRepository.DeleteAsync(region);
        }

        public async Task<SaveRegionViewModel> GetByIdSaveRegionViewModel(int id)
        {
            var region = await _regionRepository.GetByIdAsync(id);
            SaveRegionViewModel vm = new();
            vm.Id = region.Id;
            vm.Name = region.Name;
            vm.Description = region.Description;

            return vm;
        }
    }
}
