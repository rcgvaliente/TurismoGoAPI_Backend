using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static TurismoGoDOMAIN.Core.DTO.AuthDTO;
using TurismoGoDOMAIN.Infraestructure.Data;

namespace TurismoGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly TurismoGoBdContext _context;

        public EmpresaController(TurismoGoBdContext context)
        {
            _context = context;
        }

        // GET: api/empresas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetEmpresas()
        {
            return Ok(await _context.Usuarios.Where(u => u.Tipo == "empresa").ToListAsync());
        }

        // GET: api/empresas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetEmpresa(int id)
        {
            var empresa = await _context.Usuarios.FindAsync(id);

            if (empresa == null || empresa.Tipo != "empresa")
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, User empresa)
        {
            if (id != empresa.Id || empresa.Tipo != "empresa")
            {
                return BadRequest();
            }

            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/empresas/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _context.Usuarios.FindAsync(id);
            if (empresa == null || empresa.Tipo != "empresa")
            {
                return NotFound();
            }

            _context.Usuarios.Remove(empresa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpresaExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id && e.Tipo == "empresa");
        }
    }
}
