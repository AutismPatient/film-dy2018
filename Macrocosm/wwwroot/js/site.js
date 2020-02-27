// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    var page = 1;
    var cat = "";
    var tag = [];
    var title = "";
    var order = 1;
    li_add = $(".cat ul li");
    li_add.click(function (e) {
        $(this).addClass("add");
        $(this).siblings("li").removeClass("add");
    });
    tag_add = $(".tag ul li a");
    tag_add.click(function (e) {
        that = $(this).parent("li");
        console.log(that.attr("class"));
        if (that.attr("class") === "") {
            that.addClass("active");
        } else {
            that.removeClass("active");
        }
    });
    $(".sort").click(function () {
        $(".sort-div").toggle(150);
    });
    $(".sort-div input[type=checkbox]").change(function () {
        if ($(this).is(":checked")) {
            order = $(this).attr("data-sort");
            GetAll(1, true, cat, tag, title, order);
            $(".sort-div input[type=checkbox]").not($(this)).prop("checked", false);
        } else {
            order = 1;
            GetAll(1, true, cat, tag, title, order);
        }
    });
    // top
    $(window).scroll(function () {
        if ($(window).scrollTop() > 60) {
            $(".top").fadeIn(500);
        }
        else {
            $(".top").fadeOut(500);
        }
    });
    $(".top").click(function () {
        $('body,html').animate({ scrollTop: 0 }, 400);
        return false;
    });
    // copy 
    $(document).on("click", ".copy", function () {
        var copyText = document.getElementById("myInput");
        copyText.value = $(this).text();
        copyText.select();
        copyText.setSelectionRange(0, 99999);
        document.execCommand("copy");
        showtoast("text-success", "链接复制成功，请等待迅雷启动或者前往粘贴.");
    });
    // 列表请求数据
    function GetAll(index, ny, cat, tag,title,order) {
        var tags = tag.join("/");
        $.ajax({
            async: true,
            url: "/common_api/movie/movies",
            data: {
                page: index,
                menu: cat,
                category: tags,
                title: title,
                order:order
            },
            type: "get",
            dataType: 'json',
            beforeSend: function () {
                if(ny) {
                    $(".loading").show();
                }
                $(".content").html("");
            },
            success: function (data) {
                $(".loading").hide();
                if (data.status_code === 200) { // 渲染数据list
                    var result = data.data;
                    var ht = [];
                    var menu = "";
                    if (result.length <= 0) {
                        var nopre = '<div class="col-md-12 text-center" style="line-height:30;"><p class="text-danger"><i class="fas fa-exclamation-triangle"></i> 暂无数据</p></div>';
                        ht.push(nopre);
                    }
                    else {
                        menu = result[1].menu;
                        for (var i = 0; i < result.length; i++) {
                            var html = "<div class='col-lg-4 col-md-6 col-sm-6 col-xs-6'><div class='card shadow'><a href='javascript:void(0);' data-id='"+result[i].id+"'><img src='" + result[i].surfaceUrl + "'onerror='imgerrorfun();' class='card-img-top' alt=''>" +
                                "<div class='card-body pl-0 pr-0'><h5 class='card-title' data-toggle='tooltip' data-placement='top' title='" + result[i].title + "'>" + result[i].title + "</h5>" +
                                "<p class='card-text'></p></div><p class='p-info mb-1'><span><i class='fas fa-clock'></i> " + result[i].releaseTime + "</span>" +
                                "<span class='float-right'><i class='far fa-bookmark'></i> " + result[i].category + "</span></p></a></div></div>";
                            ht.push(html);
                        }

                        var dis = "";
                        if (index <= 1) {
                            dis = "disabled='disabled'";
                        }
                        var dis1 = "";
                        if (result.length < 12) {
                            dis1 = "disabled='disabled'";
                        }
                        var pre = '<div class="col-md-12 text-center page mt-3"><button ' + dis + ' type="button" class="btn btn-sm btn-outline-primary previous" data-toggle="tooltip" data-placement="top" title="第' + (index - 1) + '页"> 上一页 </button ><button type="button" class="btn btn-sm btn-outline-primary next" data-toggle="tooltip" data-placement="top" title="第' + (index + 1) + '页" ' + dis1 + '> 下一页 </button></div>';
                        ht.push(pre);
                    }
                    $(".content").html(ht);
                    if (menu === "动漫资源" || menu === "最新综艺") {
                        $(".content a p").hide();

                    } else if (menu === "游戏下载") {
                        $(".content a img").hide();
                        $(".content a p").hide();
                    }
                }
            },
            timeout: 10000,
            error: function () {
                showtoast("text-danger", "Error ! 在请求过程中发生错误.");
            }
        });
    }

    $(document).on("click", ".previous", function () {
        if (page <= 1) {
            page = 1;
        } else {
            page--;
        }
        $(this).tooltip('dispose');
        $('body,html').animate({ scrollTop: 0 }, 400);
        GetAll(page, true, cat, tag, title,order);
    });
    $(document).on("click",".next",function () {
        if (page < 1) {
            page = 1;
        } else {
            page++;
        }
        $(this).tooltip('dispose');
        $('body,html').animate({ scrollTop: 0 }, 400);
        GetAll(page, true, cat, tag, title, order);
    });

    // 按类别筛选
    $(".cat ul li").click(function () {
        var text = $(this).children("a").text();
        cat = text;
        page = 1;
        if (cat === "全部") {
            cat = "";
        }
        GetAll(1, true, cat, tag, title, order);
    });  
    // 按标签筛选
    $(".tag ul li").click(function () {
        page = 1;
        var text = $(this).children("a").text();
        if ($(this).attr("class") !== "") {
            tag.push(text);
        } else {
            tag.splice(jQuery.inArray(text, tag), 1);
        }
        GetAll(1, true, cat, tag, title, order);
    }); 
    // 按关键词搜索
    $(".input-search").bind("keypress", function (e) {

        if (e.keyCode === 13) {
            page = 1;
            title = $(this).val();
            GetAll(1, true, "", [], title, order);
        }
    });
    //排序
    //$("")
    GetAll(1, true, "", [], title, order); //初始化


    $(".content").on("click", "a", function () {
        var id = $(this).attr("data-id");
        console.log(id);
        $('#details').modal("show");
        modal_data(id);
    });

    //$('#details').on('show.bs.modal', function (e) {
        
    //});
    // 模态框数据
    function modal_data(id) {
        $.ajax({
            async: true,
            url: "/common_api/movie/detail",
            data: {
               id:id
            },
            type: "get",
            dataType: 'json',
            beforeSend: function () {
                $(".modal-content-html").html("");
                $(".modal .loading").show();
            },
            success: function (data) {
                $(".modal .loading").hide();
                if (data.status_code === 200) { // 渲染数据list
                    var result = data.data;
                    var ht = "";
                    ht += '<div class="modal-header"><h5 class="modal-title text-truncate" id="exampleModalLabel"><b>' + result.title + '</b></h5><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>';
                    ht += '<div class="modal-body">' +
                        '<div class="row"><div class="col-md-8 movie-info"><dl class="row mb-0"><dt class="col-sm-3">译名</dt><dd class="col-sm-9" data-toggle="tooltip" data-placement="top" title="' + result.alternateName + '">' + result.alternateName + '</dd>' +
                        '<dt class="col-sm-3">年代</dt><dd class="col-sm-9">' + result.age + '</dd><dt class="col-sm-3">上映时间</dt><dd class="col-sm-9">' + result.releaseTime + '</dd><dt class="col-sm-3">地区</dt><dd class="col-sm-9">' + result.origin + '</dd>' +
                        '<dt class="col-sm-3">类别</dt><dd class="col-sm-9">' + result.category + '</dd><dt class="col-sm-3 ">导演</dt><dd class="col-sm-9" data-toggle="tooltip" data-placement="top" title="' + result.director + '">' + result.director + '</dd><dt class="col-sm-3 ">主演</dt>' +
                        '<dd class="col-sm-9" data-toggle="tooltip" data-placement="top" title="' + result.protagonist + '">' + result.protagonist + '</dd><dt class="col-sm-3 ">标签</dt>' +
                        '<dd class="col-sm-9" data-toggle="tooltip" data-placement="top" title="' + result.tags + '">' + result.tags + '</dd>' +
                        '<dt class="col-sm-3">豆瓣评分</dt>' +
                        '<dd class="col-sm-9">' + result.douBanGrade + '</dd>' +
                        '<dt class="col-sm-3">IMDb评分</dt>' +
                        '<dd class="col-sm-9 text-success">' + result.imDb + '</dd>' +
                        '<dt class="col-sm-3">简介</dt>' +
                        '<dd class="col-sm-9" data-toggle="tooltip" data-placement="top" title="' + result.synopsis + '">' + result.synopsis + '</dd></dl>' +
                        '</div><div class="col-md-4 text-center"><img src="' + result.surfaceUrl + '" class="img-fluid" style="max-height:300px;" /></div></div></div>';
                    var links = [];
                    links = result.downloadLink.split(",");
                    if (links.length > 0) {
                        for (var i = 0; i < links.length; i++) {
                            var val = links[i];
                            if (val.indexOf("ftp") >= 0) {
                                ht += '<div class="downlink-div text-truncate"><a data-toggle="tooltip" data-placement="left" title="点击复制，迅雷下载" class="copy">' + val + '</a></div>';
                            } else {
                                ht += '<div class="downlink-div text-truncate"><a href ="' + val + '" data-toggle="tooltip" data-placement="left" title = "磁力链接下载" target = "_blank">' + val + '</a></div>';
                            }
                        }
                    }
                    ht += '<div class="modal-footer"><button type="button" class="btn btn-outline-primary btn-sm" data-dismiss="modal"> 关闭 </button></div>';
                    $(".modal-content-html").html(ht);
                }
            },
            timeout: 10000,
            error: function () {
                showtoast("text-danger", "Error ! 在请求过程中发生错误.");
            }
        });
    }
    $('dd').tooltip({ boundary: 'window' });
});