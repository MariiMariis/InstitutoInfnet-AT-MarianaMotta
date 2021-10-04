using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATASP2021.Models;
using ATASP2021.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Domain.Model.Models;
using Microsoft.AspNetCore.Authorization;

namespace ATASP2021.Controllers
{
    public class ProcessadorController : Controller
    {
        private readonly IProcessadorHttpService _processadorHttpService;
        private readonly IFabricanteHttpService _fabricanteHttpService;

        public ProcessadorController(IProcessadorHttpService processadorHttpService
                                  , IFabricanteHttpService fabricanteHttpService)
        {
            _processadorHttpService = processadorHttpService;
            _fabricanteHttpService = fabricanteHttpService;
        }

        // GET: Processador
        public async Task<IActionResult> Index()
        {
            return View(await _processadorHttpService.GetAllAsync());
        }

        // GET: Processador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processadorViewModel = await _processadorHttpService.GetByIdAsync(id.Value);

            if (processadorViewModel == null)
            {
                return NotFound();
            }

            return View(processadorViewModel);
        }

        // GET: Processador/Create
        [Authorize]
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["FabricanteId"] = new SelectList(await _fabricanteHttpService.GetAllAsync(),
                                                       nameof(FabricanteViewModel.Id),
                                                       nameof(FabricanteViewModel.NomeFabricante));
            return View();
        }

        // POST: Processador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ProcessadorViewModel processadorViewModel)
        {
            if (ModelState.IsValid)
            {
                var processadorViewCreated = await _processadorHttpService.CreateAsync(processadorViewModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FabricanteId"] = new SelectList(await _processadorHttpService.GetAllAsync(),
                                                       nameof(ProcessadorViewModel.Id),
                                                       nameof(ProcessadorViewModel.NomeProcessador));
            return View(processadorViewModel);
        }

        // GET: Processador/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["FabricanteId"] = new SelectList(await _fabricanteHttpService.GetAllAsync(),
                                                       nameof(FabricanteViewModel.Id),
                                                       nameof(FabricanteViewModel.NomeFabricante));

            var processadorViewModel = await _processadorHttpService.GetByIdAsync(id.Value);

            if (processadorViewModel == null)
            {
                return NotFound();
            }
            return View(processadorViewModel);
        }

        // POST: Processador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProcessadorViewModel processadorViewModel)
        {
            if (id != processadorViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var processadorViewEdited = await _processadorHttpService.UpdateAsync(processadorViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await ProcessadorViewModelExists(processadorViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(processadorViewModel);
        }

        // GET: Processador/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var processadorViewModel = await _processadorHttpService.GetByIdAsync(id.Value);

            if (processadorViewModel == null)
            {
                return NotFound();
            }

            return View(processadorViewModel);
        }

        // POST: Processador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _processadorHttpService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProcessadorViewModelExists(int id)
        {
            return await _processadorHttpService.ProcessadorModelExistsAsync(id);
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> ExistOn(string nomeProcessador, int id)
        {
            var result = nomeProcessador.Length > 3 && nomeProcessador.Length <= 150;

            return !result
                   ? Json($"O campo precisa conter entre 4 e 150 caracteres.")
                   : Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync([FromForm] string search)
        {
            if (search == null)
            {
                var list = await _processadorHttpService.GetAllAsync();
                return View("Index", list.OrderByDescending(x => x.NomeProcessador).ToList());
            }
            else
            {
                var list = await _processadorHttpService.SearchAsync(search);
                return View("Index", list.OrderByDescending(x => x.NomeProcessador).ToList());
            }
        }
    }
}
