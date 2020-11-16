
function Entrar() {


    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });

    var username = $("#txtUsername").val();
    var password = $("#txtPassword").val();
    var companyId = $("#hdSelectedCompany").val();

    $('.errorText').html('');

    var is_error = '';

    if (companyId == undefined) {
        companyId = "";
    }
    if (username === '' || password === '') {

        if (username === '') {
         //   $('#emailError').html('Digite o Utilizador');
        } else if (password === '') {
         //   $('#passwordError').html('Digite a Palavra-Passe');
        } 
        is_error = 'yes';
    }

      

    if (is_error == '') {

        $.ajax(
            {
                type: "POST",
                url: '/Home/Entrar',
                data: {
                    Utilizador: username,
                    CurrentPassword: password,
                    Filial : companyId
                },
                error: function (result)
                {
                    alert("Ocorreu um erro de Servidor");
                },
                success: function (result)
                {
                    if (result.Sucesso == false && result.MensagemErro != "") {
                        //alert(result.MensagemErro);

                        alertify.alert('Erro ao Entrar ', result.MensagemErro).set({
                            onshow: null,
                            onclose: function () {
                                
                            }
                        });
                    } else
                    {
                        if(result.Url == "BranchSelection")
                            window.location.href = "/Home/" + result.Url;
                        else 
                        alertify.alert('Sucesso', 'Bem-vindo ao Sistema').set({
                            onshow: null,
                            onclose: function ()
                            {
                                window.location.href = "/Home/" + result.Url;
                            }
                        });
                    }
                }
            }).done(function () {
                setTimeout(function () {
                    $("#overlay").fadeOut(300);
                }, 500);
            });
    }
}

function SelecteCompany(id, username) {
    $("#hdSelectedCompany").val(id);
    $("#txtUsername").val(username);
    $('#modalPassword').modal('open');
}