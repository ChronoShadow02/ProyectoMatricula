using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMatricula.Controllers
{
    public class EstudianteController : Controller
    {
        // GET: Estudiante
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Metodo que retorna la lista de estudiantes
        /// </summary>
        /// <returns></returns>
        public ActionResult EstudianteLista()
        {
            return View();
        }
        /// <summary>
        /// Metodo que Ingresa los estudiantes
        /// </summary>
        /// <returns></returns>
        public ActionResult EstudianteNuevo()
        {
            return View();
        }

        /// <summary>
        /// Metodo que Ingresa los estudiantes
        /// </summary>
        /// <returns></returns>
        public ActionResult EstudianteModifica()
        {
            return View();
        }
    }
}