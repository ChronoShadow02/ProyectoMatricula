using ProyectoMatricula.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMatricula.Controllers
{
    public class ReportesController : Controller
    {
        #region Instancia de la base de datos
            matriculaBDEntities matriculaBD = new matriculaBDEntities();
        #endregion

        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        #region Reporte de Notas por Estudiante
        /// <summary>
        /// Método que muestra el reporte de notas por estudiante
        /// </summary>
        /// <returns></returns>
         public ActionResult ReporteNotasEstudiante()
         {
         ///crear la variable que contiene los registros al 
         ///invocar el procedimiento
             List<Reporte_Notas_Por_Estudiante_Result> modeloVistaReporte = matriculaBD.Reporte_Notas_Por_Estudiante(null, null, null).ToList();
             this.Lista_Num_CuatrimestreViewBag();
             return View(modeloVistaReporte);
         }
         /// <summary>
         /// Metodo que muestra el reporte de notas por estudiante haciendo las búsquedas
         /// </summary>
         /// <param name="modeloBusqueda"></param>
         /// <returns></returns>
         [HttpPost]
         public ActionResult ReporteNotasEstudiante(Reporte_Notas_Por_Estudiante_Result modeloBusqueda)
         {
            if (modeloBusqueda.Id_Num_Cuatrimestre == 0)
            {
                List<Reporte_Notas_Por_Estudiante_Result> modeloVista =
                 this.matriculaBD.Reporte_Notas_Por_Estudiante(modeloBusqueda.Nombre_Estudiante,null, modeloBusqueda.Nombre_Curso).ToList();
                this.Lista_Num_CuatrimestreViewBag();
                return View(modeloVista);
            }
            else
            {
                List<Reporte_Notas_Por_Estudiante_Result> modeloVista =
                 this.matriculaBD.Reporte_Notas_Por_Estudiante(modeloBusqueda.Nombre_Estudiante, Convert.ToString(modeloBusqueda.Id_Num_Cuatrimestre), modeloBusqueda.Nombre_Curso).ToList();
                this.Lista_Num_CuatrimestreViewBag();
                return View(modeloVista);
            }
             

         }
        #endregion
        /*

     #region Reporte de Notas por curso
         /// <summary>
         /// Metodo que muestra el reporte de notas por curso
         /// </summary>
         /// <returns></returns>
         public ActionResult ReporteNotasCurso()
         {
             List<Reporte_Notas_Por_Curso_Result> modeloVista =
                 this.matriculaBD.Reporte_Notas_Por_Curso(null, null, null, null).ToList();
             return View(modeloVista);
         }
         /// <summary>
         /// Metodo que muestra el reporte de notas por curso haciendo las búsquedas
         /// </summary>
         /// <param name="modeloBusqueda"></param>
         /// <returns></returns>
         [HttpPost]
         public ActionResult ReporteNotasCurso(Reporte_Notas_Por_Curso_Result modeloBusqueda)
         {
             List<Reporte_Notas_Por_Curso_Result > modeloVista =
                 this.matriculaBD.Reporte_Notas_Por_Curso(modeloBusqueda.Nombre_Curso, 
                                                          modeloBusqueda.Num_Cuatrimestre, 
                                                          modeloBusqueda.Anio_Cuatrimestre, 
                                                          modeloBusqueda.Nombre_Sede).ToList();
             return View(modeloVista);
         }
        */
        //#endregion

        #region Número de cuatrimestre viewbag
        /// <summary>
        /// Metodo que muestra el número de cuatrimestre
        /// </summary>
        void Lista_Num_CuatrimestreViewBag()
        {
            this.ViewBag.Lista_Num_Cuatrimestre = this.matriculaBD.pa_Cuatrimestre_Num_CuatrimiestreViewBag().ToList();
        }
        #endregion
    }
}