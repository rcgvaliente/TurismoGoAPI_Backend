using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoGoDOMAIN.Core.Entities;
using static TurismoGoDOMAIN.Core.DTO.ReseniasDTO;

namespace TurismoGoDOMAIN.Core.DTO
{
    public class ActividadesDTO
    {
        public class ActividadesRequest()
        {
            public int EmpresaId { get; set; }

            public string Titulo { get; set; } = null!;

            public string Descripcion { get; set; } = null!;

            public string Itinerario { get; set; } = null!;
            public DateTime FechaInicio { get; set; }

            public DateTime FechaFin { get; set; }

            public decimal Precio { get; set; }

            public int Capacidad { get; set; }

            public DateTime FechaPublicacion { get; set; }
        }

        public class ActividadResponse()
        {
            public int id { get; set; }
            public int EmpresaId { get;  set; }
            public string Titulo { get;  set; }
            public string Descripcion { get;  set; }
            public string Itinerario { get;  set; }
            public string FechaInicio { get;  set; }
            public string FechaFin { get;  set; }
            public decimal Precio { get;  set; }
            public double Capacidad { get;  set; }
            public bool ReservadaPorUsuario { get; set; }
            public List<ReseniasResponse> Resenias { get; set; }
        }
    }
}
