$(function () {
    estableceEventosChange();
    ///Se obtiene los datos para mandarselos al metodo CargarDatosCurso y tener asi los cursos
    var Numero_Cuatrimestre = $("#Id_Num_Cuatrimestre").val();
    var Id_Sedes_universitarias = $("#Id_Sede").val();
    var Anio_Cuatrimestre = $("#Anio_Cuatrimestre").val();
    CargarDatosCurso(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre);
});


function estableceEventosChange() {
    $("#Id_Curso").change(function () {
        var Curso = $("#Id_Curso").val();
        var Numero_Cuatrimestre = $("#Id_Num_Cuatrimestre").val();
        var Id_Sedes_universitarias = $("#Id_Sede").val();
        var Anio_Cuatrimestre = $("#Anio_Cuatrimestre").val();
        cargarDatosEstudiantes(Numero_Cuatrimestre, Id_Sedes_universitarias,Anio_Cuatrimestre,Curso);
    });
}

///Metodo que carga los datos del curso
function CargarDatosCurso(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre) {

    var url = "/Cuatrimestre/CargaCursosSCAJson";

    var parametros = {
        Numero_Cuatrimestre: Numero_Cuatrimestre,
        Id_Sedes_universitarias: Id_Sedes_universitarias,
        Anio_Cuatrimestre: Anio_Cuatrimestre
    };
    var funcion = procesarDatosCurso;

    ejecutaAjax(url, parametros, procesarDatosCurso);
}
function procesarDatosCurso(data) {

    var ddlCursos = $("#Id_Curso");

    ddlCursos.empty();

    var NuevaOpcion = "<option value=''>Seleccione una opción</option>";

    ddlCursos.append(NuevaOpcion);

    $(data).each(function () {

        var CursoActual = this;

        var NuevaOpcion = "<option value='" + CursoActual.Id_Curso + "'>" + CursoActual.Nombre_Curso + "</option>";

        ddlCursos.append(NuevaOpcion);

    });
}
function cargarDatosEstudiantes(Numero_Cuatrimestre, Id_Sedes_universitarias, Anio_Cuatrimestre, Id_Curso) {
    var url = "/Cuatrimestre/CargaEstudianteCurso";

    var parametros = {
        Numero_Cuatrimestre: Numero_Cuatrimestre,
        Id_Sedes_universitarias: Id_Sedes_universitarias,
        Anio_Cuatrimestre: Anio_Cuatrimestre,
        Id_Curso: Id_Curso
    }
    var funcion = ProcesarDatosEstudianteDepCurso;

    ejecutaAjax(url, parametros, funcion);
}
function ProcesarDatosEstudianteDepCurso(data) {
    var ddlEstudiante = $("#ddlEstudiante");

    ddlEstudiante.empty();

    var NuevaOpcion = "<option value=''>Seleccione una opción</option>";
    ddlEstudiante.append(NuevaOpcion);

    $(data).each(function () {

        var EstudianteActual = this;

        var NuevaOpcion = "<option value='" + EstudianteActual.Id_Estudiante + "'>" + EstudianteActual.Nombre_Estudiante + "</option>";

        ddlEstudiante.append(NuevaOpcion);
    });

}