$(function () {
    validarOferta();
});

function validarOferta() {
    $("#frmOferta").validate({
        rules: {
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
                required:true
            },
            Cantidad_Estudiantes: {
                required: true,
                digits: true
            }

        }
    });
}