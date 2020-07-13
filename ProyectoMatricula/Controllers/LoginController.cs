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
        #region AutenticacionLogin
            [HttpPost]
            public ActionResult AutenticacionLogin(pa_Administrador_Select_Result pModelo)
            {

                pa_Administrador_Select_Result BuscarUsuario =
                    this.matriculaBD.pa_Administrador_Select(pModelo.Nombre_Usuario, pModelo.Contrasena).FirstOrDefault();

                ///

                if (BuscarUsuario == null)
                {
                    this.ModelState.AddModelError("", "Usuario o contraseña inválidos.Por favor inténtelo de nuevo");

                    return View("Index");
                }
                else
                {
                    this.Session.Add("logueado", true);
                    ///Se agrega todo el modelo del usuario
                    this.Session.Add("datosUsuario", BuscarUsuario);

                    ///Se agrega la fecha de ultimo ingreso
                    this.Session.Add("Nom_Usuario", BuscarUsuario.Nombre_Usuario);

                    ///index del controlador IndexMatricula al que redirecciona
                    return RedirectToAction("IndexMatricula", "Matricula");
                }
            }
        #endregion
        #region CerrarSesion
            /// <summary>
            /// Cierra la sesion y establece os valores de las variables de sesion en null
            /// </summary>
            /// <returns></returns>
            public ActionResult CerrarSesion()
            {
            ///Se actualiza la ultima vez que el usuario estuvo en el sitio web

                string NombreUsu = Convert.ToString(this.Session["Nom_Usuario"]);

                this.matriculaBD.pa_Administrador_UltimaVez_Update(NombreUsu);

                ///establecer los datos de sesion para que 
                ///cuando el layout consulte por dichos datos 
                ///redireccione al login

                this.Session.Add("logueado", null);

                this.Session.Add("datosUsuario", null);

                return RedirectToAction("Index", "Login");
            }
        #endregion

    }
}