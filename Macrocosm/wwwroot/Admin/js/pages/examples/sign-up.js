$(function () {
    var from = $('#sign_up');
    function nof(color, text) {
        $.notify({
            message: text
        },
            {
                type: color,
                allow_dismiss: true,
                newest_on_top: true,
                timer: 1000,
                placement: {
                    from: "bottom",
                    align: "center"
                },
                animate: {
                    enter: "animated fadeInDown",
                    exit: "animated fadeOutUp"
                },
                template: '<div data-notify="container" class="bootstrap-notify-container alert alert-dismissible {0} ' + "p-r-35" + '" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span> ' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
            });
    }
    from.validate({
        highlight: function (input) {
            $(input).parents('.form-line').addClass('error');
        },
        unhighlight: function (input) {
            $(input).parents('.form-line').removeClass('error');
        },
        errorPlacement: function (error, element) {
            $(element).parents('.input-group').append(error);
            $(element).parents('.form-group').append(error);
        }
    });
    $("body").keydown(function (e) {
        var curKey = e.which;
        if (curKey === 13) {
            $(".submit").click();
            return false;
        }
    });
    $(".submit").click(function () {
        if (!from.valid()) {
            return;
        }
        var button = $(this);
        $.ajax({
            async: true,
            url: "/common_api/movie/login",
            data: from.serialize(),
            type: "post",
            dataType: 'json',
            beforeSend: function () {
                button.text("正在发送请求");
                button.attr("disabled", "disabled");
            },
            success: function (data) {
                button.removeAttr("disabled");
                button.text("SIGN UP");
                if (data.status_code === 200) {
                    nof("alert-success", data.msg);
                    setTimeout(function () { window.location.href = "/system/index"; }, 1000);
                } else {
                    nof("alert-danger", data.msg);
                }
            },
            timeout: 10000,
            error: function (data) {
                nof("alert-danger", "request error.");
            }
        });
    });
});