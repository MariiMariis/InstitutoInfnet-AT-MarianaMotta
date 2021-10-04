using Domain.Model.Interface.Service;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProcessadorApiController : ControllerBase
    {
        private readonly IProcessadorService _processadorService;

        public ProcessadorApiController(IProcessadorService processadorService)
        {
            _processadorService = processadorService;
        }

        // GET: api/ProcessadorApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessadorModel>>> GetProcessadorModel()
        {
            return Ok(await _processadorService.GetAllAsync());
        }

        // GET: api/ProcessadorApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessadorModel>> GetProcessadorModel(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var processador = await _processadorService.GetByIdAsync(id);

            if (processador == null)
            {
                return NotFound();
            }

            return Ok(processador);
        }

        // PUT: api/ProcessadorApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutProcessadorModel(int id, ProcessadorModel processadorModel)
        {
            if (id != processadorModel.Id)
            {
                return BadRequest();
            }

            try
            {
                var processadorEdited = await _processadorService.UpdateAsync(processadorModel);

                return Ok(processadorEdited);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await ProcessadorModelExistsAsync(id))
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
        public async Task<ActionResult<ProcessadorModel>> PostProcessadorModel([FromBody] ProcessadorModel processadorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(processadorModel);
            }

            var processadorCreated = await _processadorService.CreateAsync(processadorModel);

            return Ok(processadorCreated);
        }

        // DELETE: api/FormularioApi/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProcessadorModel(int id)
        {
            var processador = await _processadorService.GetByIdAsync(id);

            if (processador == null)
            {
                return NotFound();
            }

            await _processadorService.DeleteAsync(processador.Id);

            
            return Ok();
        }

        private async Task<bool> ProcessadorModelExistsAsync(int id)
        {
            return await _processadorService.ProcessadorModelExistsAsync(id);
        }

        [HttpGet("Exists/{nomeProcessador}/{id}")]
        public async Task<ActionResult> GetProcessadorModel(string nomeProcessador, int id)
        {
            return Ok(await _processadorService.ExistOnAsync(nomeProcessador, id));
        }

        [HttpGet("ExistsId/{id}")]
        public async Task<ActionResult> ProcessadorModelExistsIdAsync(int id)
        {
            return Ok(await _processadorService.ProcessadorModelExistsAsync(id));
        }

        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<ProcessadorModel>>> SearchAsync(string search)
        {
            if (search == null)
            {
                var list = await _processadorService.GetAllAsync();

                return Ok(list.OrderByDescending(x => x.NomeProcessador).ToList());
            }
            else
            {
                var list = await _processadorService.SearchAsync(search);

                return Ok(list.OrderByDescending(x => x.NomeProcessador).ToList());
            }
        }
    }
}
