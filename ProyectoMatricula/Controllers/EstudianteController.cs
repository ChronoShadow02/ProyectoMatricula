using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMatricula.Modelos;
namespace ProyectoMatricula.Controllers
{
    public class EstudianteController : Controller
    {
        /// <summary>
        /// instancia dela base de datos
        /// </summary>
        matriculaBDEntities matriculaBD = new matriculaBDEntities();

        // GET: Estudiante
        public ActionResult Index()
        {
            return View();
        }
        #region EstudianteLista
            /// <summary>
            /// Metodo que retorna la lista de estudiantes
            /// </summary>
            /// <returns></returns>
            public ActionResult EstudianteLista()
            {
                ///crear la variable que contiene los registros al 
                ///invocar el procedimiento

                List<pa_Estudiantes_Select_Result> modeloVista = matriculaBD.pa_Estudiantes_Select(null, null).ToList();

                return View(modeloVista);
            }
        #endregion

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