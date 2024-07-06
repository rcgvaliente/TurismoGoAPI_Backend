using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGoDOMAIN.Core.DTO;
using TurismoGoDOMAIN.Core.Entities;
using TurismoGoDOMAIN.Infraestructure.Data;
using static TurismoGoDOMAIN.Core.DTO.ReseniasDTO;
using System.Linq;

namespace TurismoGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReseniasController : ControllerBase
    {
        private readonly TurismoGoBdContext _context;

        public ReseniasController(TurismoGoBdContext context)
        {
            _context = context;
        }
        // POST: api/resenias
        [HttpPost]
        public async Task<ActionResult<Resenias>> PostResenia(ReseniasRequest reseniasRequest)
        {
            var resenia = new Resenias
            {
                UsuarioId = reseniasRequest.UsuarioId,
                ActividadId = reseniasRequest.ActividadId,
                Calificacion = reseniasRequest.Calificacion,
                Comentario = reseniasRequest.Comentario,
                FechaPublicacion = DateTime.Now,
            };

            _context.Resenias.Add(resenia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResenia", new { id = resenia.Id }, resenia);
        }

        // GET: api/resenias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resenias>>> GetResenias()
        {
            var query = _context.Resenias
            .Include(r => r.Usuario)
            .Include(r => r.Actividad)
            .AsQueryable();


            var result = await query.Select(r => new ReseniasResponse
            {
                Id = r.Id,
                UsuarioId = r.UsuarioId,
                ActividadId = r.ActividadId,
                Calificacion = r.Calificacion,
                Comentario = r.Comentario,
                FechaPublicacion = r.FechaPublicacion.ToString("yyyy-MM-dd")
            }).ToListAsync();

            return Ok(result);
        }

        // GET: api/resenias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Resenias>> GetResenia(int id)
        {
            var resenia = await _context.Resenias
                .Include(r => r.Usuario)
                .Include(r => r.Actividad)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resenia == null)
            {
                return NotFound();
            }

            var result =  new ReseniasResponse
            {
                Id = resenia.Id,
                UsuarioId = resenia.UsuarioId,
                ActividadId = resenia.ActividadId,
                Calificacion = resenia.Calificacion,
                Comentario = resenia.Comentario,
                FechaPublicacion = resenia.FechaPublicacion.ToString("yyyy-MM-dd")
            };

            return Ok(result);
        }

        // PUT: api/resenias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResenia(int id, ReseniasRequest reseniasRequest)
        {

            var resenia = new Resenias
            {
                Id = id,
                UsuarioId = reseniasRequest.UsuarioId,
                ActividadId = reseniasRequest.ActividadId,
                Calificacion = reseniasRequest.Calificacion,
                Comentario = reseniasRequest.Comentario,
            };

            _context.Entry(resenia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReseniaExists(id))
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

        // DELETE: api/resenias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResenia(int id)
        {
            var resenia = await _context.Resenias.FindAsync(id);
            if (resenia == null)
            {
                return NotFound();
            }

            _context.Resenias.Remove(resenia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReseniaExists(int id)
        {
            return _context.Resenias.Any(e => e.Id == id);
        }
    }
}
