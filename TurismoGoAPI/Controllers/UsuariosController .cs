using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGoDOMAIN.Core.DTO;
using TurismoGoDOMAIN.Core.Entities;
using TurismoGoDOMAIN.Infraestructure.Data;
using static TurismoGoDOMAIN.Core.DTO.AuthDTO;

namespace TurismoGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly TurismoGoBdContext _context;

        public UsuariosController(TurismoGoBdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsuarios()
        {
            return Ok(await _context.Usuarios.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioRequest usuarioRequest)
        {
            // Buscar el usuario existente en la base de datos
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del usuario
            usuario.Nombre = usuarioRequest.nombre;
            usuario.Email = usuarioRequest.email;
            usuario.Password = usuarioRequest.password;

            // Marcar la entidad como modificada
            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
