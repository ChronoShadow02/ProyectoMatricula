using ProyectoMatricula.Modelos;
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
            bool sesionIniciada = false;

            ///Consultar si la variable "logueado" tiene algun valor

            if (this.Session["logueado"] != null)
            {
                sesionIniciada = Convert.ToBoolean(this.Session["logueado"]);
            }
            if (sesionIniciada == true)
            {
                ///Reconstruir los datos del modelo al objeto session
                pa_Administrador_Select_Result modelo = (pa_Administrador_Select_Result) this.Session["datosUsuario"];

                return View(modelo);
            }
            else
            {
                ///redireccionar al 
                ///index del controlador login

                return RedirectToAction("Index" , "Login");
            }
            return View();
        }
        
    }
}