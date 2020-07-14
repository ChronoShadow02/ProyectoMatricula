using ProyectoMatricula.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMatricula.Controllers
{
    public class CursosController : Controller
    {
        matriculaBDEntities matriculaBD = new matriculaBDEntities();
        // GET: Cursos
        public ActionResult Index()
        {
            return View();
        }
        #region CursosLista
        /// <summary>
        /// Metodo que retorna la lista de cursos 
        /// </summary>
        /// <returns></returns>
        public ActionResult CursosLista()
        {
            List<pa_Cursos_Select_Result> modeloVista = this.matriculaBD.pa_Cursos_Select(null,null).ToList();

            return View(modeloVista);
        }

        [HttpPost]
        /// <summary>
        /// Metodo que retorna la lista de cursos mediante las busquedas del usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult CursosLista(pa_Cursos_Select_Result modeloBusqueda)
        {
            List<pa_Cursos_Select_Result> modeloVista = 
                this.matriculaBD.pa_Cursos_Select(modeloBusqueda.Nombre_Curso,modeloBusqueda.Codigo_Curso).ToList();

            return View(modeloVista);
        }
        #endregion


        #region CursoNuevo
            public ActionResult CursoNuevo()
            {
                CursosViewBag();    
                return View();
            }
        #endregion

        #region CursosViewBag
            /// <summary>
            /// Método que retorna los cursos en los que pueden ser requisitos
            /// </summary>
            void CursosViewBag()
            {
                this.ViewBag.ListaCursos = this.matriculaBD.pa_CursosCodigos_Select();
            }
        #endregion

    }
}