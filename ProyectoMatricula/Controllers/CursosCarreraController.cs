using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMatricula.Modelos;
namespace ProyectoMatricula.Controllers
{
    public class CursosCarreraController : Controller
    {
        #region Instancia de la base de datos
            matriculaBDEntities matriculaBD = new matriculaBDEntities();
        #endregion

        // GET: CursosCarrera
        public ActionResult Index()
        {
            return View();
        }

        #region CursoCarreraLista
            /// <summary>
            /// Metodo que muestra el formulario de cursos por carrera
            /// </summary>
            /// <returns></returns>
            public ActionResult CursoCarreraLista()
            {
                List<pa_CursoCarrera_Select_Result> modeloVista = this.matriculaBD.pa_CursoCarrera_Select(null, null).ToList();

                return View(modeloVista);
            }
            [HttpPost]
            /// <summary>
            /// Metodo que muestra el formulario de cursos por carrera
            /// </summary>
            /// <returns></returns>
            public ActionResult CursoCarreraLista(pa_CursoCarrera_Select_Result modeloBusqueda)
            {
                List<pa_CursoCarrera_Select_Result> modeloVista =
                    this.matriculaBD.pa_CursoCarrera_Select(modeloBusqueda.Nombre_Curso, modeloBusqueda.Nombre_Carrera).ToList();

                return View(modeloVista);
            }
        #endregion

    }
}