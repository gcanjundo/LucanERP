$(document).ready(function() {

    var is_session_expired = 'no';

    function check_session() {

        $.ajax({
            url: base_url + parm1 + parm2 + "session_ajax",
            success: function(data) {
                if (data == '1') {
                    alertify.alert('Atenção', 'Ops! notamos que sua sessão expirou clica OK para voltar a entrar !!').set({
                        onshow: null,
                        onclose: function() {
                            window.location.href = base_url + 'Seguranca/Login';

                        }
                    });

                    is_session_expired = 'yes';
                }
            }
        });
    }

    var count_interval = setInterval(function() {
        check_session();
        if (is_session_expired == 'yes') {
            clearInterval(count_interval);
        }
    }, 10000);
});