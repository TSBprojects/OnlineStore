function ProductAdded(data) {
    var prodCount = $('.total').first().text();
    prodCount = Number(data) + Number(prodCount);
    $(".total").each(function () {
        $(this).text(prodCount);
    });
    $().toastmessage('showSuccessToast', "Товар добавлен в корзину!");
}

$(document).on('click', '.category', function (event) {
    var categoryid = $(event.currentTarget).attr("categoryid");
    window.location.href = "http://localhost:59572/plants/siftbycat" + categoryid;
});
$(document).on('click', '.tag', function (event) {
    var tagid = $(event.currentTarget).attr("tagid");
    window.location.href = "http://localhost:59572/plants/siftbytag" + tagid;
});

