using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMatricula.Modelos;
namespace ProyectoMatricula.Controllers
{
    public class SedesController : Controller
    {


        // GET: Sedes
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Muestra la lista de las sedes
        /// </summary>
        /// <returns></returns>
        public ActionResult SedesLista()
        {
            return View();
        }


        /// <summary>
        /// Ingresa los datos de las sedes
        /// </summary>
        /// <returns></returns>
        public ActionResult SedesNuevo()
        {
            return View();
        }

        /// <summary>
        /// Modifica los datos de las sedes
        /// </summary>
        /// <returns></returns>
        public ActionResult SedesModifica()
        {
            return View();
        }
    }
}