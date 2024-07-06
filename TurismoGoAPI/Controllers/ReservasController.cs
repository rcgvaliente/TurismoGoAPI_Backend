using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGoDOMAIN.Core.Entities;
using TurismoGoDOMAIN.Infraestructure.Data;
using static TurismoGoDOMAIN.Core.DTO.ReservasDTO;

namespace TurismoGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly TurismoGoBdContext _context;

        public ReservasController(TurismoGoBdContext context)
        {
            _context = context;
        }

        // POST: api/reservas
        [HttpPost]
        public async Task<ActionResult<Reservas>> PostReserva(ReservasRequest reservasRequest)
        {
            var reserva = new Reservas
            {
                UsuarioId = reservasRequest.UsuarioId,
                ActividadId = reservasRequest.ActividadId,
                FechaReserva = DateTime.Now,
                Estado = "Reservado"
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserva", new { id = reserva.Id }, reserva);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservasResponse>>> GetReservas()
        {
            var reservas = _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Actividad)
                .AsQueryable();

            var result = await reservas.Select(r => new ReservasResponse
            {
                Id = r.Id,
                UsuarioNombre = r.Usuario.Nombre, // Asegúrate de que el Usuario tiene un campo `Nombre`
                ActividadTitulo = r.Actividad.Titulo, // Asegúrate de que la Actividad tiene un campo `Titulo`
                FechaReserva = r.FechaReserva.ToString("dd-MM-yyyy"),
                Estado = r.Estado
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<ReservasResponse>>> GetReservasByUsuario(int usuarioId)
        {
            var reservas = await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Actividad)
                .Where(r => r.UsuarioId == usuarioId)
                .Select(r => new ReservasResponse
                {
                    Id = r.Id,
                    UsuarioId = r.UsuarioId,
                    ActividadId = r.ActividadId,
                    ActividadTitulo = r.Actividad.Titulo, 
                    FechaReserva = r.FechaReserva.ToString("dd-MM-yyyy HH:mm"),
                    Estado = r.Estado
                })
                .ToListAsync();

            return Ok(reservas);
        }


        // GET: api/reservas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservas>> GetReserva(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Actividad)
                .FirstOrDefaultAsync(r => r.Id == id);

            var result = new ReservasResponse
            {
                Id = id,
                UsuarioId = reserva.UsuarioId,
                ActividadId = reserva.ActividadId,
                FechaReserva = reserva.FechaReserva.ToString("dd-MM-yyyy"),
                Estado = reserva.Estado
            };

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/reservas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, ReservasRequest reservasRequest)
        {

            var reserva = new Reservas
            {
                Id = id,
                UsuarioId = reservasRequest.UsuarioId,
                ActividadId = reservasRequest.ActividadId,
            };

            _context.Entry(reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
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

        // DELETE: api/reservas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
