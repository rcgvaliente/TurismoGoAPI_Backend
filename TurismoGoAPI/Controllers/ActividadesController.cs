using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGoDOMAIN.Core.Entities;
using TurismoGoDOMAIN.Infraestructure.Data;
using static TurismoGoDOMAIN.Core.DTO.ActividadesDTO;
using static TurismoGoDOMAIN.Core.DTO.ReseniasDTO;

namespace TurismoGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController : ControllerBase
    {
        private readonly TurismoGoBdContext _context;

        public ActividadesController(TurismoGoBdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ActividadResponse>> GetActividades(int usuarioId)
        {
            var ListaActividadesDB = _context.Actividades
                .Include(a => a.Resenias)
                .ToList();

            var reservasUsuario = _context.Reservas
                .Where(r => r.UsuarioId == usuarioId)
                .Select(r => r.ActividadId)
                .ToList();

            List<ActividadResponse> ListaActividades = new List<ActividadResponse>();

            foreach (var actividadDB in ListaActividadesDB)
            {
                ActividadResponse actividadResponse = new ActividadResponse
                {
                    id = actividadDB.Id,
                    EmpresaId = actividadDB.EmpresaId,
                    Titulo = actividadDB.Titulo,
                    Descripcion = actividadDB.Descripcion,
                    Itinerario = actividadDB.Itinerario,
                    FechaInicio = actividadDB.FechaInicio.ToString("yyyy-MM-dd"),
                    FechaFin = actividadDB.FechaFin.ToString("yyyy-MM-dd"),
                    Precio = actividadDB.Precio,
                    Capacidad = actividadDB.Capacidad,
                    ReservadaPorUsuario = reservasUsuario.Contains(actividadDB.Id),
                    Resenias = actividadDB.Resenias.Select(r => new ReseniasResponse
                    {
                        Id = r.Id,
                        UsuarioId = r.UsuarioId,
                        ActividadId = r.ActividadId,
                        Calificacion = r.Calificacion,
                        Comentario = r.Comentario,
                        FechaPublicacion = r.FechaPublicacion.ToString("yyyy-MM-dd")
                    }).ToList()
                };

                ListaActividades.Add(actividadResponse);
            }

            // Ordenar la lista en orden descendente por id antes de retornarla
            return ListaActividades.OrderByDescending(a => a.id).ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<Actividades> GetActividad(int id)
        {
            var actividad = _context.Actividades.Find(id);

            if (actividad == null)
            {
                return NotFound();
            }

            return actividad;
        }

        [HttpPost]
        public ActionResult<Actividades> PostActividad(ActividadesRequest actividadesRequest)
        {
            var actividad = new Actividades{
                EmpresaId = actividadesRequest.EmpresaId,
                Titulo = actividadesRequest.Titulo,
                Descripcion = actividadesRequest.Descripcion,
                Itinerario = actividadesRequest.Itinerario,
                FechaInicio = actividadesRequest.FechaInicio,
                FechaFin = actividadesRequest.FechaFin,
                Precio = actividadesRequest.Precio,
                Capacidad = actividadesRequest.Capacidad,
                FechaPublicacion = DateTime.Now,
                Destino = ""
            };

            _context.Actividades.Add(actividad);
            _context.SaveChanges();

            return CreatedAtAction("GetActividad", new { id = actividad.Id }, actividad);
        }

        // PUT: api/Actividades/5
        [HttpPut("{id}")]
        public IActionResult PutActividad(int id, ActividadesRequest actividadesRequest)
        {
            // Buscar la actividad existente en la base de datos
            var actividadExistente = _context.Actividades.Find(id);
            if (actividadExistente == null)
            {
                return NotFound("La actividad no existe.");
            }

            // Actualizar los campos de la actividad existente con los datos de la solicitud
            actividadExistente.EmpresaId = actividadesRequest.EmpresaId;
            actividadExistente.Titulo = actividadesRequest.Titulo;
            actividadExistente.Descripcion = actividadesRequest.Descripcion;
            actividadExistente.Itinerario = actividadesRequest.Itinerario;
            actividadExistente.FechaInicio = actividadesRequest.FechaInicio;
            actividadExistente.FechaFin = actividadesRequest.FechaFin;
            actividadExistente.Precio = actividadesRequest.Precio;
            actividadExistente.Capacidad = actividadesRequest.Capacidad;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Devolver un error con detalles si ocurre una excepción de concurrencia
                return StatusCode(500, $"Error de concurrencia al actualizar la actividad: {ex.Message}");
                
            }
            catch (DbUpdateException ex)
            {
                // Manejar otros errores de actualización de base de datos
                return StatusCode(500, $"Error al actualizar la actividad en la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro tipo de error
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }

            return new JsonResult(actividadExistente);
        }

        // DELETE: api/Actividades/5
        [HttpDelete("{id}")]
        public IActionResult DeleteActividad(int id)
        {
            var actividad = _context.Actividades.Find(id);
            if (actividad == null)
            {
                return NotFound();
            }

            _context.Actividades.Remove(actividad);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
