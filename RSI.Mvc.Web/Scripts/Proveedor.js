function ajaxGetProveedor(url) {
    $("#modalRSI").html("");
    $.ajax({
        type: 'GET',
        url: url,
        success: function (result, status, xhr) {
            if (result.hasOwnProperty('Success')) {
                var alerta = '<div class="alert alert-danger alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button><strong>Error: </strong>'
                    + result.ErrorMessage
                    + '</div>';
                $("#mensajesLabel").html(alerta).show();
                window.setTimeout(function () {
                    $("#mensajesLabel").fadeOut(500, function () {
                        $("#mensajesLabel").hide('slow');
                    });
                }, 10000);
            } else {
                $("#modalRSI").html(result);
                $("#modalRSI").modal('toggle');
            }
        },
        error: function (xhr, status) {
            var alerta = '<div class="alert alert-danger alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button><strong>Error: </strong>'
                + xhr.statusText
                + '</div>';
            $("#mensajesModalLabel").html(alerta).show();
            window.setTimeout(function () {
                $("#mensajesModalLabel").fadeOut(500, function () {
                    $("#mensajesModalLabel").hide('slow');
                });
            }, 10000);
        }
    });
}

function ajaxPostCrearProveedor(url) {
    $.ajax({
        type: 'POST',
        url: url,
        data: $("#crearProveedorForm").serialize(),
        dataType: 'json',
        cache: false,
        success: function (result, status, xhr) {
            if (typeof result === 'undefined') {
                $("#modalRSI").modal('toggle');
                $("#modalRSI").html('');
                $("#gridDatosProveedor").data("kendoGrid").dataSource.read();
            }
            else {
                if (result.hasOwnProperty('Success')) {
                    mensajeLabel(result.ErrorMessage);
                } else {
                    $("#modalRSI").modal('toggle');
                    $("#modalRSI").html('');
                    $("#gridDatosProveedor").data("kendoGrid").dataSource.read();
                }
            }
        },
        error: function (xhr, status) {
            mensajeLabel(xhr.statusText);
        }
    });
}

function ajaxPostHabilitarProveedor(url) {
    $.ajax({
        type: 'POST',
        url: url,
        data: {
            __RequestVerificationToken: $('[name= "__RequestVerificationToken"]').val()
        },
        dataType: 'json',
        cache: false,
        success: function (result, status, xhr) {
            if (typeof result === 'undefined') {
                $("#gridDatosProveedor").data("kendoGrid").dataSource.read();
            }
            else {
                if (result.hasOwnProperty('Success')) {
                    mensajeLabel(result.ErrorMessage);
                } else {
                    $("#gridDatosProveedor").data("kendoGrid").dataSource.read();
                }
            }
        },
        error: function (xhr, status) {
            mensajeLabel(xhr.statusText);
        }
    });
}

function ajaxPostEditarProveedor(url) {
    $.ajax({
        type: 'POST',
        url: url,
        data: $("#editarProveedorForm").serialize(),
        dataType: 'json',
        cache: false,
        success: function (result, status, xhr) {
            if (typeof result === 'undefined') {
                $("#modalRSI").modal('toggle');
                $("#modalRSI").html('');
                $("#gridDatosProveedor").data("kendoGrid").dataSource.read();
            }
            else {
                if (result.hasOwnProperty('Success')) {
                    mensajeLabel(result.ErrorMessage);
                } else {
                    $("#modalRSI").modal('toggle');
                    $("#modalRSI").html('');
                    $("#gridDatosProveedor").data("kendoGrid").dataSource.read();
                }
            }
        },
        error: function (xhr, status) {
            mensajeLabel(xhr.statusText);
        }
    });
}

function ajaxPostEliminarProveedor(url) {
    $.ajax({
        type: 'POST',
        url: url,
        data: $("#EliminarProveedorForm").serialize(),
        dataType: 'json',
        cache: false,
        success: function (result, status, xhr) {
            if (typeof result === 'undefined') {
                $("#modalRSI").modal('toggle');
                $("#modalRSI").html('');
                $("#gridDatosProveedor").data("kendoGrid").dataSource.read();
            }
            else {
                if (result.hasOwnProperty('Success')) {
                    var alerta = '<div class="alert alert-danger alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button><strong>Error: </strong>'
                        + result.ErrorMessage
                        + '</div>';
                    $("#mensajesModalLabel").html(alerta);
                } else {
                    $("#modalRSI").modal('toggle');
                    $("#modalRSI").html('');
                    $("#gridDatosProveedor").data("kendoGrid").dataSource.read();
                }
            }
        },
        error: function (xhr, status) {
            mensajeLabel(xhr.statusText);
        }
    });
}

////////////////////////////////////////////////
function mensajeLabel(mensaje) {
    var alerta = '<div class="alert alert-danger alert-dismissable" style="font-size: 16px;"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button><strong>Error: </strong>'
        + mensaje
        + '</div>';
    $("#mensajesLabel").html(alerta).show();
    window.setTimeout(function () {
        $("#mensajesLabel").fadeOut(500, function () {
            $("#mensajesLabel").hide('slow');
        });
    }, 10000);
}


