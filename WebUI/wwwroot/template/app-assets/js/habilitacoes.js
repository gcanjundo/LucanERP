$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: "/Home/Habilitacoes",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {

                html += '&lt;tr&gt;';
                html += '&lt;td&gt;' + item.txtreferencia + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtsigla + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtdesignacao + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.estado + '&lt;/td&gt;';

                html += '&lt;td&gt;&lt;a href="#" onclick="return getbyID(' + item.txtreferencia + ')"&gt;Edit&lt;/a&gt; | &lt;a href="#" onclick="Delele(' + txtreferencia + ')"&gt;Delete&lt;/a&gt;&lt;/td&gt;';
                html += '&lt;/tr&gt;';

            });
            $('.tbody').html(html);

        },

        error: function (errormessage) {

            alert(errormessage.responseText);

        }
    });

}
function Salvar() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var Obj = {
        Codigo: $('#txtreferencia').val(),
        Sigla: $('#txtsigla').val(),
        Designacao: $('#txtdesignacao').val(),
        Situacao: $('#estado').val()

    };

    $.ajax(
        url: "/Home/Salvar",
        data: JSON.stringify(Obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",

        success: function (result) {
            loadData();

            $('#myModal').modal('hide');
        },

        error: function (errormessage) {

            alert(errormessage.responseText);

        }

    });
  
}

function GetbyID(Id) {
    $('#txtsigla').css('border-color', 'lightgrey');
    $('#txtdesignacao').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Home/GetbyID/" + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",

        success: function (result) {
            $('#txtreferencia').val(result.Codigo);
            $('#txtsigla').val(result.Sigla);
            $('#txtdesignacao').val(result.Descricao);
            $('#estado').val(result.Situacao);
            $('#myModal').modal('show');
            $('#btnEditar').show();
            $('#btnSalvar').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    return false;
}
function Update() {
    var res = validate();
    if (res == false) {
        return false;

    }
    var Obj = {
        Codigo: $('#txtreferencia').val(),
        Sigla: $('#txtsigla').val(),
        Descricao: $('#txtdesignacao').val(),
        Situacao: $('#estado').val()
    };
    $.ajax({
        url: "/Home/Update",
        data: JSON.stringify(Obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#txtreferencia').val(),
                $('#txtsigla').val(),
                $('#txtdesignacao').val(),
                $('#estado').val(),
               


        },

        error: function (errormessage) {

            alert(errormessage.responseText);

        }
    });

}

function Delele(Id) {
    var ans = confirm("Tem certeza que pretende eliminar os dados? ");
    if (ans) {
        $.ajax({
            url: "/Home/Delete/" + Id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {

                loadData();
            },
            error: function (errormessage) {

                alert(errormessage.responseText);
            }
        });

    }

}
function ClearFields() {
    $('#txtreferencia').val("");
    $('#txtsigla').val("");
    $('#txtdesignacao').val("");
    $('#estado').val("");


    $('#myModal').modal('show');
    $('#btnEditar').hide();
    $('#btnSalvar').show();
    $('#btnUpdate').hide();

    $('#txtsigla').css('border-color', 'lightgrey');
    $('#txtdesignacao').css('border-color', 'lightgrey');
    $('#estado').css('border-color', 'lightgrey');



}

function validate() {
    var isValid = true;
    if ($('#txtsigla').val().trim() == "") {
        $('#txtsigla').css('border-color', 'Red');

        isValid = false;
    }
    else {

        $('#txtsigla').css('border-color', 'lightgrey');
    }
    if ($('#txtdesignacao').val().trim() == "") {
        $('#txtdesignacao').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtdesignacao').css('border-color', 'lightgrey');
    }
    if ($('#estado').val().trim() == "") {
        $('#estado').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#estado').css('border-color', 'lightgrey');
    }
    return isValid;

}


