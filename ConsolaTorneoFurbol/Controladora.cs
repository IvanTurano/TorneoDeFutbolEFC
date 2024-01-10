using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD;
using Microsoft.EntityFrameworkCore;

namespace TorneoFutbolEntity
{
    class Controladora
    {
        static void Main()
        {
            DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder = new DbContextOptionsBuilder<TorneoFutbolContext>();
            OptionsBuilder.UseSqlServer(@"Server=IVAN\SQLEXPRESS; Database=TorneoFutbol; Trusted_Connection=True; TrustServerCertificate=True;");

            bool again = true;
            char op;

            do
            {
                char.TryParse(Interfaz.DarOpcion().ToUpper(), out op);
                string nom, ape, posi;
                int codigo,id;
                switch (op)
                {
                    case 'A':
                        codigo = int.Parse(Interfaz.PedirDato("Codigo del equipo"));
                        nom = Interfaz.PedirDato("Nombre del equipo");
                        Funciones.agregarEquipo(OptionsBuilder,codigo,nom);
                        break;
                    case 'B':
                        nom = Interfaz.PedirDato("Nombre del jugador");
                        ape = Interfaz.PedirDato("Apellido del jugador");
                        posi = Interfaz.PedirDato("Posicion");
                        codigo = int.Parse(Interfaz.PedirDato("Codigo del equipo"));
                        Funciones.agregarJugador(OptionsBuilder,nom,ape, posi,codigo);
                        break;
                    case 'C':
                        nom = Interfaz.PedirDato("Nombre del entrenador");
                        ape = Interfaz.PedirDato("Apellido del entrenador");
                        codigo = int.Parse(Interfaz.PedirDato("Codigo del equipo"));
                        Funciones.agregarEntrenador(OptionsBuilder, nom, ape, codigo);
                        break;
                    case 'D':
                        Funciones.mostrarEquipos(OptionsBuilder);
                        break;
                    case 'E':
                        Funciones.mostrarEquiposSinJugadores(OptionsBuilder);
                        codigo = int.Parse(Interfaz.PedirDato("Codigo de equipo a editar"));
                        nom = Interfaz.PedirDato("Nuevo nombre");
                        Funciones.editarEquipo(OptionsBuilder, nom, codigo);
                        break;
                    case 'F':
                        Funciones.mostrarJugadores(OptionsBuilder);
                        id = int.Parse(Interfaz.PedirDato("Id de jugador a editar"));
                        nom = Interfaz.PedirDato("Nombre del jugador");
                        ape = Interfaz.PedirDato("Apellido del jugador");
                        posi = Interfaz.PedirDato("Posicion");
                        codigo = int.Parse(Interfaz.PedirDato("Codigo del equipo"));
                        Funciones.editarJugador(OptionsBuilder,id,nom,ape,posi,codigo);
                        break;
                    case 'G':
                        Funciones.mostrarEntrenadores(OptionsBuilder);
                        id = int.Parse(Interfaz.PedirDato("Id de jugador a editar"));
                        nom = Interfaz.PedirDato("Nombre del jugador");
                        ape = Interfaz.PedirDato("Apellido del jugador");
                        codigo = int.Parse(Interfaz.PedirDato("Codigo del equipo"));
                        Funciones.editarEntrenador(OptionsBuilder,id,nom,ape,codigo);
                        break;
                    case 'H':
                        Funciones.mostrarEquiposSinJugadores(OptionsBuilder);
                        codigo = int.Parse(Interfaz.PedirDato("Codigo de equipo a eliminar"));
                        Funciones.eliminarEquipo(OptionsBuilder,codigo);
                        break;
                    case 'I':
                        Funciones.mostrarJugadores(OptionsBuilder);
                        id = int.Parse(Interfaz.PedirDato("Id del jugador a eliminar"));
                        Funciones.eliminarJugador(OptionsBuilder,id);
                        break;
                    case 'J':
                        Funciones.mostrarEntrenadores(OptionsBuilder);
                        id = int.Parse(Interfaz.PedirDato("Id del entrenador a eliminar"));
                        Funciones.eliminarEntrenador(OptionsBuilder, id);
                        break;
                    case 'S':
                        again = false;
                        break;

                }
            } while (again);

        }
    }
}
