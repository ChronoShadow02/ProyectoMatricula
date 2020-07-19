$(function () {
    ValidaCarrerasUniversitarias();
});

function ValidaCarrerasUniversitarias() {
    $("#frmCarrera").validate({
        rules: {
            Nombre_Carrera: {
                required: true,
                maxlength:50
            },
            Codigo_Carrera: {
                required: true,
                maxlength: 50
            },
            Id_Direccion_Carrera: {
                required: true
            }
        }
    });
}