using Application.Repository;
using Application.ViewModels.Tipo;
using Database;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TipoService
    {
        private readonly TipoRepository _tipoRepository;

        public TipoService(ApplicationContext dbContext)
        {
            _tipoRepository = new(dbContext);
        }

        public async Task<List<TipoViewModel>> GetAllViewModel()
        {
            var list = await _tipoRepository.GetAllAsync();
            return list.Select(s => new TipoViewModel
            {
                Name = s.Name,
                Description = s.Description,
                Id = s.Id,
            }).ToList();
        }

        public async Task Add(SaveTipoViewModel vm)
        {
            Tipo Tipo = new();
            Tipo.Name = vm.Name;
            Tipo.Description = vm.Description;

            await _tipoRepository.AddAsync(Tipo);
        }

        public async Task Update(SaveTipoViewModel vm)
        {
            Tipo Tipo = new()
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description
            };

            await _tipoRepository.UpdateAsync(Tipo);
        }

        public async Task Delete(int id)
        {
            var Tipo = await _tipoRepository.GetByIdAsync(id);
            await _tipoRepository.DeleteAsync(Tipo);
        }

        public async Task<SaveTipoViewModel> GetByIdSaveTipoViewModel(int id)
        {
            var Tipo = await _tipoRepository.GetByIdAsync(id);
            SaveTipoViewModel vm = new();
            vm.Id = Tipo.Id;
            vm.Name = Tipo.Name;
            vm.Description = Tipo.Description;

            return vm;
        }
    }
}
