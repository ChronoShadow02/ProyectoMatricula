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

        #region SedesNuevo
            /// <summary>
            /// Ingresa los datos de las sedes
            /// </summary>
            /// <returns></returns>
            public ActionResult SedesNuevo()
            {
                this.CargarDirectoresViewBag();
                return View();
            }
        [HttpPost]
        /// <summary>
        /// Ingresa los datos de las sedes
        /// </summary>
        /// <returns></returns>
        public ActionResult SedesNuevo(pa_Sedes_UniversitariasID_Select_Result modeloVista)
        {
            int cantidadRegistrosAgectados = 0;
            string mensaje = "";
            try
            {
                cantidadRegistrosAgectados = this.matriculaBD.pa_Sedes_Universitarias_Insert(modeloVista.Nombre_Sede,
                                                                                             modeloVista.Codigo_Sede,
                                                                                             modeloVista.Id_Director,
                                                                                             modeloVista.Id_Provincia,
                                                                                             modeloVista.Id_Canton,
                                                                                             modeloVista.Id_Distrito,
                                                                                             modeloVista.Direccion_Fisica);
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
            return View();
        }
        #endregion

        #region SedesModifica
            /// <summary>
            /// Modifica los datos de las sedes
            /// </summary>
            /// <returns></returns>
            public ActionResult SedesModifica(int Id_Sedes_universitarias)
            {
                pa_Sedes_UniversitariasViewBag_Select_Result modeloVista = new pa_Sedes_UniversitariasViewBag_Select_Result();

                modeloVista = this.matriculaBD.pa_Sedes_UniversitariasViewBag_Select(Id_Sedes_universitarias).FirstOrDefault();

                this.CargarDirectoresViewBag();

                this.RetornaProvinciasViewBag();

                this.RetornaCantonesViewBag(modeloVista.Id_Provincia);

                this.RetornaDistritosViewBag(modeloVista.Id_Canton);

                return View(modeloVista);
            }

            [HttpPost]
        /// <summary>
        /// Modifica los datos de las sedes
        /// </summary>
        /// <returns></returns>
        public ActionResult SedesModifica(pa_Sedes_UniversitariasID_Select_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento ejecuta insert, update, delete 
            ///no afecta registros implica que hubo un error
            int cantidadRegistrosAgectados = 0;

            string resultado = "";
            try
            {
                cantidadRegistrosAgectados = this.matriculaBD.pa_Sedes_Universitarias_Update(modeloVista.Id_Sedes_universitarias,
                                                                                            modeloVista.Nombre_Sede,
                                                                                            modeloVista.Codigo_Sede,
                                                                                            modeloVista.Id_Director,
                                                                                            modeloVista.Id_Provincia,
                                                                                            modeloVista.Id_Canton,
                                                                                            modeloVista.Id_Distrito,
                                                                                            modeloVista.Direccion_Fisica
                                                                                            );
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

            this.RetornaProvinciasViewBag();

            this.RetornaCantonesViewBag(modeloVista.Id_Provincia);

            this.RetornaDistritosViewBag(modeloVista.Id_Canton);

            this.CargarDirectoresViewBag();

            pa_Sedes_UniversitariasViewBag_Select_Result modelView = new pa_Sedes_UniversitariasViewBag_Select_Result();

            return View(modelView);

        }
        #endregion

        #region SedesElimina
            /// <summary>
            /// Metodo que muestra los datos del registro a eliminar
            /// </summary>
            /// <returns></returns>
            public ActionResult SedesElimina(int Id_Sedes_universitarias)
            {
                pa_Sedes_UniversitariasViewBag_Select_Result modeloVista = new pa_Sedes_UniversitariasViewBag_Select_Result();

                modeloVista = this.matriculaBD.pa_Sedes_UniversitariasViewBag_Select(Id_Sedes_universitarias).FirstOrDefault();

                this.CargarDirectoresViewBag();

                this.RetornaProvinciasViewBag();

                this.RetornaCantonesViewBag(modeloVista.Id_Provincia);

                this.RetornaDistritosViewBag(modeloVista.Id_Canton);

                return View(modeloVista);
            }

        [HttpPost]
            /// <summary>
            /// Metodo que elimina los datos del registro a eliminar
            /// </summary>
            /// <returns></returns>
            public ActionResult SedesElimina(pa_Sedes_UniversitariasViewBag_Select_Result modeloVista)
            {
                ///Variable que registra la cantidad de registros afectados
                ///si un procedimiento ejecuta insert, update, delete 
                ///no afecta registros implica que hubo un error
                int cantidadRegistrosAgectados = 0;

                string resultado = "";
                try
                {
                    cantidadRegistrosAgectados = this.matriculaBD.pa_Sedes_Universitarias_Delete(modeloVista.Id_Sedes_universitarias);
                }
                catch (Exception error)
                {
                    resultado = "Ocurrio un error " + error.Message;
                }
                finally
                {
                    if (cantidadRegistrosAgectados > 0)
                    {
                        resultado = "Registro Eliminado";
                    }
                    else
                    {
                        resultado += ".No se pudo eliminar";
                    }
                }
                Response.Write("<script language=javascript>alert('" + resultado + "');</script>");
                this.CargarDirectoresViewBag();

                this.RetornaProvinciasViewBag();

                this.RetornaCantonesViewBag(modeloVista.Id_Provincia);

                this.RetornaDistritosViewBag(modeloVista.Id_Canton);

                return View(modeloVista);
            }
        #endregion

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

        #region CargarDirectoresViewBag

            /// <summary>
            /// Metodo que carga los directores
            /// </summary>
            void CargarDirectoresViewBag()
            {
                this.ViewBag.ListaDirectores = this.matriculaBD.pa_Sedes_Universitarias_DirectorViewBag_Select();
            }
        #endregion

        #region RetornaProvinciasViewBag
        /// <summary>
        /// Metodo que retorna las provincias y las guarda en el ViewBag
        /// </summary>
        void RetornaProvinciasViewBag()
        {
            this.ViewBag.ListaProvincias = this.matriculaBD.RetornaProvincias(null).ToList();
        }
        #endregion

        #region RetornaCantonesViewBag
        /// <summary>
        /// Metodo que retorna las los cantones dependiendo de la provincia seleccionada
        /// </summary>
        void RetornaCantonesViewBag(int Id_Provincia)
        {
            this.ViewBag.ListaCantones = this.matriculaBD.RetornaCantones(null, Id_Provincia).ToList();
        }
        #endregion

        #region RetornaDistritosViewBag
        /// <summary>
        /// Metodo que retorna las los distritos dependiendo del cantón seleccionado
        /// </summary>
        void RetornaDistritosViewBag(int Id_Canton)
        {
            this.ViewBag.ListaDistritos = this.matriculaBD.sp_RetornaDistritos(null, Id_Canton).ToList();
        }

        #endregion
    }
}