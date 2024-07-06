using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGoDOMAIN.Infraestructure.Data;

namespace TurismoGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformesController : ControllerBase
    {
        private readonly TurismoGoBdContext _context;

        public InformesController(TurismoGoBdContext context)
        {
            _context = context;
        }

        // GET: api/informes/actividades-mas-reservadas
        [HttpGet("actividades-mas-reservadas")]
        public async Task<ActionResult<IEnumerable<object>>> GetActividadesMasReservadas()
        {
            var informe = await _context.Reservas
                .GroupBy(r => new { r.Actividad.Id, r.Actividad.Titulo })
                .Select(g => new
                {
                    ActividadId = g.Key.Id,
                    ActividadNombre = g.Key.Titulo,
                    TotalReservas = g.Count()
                })
                .OrderByDescending(x => x.TotalReservas)
                .ToListAsync();

            return Ok(informe);
        }

        [HttpGet("actividades-mejor-valoradas")]
        public async Task<ActionResult<IEnumerable<object>>> GetActividadesMejorValoradas()
        {
            var informe = await _context.Resenias
                .GroupBy(r => new { r.Actividad.Id, r.Actividad.Titulo })
                .Select(g => new
                {
                    ActividadId = g.Key.Id,
                    ActividadNombre = g.Key.Titulo,
                    PromedioValoracion = g.Average(x => x.Calificacion)
                })
                .OrderByDescending(x => x.PromedioValoracion)
                .ToListAsync();

            return Ok(informe);
        }

        // GET: api/informes/empresas-mas-activas
        [HttpGet("empresas-mas-activas")]
        public async Task<ActionResult<IEnumerable<object>>> GetEmpresasMasActivas()
        {
            var informe = await _context.Actividades
                .GroupBy(a => a.Empresa)
                .Select(g => new
                {
                    Empresa = g.Key,
                    TotalActividades = g.Count()
                })
                .OrderByDescending(x => x.TotalActividades)
                .ToListAsync();

            return Ok(informe);
        }

        // GET: api/informes/usuarios-por-actividad
        [HttpGet("usuarios-por-actividad")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsuariosPorActividad()
        {
            var informe = await _context.Reservas
                .GroupBy(r => new { r.Actividad.Id, r.Actividad.Titulo })
                .Select(g => new
                {
                    ActividadId = g.Key.Id,
                    ActividadNombre = g.Key.Titulo,
                    TotalUsuarios = g.Select(x => x.Usuario.Id).Distinct().Count()
                })
                .OrderByDescending(x => x.TotalUsuarios)
                .ToListAsync();

            return Ok(informe);
        }
    }
}
