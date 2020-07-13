using ProyectoMatricula.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMatricula.Controllers
{
    public class DireccionesCarreraController : Controller
    {
        matriculaBDEntities matriculaBD = new matriculaBDEntities();

        // GET: DireccionesCarrera
        public ActionResult Index()
        {
            return View();
        }
        #region DireccionesCarreraLista
            public ActionResult DireccionesCarreraLista()
            {
                List<pa_Direcciones_de_Carrera_Select_Result> modeloVista = 
                    this.matriculaBD.pa_Direcciones_de_Carrera_Select(null, null).ToList();
                return View(modeloVista);
            }

            [HttpPost]
            public ActionResult DireccionesCarreraLista(pa_Direcciones_de_Carrera_Select_Result modeloBusqueda)
            {

                ///crear la variable que contiene los registros al 
                ///invocar el procedimiento
                List<pa_Direcciones_de_Carrera_Select_Result> modeloVista = 
                        this.matriculaBD.pa_Direcciones_de_Carrera_Select(modeloBusqueda.Nombre_Direccion_Carrera, modeloBusqueda.Director).ToList();
                return View(modeloVista);
            }
        #endregion
        #region DireccionesCarrera
        /// <summary>
        /// Metodo que Ingresa las direcciones de carrera
        /// </summary>
        /// <returns></returns>
        public ActionResult DireccionesCarreraNuevo()
            {
                return View();
            }

        /// <summary>
        /// Metodo que Ingresa  las direcciones de carrera
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DireccionesCarreraNuevo(pa_DireccionesCarrerasRetornaID_Select_Result modeloVista)
        {
            ///NOTA: HAY QUE HACER QUE SE MUESTREN LOS NOMBRES DE LOS DIRECTORES Y LOS SUBDIRECTORES PARA INGRESARLOS EN EL PROCEDIMIENTO QUE VIENE DEL FORMULARIO
            int cantidadRegistrosAgectados = 0;
            string mensaje = "";
            try
            {
                /*cantidadRegistrosAgectados = this.matriculaBD.pa_DireccionesCarrera_Insert(modeloVista.Nombre_Carrera,
                                                                                           modeloVista.Codigo_Carrera,
                                                                                           modeloVista.
                                                                                            );*/
            }
            catch (Exception error)
            {
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

            return View();
        }
        #endregion
    }
}