var jsonCommun = {
    ajaxPostCrear: function (url, gridName) {
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
                    var grids = document.querySelectorAll('[id^="' + gridName + '"]');
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
                        var grids1 = document.querySelectorAll('[id^="' + gridName + '"]');
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
    },
    ajaxGet: function (url) {
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
    },
    confirmDelete: function (s, e) {
        jsonCommun.vars.sender = s;
        jsonCommun.vars.celdaActiva = this._cellId;
        $("#modalRSI").html("");
        const htmlModal = '<div class="modal-dialog" role="document">' +
            '<div class="modal-content">' +
            '<div class="modal-header bg-danger">' +
            '<h4 class="modal-title" style="display: inline-block;">Eliminar Cliente</h4>' +
            '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
            '<span class="text-white" aria-hidden="true">&times;</span>' +
            '</button>' +
            '</div>' +
            '<br/>' +
            '<div class="modal-body">' +
            '<h4 style="margin-top: 0; margin-bottom: 30px;">¿Está seguro de querer eliminar el registro?</h4>' +
            '<div class="modal-footer" style="background: white; margin: 0; padding: 0;">' +
            '<br/>' +
            '<button type="button" onclick="jsonCommun.deleteItem();" class="btn btn-danger" style="margin: 10px 0 20px 0" data-dismiss="modal"><i class="fa fa-check" aria-hidden="true"></i> Eliminar</button>' +
            '<button type="button" class="btn btn-default" style="margin: 10px 0 20px 0" data-dismiss="modal"><i class="fa fa-close" aria-hidden="true"></i> Cancelar</button>' +
            '<br/>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';
        $("#modalRSI").html(htmlModal);
        $("#modalRSI").modal('toggle');
    },
    errorEx: function (s, e) {
        //es un error general thow excepcion
        if (s.xhr !== null) { //si el error es desde un grid de telerik
            var json = s.xhr.responseJSON;
            jsonCommun.showError(json.message);
            s.sender.read();
            s.sender.cancelChanges();
        } else if (s.responseJSON !== undefined && s.responseJSON !== null) {//si el error es desde una accion en cualquier parte del formulario
            jsonCommun.showError(s.responseJSON);
            //s.preventDefault();
        }
        else {
            jsonCommun.error(s);
        }
    },
    error: function (args) {
        $("div.k-tooltip-validation").hide();
        if (args.errors) {
            $(".k-grid").each(function () {
                var grid = $(this).data("kendoGrid");
                if (grid !== null && grid.dataSource === args.sender) {
                    grid.one("dataBinding", function (e) {
                        e.preventDefault();   /*cancel grid rebind if error occurs*/
                        for (var error in args.errors) {
                            jsonCommun.showMessage(grid.editable.element, error, args.errors[error].errors);
                        }
                    });
                }
            });
        }
    },
    deleteItem: function () {
        var gridName = jsonCommun.vars.celdaActiva.replace("_active_cell", "");
        var grid = $("#" + gridName).data("kendoGrid");

        //se obtiene la celda activar para el grid principal
        var row = $('#' + jsonCommun.vars.celdaActiva).closest('tr');
        if (row.length == 0) {
            //si esta es 0 entonces se prosigue a la eliminacion del detalle.
            row = $(jsonCommun.vars.sender.currentTarget).closest("tr")
        }
        jsonCommun.vars.sender = '';
        jsonCommun.vars.celdaActiva = '';
        var dataItem = grid.dataItem(row);
        grid.dataSource.remove(dataItem);
        grid.dataSource.sync();
    },

    resizeGrid: function (s, e) {
        var gridElement = $("#" + s.sender._cellId.replace('_active_cell', ''));
        var heightContainer = $(window).innerHeight() - $("#header").innerHeight() - ($("#page-content").innerHeight() - gridElement.innerHeight());
        var dataArea = gridElement.find(".k-grid-content");
        var newHeight = heightContainer - 20;
        var diff = gridElement.innerHeight() - dataArea.innerHeight();
        dataArea.height(newHeight - diff);
    },
    showError: function (message) {
        $("#mensajesLabel").html('')
        const alerta = '<div class="alert alert-danger alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button><strong>Error: </strong>'
            + message
            + '</div>';
        $("#mensajesLabel").html(alerta).show();
    },
    showMessage:function(container, name, errors) {
        var validationMessageTmpl = kendo.template($("#message").html());
        //add the validation message to the form
        container.find("[data-valmsg-for=" + name + "],[data-val-msg-for=" + name + "]")
            .replaceWith(validationMessageTmpl({ field: name, message: errors[0] }))
    },
    validarAlfaNumericos: function (e, expresionRegular) {
        var query = expresionRegular;
        if (query == null || query == '' || query == undefined) {
            query = /[A-Za-z0-9 ]/;
        }
        if (!(e.key).match(query)) {
            e.preventDefault();
        }
    },
    soloNumeros: function (e) {
        var evt = (e) ? e : window.event;
        var key = (evt.keyCode) ? evt.keyCode : evt.which;
        if (key !== null) {
            key = parseInt(key, 10);
            if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                if (!jsIsUserFriendlyChar(key, "Decimals")) return false;
            }
            else if (evt.shiftKey) return false;
        }
        return true;
    },
    isNumberKey: function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    },
    validarAlfaNumericosCaracteresEspeciales: function (e) {
        var query = /[A-Za-z0-9ñÑáéíóúÁÉÍÓÚ .,_/()*-]/;
        if (!(e.key).match(query)) {
            e.preventDefault();
        }
    },
    vars: {
        celdaActiva: '',
        sender: ''
    }
};
