using System.Collections.Generic;
using System.Threading.Tasks;
using ATASP2021.Models;

namespace ATASP2021.Services
{
    public interface IProcessadorHttpService
    {
        public Task<IEnumerable<ProcessadorViewModel>> GetAllAsync();
        public Task<ProcessadorViewModel> GetByIdAsync(int id);
        public Task<ProcessadorViewModel> CreateAsync(ProcessadorViewModel processadorViewModel);
        public Task<ProcessadorViewModel> UpdateAsync(ProcessadorViewModel processadorViewModel);
        public Task DeleteAsync(int id);
        public Task<bool> ProcessadorModelExistsAsync(int id);
        public Task<bool> ExistOnAsync(string nomeProcessador, int id);
        public Task<IEnumerable<ProcessadorViewModel>> SearchAsync(string search);
    }
}
