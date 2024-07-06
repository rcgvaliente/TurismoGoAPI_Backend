using System;
using System.Collections.Generic;

namespace TurismoGoDOMAIN.Core.Entities;

public partial class Resenias
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int ActividadId { get; set; }

    public int Calificacion { get; set; }

    public string? Comentario { get; set; }

    public DateTime FechaPublicacion { get; set; }

    public virtual Actividades Actividad { get; set; } = null!;

    public virtual Usuarios Usuario { get; set; } = null!;
}
