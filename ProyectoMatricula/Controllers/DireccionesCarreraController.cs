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
                    ///Se busca algun registro que tenga la cédula que se ingresó
                    pa_Direcciones_de_Carrera_ValidarNombreCodigo_Select_Result Nombre_Y_CodigoAVerificar =
                        this.matriculaBD.pa_Direcciones_de_Carrera_ValidarNombreCodigo_Select(modeloVista.Nombre_Direccion_Carrera,
                                                                                              modeloVista.Codigo_Direccion_Carrera).FirstOrDefault();
                    /// Si a la hora de hacer la busqueda, da null,significa que no existe la cédula
                    /// por lo tanto, se puede hacer el insert,
                    /// de lo contario mostrará un mensaje de que la cédula existe

                    if (Nombre_Y_CodigoAVerificar == null)
                    {
                        cantidadRegistrosAgectados = this.matriculaBD.pa_Direcciones_de_Carrera_Insert(modeloVista.Nombre_Direccion_Carrera,
                                                                                                       modeloVista.Codigo_Direccion_Carrera,
                                                                                                       modeloVista.Id_Director,
                                                                                                       modeloVista.Id_Subdirector
                                                                                                       );
                    }
                    else
                    {
                        mensaje = "El nombre de la dirección o el código ya existe";
                    }
                
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

        #region DireccionesCarreraModificar
            public ActionResult DireccionesCarreraModificar(int Id_Direccion_Carrera)
            {
                pa_Direcciones_de_Carrera_RetornaID_Select_Result modeloVista = new pa_Direcciones_de_Carrera_RetornaID_Select_Result();

                modeloVista = this.matriculaBD.pa_Direcciones_de_Carrera_RetornaID_Select(Id_Direccion_Carrera).FirstOrDefault();

                this.CargarDirectoresViewBag();

                this.CargarSubdirectoresViewBag();

                return View(modeloVista);
            }
            [HttpPost]
            public ActionResult DireccionesCarreraModificar(pa_Direcciones_de_Carrera_RetornaID_Select_Result modelovista)
            {
                ///Variable que registra la cantidad de registros afectados
                ///si un procedimiento ejecuta insert, update, delete 
                ///no afecta registros implica que hubo un error
                int RegistrosAfectados = 0;

                string resultado = "";
            try
            {
                RegistrosAfectados = this.matriculaBD.pa_Direcciones_de_Carrera_Update(modelovista.Id_Direccion_Carrera,
                                                                                       modelovista.Nombre_Direccion_Carrera,
                                                                                       modelovista.Codigo_Direccion_Carrera,
                                                                                       modelovista.Id_Director,
                                                                                       modelovista.Id_Subdirector);
            }
            catch (Exception error)
            {
                resultado = "Ocurrio un error " + error.Message;
            }
            finally
            {
                if (RegistrosAfectados > 0)
                {
                    resultado = "Registro Modificado";
                }
                else
                {
                    resultado += ".No se pudo modificar";
                }
            }
            Response.Write("<script language=javascript>alert('" + resultado + "');</script>");

            this.CargarDirectoresViewBag();

            this.CargarSubdirectoresViewBag();

            return View(modelovista);
        }
        #endregion

        #region DireccionesCarreraEliminar
            public ActionResult DireccionesCarreraEliminar(int Id_Direccion_Carrera)
            {
                pa_Direcciones_de_Carrera_RetornaID_Select_Result modeloVista = new pa_Direcciones_de_Carrera_RetornaID_Select_Result();

                modeloVista = this.matriculaBD.pa_Direcciones_de_Carrera_RetornaID_Select(Id_Direccion_Carrera).FirstOrDefault();

                this.CargarDirectoresViewBag();

                this.CargarSubdirectoresViewBag();

                return View(modeloVista);
            }
            [HttpPost]
            public ActionResult DireccionesCarreraEliminar(pa_Direcciones_de_Carrera_RetornaID_Select_Result modeloVista)
            {
                ///Variable que registra la cantidad de registros afectados
                ///si un procedimiento ejecuta insert, update, delete 
                ///no afecta registros implica que hubo un error
                int RegistrosAfectados = 0;

                string resultado = "";
            try
            {
                RegistrosAfectados = this.matriculaBD.pa_Direcciones_de_Carrera_Delete(modeloVista.Id_Direccion_Carrera);
            }
            catch (Exception error)
            {
                resultado = "Ocurrio un error " + error.Message;
            }
            finally
            {
                if (RegistrosAfectados > 0)
                {
                    resultado = "Registro Eliminado";
                }
                else
                {
                    resultado += ".No se pudo eliminar.";
                }
            }
            Response.Write("<script language=javascript>alert('" + resultado + "');</script>");

            this.CargarDirectoresViewBag();

            this.CargarSubdirectoresViewBag();

            return View(modeloVista);
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