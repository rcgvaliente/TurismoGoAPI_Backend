using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurismoGoDOMAIN.Core.DTO
{
    public class UsuarioRequest()
    {
        public string nombre { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
