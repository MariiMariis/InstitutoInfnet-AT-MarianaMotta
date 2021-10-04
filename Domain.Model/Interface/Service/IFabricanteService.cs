using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interface.Service
{
    public interface IFabricanteService
    {
        public Task<IEnumerable<FabricanteModel>> GetAllAsync();
        public Task<FabricanteModel> GetByIdAsync(int id);
        public Task<FabricanteModel> CreateAsync(FabricanteModel fabricanteModel);
        public Task<FabricanteModel> UpdateAsync(FabricanteModel fabricanteModel);
        public Task DeleteAsync(int id);
        public Task<bool> FabricanteModelExistsAsync(int id);
        public Task<FabricanteModel> ExistAsync(string nomeFabricante, int id);
        public Task<IEnumerable<FabricanteModel>> SearchAsync(string search);
    }
}
