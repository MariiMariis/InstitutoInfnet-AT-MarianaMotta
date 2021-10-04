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
    public class ProcessadorRepository : IProcessadorRepository
    {
        private readonly FabricantesContext _context;

        public  ProcessadorRepository(FabricantesContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessadorModelExistsAsync(int id)
        {
            var processador = await _context.Processadores
                          .FirstOrDefaultAsync(e => e.Id == id);

            return processador != null;
        }

        public async Task<ProcessadorModel> CreateAsync(ProcessadorModel processadorModel)
        {
            var newProcessador =  _context.Add(processadorModel);
            await _context.SaveChangesAsync();

            return newProcessador.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var processador = await _context.Processadores.FindAsync(id);

            _context.Processadores.Remove(processador);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProcessadorModel>> GetAllAsync()
        {
            return await _context.Processadores
                        .Include(p => p.Fabricante)
                        .OrderByDescending(x => x.NomeProcessador)
                        .ToListAsync();
        }

        public async Task<ProcessadorModel> GetByIdAsync(int id)
        {
            return await _context.Processadores
                        .Include(p => p.Fabricante)
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> ExistOnAsync(string nomeProcessador, int id)
        {
            var s = await _context.Processadores
                          .FirstOrDefaultAsync(x => x.NomeProcessador == nomeProcessador && x.Id != id);

            return s != null;
        }

        public async Task<IEnumerable<ProcessadorModel>> SearchAsync(string search)
        {
            return await _context.Processadores
                         .Where(x => x.NomeProcessador.Contains(search))
                         .Include(p => p.Fabricante)
                         .OrderByDescending(x => x.NomeProcessador)
                         .ToListAsync();
        }

        public async Task<ProcessadorModel> UpdateAsync(ProcessadorModel processadorModel)
        {
            var editProcessador = _context.Processadores.Update(processadorModel);
            await _context.SaveChangesAsync();

            return editProcessador.Entity;
        }

    }
}
