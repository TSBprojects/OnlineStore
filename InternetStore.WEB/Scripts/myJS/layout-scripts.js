function SubcriberAdded(data) {
    if (data == "True"){
        $().toastmessage('showSuccessToast', "Вы дописались!");
    }
    else if (data == "False") {
        $().toastmessage('showNoticeToast', "Вы уже подписаны!");
    }else{
        $().toastmessage('showWarningToast', "Пустой email!");
    }
}

$(document).on('click', '.fa-align-right', function (event) {
    if (!$(".admin-product-button").first().hasClass("active-button")) {
        $(".admin-product-button").first().addClass("active-button");
        $(".admin-order-button").first().removeClass("active-button");
    }
    GetAllProducts();
});

$(document).on('click', '.yolo-canvas-menu-close > i', function (event) {
    $('.opacity-container').css("opacity", "0");
    setTimeout(function () {
        $(".opacity-container").html("");
    }, 500);
});

$(document).on('click', '.fa-close', function (event) {
    setTimeout(function () {
        $(".opacity-container").html("");
    }, 500);
});

function LoadBegin() {
    $('#preload-products').show();
}

function LoadSuccess() {
    $('#preload-products').hide();
    $('.opacity-container').css("opacity","1");
}

function GetAllProducts() {
    $.ajax({
        type: "POST",
        url: "/Admin/GetAllProducts",
        beforeSend: LoadBegin(),
        success: function (data) {
            LoadSuccess();
            $(".opacity-container").html(data);
        }
    });
}

function GetAllOrders() {
    $.ajax({
        type: "POST",
        url: "/Admin/GetAllOrders",
        beforeSend: LoadBegin(),
        success: function (data) {
            LoadSuccess();
            $(".opacity-container").html(data);
        }
    });
}