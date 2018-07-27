var start = true, tmp = "";
$(document).on('keyup input', '.form-input>input', function (event) {
    var query = $(event.currentTarget).val();
    if (tmp != query) {
        if (start) {
            fnDelay(GetQueryResult, query);
            start = false;
        }
        else if (query != "") {
            fnDelay(GetQueryResult, query);
        }
        else if (query == "") {
            fnDelay(GetProducts, 1);
            start = true;
        }
        tmp = query;
    }
});

function GetQueryResult(query) {
    $.ajax({
        type: "POST",
        url: "/Product/Search",
        data: "query=" + query + "&isAjax=true",
        success: function (data) {
            if (data != "") {
                $(".cat-list>li").each(function () {
                    $(this).removeClass("active-category");
                });
                history.pushState(null, null, "/plants/q=" + query);
                $('.col-md-9').first().animate( { opacity: 0}, 200,
                    function () {
                        $(".col-md-9").first().html(data);
                        $(".pagination-style-2").remove();
                        $().toastmessage('showSuccessToast', "Товары по запросу: '" + query + "'");
                    });
                $('.col-md-9').first().animate({ opacity: 1 }, 200);
            }
            else {
                $().toastmessage('showNoticeToast', "Поиск не дал результатов!");
            }
        }
    });
}

function GetProducts(pageId) {
    $(".cat-list>li").each(function () {
        $(this).removeClass("active-category");
    });
    $.ajax({
        type: "POST",
        url: "/Product/ProductList",
        data: "pageId=" + pageId + "&full=true" + "&isAjax=true",
        success: function (data) {
            history.pushState(null, null, "/plants/page1");
            $('.col-md-9').first().animate({ opacity: 0 }, 200,
                function () {
                    $(".col-md-9").first().html(data);
                });
            $('.col-md-9').first().animate({ opacity: 1, }, 200,
                function () {
                    $().toastmessage('showNoticeToast', "Вы вернулись на первую страницу товаров!");
                });
        }
    });
}

var timer = 0, _SEC = 400;
function fnDelay(func, query) {
    clearTimeout(timer);
    timer = setTimeout(function () {
        func(query);
    }, _SEC);
};


$(window).on('popstate', function () {
    if (location.href.search(/page/i) != -1) {
        GetProducts(location.href.substring(location.href.search(/page/i) + 4));
    }
});

