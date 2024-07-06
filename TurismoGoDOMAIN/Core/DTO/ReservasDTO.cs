using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurismoGoDOMAIN.Core.DTO
{
    public class ReservasDTO
    {
        public class ReservasRequest {
            public int UsuarioId { get; set; }
            public int ActividadId { get; set; }

        }

        public class ReservasResponse
        {
            public int Id { get; set; }
            public int UsuarioId { get; set; }
            public int ActividadId { get; set; }
            public string ActividadTitulo { get; set; }
            public string FechaReserva { get; set; }
            public string UsuarioNombre { get; set; }
            public string Estado { get; set; }

        }
    }
}
