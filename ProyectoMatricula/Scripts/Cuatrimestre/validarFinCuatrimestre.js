$(function () {
    validarFinCuatrimestre();
});

function validarFinCuatrimestre() {
    $("#frmFincuatrimestre").validate({
        rules: {
            Id_Cuatrimeste: {
                required:true
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