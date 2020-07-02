$(function () {
    ValidarLogin();
});

function ValidarLogin() {
    $("#frmLogin").validate({
        rules: {
            Nombre_Usuario: {
                required: true,
                maxlength: 50
            },
            Contrasena: {
                required: true,
                maxlength: 50
            }
        }
    });
}