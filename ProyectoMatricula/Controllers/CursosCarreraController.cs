using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMatricula.Modelos;
namespace ProyectoMatricula.Controllers
{
    public class CursosCarreraController : Controller
    {
        #region Instancia de la base de datos
            matriculaBDEntities matriculaBD = new matriculaBDEntities();
        #endregion

        // GET: CursosCarrera
        public ActionResult Index()
        {
            return View();
        }

        #region CursoCarreraLista
            /// <summary>
            /// Metodo que muestra el formulario de cursos por carrera
            /// </summary>
            /// <returns></returns>
            public ActionResult CursoCarreraLista()
            {
                List<pa_CursoCarrera_Select_Result> modeloVista = this.matriculaBD.pa_CursoCarrera_Select(null, null).ToList();

                return View(modeloVista);
            }
            [HttpPost]
            /// <summary>
            /// Metodo que muestra el formulario de cursos por carrera
            /// </summary>
            /// <returns></returns>
            public ActionResult CursoCarreraLista(pa_CursoCarrera_Select_Result modeloBusqueda)
            {
                List<pa_CursoCarrera_Select_Result> modeloVista =
                    this.matriculaBD.pa_CursoCarrera_Select(modeloBusqueda.Nombre_Curso, modeloBusqueda.Nombre_Carrera).ToList();

                return View(modeloVista);
            }
        #endregion

        #region CursoCarreraNuevo
        /// <summary>
        /// Metodo que muestra el formulario de registro de cursos por carrera
        /// </summary>
        /// <returns></returns>
        public ActionResult CursoCarreraNuevo()
        {
            this.CargarCursosViewBag();
            this.CargarNombreDireccionesCarreraViewBag();
            return View();
        }
        /// <summary>
        /// Metodo que registra la informacion obtenida de la vista anterior
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CursoCarreraNuevo(pa_CursoCarrera_Select_Result modeloVista)
        {
            ///variable que registra la cantidad de registros afectados.
            ///si un insert, update o delete no afecta registros,hay error
            int cantidadRegistrosAgectados = 0;
            string mensaje = "";
            try
            {
                cantidadRegistrosAgectados = this.matriculaBD.pa_CursoCarrera_Insert(modeloVista.Id_Curso,
                                                                                     modeloVista.Id_Carrera_Universitaria);
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

            this.CargarCursosViewBag();

            this.CargarNombreDireccionesCarreraViewBag();

            return View();
        }
        #endregion

        #region CursoCarreraModifica
        /// <summary>
        /// Metodo que muestra los datos seleccionados dependiendo del Id_Cursos_Por_Carrera
        /// </summary>
        /// <returns></returns>
        public ActionResult CursoCarreraModifica(int Id_Cursos_Por_Carrera)
            {
            pa_CursoCarreraRetornaID_Select_Result modeloVista = new pa_CursoCarreraRetornaID_Select_Result();
                        
               modeloVista =  this.matriculaBD.pa_CursoCarreraRetornaID_Select(Id_Cursos_Por_Carrera).FirstOrDefault();

                this.CargarCursosViewBag();

                this.CargarNombreDireccionesCarreraViewBag();

                return View(modeloVista);
            }
        [HttpPost]
        public ActionResult CursoCarreraModifica(pa_CursoCarreraRetornaID_Select_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento ejecuta insert, update, delete 
            ///no afecta registros implica que hubo un error
            int cantidadRegistrosAgectados = 0;

            string resultado = "";
            try
            {
                cantidadRegistrosAgectados = this.matriculaBD.pa_CursoCarrera_Update(modeloVista.Id_Cursos_Por_Carrera,
                                                                                     modeloVista.Id_Curso,
                                                                                     modeloVista.Id_Carrera_Universitaria);
            }
            catch(Exception error)
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
            this.CargarCursosViewBag();
            this.CargarNombreDireccionesCarreraViewBag();
            return View(modeloVista);
        }
        #endregion

        #region CursoCarreraElimina
        /// <summary>
        /// Metodo que muestra la vista con los datos seleccionados dependiendo del Id_Cursos_Por_Carrera
        /// </summary>
        /// <param name="Id_Cursos_Por_Carrera"></param>
        /// <returns></returns>
        public ActionResult CursoCarreraElimina(int Id_Cursos_Por_Carrera)
        {
            pa_CursoCarreraRetornaID_Select_Result modeloVista = new pa_CursoCarreraRetornaID_Select_Result();

            modeloVista = this.matriculaBD.pa_CursoCarreraRetornaID_Select(Id_Cursos_Por_Carrera).FirstOrDefault();

            this.CargarCursosViewBag();

            this.CargarNombreDireccionesCarreraViewBag();

            return View(modeloVista);
        }
        [HttpPost]
        /// <summary>
        /// Metodo que elimina el registro
        /// </summary>
        /// <param name="Id_Cursos_Por_Carrera"></param>
        /// <returns></returns>
        public ActionResult CursoCarreraElimina(pa_CursoCarreraRetornaID_Select_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento ejecuta insert, update, delete 
            ///no afecta registros implica que hubo un error
            int cantidadRegistrosAgectados = 0;

            string resultado = "";
            try
            {
                cantidadRegistrosAgectados = this.matriculaBD.pa_CursoCarrera_Delete(modeloVista.Id_Cursos_Por_Carrera);
            }
            catch (Exception error)
            {
                resultado = "Ocurrio un error " + error.Message;
            }
            finally
            {
                if (cantidadRegistrosAgectados > 0)
                {
                    resultado = "Registro eliminado";
                }
                else
                {
                    resultado += ".No se pudo eliminar";
                }
            }
            Response.Write("<script language=javascript>alert('" + resultado + "');</script>");

            this.CargarCursosViewBag();

            this.CargarNombreDireccionesCarreraViewBag();

            return View(modeloVista);
        }
        #endregion

        #region CargarCursosViewBag
        /// <summary>
        /// Método que retorna los cursos en los que pueden ser requisitos
        /// </summary>
        void CargarCursosViewBag()
            {
                this.ViewBag.ListaCursos = this.matriculaBD.pa_CursosCodigos_Select().ToList();
            }
        #endregion

        #region CargarNombreDireccionesCarreraViewBag
            /// <summary>
            /// Metodo que carga el nombre y el id de las direcciones de carrera
            /// </summary>
            void CargarNombreDireccionesCarreraViewBag()
            {
                this.ViewBag.ListaNombresDireccionesCarrera = this.matriculaBD.pa_CursoCarreraNombreDireccionViewBag_Select().ToList();
            }
        #endregion

    }
}