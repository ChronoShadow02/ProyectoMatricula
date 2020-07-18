$(function () {
    crearDatepickerFecha();
});

/*Crea el formato datepicker al input*/
function crearDatepickerFecha() {
    $("#txtFechaContratacion").datepicker({
        changeMonth: true,
        changeYear: true,
        ///Se muestran 50 años antes hasta el 2021
        ///ese "c" es el año actual
        yearRange: "c-50:c+1",
        dateFormat: "yy/mm/dd"
    });
}