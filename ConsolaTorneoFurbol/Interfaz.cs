using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorneoFutbolEntity
{
    class Interfaz
    {
        static Interfaz()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static string DarOpcion()
        {
            Console.Clear();
            Console.WriteLine("*************************");
            Console.WriteLine("*     Torneo de Futbol  *");
            Console.WriteLine("*************************");
            Console.WriteLine("\n[A] Registrar un equipo.");
            Console.WriteLine("\n[B] Registrar un jugador.");
            Console.WriteLine("\n[C] Registrar un entrenador.");
            Console.WriteLine("\n[D] Mostrar todos los equipos.");
            Console.WriteLine("\n[E] Editar nombre de un equipo.");
            Console.WriteLine("\n[F] Editar un jugador.");
            Console.WriteLine("\n[G] Editar un entrenador.");
            Console.WriteLine("\n[H] Eliminar un equipo.");
            Console.WriteLine("\n[I] Eliminar un jugador.");
            Console.WriteLine("\n[J] Eliminar un entrenador");

            Console.WriteLine("\n[S] Salir de la aplicación.");
            Console.WriteLine("\n**********************************************");
            return Interfaz.PedirDato("opción elegida");
        }
        public static string PedirDato(string nombDato)
        {
            Console.Write("[?] Ingrese " + nombDato + ": ");
            string ingreso = Console.ReadLine();
            while (ingreso == "")
            {
                Console.Write("[!] " + nombDato + "es de ingreso OBLIGATORIO:");
                ingreso = Console.ReadLine();
            }
            Console.Clear();
            return ingreso.Trim();

        }
        public static void MostrarInfo(string mensaje)
        {
            Console.WriteLine(mensaje);
            Console.Write("<Pulse Enter>");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
