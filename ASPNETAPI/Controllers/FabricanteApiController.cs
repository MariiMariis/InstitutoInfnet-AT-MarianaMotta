
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Domain.Model.Models;
using Domain.Model.Interface.Service;

namespace ASPNETAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FabricanteApiController : ControllerBase
    {
        private readonly IFabricanteService _fabricanteService;

        public FabricanteApiController(IFabricanteService fabricanteService)
        {
            _fabricanteService = fabricanteService;
        }

        // GET: api/FabricanteApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FabricanteModel>>> GetFabricanteModel()
        {
            return Ok(await _fabricanteService.GetAllAsync());
        }

        // GET: api/FabricanteApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FabricanteModel>> GetFabricanteModel(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var fabricante = await _fabricanteService.GetByIdAsync(id);

            if (fabricante == null)
            {
                return NotFound();
            }

            return Ok(fabricante);
        }

        // PUT: api/FabricanteApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutFabricanteModel(int id, FabricanteModel fabricanteModel)
        {
            if (id != fabricanteModel.Id)
            {
                return BadRequest();
            }

            try
            {
                var fabricanteEdited = await _fabricanteService.UpdateAsync(fabricanteModel);

                return Ok(fabricanteEdited);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await FabricanteModelExistsAsync(id))
                {
                    throw;
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // POST: api/FormularioApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<FabricanteModel>> PostFabricanteModel(FabricanteModel fabricanteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(fabricanteModel);
            }

            var fabricanteCreated = await _fabricanteService.CreateAsync(fabricanteModel);

            return Ok(fabricanteCreated);
        }

        // DELETE: api/FormularioApi/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFabricanteModel(int id)
        {
            var fabricante = await _fabricanteService.GetByIdAsync(id);

            if (fabricante == null)
            {
                return NotFound();
            }

            await _fabricanteService.DeleteAsync(fabricante.Id);

            return Ok();
        }

        private async Task<bool> FabricanteModelExistsAsync(int id)
        {
            return await _fabricanteService.FabricanteModelExistsAsync(id);
        }

        [HttpGet("Exists/{nomeFabricante}/{id}")]
        public async Task<ActionResult<FabricanteModel>> GetFabricanteModel(string nomeFabricante, int id)
        {
            var fabricante = await _fabricanteService.ExistAsync(nomeFabricante, id);

            if (fabricante == null)
            {
                return NotFound();
            }

            return fabricante;
        }

        [HttpGet("ExistsId/{id}")]
        public async Task<ActionResult> FabricanteModelExistsIdAsync(int id)
        {
            return Ok(await _fabricanteService.FabricanteModelExistsAsync(id));
        }

        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<FabricanteModel>>> SearchAsync(string search)
        {
            if (search == null)
            {
                var list = await _fabricanteService.GetAllAsync();

                return Ok(list.OrderByDescending(x => x.NomeFabricante).ToList());
            }
            else
            {
                var list = await _fabricanteService.SearchAsync(search);

                return Ok(list.OrderByDescending(x => x.NomeFabricante).ToList());
            }
        }
    }
}
