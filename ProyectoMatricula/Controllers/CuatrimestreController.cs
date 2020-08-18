using Microsoft.Ajax.Utilities;
using ProyectoMatricula.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMatricula.Controllers
{
    public class CuatrimestreController : Controller
    {
        #region Instancia  de la base de datos
            matriculaBDEntities matriculaBD = new matriculaBDEntities();
        #endregion


        // GET: Cuatrimestre
        public ActionResult CuatrimestreNuevo()
        {
            this.Lista_Num_CuatrimestreViewBag();
            this.CargarSedesUniversitariasViewbag();
            return View();
        }
        [HttpPost]
        public ActionResult CuatrimestreNuevo(pa_Cuatrimestre_Select_Result modeloVista)
        {
            string mensaje = "";
            int RegistrosAfectados = 0;

            try
            {
                ///Se verifica si ya existe un cuatrimestre por año y por sede
               pa_Cuatrimestre_Select_Result ModeloVerificar = this.matriculaBD.pa_Cuatrimestre_Select(modeloVista.Id_Num_Cuatrimestre,
                                                                                                       modeloVista.Anio_Cuatrimestre,
                                                                                                       modeloVista.Id_Sede_Universitarias).FirstOrDefault();


                ///Se consulta si el numero de cuatrimestre, el año 
                ///y la sede ingresada existe en la base de datos
                if (ModeloVerificar.Id_Num_Cuatrimestre == modeloVista.Id_Num_Cuatrimestre
                    && ModeloVerificar.Anio_Cuatrimestre == modeloVista.Anio_Cuatrimestre
                    && ModeloVerificar.Id_Sede_Universitarias == modeloVista.Id_Sede_Universitarias)
                {
                    mensaje = "Este cuatrimestre por año y por sede ya existe.";
                }

                ///Se consulta si ya existe 
                if (true)
                {

                }

                ///Si el resultado del modelo verificar es null( o no existe en la base de datos)
                ///Se ingresará e
                if (ModeloVerificar == null)
                {
                    RegistrosAfectados = this.matriculaBD.pa_Cuatrimestre_Insert(modeloVista.Id_Num_Cuatrimestre,
                                                                                 modeloVista.Anio_Cuatrimestre,
                                                                                 modeloVista.Id_Sede_Universitarias);
                }
            }
            catch (Exception error)
            {
                mensaje = "Hubo un error." + error.Message;
                
            }
            finally
            {
                if ( RegistrosAfectados > 0 )
                {
                    mensaje = "Registro insertado";
                }
                else
                {
                    mensaje += " No se pudo ingresar.";
                }
            }
            return View();
        }

        #region Número de cuatrimestre viewbag
            /// <summary>
            /// Metodo que muestra el número de cuatrimestre
            /// </summary>
            void Lista_Num_CuatrimestreViewBag()
            {
                this.ViewBag.Lista_Num_Cuatrimestre = this.matriculaBD.pa_Cuatrimestre_Num_CuatrimiestreViewBag().ToList();
            }
        #endregion

        #region Nombre de Sedes Universitarias
            /// <summary>
            /// Método que obtiene las sedes universitarias
            /// </summary>
            void CargarSedesUniversitariasViewbag()
            {
                this.ViewBag.ListaSedes = this.matriculaBD.pa_Sedes_UniversitariasID_Select().ToList();
            }
        #endregion
    }
}