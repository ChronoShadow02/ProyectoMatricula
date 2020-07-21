$(function () {
    ValidarDirecciones();
});


function ValidarDirecciones() {
    $("#frmDireccion").validate({
        rules: {
            Nombre_Direccion_Carrera: {
                required: true,
                maxlength: 50
            },
            Codigo_Direccion_Carrera: {
                required: true,
                maxlength: 50
            },
            Id_Director: {
                required: true
            },
            Id_Subdirector: {
                required: true
            }
        }
    });
}