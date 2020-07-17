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
            /// <summary>
            /// Metodo que muestra el formulario de nuevo curso
            /// </summary>
            /// <returns></returns>
            public ActionResult CursoNuevo()
            {
                CursosViewBag();    
                return View();
            }

            [HttpPost]
            /// <summary>
            /// Httpost de nuevo curso(Ingresa los datos que vienen de la vista en la BD)
            /// </summary>
            /// <returns></returns>
            public ActionResult CursoNuevo(pa_CursosRetornaSelectID_Select_Result modeloVista)
            {
                ///variable que registra la cantidad de registros afectados.
                ///si un insert, update o delete no afecta registros,hay error
            
                    int cantidadRegistrosAgectados = 0;
                    string mensaje = "";
                try
                {
                    cantidadRegistrosAgectados = this.matriculaBD.pa_Cursos_Insert(modeloVista.Nombre_Curso,
                                                                                   modeloVista.Codigo_Curso,
                                                                                   modeloVista.Codigo_Requisito);
                }
                catch (Exception error)
                {
                    ///Administrar las excepciones o errores que pasen en el try
                
                    mensaje = "Ocurrió un error: " + error.Message;

                }
                finally
                {
                    if (cantidadRegistrosAgectados > 0)
                    {
                        mensaje = "Registro Insertado";
                    }
                    else
                    {
                        mensaje += " .No se pudo ingresar";
                    }
                }

                Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
                CursosViewBag();

                return View();
            }
        #endregion

        #region CursoModificar
            public ActionResult CursoModificar(int Id_Curso)
                {
                    pa_CursosViewBag_Select_Result modeloVista = new pa_CursosViewBag_Select_Result();

                    modeloVista = this.matriculaBD.pa_CursosViewBag_Select(Id_Curso).FirstOrDefault();

                    CursosViewBag();

                    return View(modeloVista);
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