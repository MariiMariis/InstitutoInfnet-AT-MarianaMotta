using Domain.Model.Interface.Service;
using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interface.Repositorio;

namespace Domain.Service.Services
{
    public class FabricanteService : IFabricanteService
    {
        private readonly IFabricanteRepository _repository;

        public FabricanteService(IFabricanteRepository processadorRepository)
        {
            _repository = processadorRepository;
        }

        public async Task<FabricanteModel> CreateAsync(FabricanteModel fabricanteModel)
        {
            return await _repository.CreateAsync(fabricanteModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> FabricanteModelExistsAsync(int id)
        {
            return await _repository.FabricanteModelExistsAsync(id);
        }

        public async Task<IEnumerable<FabricanteModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<FabricanteModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<FabricanteModel> ExistAsync(string nomeFabricante, int id)
        {
            return await _repository.ExistAsync(nomeFabricante.Trim(), id);
        }

        public async Task<IEnumerable<FabricanteModel>> SearchAsync(string search)
        {
            return await _repository.SearchAsync(search);
        }

        public async Task<FabricanteModel> UpdateAsync(FabricanteModel fabricanteModel)
        {
            return await _repository.UpdateAsync(fabricanteModel);
        }
    }
}
