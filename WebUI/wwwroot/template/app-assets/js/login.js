function Entrar() {
    var username = $("#txtUsername").val();
    var password = $("#txtPassword").val();

    $('.errorText').html('');

    var is_error = '';

    
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
                    CurrentPassword: password
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
            });

    }

    
     
    /**
     * if (is_error === '') {

        jQuery.ajax({
            type: 'post',
            url: 'login/check_seguranca',
            data: 'username=' + username + '&password=' + password,
            success: function(data) {

                var response = jQuery.parseJSON(data);
                if (response.result === 'empresa') {

                    alertify.alert('Bem Vindo ', 'Esolha uma empressa que pretende trabalhar').set({
                        onshow: null,
                        onclose: function() {
                            window.location.href = 'Empresa';
                        }
                    });

                }
                if ((response.result === 'home')) {

                    alertify.alert('Bem Vindo. ', 'Ha pagina principal').set({
                        onshow: null,
                        onclose: function() {

                            //por ser outro controller colocamos a url com http://localhost/kitanda
                            window.location.href = 'http://localhost/kitanda/Home/Painel';
                        }
                    });

                } else {

                    jQuery('#result').html(response.msg);
                    // location.reload();

                }

            }
        });
    }
     */

}

function SelecteCompany(id, username) {
    $("#hdSelectedCompany").val(id);
    $("#txtUsername").val(username);
    $('#modalPassword').modal('open');
}