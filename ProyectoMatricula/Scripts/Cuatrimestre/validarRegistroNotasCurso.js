$(function () {
    validaRegistroNotasCurso();
});

function validaRegistroNotasCurso() {
    $("#frmRegistroNotas").validate({
        rules: {
            Id_Curso: {
                required:true
            },
            Id_Estudiante: {
                required:true
            },
            Nota: {
                required: true,
                digits: true,
                maxlength: 3,
                max: 100,
                min:0
            },
            Id_Num_Cuatrimestre: {
                required:true
            },
            Id_Sedes_universitarias: {
                required:true
            },
            Anio_Cuatrimestre: {
                required:true
            }
        }
    });
}