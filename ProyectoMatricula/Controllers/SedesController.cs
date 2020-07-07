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
        matriculaBDEntities matriculaBD = new matriculaBDEntities();

        // GET: Sedes
        public ActionResult Index()
        {
            return View();
        }
        #region SedesLista
            /// <summary>
            /// Muestra la lista de las sedes
            /// </summary>
            /// <returns></returns>
            public ActionResult SedesLista()
            {
                return View();
            }
            [HttpPost]
            /// <summary>
            /// Muestra la lista de las sedes con 
            /// </summary>
            /// <returns></returns>
            public ActionResult SedesLista(pa_Sedes_Universitarias_Select_Result ModeloBusqueda)
            {
                List<pa_Sedes_Universitarias_Select_Result> modeloVista = this.matriculaBD.pa_Sedes_Universitarias_Select(ModeloBusqueda.Nombre_Sede).ToList();

                return View(modeloVista);
            }
        #endregion



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