using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMatricula.Controllers
{
    public class CuatrimestreController : Controller
    {
        // GET: Cuatrimestre
        public ActionResult CuatrimestreNuevo()
        {
            return View();
        }
    }
}