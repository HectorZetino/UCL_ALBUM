using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UCL_StickerAlbum.Models
{
    public class Equipo : IComparable
    {
        //Nombre del equipo/club
        [Display(Name = "Equipo")]
        public string NombreEquipo { get; set; }
        //Club
        [Display(Name = "Club")]
        public string Club { get; set; }
        //Listado de jugadores
        public List<Jugador> Jugadores { get; set; }
        //Nombre de los stickers ya registrados
        [Display(Name = "Registradas")]
        public int Registradas { get; set; }
        //Nombre de los stickers faltantes
        [Display(Name = "Restantes")]
        public int Restantes { get; set; }
        //Se recibirán 22 jugadores en dicho listado por reglas de convocatorio de 2 equipo de 11 jugadores
        public Equipo(string nombreEquipo, string club, List<int> jugadores)
        {
            NombreEquipo = nombreEquipo;
            Club = club;
            Jugadores = new List<Jugador>(22);

            for (int i = 0; i < Jugadores.Capacity; i++)
            {
                var j = new Jugador(NombreEquipo + (i + 1));
                Jugadores.Add(j);
            }
            foreach (var item in jugadores)
            {
                var nombre = NombreEquipo + item;

                for (int i = 0; i < Jugadores.Count; i++)
                {
                    if (Jugadores[i].Nombre == nombre)
                    {
                        if (Jugadores[i].Registrada == true)
                        {
                            Jugadores[i].Repetida++;
                        }
                        else
                        {
                            Jugadores[i].Registrada = true;
                        }

                    }
                }
            }
        }
        //Calculo de los stickers ya registrados y los restantes, así como detectar si se obtiene un sticker que no se tenía
        public void Calcular()
        {
            Registradas = 0;
            Restantes = 0;
            foreach (var item in Jugadores)
            {
                if (item.Registrada)
                {
                    Registradas++;
                }
                else
                {
                    Restantes++;
                }
            }
        }
        public int CompareTo(object obj)
        {
            var comparer = (Equipo)obj;
            return NombreEquipo.CompareTo(comparer.NombreEquipo);
        }
    }
}