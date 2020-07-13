using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
			public ActionResult EstudianteNuevo(pa_EstudiantesRetornaSelectID_Select_Result modeloVista)
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
			/// Metodo que muestra el formulario para que se Ingresen los estudiantes
			/// </summary>
			/// <returns></returns>
			public ActionResult EstudianteModifica(int Id_Estudiante)
			{
				pa_EstudiantesViewBag_Select_Result modeloVista = new pa_EstudiantesViewBag_Select_Result();

				modeloVista = this.matriculaBD.pa_EstudiantesViewBag_Select(Id_Estudiante).FirstOrDefault();

				this.RetornaProvinciasViewBag();

				this.RetornaCantonesViewBag(modeloVista.Id_Provincia);

				this.RetornaDistritosViewBag(modeloVista.Id_Canton);

				return View(modeloVista);
			}

		[HttpPost]
		/// <summary>
		/// Metodo que Ingresa los estudiantes mediante el procedimiento almacenado
		/// </summary>
		/// <returns></returns>
		public ActionResult EstudianteModifica(pa_EstudiantesRetornaSelectID_Select_Result modeloVista)
		{
			///Variable que registra la cantidad de registros afectados
			///si un procedimiento ejecuta insert, update, delete 
			///no afecta registros implica que hubo un error
			int cantidadRegistrosAgectados = 0;

			string resultado = "";
			string carne = "";
			try
			{
				pa_EstudiantesViewBag_Select_Result modeloCarneEstudiante = new pa_EstudiantesViewBag_Select_Result();

				modeloCarneEstudiante = this.matriculaBD.pa_EstudiantesViewBag_Select(modeloVista.Id_Estudiante).FirstOrDefault();

				carne = modeloCarneEstudiante.Carne;

				cantidadRegistrosAgectados = this.matriculaBD.pa_Estudiantes_Update(modeloVista.Id_Estudiante,
																				    modeloVista.Nombre_Estudiante,
																					modeloVista.Cedula_Estudiante,
																					modeloVista.Id_Provincia,
																					modeloVista.Id_Canton,
																					modeloVista.Id_Distrito,
																					modeloVista.Fecha_Inicio_U,
																					carne);
			}
			catch (Exception error)
			{

				resultado = "Ocurrio un error " + error.Message;

			}
			finally
			{
				if (cantidadRegistrosAgectados > 0)
				{
					resultado = "Registro Modificado";
				}
				else
				{
					resultado += ".No se pudo modificar";
				}
			}
			Response.Write("<script language=javascript>alert('" + resultado + "');</script>");

			this.RetornaProvinciasViewBag();

			this.RetornaCantonesViewBag(modeloVista.Id_Provincia);

			this.RetornaDistritosViewBag(modeloVista.Id_Canton);

			pa_EstudiantesViewBag_Select_Result modelview = new pa_EstudiantesViewBag_Select_Result();

			return View(modelview);
		}
		#endregion

		#region EstudianteElimina
		/// <summary>
		/// Metodo que elimina los estudiantes por medio del id_estudiante
		/// </summary>
		/// <returns></returns>
		public ActionResult EstudianteEliminar(int Id_Estudiante)
		{
			pa_EstudiantesViewBag_Select_Result modeloVista = new pa_EstudiantesViewBag_Select_Result();

			modeloVista = this.matriculaBD.pa_EstudiantesViewBag_Select(Id_Estudiante).FirstOrDefault();

			/// se agregan los datos de las provincias, cantones y distritos
			this.RetornaProvinciasViewBag();

			this.RetornaCantonesViewBag(modeloVista.Id_Provincia);

			this.RetornaDistritosViewBag(modeloVista.Id_Canton);

			return View(modeloVista);
		}

		[HttpPost]
		public ActionResult EstudianteEliminar(pa_EstudiantesViewBag_Select_Result modeloVista)
		{
			int RegistrosAfectados = 0;
			string resultado = "";
			try
			{
				RegistrosAfectados = this.matriculaBD.pa_Estudiantes_Delete(modeloVista.Id_Estudiante);
			}
			catch (Exception error)
			{
				resultado = "Ocurrio un error " + error.Message;
			}
			finally
			{
				if (RegistrosAfectados > 0)
				{
					resultado = "Registro Eliminado";
				}
				else
				{
					resultado += ".No se pudo eliminar";
				}
			}
			Response.Write("<script language=javascript>alert('" + resultado + "');</script>");

			/// se agregan los datos de las provincias, cantones y distritos
			this.RetornaProvinciasViewBag();

			this.RetornaCantonesViewBag(modeloVista.Id_Provincia);

			this.RetornaDistritosViewBag(modeloVista.Id_Canton);

			return View(modeloVista);

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

		#region RetornaProvinciasViewBag
		/// <summary>
		/// Metodo que retorna las provincias y las guarda en el ViewBag
		/// </summary>
		void RetornaProvinciasViewBag()
		{
			this.ViewBag.ListaProvincias = this.matriculaBD.RetornaProvincias(null).ToList();
		}
		#endregion

		#region RetornaCantonesViewBag
		/// <summary>
		/// Metodo que retorna las los cantones dependiendo de la provincia seleccionada
		/// </summary>
		void RetornaCantonesViewBag(int Id_Provincia)
		{
			this.ViewBag.ListaCantones = this.matriculaBD.RetornaCantones(null, Id_Provincia).ToList();
		}
		#endregion

		#region RetornaDistritosViewBag
		/// <summary>
		/// Metodo que retorna las los distritos dependiendo del cantón seleccionado
		/// </summary>
		void RetornaDistritosViewBag(int Id_Canton)
		{
			this.ViewBag.ListaDistritos = this.matriculaBD.sp_RetornaDistritos(null, Id_Canton).ToList();
		}

		#endregion
	}
}