using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurismoGoDOMAIN.Core.Interfaces;
using static TurismoGoDOMAIN.Core.DTO.AuthDTO;

namespace TurismoGoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var result = _authService.Register(request);
            if (!result)
            {
                return BadRequest(new { message = "User already exists" });
            }

            return Ok(new { message = "Registration successful" });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login(AuthRequest request)
        {
            var response = _authService.Authenticate(request);
            if (response == null)
            {
                return Unauthorized(new { message = "Email o contraseña incorrecto" });
            }

            return Ok(response);
        }

        // POST: api/auth/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout successful" });
        }
    }
}
