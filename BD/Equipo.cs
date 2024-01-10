using System;
using System.Collections.Generic;

namespace BD;

public partial class Equipo
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Entrenador> Entrenadors { get; set; } = new List<Entrenador>();

    public virtual ICollection<Jugador> Jugadors { get; set; } = new List<Jugador>();
}
