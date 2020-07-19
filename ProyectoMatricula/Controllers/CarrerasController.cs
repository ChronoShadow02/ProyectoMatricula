using Microsoft.Ajax.Utilities;
using ProyectoMatricula.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ProyectoMatricula.Controllers
{
    public class CarrerasController : Controller
    {
        matriculaBDEntities matriculaBD = new matriculaBDEntities();
        // GET: Carreras
        public ActionResult Index()
        {
            return View();
        }

        #region CarrerasLista
            /// <summary>
            /// Metodo que lista las carreras
            /// </summary>
            /// <returns></returns>
            public ActionResult CarrerasLista()
            {
                List<pa_Carreras_Select_Result> modeloVista = this.matriculaBD.pa_Carreras_Select(null, null).ToList();

                return View(modeloVista);
            }

            [HttpPost]
            /// <summary>
            /// Metodo que realiza la busqueda por nombre de carrera o codigo de carrera y muestra los resultados
            /// </summary>
            /// <returns></returns>
            public ActionResult CarrerasLista(pa_Carreras_Select_Result modeloBusqueda)
            {
                List<pa_Carreras_Select_Result> modeloVista = 
                    this.matriculaBD.pa_Carreras_Select(modeloBusqueda.Nombre_Carrera, modeloBusqueda.Codigo_Carrera).ToList();

                return View(modeloVista);
            }
        #endregion

        #region CarrerasNuevo
            /// <summary>
            /// Metodo que lista las carreras
            /// </summary>
            /// <returns></returns>
            public ActionResult CarrerasNuevo()
            {
                this.CargarNombreDireccionesCarrera();
                return View();
            }
        [HttpPost]
        /// <summary>
        /// Metodo que registra las carreras mediante el método post
        /// </summary>
        /// <returns></returns>
        public ActionResult CarrerasNuevo(pa_CarrerasRetornaSelectID_Select_Result modeloVista)
        {
            ///variable que cuenta si se afectó una fila, si no afecta, hay un error 
            int cantidadRegistrosAgectados = 0; 
            ///variable que capta los errores 
            string mensaje = ""; 

            try
            {
                cantidadRegistrosAgectados = this.matriculaBD.pa_Carreras_Insert(modeloVista.Nombre_Carrera,
                                                                                 modeloVista.Codigo_Carrera,
                                                                                 modeloVista.Id_Direccion_Carrera);
            }
            catch (Exception error)
            {
                mensaje = "Ocurrió un error: " + error.Message;

            }
            finally
            {
                if (cantidadRegistrosAgectados > 0)
                {
                    mensaje = "Registro agregado.";
                }
                else
                {
                    mensaje += " .No se pudo ingresar.";
                }
            }
            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
            this.CargarNombreDireccionesCarrera();
            return View();
        }
        #endregion

        #region CarrerasModifica
            /// <summary>
            /// Muestra los datos las carreras universitarias dependiendo del Id de la carrera
            /// </summary>
            /// <returns></returns>
            public ActionResult CarrerasModifica(int Id_Carrera_Univeriatria)
            {
                pa_CarrerasViewBag_Select_Result modeloVista = new pa_CarrerasViewBag_Select_Result();

                modeloVista = this.matriculaBD.pa_CarrerasViewBag_Select(Id_Carrera_Univeriatria).FirstOrDefault();

                this.CargarNombreDireccionesCarrera();

                return View(modeloVista);
            }
            [HttpPost]
            /// <summary>
            /// Modifica los datos las carreras universitarias dependiendo del Id de la carrera
            /// </summary>
            /// <returns></returns>
            public ActionResult CarrerasModifica(pa_CarrerasViewBag_Select_Result modeloVista)
            {
                ///Variable que registra la cantidad de registros afectados
                ///si un procedimiento ejecuta insert, update, delete 
                ///no afecta registros implica que hubo un error
                int cantidadRegistrosAgectados = 0;

                string resultado = "";
                try
                {
                    cantidadRegistrosAgectados = this.matriculaBD.pa_Carreras_Update(modeloVista.Id_Carrera_Univeriatria,
                                                                                     modeloVista.Nombre_Carrera,
                                                                                     modeloVista.Codigo_Carrera,
                                                                                     modeloVista.Id_Direccion_Carrera);
                }
                catch (Exception error)
                {
                    resultado = "Ocurrio un error " + error.Message;
                }
                finally
                {
                    if (cantidadRegistrosAgectados > 0)
                    {
                        resultado = "Registro Modificado";
                    }
                    else
                    {
                        resultado += ".No se pudo modificar";
                    }
                }
                Response.Write("<script language=javascript>alert('" + resultado + "');</script>");

                this.CargarNombreDireccionesCarrera();

                return View(modeloVista);
            }
        #endregion

        #region CarrerasElimina
            /// <summary>
            /// Muestra los datos las carreras universitarias dependiendo del Id de la carrera
            /// </summary>
            /// <returns></returns>
            public ActionResult CarrerasElimina(int Id_Carrera_Univeriatria)
            {
                pa_CarrerasViewBag_Select_Result modeloVista = new pa_CarrerasViewBag_Select_Result();

                modeloVista = this.matriculaBD.pa_CarrerasViewBag_Select(Id_Carrera_Univeriatria).FirstOrDefault();

                this.CargarNombreDireccionesCarrera();

                return View(modeloVista);
            }

            [HttpPost]
            /// <summary>
            /// Metodo que elimina los datos las carreras universitarias dependiendo del Id de la carrera
            /// </summary>
            /// <returns></returns>
            public ActionResult CarrerasElimina(pa_CarrerasViewBag_Select_Result modeloVista)
            {
                ///Variable que registra la cantidad de registros afectados
                ///si un procedimiento ejecuta insert, update, delete 
                ///no afecta registros implica que hubo un error
                int cantidadRegistrosAgectados = 0;

                string resultado = "";
                try
                {
                    cantidadRegistrosAgectados = this.matriculaBD.pa_Carreras_Delete(modeloVista.Id_Carrera_Univeriatria);
                }
                catch (Exception error)
                {
                    resultado = "Ocurrio un error " + error.Message;
                }
                finally
                {
                    if (cantidadRegistrosAgectados > 0)
                    {
                        resultado = "Registro Modificado";
                    }
                    else
                    {
                        resultado += ".No se pudo modificar";
                    }
                }
                Response.Write("<script language=javascript>alert('" + resultado + "');</script>");

                this.CargarNombreDireccionesCarrera();
                return View(modeloVista);
            }
        #endregion

        #region CargarNombreDireccionesCarrera
        /// <summary>
        /// Metodo que carga el nombre y el id de las direcciones de carrera
        /// </summary>
        void CargarNombreDireccionesCarrera()
            {
                this.ViewBag.ListaNombresDireccionesCarrera = this.matriculaBD.pa_CarrerasNombreDireccionViewBag_Select();
            }
        #endregion
    }
}