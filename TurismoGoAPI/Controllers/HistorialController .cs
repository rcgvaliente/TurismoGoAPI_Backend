using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TurismoGoDOMAIN.Core.Entities;
using TurismoGoDOMAIN.Infraestructure.Data;

namespace TurismoGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        private readonly TurismoGoBdContext _context;

        public HistorialController(TurismoGoBdContext context)
        {
            _context = context;
        }



        // GET: api/historial/{usuarioId}
        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Reservas>>> GetHistorialByUsuarioId(int usuarioId)
        {
            return await _context.Reservas
                .Include(r => r.Actividad)
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}
