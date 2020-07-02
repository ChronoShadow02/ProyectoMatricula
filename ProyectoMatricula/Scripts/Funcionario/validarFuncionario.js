$(function () {
    validaFuncionario();
});

function validaFuncionario() {
    $('#frmFuncionario').validate({
        rules: {
            Nombre_Funcionario: {
                required: true,
                maxlength: 50
            },
            Cedula_Funcionario: {
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
            Fecha_Contratacion: {
                required: true
            }

        }    
    });
}