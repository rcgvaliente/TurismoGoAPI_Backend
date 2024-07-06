using System;
using System.Collections.Generic;

namespace TurismoGoDOMAIN.Core.Entities;

public partial class Actividades
{
    public int Id { get; set; }

    public int EmpresaId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Itinerario { get; set; } = null!;

    public string Destino { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public decimal Precio { get; set; }

    public int Capacidad { get; set; }

    public DateTime FechaPublicacion { get; set; }

    public virtual Usuarios Empresa { get; set; } = null!;

    public virtual ICollection<Historial> Historial { get; set; } = new List<Historial>();

    public virtual ICollection<Reservas> Reservas { get; set; } = new List<Reservas>();

    public virtual ICollection<Resenias> Resenias { get; set; } = new List<Resenias>();
}
