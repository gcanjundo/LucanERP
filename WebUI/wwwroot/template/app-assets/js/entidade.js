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
                html += '&lt;td&gt;' + item.txtreferencia + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtnomecompleto + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtnomecomercial + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtcategoria + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.tipo + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtidentificacao + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.pais + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtrua + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtbairro + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtmorada + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txttelefone + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txttelefonealteranativo + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtfax + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtwebsite + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtdesconto + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtlimitecredito + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtdistrito + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtcaixapostal + '&lt;/td&gt;';
             
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
            Codigo: $('#txtreferencia').val(),
            NomeCompleto: $('#txtnomecompleto').val(),
            NomeComercial: $('#txtnomecomercial').val(),
            Categoria: $('#txtcategoria').val(),
            Tipo: $('#tipo').val(),
            Identificacao: $('#txtidentificacao').val(),
            Pais: $('#pais').val(),
            Rua: $('#txtrua').val(),
            Bairro: $('#txtbairro').val(),
            Morada: $('#txtmorada').val(),
            Telefone: $('#txttelefone').val(),
            TelefoneAlternativo: $('#txttelefonealteranativo').val(),
            Fax: $('#txtfax').val(),
            Website: $('#txtwebsite').val(),
            Desconto: $('#txtdesconto').val(),
            LimiteCredito: $('#txtlimitecredito').val(),
            Distrito: $('#txtdistrito').val(),
            CaixaPostal: $('#txtcaixapostal').val()

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
           
            $('#txtreferencia').val(result.Codigo),
                $('#txtnomecompleto').val(result.NomeCompleto),
                $('#txtnomecomercial').val(result.NomeComercial),
                $('#txtcategoria').val(result.Categoria),
                $('#tipo').val(result.Tipo),
                $('#txtidentificacao').val(result.Identificacao),
                $('#pais').val(result.Pais),
                $('#txtrua').val(result.Rua),
                $('#txtbairro').val(result.Bairro),
                $('#txtmorada').val(result.Morada),
                $('#txttelefone').val(result.Telefone),
                $('#txttelefonealteranativo').val(result.TelefoneAlternativo),
                $('#txtfax').val(result.Fax),
                $('#txtwebsite').val(result.Website),
                $('#txtdesconto').val(result.Desconto),
                $('#txtlimitecredito').val(result.LimiteCredito),
                $('#txtdistrito').val(result.Distrito),
                $('#txtcaixapostal').val(result.CaixaPostal)
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
        NomeCompleto: $('#txtnomecompleto').val(),
        NomeComercial: $('#txtnomecomercial').val(),
        Categoria: $('#txtcategoria').val(),
        Tipo: $('#tipo').val(),
        Identificacao: $('#txtidentificacao').val(),
        Pais: $('#pais').val(),
        Rua: $('#txtrua').val(),
        Bairro: $('#txtbairro').val(),
        Morada: $('#txtmorada').val(),
        Telefone: $('#txttelefone').val(),
        TelefoneAlternativo: $('#txttelefonealteranativo').val(),
        Fax: $('#txtfax').val(),
        Website: $('#txtwebsite').val(),
        Desconto: $('#txtdesconto').val(),
        LimiteCredito: $('#txtlimitecredito').val(),
        Distrito: $('#txtdistrito').val(),
        CaixaPostal: $('#txtcaixapostal').val()
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
             $('#txtnomecompleto').val(),
             $('#txtnomecomercial').val(),
             $('#txtcategoria').val(),
             $('#tipo').val(),
             $('#txtidentificacao').val(),
             $('#pais').val(),
             $('#txtrua').val(),
             $('#txtbairro').val(),
             $('#txtmorada').val(),
             $('#txttelefone').val(),
             $('#txttelefonealteranativo').val(),
             $('#txtfax').val(),
             $('#txtwebsite').val(),
             $('#txtdesconto').val(),
             $('#txtlimitecredito').val(),
             $('#txtdistrito').val(),
             $('#txtcaixapostal').val()
               


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
        $('#txtreferencia').val(""),
        $('#txtnomecompleto').val(""),
        $('#txtnomecomercial').val(""),
        $('#txtcategoria').val(""),
        $('#tipo').val(""),
        $('#txtidentificacao').val(""),
        $('#pais').val(""),
        $('#txtrua').val(""),
        $('#txtbairro').val(""),
        $('#txtmorada').val(""),
        $('#txttelefone').val(""),
        $('#txttelefonealteranativo').val(""),
        $('#txtfax').val(""),
        $('#txtwebsite').val(""),
        $('#txtdesconto').val(""),
        $('#txtlimitecredito').val(""),
        $('#txtdistrito').val(""),
        $('#txtcaixapostal').val("")


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
    if ($('#txtnomecompleto').val().trim() == "") {
        $('#txtnomecompleto').css('border-color', 'Red');

        isValid = false;
    }
    else {

        $('#txtnomecompleto').css('border-color', 'lightgrey');
    }
    if ($('#txtnomecomercial').val().trim() == "") {
        $('#txtnomecomercial').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtnomecomercial').css('border-color', 'lightgrey');
    }
    if ($('#txtcategoria').val().trim() == "") {
        $('#txtcategoria').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtcategoria').css('border-color', 'lightgrey');
    }
    if ($('#tipo').val().trim() == "") {
        $('#tipo').css('border-color', 'Red');

        isValid = false;
    }
    else {

        $('#tipo').css('border-color', 'lightgrey');
    }
    if ($('#txtidentificacao').val().trim() == "") {
        $('#txtidentificacao').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtidentificacao').css('border-color', 'lightgrey');
    }
    if ($('#pais').val().trim() == "") {
        $('#pais').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#pais').css('border-color', 'lightgrey');
    }
    if ($('#txtrua').val().trim() == "") {
        $('#txtrua').css('border-color', 'Red');

        isValid = false;
    }
    else {

        $('#txtrua').css('border-color', 'lightgrey');
    }
    if ($('#txtbairro').val().trim() == "") {
        $('#txtbairro').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtbairro').css('border-color', 'lightgrey');
    }
    if ($('#txtmorada').val().trim() == "") {
        $('#txtmorada').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtmorada').css('border-color', 'lightgrey');
    }
    if ($('#txttelefone').val().trim() == "") {
        $('#txttelefone').css('border-color', 'Red');

        isValid = false;
    }
    else {

        $('#txttelefone').css('border-color', 'lightgrey');
    }
    if ($('#txttelefonealteranativo').val().trim() == "") {
        $('#txttelefonealteranativo').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txttelefonealteranativo').css('border-color', 'lightgrey');
    }
    if ($('#txtfax').val().trim() == "") {
        $('#txtfax').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtfax').css('border-color', 'lightgrey');
    }
    if ($('#txtwebsite').val().trim() == "") {
        $('#txtwebsite').css('border-color', 'Red');

        isValid = false;
    }
    else {

        $('#txtwebsite').css('border-color', 'lightgrey');
    }
    if ($('#txtdesconto').val().trim() == "") {
        $('#txtdesconto').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtdesconto').css('border-color', 'lightgrey');
    }
    if ($('#txtlimitecredito').val().trim() == "") {
        $('#txtlimitecredito').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtlimitecredito').css('border-color', 'lightgrey');
    }
    if ($('#txtdistrito').val().trim() == "") {
        $('#txtdistrito').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtdistrito').css('border-color', 'lightgrey');
    }
    if ($('#txtcaixapostal').val().trim() == "") {
        $('#txtcaixapostal').css('border-color', 'Red');
        isValid = false;
    }
    else {

        $('#txtcaixapostal').css('border-color', 'lightgrey');
    }
    return isValid;

}
