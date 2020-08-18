$(function () {
    validarCuatrimestre();
});

function validarCuatrimestre() {
    $("#frmCuatrimestreNuevo").validate({
        rules: {
            Id_Num_Cuatrimestre: {
                required: true
            },
            Id_Sede_Universitarias: {
                required:true
            },
            Anio_Cuatrimestre: {
                required: true,
                digits: true,
                maxlength:4
            }
        }
    });
}