using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interface.Repositorio
{
    public interface IProcessadorRepository
    {
        public Task<IEnumerable<ProcessadorModel>> GetAllAsync();
        public Task<ProcessadorModel> GetByIdAsync(int id);
        public Task<ProcessadorModel> CreateAsync(ProcessadorModel processadorModel);
        public Task<ProcessadorModel> UpdateAsync(ProcessadorModel processadorModel);
        public Task DeleteAsync(int id);
        public Task<bool> ProcessadorModelExistsAsync(int id);
        Task<bool> ExistOnAsync(string nomeProcessador, int id);
        public Task<IEnumerable<ProcessadorModel>> SearchAsync(string search);
    }
}
