using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMatricula.Controllers
{
    public class FuncionarioController : Controller
    {
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
            return View();
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