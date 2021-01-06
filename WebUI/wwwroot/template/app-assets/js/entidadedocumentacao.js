$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: "/Home/Entidade",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {

                html += '&lt;tr&gt;';
                html += '&lt;td&gt;' + item.txtnumero + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtreferencia + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtnomecomercial + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtdocumento + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtentidade + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtemissao + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtvalidade + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtlocal + '&lt;/td&gt;';
                
             
                html += '&lt;td&gt;&lt;a href="#" onclick="return GetbyID(' + item.txtreferencia + ')"&gt;Edit&lt;/a&gt; | &lt;a href="#" onclick="Delele(' + txtreferencia + ')"&gt;Delete&lt;/a&gt;&lt;/td&gt;';
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
        Numero: $('#txtnumero').val(),
        Referencia: $('#txtreferencia').val(),
        Documento: $('#txtdocumento').val(),
        Entidade: $('#txtentidade').val(),
        Emissao: $('#txtemissao').val(),
        Validade: $('#txtvalidade').val(),
        Local: $('#txtlocal').val()
          

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

            $('#txtnumero').val(result.Numero),
                $('#txtreferencia').val(result.Referencia),
                $('#txtdocumento').val(result.Documento),
                $('#txtentidade').val(result.Entidade),
                $('#txtemissao').val(result.Emissao),
                $('#txtvalidade').val(result.Validade),
                $('#txtlocal').val(result.Local),
               
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
        Numero: $('#txtnumero').val(),
        Referencia: $('#txtreferencia').val(),
        Documento: $('#txtdocumento').val(),
        Entidade: $('#txtentidade').val(),
        Emissao: $('#txtemissao').val(),
        Validade: $('#txtvalidade').val(),
        Local: $('#txtlocal').val()
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
               $('#txtnumero').val(),
                $('#txtreferencia').val(),
                $('#txtdocumento').val(),
                $('#txtentidade').val(),
                $('#txtemissao').val(),
                $('#txtvalidade').val(),
                $('#txtlocal').val()
           
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
    $('#txtnumero').val(""),
        $('#txtreferencia').val(""),
        $('#txtdocumento').val(""),
        $('#txtentidade').val(""),
        $('#txtemissao').val(""),
        $('#txtvalidade').val(""),
        $('#txtlocal').val("")
        

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
    if ($('#txtnumero').val().trim() == "") {
        $('#txtnumero').css('border-color', 'Red');

        isValid = false;
    }
    else {

        $('#txtnumero').css('border-color', 'lightgrey');
    }
    if ($('#txtreferencia').val().trim() == "") {
        $('#txtreferencia').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtreferencia').css('border-color', 'lightgrey');
    }
    if ($('#txtdocumento').val().trim() == "") {
        $('#txtdocumento').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtdocumento').css('border-color', 'lightgrey');
    }
    if ($('#txtentidade').val().trim() == "") {
        $('#txtentidade').css('border-color', 'Red');

        isValid = false;
    }
    else {

        $('#txtentidade').css('border-color', 'lightgrey');
    }
    if ($('#txtemissao').val().trim() == "") {
        $('#txtemissao').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtemissao').css('border-color', 'lightgrey');
    }
    if ($('#txtvalidade').val().trim() == "") {
        $('#txtvalidade').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtvalidade').css('border-color', 'lightgrey');
    }
    if ($('#txtlocal').val().trim() == "") {
        $('#txtlocal').css('border-color', 'Red');

        isValid = false;
    }
    else {

        $('#txtlocal').css('border-color', 'lightgrey');
    }
    
    return isValid;

}
