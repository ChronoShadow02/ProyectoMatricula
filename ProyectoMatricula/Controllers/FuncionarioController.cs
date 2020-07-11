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

        /// <summary>
        /// Funcion que realiza busqueda de funcionarios por medio de nombre y cedula
        /// </summary>
        /// <param name="modeloBusqueda">modelo a buscar </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FuncionarioLista(pa_Funcionarios_Select_Result modeloBusqueda)
        {
            ///crear la variable que contiene los registros al 
            ///invocar el procedimiento
            List<pa_Funcionarios_Select_Result> modeloVista =
                matriculaBD.pa_Funcionarios_Select(modeloBusqueda.Nombre_Funcionario, modeloBusqueda.Cedula_Funcionario).ToList();

            return View(modeloVista);
        }
        #endregion


        #region FuncionarioNuevo
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
        public ActionResult FuncionarioNuevo(pa_FuncionariosRetornaSelectID_Select_Result modeloVista)
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


        #region FuncionarioModifica
        /// <summary>
        /// Metodo que Muestra el funcionario a modificar
        /// </summary>
        /// <returns></returns>
        public ActionResult FuncionarioModifica(int Id_Funcionario)
        {
            pa_FuncionariosViewBag_Select_Result modeloVista = new pa_FuncionariosViewBag_Select_Result();

            modeloVista = matriculaBD.pa_FuncionariosViewBag_Select(Id_Funcionario).FirstOrDefault();

            this.RetornaProvinciasViewBag();

            this.RetornaCantonesViewBag(modeloVista.Id_Provincia);

            this.RetornaDistritosViewBag(modeloVista.Id_Canton);

            return View(modeloVista);
        }

        /// <summary>
        /// Metodo que Modifica los funcionarios
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FuncionarioModifica(pa_FuncionariosRetornaSelectID_Select_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento ejecuta insert, update, delete 
            ///no afecta registros implica que hubo un error
            int cantidadRegistrosAgectados = 0;

            string resultado = "";
            try
            {
                cantidadRegistrosAgectados = this.matriculaBD.pa_Funcionarios_Update(modeloVista.Id_Funcionario,
                                                                                     modeloVista.Nombre_Funcionario,
                                                                                     modeloVista.Cedula_Funcionario,
                                                                                     modeloVista.Id_Provincia,
                                                                                     modeloVista.Id_Canton,
                                                                                     modeloVista.Id_Distrito,
                                                                                     modeloVista.Fecha_Contratacion);
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

            pa_FuncionariosViewBag_Select_Result modelView = new pa_FuncionariosViewBag_Select_Result();

            return View(modelView);
        }
        #endregion

        #region FuncionarioElimina
            /// <summary>
            /// Metodo que elimina los funcionarios
            /// </summary>
            /// <returns></returns>
            public ActionResult FuncionarioElimina(int Id_Funcionario)
            {
                ///Se obtiene el registro que se desea eliminar mediante el procedimiento almacenado
                pa_FuncionariosViewBag_Select_Result modeloVista = new pa_FuncionariosViewBag_Select_Result();

                modeloVista = matriculaBD.pa_FuncionariosViewBag_Select(Id_Funcionario).FirstOrDefault();

                /// se agregan los datos de las provincias, cantones y distritos
                this.RetornaProvinciasViewBag();

                this.RetornaCantonesViewBag(modeloVista.Id_Provincia);

                this.RetornaDistritosViewBag(modeloVista.Id_Canton);

                return View(modeloVista);
            }

            [HttpPost]
            public ActionResult FuncionarioElimina(pa_FuncionariosRetornaSelectID_Select_Result modeloVista)
            {
                ///Variable que registra la cantidad de registros afectados
                ///si un procedimiento ejecuta insert, update, delete 
                ///no afecta registros implica que hubo un error
                int cantidadRegistrosAgectados = 0;

                string resultado = "";
                try
                {
                    cantidadRegistrosAgectados = this.matriculaBD.pa_Funcionarios_Delete(modeloVista.Id_Funcionario);
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
                return View(modeloVista);
            }

        #endregion

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
            this.ViewBag.ListaCantones = this.matriculaBD.RetornaCantones(null,Id_Provincia).ToList();
        }
        #endregion

        #region RetornaDistritosViewBag
        /// <summary>
        /// Metodo que retorna las los distritos dependiendo del cantón seleccionado
        /// </summary>
        void RetornaDistritosViewBag(int Id_Canton)
            {
                this.ViewBag.ListaDistritos = this.matriculaBD.sp_RetornaDistritos(null,Id_Canton).ToList();
            }

        #endregion
    }
    
}