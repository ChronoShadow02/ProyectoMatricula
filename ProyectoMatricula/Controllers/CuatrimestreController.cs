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
        #region CuatrimestreLista
            /// <summary>
            /// Metodo que lista los cuatrimestres
            /// </summary>
            /// <returns></returns>
            public ActionResult CuatrimestreLista()
            {
                List<pa_Cuatrimestre_Select_Result> modeloVista = this.matriculaBD.pa_Cuatrimestre_Select(null, null, null).ToList();

                return View(modeloVista);
            }

        #endregion
        #region CuatrimestreNuevo
        public ActionResult CuatrimestreNuevo()
            {
                this.Lista_Num_CuatrimestreViewBag();
                this.CargarSedesUniversitariasViewbag();
                return View();
            }
            [HttpPost]
            public ActionResult CuatrimestreNuevo(pa_CuatrimestreDiferenteSedes_Result modeloVista)
            {
                string mensaje = "";
                int RegistrosAfectados = 0;

                try
                {


                    #region Verificacion de cuatrimestre por año y por sede
                    ///Se verifica si ya existe un cuatrimestre por año y por sede
                    ///NOTA: ESTE SELECT ESTA FALLANDO, HAY QUE USAR EL SELECT CON TODO "AND" SIN BUSQUEDAS PARA QUE TODO LO DEMAS SIRVA
                    pa_CuatrimestreDiferenteSedes_Result ModeloVerificar 
                             = this.matriculaBD.pa_CuatrimestreDiferenteSedes(modeloVista.Id_Num_Cuatrimestre,
                                                                              modeloVista.Anio_Cuatrimestre,
                                                                              modeloVista.Id_Sede_Universitarias).FirstOrDefault();

                    if (ModeloVerificar != null)
                    {
                        ///Se consulta si el numero de cuatrimestre, el año 
                        ///y la sede ingresada existe en la base de datos
                        if (ModeloVerificar.Id_Num_Cuatrimestre == modeloVista.Id_Num_Cuatrimestre
                            && ModeloVerificar.Anio_Cuatrimestre == modeloVista.Anio_Cuatrimestre
                            && ModeloVerificar.Id_Sede_Universitarias == modeloVista.Id_Sede_Universitarias)
                        {
                            mensaje = "Este cuatrimestre por año y por sede ya existe.";
                        }

                    }
                    #endregion

                    #region Verificacion de Solo cuatrimestre e insercion de datos si no existe la verificacion anterior
                    ///Se consulta si ya existen los cuatrimestes que el usuario ingresó
                    pa_Cuatrimestre_VerificaSoloCuatrimestre_Result VerificaCuatrimestre
                        = this.matriculaBD.pa_Cuatrimestre_VerificaSoloCuatrimestre(modeloVista.Anio_Cuatrimestre, 
                                                                                    modeloVista.Id_Sede_Universitarias).FirstOrDefault();


                    if (VerificaCuatrimestre != null)
                    {
                        ///Si el resultado del modelo verificar es null( o no existe en la base de datos)
                        ///Se ingresará en la base de datos
                        if (ModeloVerificar == null && VerificaCuatrimestre.Cuatrimestres_Por_Anio <= 3)
                        {
                            RegistrosAfectados = this.matriculaBD.pa_Cuatrimestre_Insert(modeloVista.Id_Num_Cuatrimestre,
                                                                                         modeloVista.Anio_Cuatrimestre,
                                                                                         modeloVista.Id_Sede_Universitarias);
                        }

                        if (VerificaCuatrimestre.Cuatrimestres_Por_Anio > 3)
                        {
                            mensaje += "No se pueden ingresar más cuatrimestres.";
                        }

                    }
                    #endregion

                    ///Si del todo no existe el registro
                    if (ModeloVerificar == null && VerificaCuatrimestre == null)
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
                    if (RegistrosAfectados > 0)
                    {
                        mensaje = "Registro insertado";
                    }
                    else
                    {
                        mensaje += " No se pudo ingresar.";
                    }
                }
                Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
                this.Lista_Num_CuatrimestreViewBag();
                this.CargarSedesUniversitariasViewbag();
                return View();
            }
        #endregion


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