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

        #region DireccionesCarreraNuevo
            /// <summary>
            /// Metodo que Ingresa las direcciones de carrera
            /// </summary>
            /// <returns></returns>
            public ActionResult DireccionesCarreraNuevo()
            {
                this.CargarDirectoresViewBag();
                this.CargarSubdirectoresViewBag();
                return View();
            }

        /// <summary>
        /// Metodo que Ingresa  las direcciones de carrera
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DireccionesCarreraNuevo(pa_DireccionesCarrerasRetornaID_Select_Result modeloVista)
        {
            
            int cantidadRegistrosAgectados = 0;
            string mensaje = "";
            try
            {
                cantidadRegistrosAgectados = this.matriculaBD.pa_DireccionesCarrera_Insert(modeloVista.Nombre_Direccion_Carrera,
                                                                                           modeloVista.Codigo_Direccion_Carrera,
                                                                                           modeloVista.Id_Director,
                                                                                           modeloVista.Id_Subdirector
                                                                                           );
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
            this.CargarDirectoresViewBag();
            this.CargarSubdirectoresViewBag();
            return View();
        }
        #endregion

        #region CargarDirectoresViewBag

        /// <summary>
        /// Metodo que carga los directores
        /// </summary>
        void CargarDirectoresViewBag()
        {
            this.ViewBag.ListaDirectores = this.matriculaBD.pa_Sedes_Universitarias_DirectorViewBag_Select().ToList();
        }
        #endregion

        #region CargarSubdirectoresViewBag
        /// <summary>
        /// Metodo que carga los Subdirectores
        /// </summary>
        void CargarSubdirectoresViewBag()
        {
            this.ViewBag.ListaSubdirectores = this.matriculaBD.pa_Sedes_Universitarias_SubdirectorViewBag_Select().ToList();
        }
        #endregion
    }
}