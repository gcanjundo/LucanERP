$(document).ready(function ()
{   
    loadData(); 
});

function loadData() {
    $.ajax({
        url: "/Home/Armazem",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function(result) {
            var html = '';
            $.each(result, function (key, item) {
            
                html += '&lt;tr&gt;';
                html += '&lt;td&gt;' + item.txtsigla + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.txtdesignacao + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.cbactivo + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.cbWarehouseType + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.cbAlertaStockMinimo + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.cbPermiteStockNegativo + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.cbAlertaStockNegativo + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.cbPOS + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.cbIN + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.cbOUT + '&lt;/td&gt;';
                html += '&lt;td&gt;' + item.cbPOS + '&lt;/td&gt;';
                html += '&lt;td&gt;&lt;a href="#" onclick="return getbyID(' + item.txtarmazemid + ')"&gt;Edit&lt;/a&gt; | &lt;a href="#" onclick="Delele(' + txtarmazemid + ')"&gt;Delete&lt;/a&gt;&lt;/td&gt;';
                html += '&lt;/tr&gt;';
                
            });
            $('.tbody').html(html);
            
        },
        
        error: function(errormessage) {
            
            alert(errormessage.responseText);
           
        }
    });
    
}
function Salvar()
{
    var res = validate();
    if (res == false) {
        return false;
    }
   
    var Obj = {
        Codigo: $('#txtarmazemId').val(),
        Sigla: $('#txtsigla').val(),
        Descricao: $('#txtdesignacao').val(),
        Status: $('#cbactivo').val(),
        AlertaStockMinimo: $('#cbAlertaStockMinimo').val(),
        PermiteStockNegativo: $('#cbPermiteStockNegativo').val(),
        AlertaStockNegativo: $('#cbAlertaStockNegativo').val(),
        Tipo: $('#cbWarehouseType').val(),
        EnablePOS: $('#cbPOS').val(),
        AllowIncome: $('#cbIN').val(),
        AllowOutcome: $('#cbOUT').val(),
        IsForRestTipo: $('#isForRest').val(),
        TablePriceID: $('#tablepriceId').val()
    };
    
    $.ajax(
        url: "/Home/Salvar",
        data: JSON.stringify(Obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        
        success: function(result) {     
            loadData();
            
            $('#myModal').modal('hide');  
        },
        
        error: function(errormessage) {
            
            alert(errormessage.responseText);
           
        }

    });
  
}  

function GetbyID(Id) {
    $('#txtsigla').css('border-color', 'lightgrey');
    $('#txtdesignacao').css('border-color', 'lightgrey');
   
    $.ajax({
        url: "/Home/GetbyID/"+Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",

        success: function (result) {
            $('#txtarmazemId').val(result.Codigo);
            $('#txtsigla').val(result.Sigla);
            $('#txtdesignacao').val(result.Descricao);
            $('#cbactivo').val(result.Status);
            $('#cbAlertaStockMinimo').val(result.AlertaStockMinimo);
            $('#cbPermiteStockNegativo').val(result.PermiteStockNegativo);
            $('#cbAlertaStockNegativo').val(result.AlertaStockNegativo);
            $('#cbWarehouseType').val(result.Tipo);
            $('#cbPOS').val(result.EnablePOS);
            $('#cbIN').val(result.AllowIncome);
            $('#cbOUT').val(result.AllowOutcome);
            $('#isForRest').val(result.IsForRestTipo);
            $('#tablepriceId').val(result.TablePriceID);
            $('#myModal').modal('show');
            $('#btnEditar').show();
            $('#btnSalvar').hide();
        },
        error: function(errormessage) {
            alert(errormessage.responseText);
        }
    });
   
    return false;
}
function Update()
{
    var res = validate();
    if (res == false)
    {
        return false;
        
    }
    var Obj = {
        Codigo: $('#txtarmazemId').val(),
        Sigla: $('#txtsigla').val(),
        Descricao: $('#txtdesignacao').val(),
        Status: $('#cbactivo').val(),
        AlertaStockMinimo: $('#cbAlertaStockMinimo').val(),
        PermiteStockNegativo: $('#cbPermiteStockNegativo').val(),
        AlertaStockNegativo: $('#cbAlertaStockNegativo').val(),
        Tipo: $('#cbWarehouseType').val(),
        EnablePOS: $('#cbPOS').val(),
        AllowIncome: $('#cbIN').val(),
        AllowOutcome: $('#cbOUT').val(),
        IsForRestTipo: $('#isForRest').val(),
        TablePriceID: $('#tablepriceId').val()
    };
    $.ajax({
        url: "/Home/Update",
        data: JSON.stringify(Obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function(result) {
            loadData();
            $('#myModal').modal('hide');
            $('#txtarmazemId').val("");
            $('#txtsigla').val(),
            $('#txtdesignacao').val(),
            $('#cbactivo').val(),
            $('#cbAlertaStockMinimo').val(),
            $('#cbPermiteStockNegativo').val(),
            $('#cbAlertaStockNegativo').val(),
            $('#cbWarehouseType').val(),
            $('#cbPOS').val(),
            $('#cbIN').val(),
            $('#cbOUT').val(),
            $('#isForRest').val(),
            $('#tablepriceId').val()
            
        },
       
        error: function(errormessage) {
            
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
    $('#txtarmazemId').val("");
    $('#txtsigla').val("");
    $('#txtdesignacao').val("");
    $('#cbactivo').val("");
    $('#cbAlertaStockMinimo').val("");
    $('#cbPermiteStockNegativo').val("");
    $('#cbAlertaStockNegativo').val("");
    $('#cbWarehouseType').val("");
    $('#cbPOS').val("");
    $('#cbIN').val("");
    $('#cbOUT').val("");
    $('#isForRest').val("");
    $('#tablepriceId').val("");
    $('#myModal').modal('show');
    $('#btnEditar').hide();
    $('#btnSalvar').show();
    $('#btnUpdate').hide();
   
    $('#txtsigla').css('border-color', 'lightgrey');
    $('#txtdesignacao').css('border-color', 'lightgrey');
   
    
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
    
    return isValid;
    
}

​
