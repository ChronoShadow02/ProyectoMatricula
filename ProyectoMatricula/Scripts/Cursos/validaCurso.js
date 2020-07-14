$(function () {
    validaCurso();
});

function validaCurso() {
    $("#frmCurso").validate({
        rules: {
            Nombre_Curso: {
                required: true,
                maxlength:50
            },
            Codigo_Curso: {
                required: true,
                maxlength: 50
            },
            Codigo_Requisito: {
                required:true
            }
        }
    });
}