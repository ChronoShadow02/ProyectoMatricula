$(function () {
    validarEstudiante();
});

function validarEstudiante() {
    $("#frmEstudiante").validate({
        rules: {
            Nombre_Estudiante: {
                required: true,
                maxlength: 50
            },
            Cedula_Estudiante: {
                required: true,
                maxlength: 50
            },
            Id_Provincia: {
                required: true
            },
            Id_Canton: {
                required: true
            },
            Id_Distrito: {
                required: true
            },
            Fecha_Inicio_U: {
                required:true
            }
        }
    });
}