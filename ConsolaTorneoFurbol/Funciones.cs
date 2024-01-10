using BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TorneoFutbolEntity
{
    class Funciones
    {
        public static void agregarEquipo(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder, int cod, string nom )
        {
            using(var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                Equipo equi = new Equipo()
                {
                    Codigo = cod,
                    Nombre = nom
                };
                context.Add(equi);
                context.SaveChanges();
            }
        }

        public static void mostrarEquipos(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                List<Equipo> equipos = context.Equipos.Include(j => j.Jugadors).Include(e => e.Entrenadors).ToList();

                foreach(var equip in equipos)
                {
                    Console.WriteLine( "\n-----------------------------------------------------------------------------------------\n");

                    Console.WriteLine($" Codigo: {equip.Codigo} \n Nombre: {equip.Nombre} \n Jugadores: \n");

                    foreach(var jug in equip.Jugadors)
                    {
                        Console.WriteLine($" {jug.Nombre} {jug.Apellido} - Posicion: {jug.Posicion}\n");
                    }

                    Console.WriteLine(" Entrenador: \n");

                    foreach(var ent in equip.Entrenadors)
                    {
                        Console.WriteLine($" {ent.Nombre}  {ent.Apellido}\n");
                    }
  
                }
                Console.WriteLine("Pulse enter para continuar...");
                Console.ReadLine();
            }
        }

        public static void mostrarEquiposSinJugadores(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                List<Equipo> equipos = context.Equipos.Include(j => j.Jugadors).Include(e => e.Entrenadors).ToList();

                foreach (var equip in equipos)
                {
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------\n");

                    Console.WriteLine($" Codigo: {equip.Codigo} - Nombre: {equip.Nombre}");

                }
            }
        }

        public static void mostrarJugadores(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                List<Jugador> jugadores = context.Jugadors.OrderBy(e=> e.Nombre).ToList();

                foreach(var jug in jugadores)
                {
                    Console.WriteLine($"Id: {jug.Id} - {jug.Nombre} {jug.Apellido} - {jug.Posicion} - Codigo de equipo: {jug.NumEquipo} \n");
                }
            }
        }

        public static void mostrarEntrenadores(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                List<Entrenador> ents = context.Entrenadors.ToList();

                foreach(var entrenador in ents)
                {
                    Console.WriteLine($"Id: {entrenador.Id} - Nombre: {entrenador.Nombre} - Apellido: {entrenador.Apellido} - Codigo de equipo: {entrenador.NumEquipo} \n");
                }
            }
        }

        public static void agregarJugador(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder,string nom,string ape,string pos,int codEquipo)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                Jugador jug = new Jugador()
                {
                    Nombre = nom,
                    Apellido = ape,
                    Posicion = pos,
                    NumEquipo = codEquipo
                };

                context.Add(jug);
                context.SaveChanges();
            }
        }

        public static void agregarEntrenador(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder, string nom, string ape, int codEquipo)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                Entrenador ent = new Entrenador()
                {
                    Nombre = nom,
                    Apellido = ape,
                    NumEquipo = codEquipo
                };

                context.Add(ent);
                context.SaveChanges();
            }
        }

        public static void editarEquipo(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder, string nom, int cod)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                Equipo equip = context.Equipos.Find(cod);
                if (equip != null)
                {
                    equip.Nombre = nom;

                    context.Entry(equip).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Equipo no encontrado");
                }
            }
        }

        public static void editarJugador(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder,int id,string nom,string ape, string pos, int codEquipo)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                Jugador jug = context.Jugadors.Find(id);
                if(jug != null)
                {
                    jug.Nombre = nom;
                    jug.Apellido = ape;
                    jug.Posicion = pos;
                    jug.NumEquipo = codEquipo; 

                    context.Entry(jug).State = EntityState.Modified;
                    context.SaveChanges();

                }
                else
                {
                    Console.WriteLine("Jugador no existe");
                }
            }
        }

        public static void editarEntrenador(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder, int id, string nom, string ape, int codEquipo)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                Entrenador ent = context.Entrenadors.Find(id);

                if (ent != null)
                {
                    ent.Nombre = nom;
                    ent.Apellido = ape;
                    ent.NumEquipo = codEquipo;

                    context.Entry(ent).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Entrenador no existe");
                }
            }
        }

        public static void eliminarEquipo(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder,int cod)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                Equipo equip = context.Equipos.Find(cod);

                if (equip != null)
                {

                    List<Jugador> jugadores = context.Jugadors.Where(e => e.NumEquipo == cod).ToList();
                    foreach (Jugador jug in jugadores)
                    {
                        context.Jugadors.Remove(jug);
                    }
 
                    context.Equipos.Remove(equip);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("No exise ese equipo");
                }
            }
        }

        public static void eliminarJugador(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder, int id)
        {
            using (var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                Jugador jug = context.Jugadors.Find(id);

                if (jug != null)
                {
                    context.Jugadors.Remove(jug);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Jugador no existe");
                }
            }

        }

        public static void eliminarEntrenador(DbContextOptionsBuilder<TorneoFutbolContext> OptionsBuilder, int id)
        {
            using(var context = new TorneoFutbolContext(OptionsBuilder.Options))
            {
                Entrenador ent = context.Entrenadors.Find(id);

                if (ent != null)
                {
                    context.Entrenadors.Remove(ent);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Entrenador no existe");
                }
            }
        }
    }
}
