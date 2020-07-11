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
                List<pa_Sedes_Universitarias_Select_Result> modeloVista = new List<pa_Sedes_Universitarias_Select_Result>();

                modeloVista = matriculaBD.pa_Sedes_Universitarias_Select(null).ToList();
                return View(modeloVista);
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


        #region MyRegion
            /// <summary>
            /// Ingresa los datos de las sedes
            /// </summary>
            /// <returns></returns>
            public ActionResult SedesNuevo()
            {
                return View();
            }
        #endregion


        /// <summary>
        /// Modifica los datos de las sedes
        /// </summary>
        /// <returns></returns>
        public ActionResult SedesModifica()
        {
            return View();
        }

        ///Metodos en los que se puede visualizar las provincias, cantones y distritos
        #region RetornaProvincias
        /// <summary>
        /// Metodo que devuelve las provincias
        /// </summary>
        /// <returns></returns>
        public ActionResult RetornaProvincias()
        {
            List<RetornaProvincias_Result> Provincias = matriculaBD.RetornaProvincias(null).ToList();

            return Json(Provincias);
        }
        #endregion

        #region RetornaCantones
        /// <summary>
        /// Metodo que devuelve los cantones
        /// </summary>
        /// <returns></returns>
        public ActionResult RetornaCantones(int Id_Provincia)
        {
            List<RetornaCantones_Result> Cantones = matriculaBD.RetornaCantones(null, Id_Provincia).ToList();

            return Json(Cantones);
        }
        #endregion

        #region RetornaDiatritos
        /// <summary>
        /// Metodo que retorna los distritos
        /// </summary>
        /// <returns></returns>
        public ActionResult RetornaDistritos(int Id_Canton)
        {
            List<sp_RetornaDistritos_Result> Distritos = matriculaBD.sp_RetornaDistritos(null, Id_Canton).ToList();
            return Json(Distritos);
        }
        #endregion

    }
}