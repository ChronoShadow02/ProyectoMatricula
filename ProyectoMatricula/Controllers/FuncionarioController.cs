using ProyectoMatricula.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ProyectoMatricula.Controllers
{
    public class FuncionarioController : Controller
    {
        /// <summary>
        /// Instancia de la base de datos
        /// </summary>
        matriculaBDEntities matriculaBD = new matriculaBDEntities();

        // GET: Funcionario
        public ActionResult Index()
        {
            return View();
        }
        #region FuncionarioLista
            /// <summary>
            /// Metodo que lista los funcionarios
            /// </summary>
            /// <returns></returns>
            public ActionResult FuncionarioLista()
            {
                ///crear la variable que contiene los registros al 
                ///invocar el procedimiento
                List<pa_Funcionarios_Select_Result> modeloVista = matriculaBD.pa_Funcionarios_Select(null, null).ToList();

                return View(modeloVista);
            }
        #endregion

        /// <summary>
        /// Metodo que Ingresa los funcionarios
        /// </summary>
        /// <returns></returns>
        public ActionResult FuncionarioNuevo()
        {
            return View();
        }

        /// <summary>
        /// Metodo que Ingresa los funcionarios mediante el método post
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FuncionarioNuevo(pa_Funcionarios_Select_Result modeloVista)
        {
            int cantidadRegistrosAgectados = 0;
            string mensaje = "";
            try
            {
                cantidadRegistrosAgectados = matriculaBD.pa_Funcionarios_Insert(modeloVista.Nombre_Funcionario,
                                                                                modeloVista.Cedula_Funcionario,
                                                                                modeloVista.Id_Provincia,
                                                                                modeloVista.Id_Canton,
                                                                                modeloVista.Id_Distrito,
                                                                                modeloVista.Fecha_Contratacion
                                                                                );
            }
            catch (Exception error)
            {
                mensaje = "Ocurrió un error: " + error.Message;

            }
            finally
            {
                if (cantidadRegistrosAgectados>0)
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

        /// <summary>
        /// Metodo que Modifica los funcionarios
        /// </summary>
        /// <returns></returns>
        public ActionResult FuncionarioModifica()
        {
            return View();
        }


        /// <summary>
        /// Metodo que Modifica los funcionarios
        /// </summary>
        /// <returns></returns>
        public ActionResult FuncionarioElimina(int Id_Funcionario)
        {
            ///Se obtiene el registro que se desea eliminar mediante el procedimiento almacenado
            pa_FuncionariosID_Select_Result modeloVista = new pa_FuncionariosID_Select_Result();
            modeloVista = matriculaBD.pa_FuncionariosID_Select(Id_Funcionario).FirstOrDefault();

            /// se agregan los datos de las provincias, cantones y distritos
            return View();
        }

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

        /// <summary>
        /// Metodo que retorna los distritos
        /// </summary>
        /// <returns></returns>
        public ActionResult RetornaDistritos()
        {
            
            
            return View();
        }

    }
}