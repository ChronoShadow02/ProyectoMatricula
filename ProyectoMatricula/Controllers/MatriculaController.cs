using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMatricula.Controllers
{
    public class MatriculaController : Controller
    {
        // GET: Matricula
        public ActionResult Index()
        {
            return View();
        }
        ///Vista en la que se accesa al login
        public ActionResult IndexMatricula()
        {
            return View();
        }
        
    }
}