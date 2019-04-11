using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCL_StickerAlbum.Instancia;
using UCL_StickerAlbum.Models;

namespace UCL_StickerAlbum.Controllers
{
    public class AlbumController : Controller
    {
        // GET: Album
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VerEquipos()
        {
            return View(Datos.Instance.UCLAlbum._Equipos);
        }
        public ActionResult DetailsEquipo(string id)
        {

            return View(Datos.Instance.UCLAlbum.Equipos[id].Jugadores);
        }

        public ActionResult EditarJugador(string id)
        {
            return View(Datos.Instance.UCLAlbum.General[id]);
        }

        [HttpPost]
        public ActionResult EditarJugador(string id, FormCollection collection)
        {
            bool obtenida = false;
            var aux = collection["check"];

            if (aux == "Si" || aux == "si" || aux == "SI")
            {
                obtenida = true;
            }
            else
            {
                obtenida = false;
            }

            var repetida = int.Parse(collection["Repetida"]);

            //Genera id único para los stickers registrados
            Datos.Instance.UCLAlbum.General[id].Registrada = obtenida;
            //Genera id único para los stikers repetidos
            Datos.Instance.UCLAlbum.General[id].Repetida = repetida;

            Predicate<Jugador> BuscadorJugador = (Jugador jugador) => { return jugador.Nombre == id; };

            //Detección de cada uno de los sticker y evalua si serán registrados o estarán repetidos
            Datos.Instance.UCLAlbum._General.Find(BuscadorJugador).Registrada = obtenida;
            Datos.Instance.UCLAlbum._General.Find(BuscadorJugador).Repetida = repetida;

            foreach (var item in Datos.Instance.UCLAlbum.Equipos)
            {
                try
                {
                    item.Value.Jugadores.Find(BuscadorJugador).Registrada = obtenida;
                    item.Value.Jugadores.Find(BuscadorJugador).Repetida = repetida;

                    var name = item.Key;

                    Predicate<Equipo> BuscadorEquipo = (Equipo equipo) => { return equipo.NombreEquipo == name; };

                    Datos.Instance.UCLAlbum._Equipos.Find(BuscadorEquipo).Jugadores.Find(BuscadorJugador).Registrada = obtenida;
                    Datos.Instance.UCLAlbum._Equipos.Find(BuscadorEquipo).Jugadores.Find(BuscadorJugador).Repetida = repetida;
                    Datos.Instance.UCLAlbum._Equipos.Find(BuscadorEquipo).Calcular();
                }
                catch (Exception e)
                {
                    //Captura de excepciones para evitar errores o paros de ejecución
                }
            }
            return RedirectToAction("VerEquipos");
        }
        public ActionResult VerGeneral()
        {
            return View(Datos.Instance.UCLAlbum._General);
        }
        public ActionResult DetallesGeneral(string id)
        {
            var ID = id;
            return RedirectToAction("EditarJugador", "Album", new { id = ID });
        }
    }
}
