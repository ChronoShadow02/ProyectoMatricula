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
        /// Metodo que Ingresa los funcionarios
        /// </summary>
        /// <returns></returns>
        public ActionResult FuncionarioNuevo()
        {
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
    }
}