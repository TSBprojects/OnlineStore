
// Products
$(document).on('click', '.fa-pencil-square-o', function (event) {
    var currentTarget = $(event.currentTarget);
    var productid = currentTarget.parent().parent().attr('productid');
    if ($("#form-block-" + productid).html() != "") {
        $("#form-block-" + productid).animate({ opacity: "0" }, 200,
            function () {
                $("#form-block-" + productid).html("");
                currentTarget.parent().parent().animate({ height: "104px" }, 200)
            });
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Admin/ChangeProduct",
            data: "prodId=" + productid,
            success: function (data) {
                currentTarget.parent().parent().animate({ height: "711px" }, 200,
                    function () {
                        $("#form-block-" + productid).html(data);
                        $("#form-block-" + productid).animate({ opacity: "1" }, 200)
                    });
            }
        });
    }
});

$(document).on('click', '.admin-form-submit', function (event) {
    var data;
    var inputName;
    var inputValue;
    var currentTarget = $(event.currentTarget);
    var formFields = currentTarget.closest(".field-block").children(".form-field");
    var files = currentTarget.closest(".field-block").children(".upload-file").children("input")[0].files;
    var productid = formFields.first().children(".input-field").val();

    if (window.FormData !== undefined) {
        data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append("file" + i, files[i]);
        }
        formFields.each(function () {
            inputName = $(this).children(".input-field").attr("name");
            inputValue = $(this).children(".input-field").val();
            data.append(inputName, inputValue);
        });
        currentTarget.closest(".field-block").children(".image-settings-block").each(function () {
            if ($(this).children(".image-settings").prop("checked")) {
                inputName = $(this).children(".image-settings").attr("name");
                inputValue = $(this).children(".image-settings").val();
                data.append(inputName, inputValue);
            }
        });
        $.ajax({
            type: "POST",
            url: "/Admin/ChangeProductPost",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result == "success")
                {
                    $().toastmessage('showSuccessToast', "Товар изменен!");
                    $("#form-block-" + productid).animate({ opacity: "0" }, 200,
                        function () {
                            $("#form-block-" + productid).html("");
                            $("#form-block-" + productid).closest(".prod-block-admin").animate({ height: "104px" }, 200)
                        });
                }
                else
                {
                    currentTarget.closest(".field-block").closest("form").closest("div").html(result);
                }
            },
        });
    } else {
        $().toastmessage('showWarningToast', "Ваш браузер не поддерживает Html5!");
    }
});



$(document).on('click', '.desc-prod-add-row-admin', function (event) {
    AddProduct($(event.currentTarget));
});

$(document).on('click', '.admin-form-submit-newprod', function (event) {
    var data;
    var inputName;
    var inputValue;
    var currentTarget = $(event.currentTarget);
    var container = currentTarget.parent("p").parent(".field-block").parent(".admin-form").parent(".form-prod-block").parent(".prod-add-block-admin");
    var formFields = currentTarget.closest(".field-block").children(".form-field");
    var files = currentTarget.closest(".field-block").children(".upload-file").children("input")[0].files;

    if (window.FormData !== undefined) {
        data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append("file" + i, files[i]);
        }
        formFields.each(function () {
            inputName = $(this).children(".input-field").attr("name");
            inputValue = $(this).children(".input-field").val();
            data.append(inputName, inputValue);
        });
        $.ajax({
            type: "POST",
            url: "/Admin/AddProductPost",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result == "success") {
                    $().toastmessage('showSuccessToast', "Товар добавлен!");
                    $("#form-block-add").animate({ opacity: "0" }, 200,
                        function () {
                            $("#form-block-add").html("");
                            container.animate({ height: "54px" }, 200)
                        });
                }
                else {
                    currentTarget.closest(".field-block").closest("form").closest("div").html(result);
                }
            },
        });
    } else {
        $().toastmessage('showWarningToast', "Ваш браузер не поддерживает Html5!");
    }
});

function AddProduct(currentTarget) {
    var container = currentTarget.parent(".prod-add-block-admin");
    var formBlock = container.children(".form-prod-block");
    if (formBlock.html() != "") {
        formBlock.animate({ opacity: "0" }, 200,
            function () {
                formBlock.html("");
                container.animate({ height: "54px" }, 200)
            });
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Admin/AddProduct",
            success: function (data) {
                container.animate({ height: "567px" }, 200,
                    function () {
                        formBlock.html(data);
                        formBlock.animate({ opacity: "1" }, 200)
                    });
            }
        });
    }
}



$(document).on('click', '.fa-minus-square', function (event) {
    RemoveProduct($(event.currentTarget));
});

function RemoveProduct(currentTarget) {
    var productid = currentTarget.parent().parent().attr('productid');
    $.ajax({
        type: "POST",
        url: "/Admin/RemoveProduct",
        data: "prodId=" + productid,
        success: function (data) {
            $().toastmessage('showSuccessToast', "Товар удалён!");
            currentTarget.parent().parent().animate({ opacity: "0", height: "0px", "margin-bottom": "-22px", padding: "0" }, 500,
            function () {
                currentTarget.parent().parent().remove();
            });
        }
    });
}


// Orders
$(document).on('click', '.admin-product-button', function (event) {
    if (!$(event.currentTarget).hasClass("active-button")){
        $(event.currentTarget).addClass("active-button");
        $(".admin-order-button").first().removeClass("active-button");
        GetAllProducts();
    }
});

$(document).on('click', '.admin-order-button', function (event) {
    if (!$(event.currentTarget).hasClass("active-button")) {
        $(event.currentTarget).addClass("active-button");
        $(".admin-product-button").first().removeClass("active-button");
        GetAllOrders();
    }
});

$(document).on('click', '.user-orders-submit', function (event) {
    var currentTarget = $(event.currentTarget);
    var container = currentTarget.parent("p").parent(".user-orders");
    var statusBlock = container.children(".user-orders-cur-info").children(".order-status").children("span");
    var orderid = currentTarget.attr('orderid');
    $.ajax({
        type: "POST",
        url: "/Admin/OrderCompleted",
        data: "orderId=" + orderid,
        success: function (data) {
            statusBlock.text("Completed");
            $().toastmessage('showSuccessToast', "Заказ подтверждён!");
            currentTarget.remove();
        }
    });
});
