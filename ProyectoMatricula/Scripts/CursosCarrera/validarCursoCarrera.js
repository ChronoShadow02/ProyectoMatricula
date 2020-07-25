$(function () {
    valdarCursoPorCarrera();
});

function valdarCursoPorCarrera() {
    $("#frmCursoPorCarrera").validate({
        rules: {
            Id_Curso: {
                required:true
            },
            Id_Carrera_Universitaria: {
                required:true
            }
        }
    });
}