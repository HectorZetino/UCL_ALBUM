using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCL_StickerAlbum.Models;

namespace UCL_StickerAlbum.Instancia
{
    public class Datos
    {
        private static Datos _instance = null;

        public static Datos Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Datos();
                }
                return _instance;
            }

        }

        public Album UCLAlbum = new Album();
    }
}