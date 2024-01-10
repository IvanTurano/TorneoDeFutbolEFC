using System;
using System.Collections.Generic;

namespace BD;

public partial class Jugador
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Posicion { get; set; } = null!;

    public int? NumEquipo { get; set; }

    public virtual Equipo? NumEquipoNavigation { get; set; }
}
