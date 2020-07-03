$(function () {
    validarSedes();
});

function validarSedes() {
    $("#frmSedes").validate({
        rules: {
            Nombre_Sede: {
                required: true,
                maxlength: 50
            },
            Codigo_Sede: {
                required: true,
                maxlength: 50
            },
            Id_Director: {
                required:true
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
            Direccion_Fisica: {
                required: true,
                maxlength:50
            }
        }
    });
}