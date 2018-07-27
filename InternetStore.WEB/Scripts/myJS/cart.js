$(document).on('click', '.fa-times', function (event) {
    var quanlityValue;
    var currentTarget = $(event.currentTarget);
    var currentProdCount = $(".total-product-count").first().text();
    var orderItemId = currentTarget.parent().parent().attr("orderItemId");
    $.ajax({
        type: "POST",
        url: "/Cart/RemoveProductFromCart",
        data: "orderItemId=" + orderItemId,
        success: function (data) {
            quanlityValue = currentTarget.parent().parent().children(".td-quanlity").children(".quanlity-value").val();
            currentTarget.parent().parent().children(".td-total-price").children(".total-product-price").text("0");
            currentTarget.parent().parent().children(".td-quanlity").children(".order-Item-Id").val(0);
            currentTarget.parent().parent().attr("orderitemid", "0");
            $(".total-product-count").each(function () {
                $(this).text(currentProdCount - quanlityValue);
            });
            currentTarget.parent().parent().attr("style", "display: none;");
            GetCartTotalPrice();
            IsCartEmty();
            $().toastmessage('showSuccessToast', "Товар удалён из корзины!");
        }
    });
});

function CartWasUpdate() {
    var totalPrice;
    var quanlityValue;
    var price;
    $.ajax({
        type: "POST",
        url: "/Cart/ProductCount",
        success: function (data) {
            $(".total-product-price").each(function () {
                quanlityValue = $(this).parent().parent().children(".td-quanlity").children(".quanlity-value").val();
                price = $(this).parent().parent().children(".td-price").children(".price").text();
                price = price.substr(1, price.length - 1).split(".")[0];
                totalPrice = price * quanlityValue;
                $(this).text("$" + totalPrice + ".00");
            });
            $(".total-product-count").each(function () {
                $(this).text(data);
            });
            GetCartTotalPrice();
        }
    });
    $().toastmessage('showSuccessToast', "Корзина обновлена!");
}

function GetCartTotalPrice() {
    var totalPrice = 0;
    var totalProductPrice;
    $(".total-product-price").each(function () {
       
        totalProductPrice = $(this).text();
        totalProductPrice = totalProductPrice.substr(1, totalProductPrice.length - 1).split(".")[0];
        totalPrice += +totalProductPrice;
    });
    $(".total-cart-price").text("$" + totalPrice + ".00");
}

function IsCartEmty(){
    if($(".total-product-count").first().text() == 0)
    {
        $(".col-md-6").first().remove();
        $(".button-cart").first().remove();
        $("#cart-container").append(
            "<div class='emty-data'>"+
                 "У вас ещё нет товаров в корзине."+
            "</div>"
            );
    }
}