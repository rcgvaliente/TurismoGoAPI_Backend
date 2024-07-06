using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurismoGoDOMAIN.Core.DTO
{
    public class AuthDTO
    {
        public class AuthRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class AuthResponse
        {
            public string Token { get; set; }
            public string Tipo { get; set; }
            public int Id { get; set; } 
        }

        public class RegisterRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Nombre { get; set; }
            public string Tipo { get; set; } 
        }

        public class User
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public string Nombre { get; set; }
            public string Tipo { get; set; }
        }
    }
}
