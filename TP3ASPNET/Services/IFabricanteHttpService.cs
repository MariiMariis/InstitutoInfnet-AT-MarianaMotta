using System.Collections.Generic;
using System.Threading.Tasks;
using ATASP2021.Models;

namespace ATASP2021.Services
{
    public interface IFabricanteHttpService
    {
        public Task<IEnumerable<FabricanteViewModel>> GetAllAsync();
        public Task<FabricanteViewModel> GetByIdAsync(int id);
        public Task<FabricanteViewModel> CreateAsync(FabricanteViewModel fabricanteViewModel);
        public Task<FabricanteViewModel> UpdateAsync(FabricanteViewModel fabricanteViewModel);
        public Task DeleteAsync(int id);
        public Task<bool> FabricanteModelExistsAsync(int id);
        public Task<FabricanteViewModel> ExistAsync(string nomeFabricante, int id);
        public Task<IEnumerable<FabricanteViewModel>> SearchAsync(string search);
    }
}
