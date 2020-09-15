function ajaxGetPlanTuristico(url) {
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

function ajaxPostCrearPlanTuristico(url) {
    $.ajax({
        type: 'POST',
        url: url,
        data: $("#crearPlanTuristicoForm").serialize(),
        dataType: 'json',
        cache: false,
        success: function (result, status, xhr) {
            if (typeof result === 'undefined') {
                $("#modalRSI").modal('toggle');
                $("#modalRSI").html('');
                $("#gridDatosPlanTuristico").data("kendoGrid").dataSource.read();
            }
            else {
                if (result.hasOwnProperty('Success')) {
                    mensajeLabel(result.ErrorMessage);
                } else {
                    $("#modalRSI").modal('toggle');
                    $("#modalRSI").html('');
                    $("#gridDatosPlanTuristico").data("kendoGrid").dataSource.read();
                }
            }
        },
        error: function (xhr, status) {
            mensajeLabel(xhr.statusText);
        }
    });
}

function ajaxPostCrear(url) {
    $.ajax({
        type: 'POST',
        url: url,
        data: $("#Form").serialize(),
        dataType: 'json',
        cache: false,
        success: function (result, status, xhr) {
            if (typeof result === 'undefined') {
                $("#modalRSI").modal('toggle');
                $("#modalRSI").html('');
                var grids = document.querySelectorAll('[id^="gridDatosPlanTuristico"]');
                for (var i = 0; i < grids.length; i++) {
                    $("#" + grids[i].id).data("kendoGrid").dataSource.read();
                }

            }
            else {
                if (result.hasOwnProperty('Success')) {
                    var alerta = '<div class="alert alert-danger alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button><strong>Error: </strong>'
                        + result.ErrorMessage
                        + '</div>';
                    $("#mensajesModalLabel").html(alerta);
                }
                else {
                    $("#modalRSI").modal('toggle');
                    $("#modalRSI").html('');
                    var grids1 = document.querySelectorAll('[id^="gridDatosPlanTuristico"]');
                    for (var j = 0; j < grids1.length; j++) {
                        $("#" + grids1[j].id).data("kendoGrid").dataSource.read();
                    }
                }
            }
        },
        error: function (xhr, status) {
            var alerta = '<div class="alert alert-danger alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button><strong>Error: </strong>'
                + xhr.statusText
                + '</div>';
            $("#mensajesModalLabel").html(alerta);
        }
    });
}

function ajaxPostCrearDetalle(url) {
    $.ajax({
        type: 'POST',
        url: url,
        data: $("#Form").serialize(),
        dataType: 'json',
        cache: false,
        success: function (result, status, xhr) {
            if (typeof result === 'undefined') {
                $("#modalRSI").modal('toggle');
                $("#modalRSI").html('');
                var grids = document.querySelectorAll('[id^="gridDatosPlanTuristico"]');
                for (var i = 0; i < grids.length; i++) {
                    $("#" + grids[i].id).data("kendoGrid").dataSource.read();
                }
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
                    var grids1 = document.querySelectorAll('[id^="gridDatosPlanTuristico"]');
                    for (var j = 0; j < grids1.length; j++) {
                        $("#" + grids1[j].id).data("kendoGrid").dataSource.read();
                    }
                }
            }
        },
        error: function (xhr, status) {
            var alerta = '<div class="alert alert-danger alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button><strong>Error: </strong>'
                + xhr.statusText
                + '</div>';
            $("#mensajesModalLabel").html(alerta);
        }
    });
}

function ajaxPostHabilitarPlanTuristico(url) {
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
                $("#gridDatosPlanTuristico").data("kendoGrid").dataSource.read();
            }
            else {
                if (result.hasOwnProperty('Success')) {
                    mensajeLabel(result.ErrorMessage);
                } else {
                    $("#gridDatosPlanTuristico").data("kendoGrid").dataSource.read();
                }
            }
        },
        error: function (xhr, status) {
            mensajeLabel(xhr.statusText);
        }
    });
}

function ajaxPostEditarPlanTuristico(url) {
    $.ajax({
        type: 'POST',
        url: url,
        data: $("#editarPlanTuristicoForm").serialize(),
        dataType: 'json',
        cache: false,
        success: function (result, status, xhr) {
            if (typeof result === 'undefined') {
                $("#modalRSI").modal('toggle');
                $("#modalRSI").html('');
                $("#gridDatosPlanTuristico").data("kendoGrid").dataSource.read();
            }
            else {
                if (result.hasOwnProperty('Success')) {
                    mensajeLabel(result.ErrorMessage);
                } else {
                    $("#modalRSI").modal('toggle');
                    $("#modalRSI").html('');
                    $("#gridDatosPlanTuristico").data("kendoGrid").dataSource.read();
                }
            }
        },
        error: function (xhr, status) {
            mensajeLabel(xhr.statusText);
        }
    });
}

function ajaxPostEliminarPlanTuristico(url) {
    $.ajax({
        type: 'POST',
        url: url,
        data: $("#EliminarPlanTuristicoForm").serialize(),
        dataType: 'json',
        cache: false,
        success: function (result, status, xhr) {
            if (typeof result === 'undefined') {
                $("#modalRSI").modal('toggle');
                $("#modalRSI").html('');
                $("#gridDatosPlanTuristico").data("kendoGrid").dataSource.read();
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
                    $("#gridDatosPlanTuristico").data("kendoGrid").dataSource.read();
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

////////////////////////////////////////////////





