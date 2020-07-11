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
    ///Evento change de la lista de provincias 
    $("#Provincia").change(function () {

        var provincia = $("#Provincia").val();
        ///llamar la funcion de cargar cantones asociados a la provincia seleccionada
        cargaDropdownListCantones(provincia);

                var Canton = $("#Canton").val();

        cargaDropdownListDistritos(Canton);
    });

    $("#Canton").change(function () {

        var Canton = $("#Canton").val();

        cargaDropdownListDistritos(Canton);
    });
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


    /*$(data).each(function () {

        var ProvinciaActual = this;

        var NuevaOpcion = "<option value='" + ProvinciaActual.id_Provincia + "'>" + ProvinciaActual.nombre + "</option>";

        ddlProvincia.append(NuevaOpcion);
    });*/
}

///carga los registros de los cantones
function cargaDropdownListCantones(pIdProvincia) {

    ///dirección a donde se enviarán los datos
    var url = '/Funcionario/RetornaCantones';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        Id_Provincia: pIdProvincia
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            procesarResultadoCantones(data);
        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}


function procesarResultadoCantones(data) {
    ///se posiciona en la lista de provincias
    var ddlCanton = $("#Canton");

    ///se limpian todas las opciones de la lista
    ddlCanton.empty();

    // se crea la primera opcion de la lista, con valor vacío y texto "Seleccione la provincia"

    var NuevaOpcion = "<option value=''>Seleccione el cantón</option>";

    ///Se agrega la nueva opcion al dropdown
    ddlCanton.append(NuevaOpcion);

    $(data).each(function () {

        var CantonActual = this;

        var NuevaOpcion = "<option value='" + CantonActual.id_Canton + "'>" + CantonActual.nombre + "</option>";

        ddlCanton.append(NuevaOpcion);
    });
}


function cargaDropdownListDistritos(pId_Canton) {

    var url = "/Funcionario/RetornaDistritos";
    var parametros = {
        Id_Canton: pId_Canton
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            procesarResultadoDistritos(data);
        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}

function procesarResultadoDistritos(data) {

    ///mediante un selector nos pocicionamos en la lista de cantones

    ddlDistrito = $("#Distrito");

    ddlDistrito.empty();

    var NuevaOpcion = "<option value=''>Seleccione un distrito</option>";

    ddlDistrito.append(NuevaOpcion);

    $(data).each(function () {

        var DistritoActual = this;

        var NuevaOpcion = "<option value='" + DistritoActual.id_Distrito + "'>" + DistritoActual.nombre + "</option>";

        ddlDistrito.append(NuevaOpcion);

    });
}