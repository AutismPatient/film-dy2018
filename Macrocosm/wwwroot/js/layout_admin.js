
$(function () {
    UserInfo();
    function UserInfo() {
        $.ajax({
            async: true,
            url: "/common_api/movie/userinfo",
            type: "get",
            dataType: 'json',
            success: function (data) {
                var user = data.data;
                var html = '<div class="name" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' + user.nickName+'</div>' +
                    '<div class="email">' + user.userName +'</div>' +
                    '<div class="btn-group user-helper-dropdown">' +
                    '<i class="material-icons" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">keyboard_arrow_down</i>' +
                    '<ul class="dropdown-menu pull-right">' +
                    '<li><a href="javascript:void(0);" id="update-password" data-id="' + user.id +'"><i class="material-icons">shopping_cart</i>修改密码</a></li>' +
                    ' <li role="separator" class="divider"></li>' +
                    '<li><a href="/system/Logout" id="logout"><i class="material-icons">input</i>注销登录</a></li>' +
                    '</ul>' +
                    '</div>';
                $(".info-container").html(html);
            },
            timeout: 20000,
            error: function (data) {
                nof("alert-danger", "request error.");
            }
        });
    }
});