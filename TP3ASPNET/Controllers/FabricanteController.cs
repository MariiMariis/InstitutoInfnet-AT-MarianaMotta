using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Domain.Model.Models;
using Data.Data;
using ATASP2021.Services;
using ATASP2021.Models;
using Microsoft.AspNetCore.Authorization;

namespace ATASP2021.Controllers
{
    public class FabricanteController : Controller
    {
        private readonly IFabricanteHttpService _fabricanteHttpService;

        public FabricanteController(IFabricanteHttpService fabricanteHttpService)
        {
            _fabricanteHttpService = fabricanteHttpService;
        }

        // GET: Fabricante
        public async Task<IActionResult> Index()
        {
            return View(await _fabricanteHttpService.GetAllAsync());
        }

        // GET: Fabricante/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricanteViewModel = await _fabricanteHttpService.GetByIdAsync(id.Value);

            if (fabricanteViewModel == null)
            {
                return NotFound();
            }

            return View(fabricanteViewModel);
        }

        // GET: Fabricante/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fabricante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( FabricanteViewModel fabricanteViewModel)
        {
            if (ModelState.IsValid)
            {
                var fabricanteViewCreated = await _fabricanteHttpService.CreateAsync(fabricanteViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(fabricanteViewModel);
        }

        // GET: Fabricante/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricanteViewModel = await _fabricanteHttpService.GetByIdAsync(id.Value);
            if (fabricanteViewModel == null)
            {
                return NotFound();
            }
            return View(fabricanteViewModel);
        }

        // POST: Fabricante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FabricanteViewModel fabricanteViewModel)
        {
            if (id != fabricanteViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var escolaViewEdited = await _fabricanteHttpService.UpdateAsync(fabricanteViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await FabricanteViewModelExists(fabricanteViewModel.Id))
                    {
                        throw;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fabricanteViewModel);
        }

        // GET: Fabricante/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricanteViewModel = await _fabricanteHttpService.GetByIdAsync(id.Value);
            if (fabricanteViewModel == null)
            {
                return NotFound();
            }

            return View(fabricanteViewModel);
        }

        // POST: Fabricante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _fabricanteHttpService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FabricanteViewModelExists(int id)
        {
            return await _fabricanteHttpService.FabricanteModelExistsAsync(id);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Exist(string nomeFabricante, int id)
        {
            var result = nomeFabricante.Length > 3 && nomeFabricante.Length <= 150;

            return !result
                   ? Json($"O nome do fabricante deve conter entre 4 e 150 caracteres.")
                   : Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync([FromForm] string search)
        {
            if (search == null)
            {
                var list = await _fabricanteHttpService.GetAllAsync();
                return View("Index", list.OrderByDescending(x => x.NomeFabricante).ToList());
            }
            else
            {
                var list = await _fabricanteHttpService.SearchAsync(search);
                return View("Index", list.OrderByDescending(x => x.NomeFabricante).ToList());
            }
        }

    }
}
