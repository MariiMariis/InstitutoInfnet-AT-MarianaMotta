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
    public class ProcessadorService : IProcessadorService
    {
        private readonly IProcessadorRepository _repository;

        public ProcessadorService(IProcessadorRepository processadorRepository)
        {
            _repository = processadorRepository;
        }

        public async Task<bool> ProcessadorModelExistsAsync(int id)
        {
            return await _repository.ProcessadorModelExistsAsync(id);
        }

        public async Task<ProcessadorModel> CreateAsync(ProcessadorModel processadorModel)
        {
            return await _repository.CreateAsync(processadorModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProcessadorModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProcessadorModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> ExistOnAsync(string nomeProcessador, int id)
        {
            return await _repository.ExistOnAsync(nomeProcessador, id);
        }

        public async Task<IEnumerable<ProcessadorModel>> SearchAsync(string search)
        {
            return await _repository.SearchAsync(search);
        }

        public async Task<ProcessadorModel> UpdateAsync(ProcessadorModel processadorModel)
        {
            return await _repository.UpdateAsync(processadorModel);
        }
    }
}
