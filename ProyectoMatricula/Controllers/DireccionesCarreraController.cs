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
    }
}