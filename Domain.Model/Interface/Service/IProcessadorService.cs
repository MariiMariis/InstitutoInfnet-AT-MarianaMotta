using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interface.Service
{
    public interface IProcessadorService
    {
        public Task<IEnumerable<ProcessadorModel>> GetAllAsync();
        public Task<ProcessadorModel> GetByIdAsync(int id);
        public Task<ProcessadorModel> CreateAsync(ProcessadorModel processadorModel);
        public Task<ProcessadorModel> UpdateAsync(ProcessadorModel processadorModel);
        public Task DeleteAsync(int id);
        public Task<bool> ProcessadorModelExistsAsync(int id);
        public Task<bool> ExistOnAsync(string nomeProcessador, int id);
        public Task<IEnumerable<ProcessadorModel>> SearchAsync(string search);
    }
}
