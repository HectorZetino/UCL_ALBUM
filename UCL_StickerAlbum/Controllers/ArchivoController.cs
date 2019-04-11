using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using UCL_StickerAlbum.Models;
using UCL_StickerAlbum.Controllers;
using UCL_StickerAlbum.Instancia;

namespace UCL_StickerAlbum.Controllers
{
    public class ArchivoController : Controller
    {
        public ActionResult CargarArchivo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CargarArchivo(HttpPostedFileBase postedFile)
        {
            var FilePath = string.Empty;
            //Evaluar si el path está vacío, existe o no.
            if (postedFile != null)
            {
                var path = Server.MapPath("~/CargaCSV/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                FilePath = path + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(FilePath);
                var CsvData = System.IO.File.ReadAllText(FilePath);
                foreach (var fila in CsvData.Split('\n'))
                {
                    var _fila = fila.Trim();
                    if (!string.IsNullOrEmpty(fila))
                    {
                        var linea = _fila.Split(',');
                        var listadoJugadores = new List<int>();
                        foreach (var num in linea)
                        {
                            int No;
                            if (int.TryParse(num, out No))
                            {
                                listadoJugadores.Add(int.Parse(num));
                            }
                        }
                        //Realizar un "Sort" definido para el listado presentado
                        listadoJugadores.Sort();
                        var equipo = new Equipo(linea[0], linea[1], listadoJugadores);
                        equipo.Calcular();
                        Datos.Instance.UCLAlbum._Equipos.Add(equipo);
                        Datos.Instance.UCLAlbum._Equipos.Sort();
                        Datos.Instance.UCLAlbum.Equipos.Add(equipo.NombreEquipo, equipo);
                        foreach (var j in equipo.Jugadores)
                        {
                            Datos.Instance.UCLAlbum.General.Add(j.Nombre, j);
                            Datos.Instance.UCLAlbum._General.Add(j);
                        }
                    }
                }
                System.IO.File.Delete(FilePath);
                Directory.Delete(path);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
