using Data.Data;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interface.Repositorio;

namespace Data.Repositorio
{
    public class FabricanteRepository : IFabricanteRepository
    {
        private readonly FabricantesContext _context;

        public FabricanteRepository(FabricantesContext context)
        {
            _context = context;
        }

        public async Task<FabricanteModel> CreateAsync(FabricanteModel fabricanteModel)
        {
            var newFabricante = _context.Fabricantes.Add(fabricanteModel);
            await _context.SaveChangesAsync();

            return newFabricante.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var fabricante = await _context.Fabricantes.FindAsync(id);

            _context.Fabricantes.Remove(fabricante);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> FabricanteModelExistsAsync(int id)
        {
            var exists = await _context.Fabricantes.FirstOrDefaultAsync(x => x.Id == id);

            return exists != null;
        }

        public async Task<IEnumerable<FabricanteModel>> GetAllAsync()
        {
            return await _context.Fabricantes
                         .Include(p => p.Processadores)
                         .OrderByDescending(x => x.NomeFabricante)
                         .ToListAsync();
        }

        public async Task<FabricanteModel> GetByIdAsync(int id)
        {
            return await _context.Fabricantes
                         .Include(p => p.Processadores)
                         .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<FabricanteModel> ExistAsync(string nomeFabricante, int id)
        {
            var fabricante = await _context.Fabricantes
                    .FirstOrDefaultAsync(x => x.NomeFabricante.Contains(nomeFabricante) && x.Id != id);

            return fabricante;
        }

        public async Task<IEnumerable<FabricanteModel>> SearchAsync(string search)
        {
            return await _context.Fabricantes
                         .Where(x => x.NomeFabricante.Contains(search))
                         .Include(p => p.Processadores)
                         .OrderByDescending(x => x.NomeFabricante)
                         .ToListAsync();
        }

        public async Task<FabricanteModel> UpdateAsync(FabricanteModel fabricante)
        {
            var editFabricante = _context.Fabricantes.Update(fabricante);
            await _context.SaveChangesAsync();

            return editFabricante.Entity;
        }
    }
}
