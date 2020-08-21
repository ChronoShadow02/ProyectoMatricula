$(function () {
    validaMatricula();
});

function validaMatricula() {
    $("#frmMatriculaEstudiante").validate({
        rules: {
            Id_Estudiante: {
                required:true
            },
            Id_Curso: {
                required: true
            },
            Id_Sedes_universitarias: {
                required: true
            },
            Id_Num_Cuatrimestre: {
                required: true
            },
            Anio_Cuatrimestre: {
                required: true,
                digits: true
            }
        }
    });
}