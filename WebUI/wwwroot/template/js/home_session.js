$(document).ready(function () {

    var is_session_expired = 'no';
    function check_session()
    {
        $.ajax({
            url: 'check_session_ajax',
            success: function (data)
            {
                if (data === '1')
                {
                    alert('Olá notamos que sua sessão expirou clica OK pata voltar a entrar !!!');
                    window.location.href = 'index';
                    is_session_expired = 'yes';
                }
            }
        })
    }

    var count_interval = setInterval(function () {
        check_session();
        if (is_session_expired === 'yes')
        {
            clearInterval(count_interval);
        }
    }, 10000);

});

