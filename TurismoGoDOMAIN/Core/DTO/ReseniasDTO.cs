using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurismoGoDOMAIN.Core.DTO
{
    public class ReseniasDTO
    {
        public class ReseniasRequest {

            public int UsuarioId { get; set; }

            public int ActividadId { get; set; }

            public int Calificacion { get; set; }

            public string? Comentario { get; set; }

        }

        public class ReseniasResponse
        {
            public int Id { get; set; }
            public int UsuarioId { get; set; }

            public int ActividadId { get; set; }

            public int Calificacion { get; set; }

            public string? Comentario { get; set; }

            public string FechaPublicacion { get; set; }
        }
    }
}
