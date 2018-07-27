$(document).on('click', '.cat-list>li', function (event) {
    var categoryName = $(event.currentTarget).children("a").text();
    var categoryid = $(event.currentTarget).attr('categoryid');
    var currentTarget = $(event.currentTarget);
    if (currentTarget.hasClass("active-category"))
    {
        currentTarget.removeClass("active-category");
        GetProducts(1);
    }
    else
    {
        $.ajax({
            type: "POST",
            url: "/Product/SiftByCategory",
            data: "categoryId=" + categoryid + "&isAjax=true",
            success: function (data) {
                if (data != "") {
                    $(".cat-list>li").each(function () {
                        $(this).removeClass("active-category");
                    });
                    currentTarget.addClass("active-category");
                    $('.col-md-9').first().animate({ opacity: 0 }, 200,
                        function () {
                            $(".col-md-9").first().html(data);
                        });
                    $().toastmessage('showSuccessToast', "Товары отсеяны по категории " + categoryName + "!");
                    history.pushState(null, null, "/plants/siftbycat" + categoryid);
                    $('.col-md-9').first().animate({ opacity: 1 }, 200);
                }
                else {
                    $().toastmessage('showNoticeToast', "Нет товаров в категории " + categoryName + "!");
                }
            }
        });
    }
});

$(window).on('popstate', function () {
    if (location.href.search(/siftbycat/i) != -1) {
        Siftbycat(location.href.substring(location.href.search(/siftbycat/i) + 9));
    }
});

function Siftbycat(categoryid)
{
    $.ajax({
        type: "POST",
        url: "/Product/SiftByCategory",
        data: "categoryId=" + categoryid + "&isAjax=true",
        success: function (data) {
            $(".cat-list>li").each(function () {
                if ($(this).attr("categoryid") == categoryid)
                {
                    $(this).addClass("active-category");
                }
                else
                {
                    $(this).removeClass("active-category");
                }
            });
            if (data != "") {
                $('.col-md-9').first().animate({ opacity: 0 }, 200,
                    function () {
                        $(".col-md-9").first().html(data);
                    });

                $('.col-md-9').first().animate({ opacity: 1 }, 200);
            }
        }
    });
}
