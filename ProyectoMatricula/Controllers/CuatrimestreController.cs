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
            List<pa_Cuatrimestre_Select_Result> modeloVista = this.matriculaBD.pa_Cuatrimestre_Select(null).ToList();

            this.Lista_Num_CuatrimestreViewBag();
            this.CargarSedesUniversitariasViewbag();
            return View(modeloVista);
        }
        [HttpPost]
        public ActionResult CuatrimestreLista(pa_Cuatrimestre_Select_Result modeloBusqueda)
        {
            List<pa_Cuatrimestre_Select_Result> modeloVista =
                this.matriculaBD.pa_Cuatrimestre_Select(modeloBusqueda.Nombre_Sede).ToList();
            this.Lista_Num_CuatrimestreViewBag();
            this.CargarSedesUniversitariasViewbag();
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

        #region OfertaCursosPorSede
        /// <summary>
        /// Método que Muestra la vista del formulario de Oferta de cursos
        /// </summary>
        /// <returns></returns>
        public ActionResult OfertaCursosPorSede(int Numero_Cuatrimestre, int Id_Sedes_universitarias, int Anio_Cuatrimestre)
        {
            pa_Curso_x_Sede_RetornaID_Select_Result modeloVista = new pa_Curso_x_Sede_RetornaID_Select_Result();

            modeloVista = this.matriculaBD.pa_Curso_x_Sede_RetornaID_Select(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre).FirstOrDefault();

            this.Lista_Num_CuatrimestreViewBag();
            this.CargarSedesUniversitariasViewbag();
            this.CargarCursosViewBag();
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult OfertaCursosPorSede(pa_Curso_x_Sede_SoloID_Select_Result modeloVista, pa_Curso_x_Sede_RetornaID_Select_Result OtroModelo)
        {


            int RegistrosAfectados = 0;
            string mensaje = "";
            try
            {
                ///Se verifica si  ya existe cierto curso en x cuatrimestre en x sede 

                pa_Curso_x_Sede_VerificaCurso_Select_Result ModeloVerificar =
                    this.matriculaBD.pa_Curso_x_Sede_VerificaCurso_Select(modeloVista.Id_Curso,
                                                                          modeloVista.Id_Sedes_universitarias,
                                                                          OtroModelo.Numero_Cuatrimestre,
                                                                          OtroModelo.Anio_Cuatrimestre).FirstOrDefault();
                ///Si es diferente de  null o si esta en la base de datos
                if (ModeloVerificar != null)///antes estaba ModeloVerificar
                {
                    mensaje = "Ya existe ese curso por cuatrimestre en dicha sede.";
                }
                else
                {
                    RegistrosAfectados = this.matriculaBD.pa_Cuatrimestre_OfertaCursos_Insert(modeloVista.Id_Curso,
                                                                                              modeloVista.Id_Sedes_universitarias,
                                                                                              OtroModelo.Numero_Cuatrimestre,
                                                                                              modeloVista.Cantidad_Estudiantes,
                                                                                              OtroModelo.Anio_Cuatrimestre);
                }

            }
            catch (Exception error)
            {
                mensaje = "Hubo un error. " + error.Message;

            }
            finally {

                if (RegistrosAfectados > 0)
                {
                    mensaje = "Oferta ingresada";
                }
                else
                {
                    mensaje += "No se pudo ingresar la oferta.";
                }
                Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
            }
            this.Lista_Num_CuatrimestreViewBag();
            this.CargarSedesUniversitariasViewbag();
            this.CargarCursosViewBag();
            return View(OtroModelo);
        }
        #endregion

        #region MatriculaEstudianteCurso
        /// <summary>
        /// Método que retorna el resultado de lo que esta dentro xd(
        /// </summary>
        /// <returns></returns>
        public ActionResult MatriculaEstudianteCurso(int Numero_Cuatrimestre, int Id_Sedes_universitarias, int Anio_Cuatrimestre)
        {
            pa_Curso_x_Sede_RetornaID_Select_Result modeloVista = new pa_Curso_x_Sede_RetornaID_Select_Result();

            modeloVista = this.matriculaBD.pa_Curso_x_Sede_RetornaID_Select(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre).FirstOrDefault();

            this.Lista_Num_CuatrimestreViewBag();
            this.CargarSedesUniversitariasViewbag();
            this.CargarCursosViewBag();
            this.CargaListaEstudianteViewBag();
            this.cargaCursosSCA(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre);
            return View(modeloVista);
        }
        [HttpPost]
        public ActionResult MatriculaEstudianteCurso(pa_Curso_x_Sede_SoloID_Select_Result modeloVista, pa_Curso_x_Sede_RetornaID_Select_Result OtroModelo, pa_Curso_x_Cuatrimestre_ListaEstudiantes_Result modeloEstudiante)
        {
            int RegistrosAfectados = 0;
            bool VerificaID_Est = true;
            string mensaje = "";
            try
            {

                ///Se obtiene el id del cuatrimestre del para hacer la inserción
                ///
                pa_Curso_x_Cuatrimestre_Id_Cuatrimestre_Result modeloSoloId_Cuatrimestre =
                    this.matriculaBD.pa_Curso_x_Cuatrimestre_Id_Cuatrimestre(OtroModelo.Numero_Cuatrimestre,
                                                                             modeloVista.Anio_Cuatrimestre,
                                                                             modeloVista.Id_Sedes_universitarias).FirstOrDefault();

                ///Se verifica que no hayan registros similiares
                List<pa_Curso_x_CuatrimestreVerificar_Result> modeloVerificar
                     = this.matriculaBD.pa_Curso_x_CuatrimestreVerificar(modeloVista.Id_Curso,
                                                                         OtroModelo.Numero_Cuatrimestre,
                                                                         modeloVista.Anio_Cuatrimestre,
                                                                         modeloSoloId_Cuatrimestre.Id_Cuatrimeste,
                                                                         modeloEstudiante.Id_Estudiante,
                                                                         modeloVista.Id_Sedes_universitarias).ToList();

                ///Busca en el resultado del procedimiento esta el Id estudiante
                foreach (pa_Curso_x_CuatrimestreVerificar_Result modeloEst in modeloVerificar)
                {
                    if (modeloEst.Id_Estudiante == modeloEstudiante.Id_Estudiante)
                    {
                        mensaje = "No puede haber estudiantes matriculados dos o más veces en un mismo curso por cuatrimestre en alguna de las sedes universitarias.";
                        VerificaID_Est = true;
                    }
                    else
                    {
                        VerificaID_Est = false;
                    }
                }
                if (VerificaID_Est == false)
                {
                    RegistrosAfectados = this.matriculaBD.pa_Curso_x_Cuatrimestre_Insert(modeloVista.Id_Curso,
                                                                                                OtroModelo.Numero_Cuatrimestre,
                                                                                                modeloVista.Anio_Cuatrimestre,
                                                                                                modeloSoloId_Cuatrimestre.Id_Cuatrimeste,
                                                                                                modeloEstudiante.Id_Estudiante,
                                                                                                modeloVista.Id_Sedes_universitarias);

                    ///Se hace un insert aqui por la relacion que existe entre el curso y el estudiante
                    this.matriculaBD.pa_Cursos_x_Estudiante_Insert(modeloVista.Id_Curso, modeloEstudiante.Id_Estudiante);

                    ////FALTA HACER EL INSERT DE CURSOSXCARRARAXSEDE

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
                    mensaje = "Matricula ingresada";
                }
                else
                {
                    mensaje += ".No se pudo ingresar";
                }
                Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
            }
            this.Lista_Num_CuatrimestreViewBag();
            this.CargarSedesUniversitariasViewbag();
            this.CargarCursosViewBag();
            this.CargaListaEstudianteViewBag();
            this.cargaCursosSCA(OtroModelo.Numero_Cuatrimestre, modeloVista.Id_Sedes_universitarias, modeloVista.Anio_Cuatrimestre);
            return View(OtroModelo);
        }
        #endregion

        #region RegistroNotasCurso
        /// <summary>
        /// Metodo que retorna la vista RegistroNotasCurso dependiendo del cuatrimestre,año y sede
        /// </summary>
        /// <param name="Numero_Cuatrimestre"></param>
        /// <param name="Id_Sedes_universitarias"></param>
        /// <param name="Anio_Cuatrimestre"></param>
        /// <returns></returns>
        public ActionResult RegistroNotasCurso(int Numero_Cuatrimestre, int Id_Sedes_universitarias, int Anio_Cuatrimestre)
        {
            pa_Curso_x_Sede_RetornaID_Select_Result modeloVista = new pa_Curso_x_Sede_RetornaID_Select_Result();

            modeloVista = this.matriculaBD.pa_Curso_x_Sede_RetornaID_Select(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre).FirstOrDefault();

            ///Se obtiene el id del cuatrimestre 
            pa_Curso_x_Cuatrimestre_Id_Cuatrimestre_Result modeloSoloId_Cuatrimestre =
                this.matriculaBD.pa_Curso_x_Cuatrimestre_Id_Cuatrimestre(modeloVista.Numero_Cuatrimestre,
                                                                         modeloVista.Anio_Cuatrimestre,
                                                                         modeloVista.Id_Sedes_universitarias).FirstOrDefault();

            this.Lista_Num_CuatrimestreViewBag();
            this.CargarSedesUniversitariasViewbag();
            this.cargaCursosSCA(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre);
            this.CargaCursosSCAJson(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre);
            return View(modeloVista);
        }
        [HttpPost]
        ///Metodo que manda todos los datos obtenidos hacia la base de datos
        public ActionResult RegistroNotasCurso(pa_Curso_x_Sede_RetornaID_Select_Result modeloVista, pa_CursosXEstudiante_Modelo_Result modeloEstudiante)
        {
            int RegistrosAfectados = 0;
            string mensaje = "";
            try
            {
                RegistrosAfectados = this.matriculaBD.pa_CursosXEstudiante_Insert(modeloEstudiante.Id_Estudiante,
                                                                                  modeloEstudiante.Id_Curso,
                                                                                  Convert.ToDouble(modeloEstudiante.Estado_Nota),
                                                                                  "En curso");
            }
            catch (Exception error)
            {
                mensaje = "Hubo un error." +error.Message;
            }
            finally
            {
                if (RegistrosAfectados > 0)
                {
                    mensaje = "Regitro ingresado.";
                }
                else
                {
                    mensaje += "No se pudo ingresar";
                }   
            }
            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
            this.Lista_Num_CuatrimestreViewBag();
            this.CargarSedesUniversitariasViewbag();
            return View(modeloVista);
        }
        #endregion

        #region Finalización de curso
        public ActionResult FinalizarCurso(int Numero_Cuatrimestre, int Id_Sedes_universitarias, int Anio_Cuatrimestre)
        {
            pa_Curso_x_Sede_RetornaID_Select_Result modeloVista = new pa_Curso_x_Sede_RetornaID_Select_Result();

            modeloVista = this.matriculaBD.pa_Curso_x_Sede_RetornaID_Select(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre).FirstOrDefault();

            this.Lista_Num_CuatrimestreViewBag();
            this.CargarSedesUniversitariasViewbag();
            this.cargaCursosSCA(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre);
            return View(modeloVista);
        }
        [HttpPost]
        public ActionResult FinalizarCurso(pa_Curso_x_Sede_RetornaID_Select_Result modeloVista)
        {
            return View();
        }
        #endregion

        #region IniciarCuatrimestre
        public ActionResult IniciarCuatrimestre(int Id_Cuatrimeste)
        {
            string mensaje = "";
            ///Verifica si el cuatrimestre ya se ha iniciado
            ///
            pa_CuatrimesteVerificarInicio_Result VerificarInicio = this.matriculaBD.pa_CuatrimesteVerificarInicio(Id_Cuatrimeste).FirstOrDefault();

            if (VerificarInicio.Inicio_Cuatrimestre == "S")
            {
                mensaje = "El cuatrimestre ya ha sido iniciado.";
                Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
            }
            else
            {
                this.matriculaBD.pa_CuatrimestreInicio(Id_Cuatrimeste);
            }
            ///HAY QUE ARREGLAR ESTA PARTE, HACE EL UPDATE PERO NO TIENE LA VISTA
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
        #region CargarCursosViewBag
        /// <summary>
        /// Método que retorna los cursos en los que pueden ser requisitos
        /// </summary>
        void CargarCursosViewBag()
        {
            this.ViewBag.ListaCursos = this.matriculaBD.pa_CursosCodigos_Select().ToList();
        }
        #endregion
        #region ListaEstudiantes
        void CargaListaEstudianteViewBag()
        {
            this.ViewBag.ListaEstudiantes = this.matriculaBD.pa_Curso_x_Cuatrimestre_ListaEstudiantes().ToList();
        }
        #endregion
        #region Lista de cursos por sede por cuatrimestre por año
        /// <summary>
        /// Metodo que busca si existen cursos en x sede en x cuatrimestre y en ese año
        /// </summary>
        /// <param name="Numero_Cuatrimestre"></param>
        /// <param name="Id_Sedes_universitarias"></param>
        /// <param name="Anio_Cuatrimestre"></param>
        void cargaCursosSCA(int Numero_Cuatrimestre, int Id_Sedes_universitarias, int Anio_Cuatrimestre)
        {
            this.ViewBag.ListaCursosSCA = this.matriculaBD.pa_Curso_x_CuatrimestreListaCursos(Id_Sedes_universitarias,
                                                                                              Numero_Cuatrimestre,
                                                                                              Anio_Cuatrimestre).ToList();
        }
        #endregion

        #region Carga de cursos por sede por año JSON
        /// <summary>
        /// Metodo que retorna la lista de cursos dependiendo de la sede, cuatrimestre y año
        /// </summary>
        /// <param name="Numero_Cuatrimestre"></param>
        /// <param name="Id_Sedes_universitarias"></param>
        /// <param name="Anio_Cuatrimestre"></param>
        /// <returns></returns>
        public ActionResult CargaCursosSCAJson(int Numero_Cuatrimestre, int Id_Sedes_universitarias, int Anio_Cuatrimestre)
        {
            List<pa_Curso_x_CuatrimestreListaCursos_Result> ListaCursos 
                = this.matriculaBD.pa_Curso_x_CuatrimestreListaCursos(Id_Sedes_universitarias,Numero_Cuatrimestre,Anio_Cuatrimestre).ToList();

            return Json(ListaCursos);
        }
        #endregion

        #region Cargar estudiantes por curso por sede por año y por cuatrimestre
        public ActionResult CargaEstudianteCurso(int Numero_Cuatrimestre, int Id_Sedes_universitarias, int Anio_Cuatrimestre,int Id_Curso)
        {
            List<pa_Cuatrimestre_VerificaCursosEstudiante_Result> ListaEstudiantes
                = this.matriculaBD.pa_Cuatrimestre_VerificaCursosEstudiante(Id_Curso, Numero_Cuatrimestre, Anio_Cuatrimestre, Id_Sedes_universitarias).ToList();

            return Json(ListaEstudiantes);
        }
        #endregion
    }
}