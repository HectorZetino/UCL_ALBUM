using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCL_StickerAlbum.Models
{
    public class Album
    {
        //Uso de diccionario para los equipos, jugadores y vista general del llenado del álbum
        public Dictionary<string, Equipo> Equipos = new Dictionary<string, Equipo>();
        public Dictionary<string, Jugador> General = new Dictionary<string, Jugador>();
        public List<Equipo> _Equipos = new List<Equipo>();
        public List<Jugador> _General = new List<Jugador>();

        //Contructor vacío
        public Album()
        {

        }
    }
}