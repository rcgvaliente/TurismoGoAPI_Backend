using System;
using System.Collections.Generic;

namespace TurismoGoDOMAIN.Core.Entities;

public partial class Usuarios
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Actividades> Actividades { get; set; } = new List<Actividades>();

    public virtual ICollection<Historial> Historial { get; set; } = new List<Historial>();

    public virtual ICollection<Reservas> Reservas { get; set; } = new List<Reservas>();

    public virtual ICollection<Resenias> Resenias { get; set; } = new List<Resenias>();
}
