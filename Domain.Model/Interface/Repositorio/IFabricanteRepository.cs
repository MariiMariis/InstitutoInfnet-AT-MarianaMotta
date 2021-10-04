using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interface.Repositorio
{
    public interface IFabricanteRepository
    {
        public Task<IEnumerable<FabricanteModel>> GetAllAsync();
        public Task<FabricanteModel> GetByIdAsync(int id);
        public Task<FabricanteModel> CreateAsync(FabricanteModel fabricanteModel);
        public Task<FabricanteModel> UpdateAsync(FabricanteModel fabricante);
        public Task DeleteAsync(int id);
        public Task<bool> FabricanteModelExistsAsync(int id);
        public Task<FabricanteModel> ExistAsync(string nomeFabricante, int id);
        public Task<IEnumerable<FabricanteModel>> SearchAsync(string search);
    }
}
