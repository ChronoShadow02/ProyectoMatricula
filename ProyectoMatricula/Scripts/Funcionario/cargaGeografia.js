$(function () {
    ///llamamos a la función que se encargará de crear los eventos
    //que nos permitirán controlar cuando se haga una selección en las respectivas listas
    estableceEventosChange();
    ///Carga inicialmente la lista der provincias, ya que es 
    //la lista con la que iniciaremos.
    cargaDropdownListProvincias();
});

//función que registrará los eventos necesarios para "monitorear"
//cuando se ejecute el método change de las respectivas listas
function estableceEventosChange() {
}


///carga los registros de las provincias
function cargaDropdownListProvincias() {
    ///dirección a donde se enviarán los datos
    var url = '/Funcionario/RetornaProvincias';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            procesarResultadoProvincias(data);
        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}

/*
 * toma el resultado del método RetornaProvincias
 * y lo procesa, recorriendo cada posición
 */
function procesarResultadoProvincias(data) {

    ///se posiciona en la lista de provincias
    var ddlProvincia = $("#Provincia");

    ///se limpian todas las opciones de la lista
    ddlProvincia.empty();

    // se crea la primera opcion de la lista, con valor vacío y texto "Seleccione la provincia"

    var NuevaOpcion = "<option value=''>Seleccione la provincia</option>";

    ///Se agrega la nueva opcion al dropdown
    ddlProvincia.append(NuevaOpcion);

    $(data).each(function(){

        var ProvinciaActual = this;

        var NuevaOpcion = "<option value='" + ProvinciaActual.id_Provincia + "'>" + ProvinciaActual.nombre + "</option>";

        ddlProvincia.append(NuevaOpcion);
    });
}

///carga los registros de los cantones
function cargaDropdownListCantones(pIdProvincia) {

    ///dirección a donde se enviarán los datos
    var url = '';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {

        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}


function procesarResultadoCantones(data) {

}