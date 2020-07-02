using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMatricula.Modelos;
namespace ProyectoMatricula.Controllers
{
    public class LoginController : Controller
    {
        /// Instrancia del mododelo de base de datos

        matriculaBDEntities matriculaBD = new matriculaBDEntities();
        

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AutenticacionLogin(pa_Administrador_Select_Result pModelo)
        {

            pa_Administrador_Select_Result BuscarUsuario = 
                this.matriculaBD.pa_Administrador_Select(pModelo.Nombre_Usuario, pModelo.Contrasena).FirstOrDefault();

            ///

            if (BuscarUsuario == null)
            {
                this.ModelState.AddModelError("","Usuario o contraseña inválidos.Por favor inténtelo de nuevo");

                return View("Index");
            }
            else
            {
                this.Session.Add("logueado", true);
                ///Se agrega todo el modelo del usuario
                this.Session.Add("datosUsuario",BuscarUsuario);

                ///index del controlador IndexMatricula
                return RedirectToAction("IndexMatricula", "Matricula");
            }
        }
        /// <summary>
        /// Cierra la sesion y establece os valores de las variables de sesion en null
        /// </summary>
        /// <returns></returns>
        public ActionResult CerrarSesion()
        {
            ///establecer los datos de sesion para que 
            ///cuando el layout consulte por dichos datos 
            ///redireccione al login

            this.Session.Add("logueado", null);

            this.Session.Add("datosUsuario", null);

            return RedirectToAction("Index", "Login");
        }
    }
}