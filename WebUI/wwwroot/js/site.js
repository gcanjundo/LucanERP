$(document).ready(function () {
    showInPopup = (url, title) => {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#form-modal .modal-body').html(res);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');
                },
                error: function (err) {
                    console.log(err);
                }
            })
            return false;
        } catch (ex) {
            console.log(ex);
        }
    }

    jQueryAjaxPost = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#view-all').html(res.html)
                        $('#form-modal .modal-body').html('');
                        $('#form-modal .modal-title').html('');
                        $('#form-modal').modal('hide');
                    }
                    else
                        $('#form-modal .modal-body').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }
 
    jQueryAjaxDelete = form => {
        if (confirm('Tem certeza que deseja apagar este registro?')) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        $('#view-all').html(res.html);
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex)
            }
        }

        return false;
    }
});