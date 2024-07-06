using System;
using System.Collections.Generic;

namespace TurismoGoDOMAIN.Core.Entities;

public partial class Historial
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int ActividadId { get; set; }

    public DateTime FechaActividad { get; set; }

    public virtual Actividades Actividad { get; set; } = null!;

    public virtual Usuarios Usuario { get; set; } = null!;
}
