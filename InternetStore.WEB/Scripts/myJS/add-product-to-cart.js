$(document).on('click', '.add-to-cart-wrap', function (event) {
    var prodCount = $('.total').first().text();
    var productid = $(event.currentTarget).attr('productid');
    $.ajax({
        type: "POST",
        url: "/Cart/AddProduct",
        data: "prodId=" + productid + "&count=1",
        success: function (data) {
            prodCount++;
            $().toastmessage('showSuccessToast', "Товар добавлен в корзину!");
            $(".total").each(function () {
                $(this).text(prodCount);
            });
        }
    });
});

//$(document).on('click', '.add-new-product', function (event) {
//    var currentTarget = $(event.currentTarget);
//    $.ajax({
//        type: "POST",
//        url: "/Cart/AddProduct",
//        data: "prodId=" + productid + "&count=1",
//        success: function (data) {
//            prodCount++;
//            $().toastmessage('showSuccessToast', "Товар добавлен в корзину!");
//            $(".total").each(function () {
//                $(this).text(prodCount);
//            });
//        }
//    });
//});