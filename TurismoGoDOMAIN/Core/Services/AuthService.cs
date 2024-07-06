using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TurismoGoDOMAIN.Core.Interfaces;
using static TurismoGoDOMAIN.Core.DTO.AuthDTO;
using TurismoGoDOMAIN.Infraestructure.Data;
using TurismoGoDOMAIN.Core.Entities;

namespace TurismoGoDOMAIN.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly TurismoGoBdContext _context;
        private readonly string _key;

        public AuthService(TurismoGoBdContext context, IConfiguration configuration)
        {
            _context = context;     
            _key = "this_is_a_very_secure_key_with_32_chars";
        }

        public AuthResponse Authenticate(AuthRequest request)
        {
            var user = _context.Usuarios.SingleOrDefault(x => x.Email == request.Email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Tipo)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthResponse { Token = tokenHandler.WriteToken(token),Tipo=user.Tipo, Id = user.Id };
        }

        public bool Register(RegisterRequest request)
        {
            if (_context.Usuarios.Any(x => x.Email == request.Email))
            {
                return false;
            }

            var user = new Usuarios
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Tipo = request.Tipo,
                Password = request.Password
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();
            return true;
        }
    }
}
