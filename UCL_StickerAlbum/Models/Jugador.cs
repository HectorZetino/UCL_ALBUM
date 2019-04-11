using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCL_StickerAlbum.Models
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public bool Registrada { get; set; }
        public int Repetida { get; set; }
        //Seteo de valores a variables previamente declaradas
        public Jugador(string nombre)
        {
            Nombre = nombre;
            Registrada = false;
            Repetida = 0;
        }
    }
}