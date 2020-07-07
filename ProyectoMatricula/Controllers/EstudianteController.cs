using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMatricula.Modelos;
namespace ProyectoMatricula.Controllers
{
	public class EstudianteController : Controller
	{
		/// <summary>
		/// instancia de la base de datos
		/// </summary>
		matriculaBDEntities matriculaBD = new matriculaBDEntities();

		// GET: Estudiante
		public ActionResult Index()
		{
			return View();
		}
		#region EstudianteLista
			/// <summary>
			/// Metodo que retorna la lista de estudiantes
			/// </summary>
			/// <returns></returns>
			public ActionResult EstudianteLista()
			{
				///crear la variable que contiene los registros al 
				///invocar el procedimiento

				List<pa_Estudiantes_Select_Result> modeloVista = matriculaBD.pa_Estudiantes_Select(null, null).ToList();

				return View(modeloVista);
			}

			/// <summary>
			/// Metodo que retorna la lista de estudiantes de la busqueda realizada
			/// </summary>
			/// <param name="modeloBusqueda"></param>
			/// <returns></returns>
			[HttpPost]
			public ActionResult EstudianteLista(pa_Estudiantes_Select_Result modeloBusqueda)
				{
					List<pa_Estudiantes_Select_Result> modeloVista = 
						matriculaBD.pa_Estudiantes_Select(modeloBusqueda.Nombre_Estudiante,modeloBusqueda.Carne).ToList();

					return View(modeloVista);
				}
		#endregion

		#region EstudianteNuevo
			/// <summary>
			/// Metodo que muestra el formulario de ingresar estudiantes
			/// </summary>
			/// <returns></returns>
			public ActionResult EstudianteNuevo()
			{
				return View();
			}
			
			/// <summary>
			/// Metodo que hace el registro de los datos que se ingresaron en el formulario
			/// </summary>
			/// <returns></returns>
			[HttpPost]
			public ActionResult EstudianteNuevo(pa_Estudiantes_Select_Result modeloVista)
			{
				int cantidadRegistrosAgectados = 0;
				int registrosAfectados = 0;
				string mensaje = "";
				string carneEstudiante = "";
				try
				{
				///Se ejecuta el procedimiento almacennado
					cantidadRegistrosAgectados = matriculaBD.pa_Estudiantes_Insert(modeloVista.Nombre_Estudiante,
																				   modeloVista.Cedula_Estudiante,
																				   modeloVista.Id_Provincia,
																				   modeloVista.Id_Canton,
																				   modeloVista.Id_Distrito,
																				   modeloVista.Fecha_Inicio_U,
																				   carneEstudiante);
					///almacenamos en una variable la cedula del estudiante recien ingresado
					///para ello se utiliza el procedimiento almacenado pa_RetornaEstudianteID_Select
					pa_RetornaEstudianteID_Select_Result DatosEstudianteIngresado = new pa_RetornaEstudianteID_Select_Result();
					DatosEstudianteIngresado = matriculaBD.pa_RetornaEstudianteID_Select(modeloVista.Cedula_Estudiante).FirstOrDefault();

						///Variable que contiene la cédula ingresada
						string CedulaIngresada = modeloVista.Cedula_Estudiante;

						///Se consulta si la variable DatosEstudianteIngresado no es nula(osea que el dato está o existe)
						///
						if (DatosEstudianteIngresado != null)
						{

							///Se concatena el año y la el identificador del estudiante
							carneEstudiante = modeloVista.Fecha_Inicio_U.ToString("yyyy") +"-"+ DatosEstudianteIngresado.Id_Estudiante;

							///Se actualiza el registro nuevo para actualizar el carnet
							registrosAfectados =
									   this.matriculaBD.pa_Estudiante_IngresarCarne_Update(CedulaIngresada, carneEstudiante);
						}
						else
						{
							///Se consulta si la cédula está repetida, si es asi, entonces aparecerá un mensaje 
							///informando que ya cédula está ingresada
							string datoCedula = DatosEstudianteIngresado.Cedula_Estudiante;
							if (datoCedula == CedulaIngresada)
							{
								mensaje = "Esta cédula ya está ingresada.";
							}
						}
				}
				catch (Exception error)
				{
					mensaje = "Hubo un error. " +error.Message + " " + error.Source+" " + error.TargetSite;

				}
				finally
				{
					if ( cantidadRegistrosAgectados > 0 )
					{
						mensaje = "Registro agregado";
					}
					else
					{
						mensaje +="No se pudo ingresar";
					}
				}
			Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
			return View();
			}
		#endregion

		#region EstudianteModifica
			/// <summary>
			/// Metodo que Ingresa los estudiantes
			/// </summary>
			/// <returns></returns>
			public ActionResult EstudianteModifica()
			{
				return View();
			}
		#endregion

		#region EstudianteElimina
			/// <summary>
			/// Metodo que elimina los estudiantes por medio del id_estudiante
			/// </summary>
			/// <returns></returns>
			public ActionResult EstudianteElimina()
			{
				return View();
			}
		#endregion

		#region RetornaProvincias
		/// <summary>
		/// Metodo que devuelve las provincias
		/// </summary>
		/// <returns></returns>
		public ActionResult RetornaProvincias()
		{
			List<RetornaProvincias_Result> Provincias = matriculaBD.RetornaProvincias(null).ToList();

			return Json(Provincias);
		}
		#endregion

		#region RetornaCantones
		/// <summary>
		/// Metodo que devuelve los cantones
		/// </summary>
		/// <returns></returns>
		public ActionResult RetornaCantones(int Id_Provincia)
		{
			List<RetornaCantones_Result> Cantones = matriculaBD.RetornaCantones(null, Id_Provincia).ToList();

			return Json(Cantones);
		}
		#endregion

		#region RetornaDiatritos
		/// <summary>
		/// Metodo que retorna los distritos
		/// </summary>
		/// <returns></returns>
		public ActionResult RetornaDistritos(int Id_Canton)
		{
			List<sp_RetornaDistritos_Result> Distritos = matriculaBD.sp_RetornaDistritos(null, Id_Canton).ToList();
			return Json(Distritos);
		}
		#endregion
	}
}