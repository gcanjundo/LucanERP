
function add_utilizador() {

    var username = jQuery('#username').val();
    var fullname = jQuery('#fullname').val();
    var password = jQuery('#password').val();
    var image = jQuery('#image').val();
    var level = jQuery('#level').val();
    var id = jQuery('#id').val();

    jQuery('.field_error').html('');

    var is_error = '';

    if (username == '') {
        jQuery('#username_error').html('utilizador');
        is_error = 'yes';
    }
    if (fullname == '') {
        jQuery('#fullname_error').html('nome completo');
        is_error = 'yes';
    }
    if (password == '') {
        jQuery('#password_error').html('senha');
        is_error = 'yes';
    }
    if (image == '') {
        jQuery('#image_error').html('seleciona image');
        is_error = 'yes';
    } else {
        var array = image.split(/\.(?=[^\.]+$)/);
        var count = array.length;
        var ext = array[count - 1].toLowerCase();
        if (ext == 'png' || ext == 'jpg' || ext == 'jpeg') {

        } else {
            jQuery('#image_error').html('Selecione somente imagem png, jpg ou jpeg');
            is_error = 'yes';
        }
    }
    if (level == '') {
        jQuery('#nivel_error').html('nivel');
        is_error = 'yes';
    }

    if (is_error == '') {
        var options = {success: function (data)
            {
                alertify.alert('Atenção', data).set({onshow: null, onclose: function () {
                        window.location.href = 'http://localhost/kits-start/home';
                    }});

            }};
        $("#frmData").ajaxForm(options).submit();
    }
}
